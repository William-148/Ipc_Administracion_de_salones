using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MessagingToolkit.QRCode.Codec;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.administrador.Reserva
{
    public partial class ver_reserva : System.Web.UI.Page
    {
        private const string FmtFechaTextBox = "yyyy-MM-dd";
        private const string horaLimiteI = "06:00";
        private const string horaLimiteS = "21:00";
        private const string duracionLimite = "10:00";
        private const string formatoFecha = "dd/MM/yyyy";
        private const string formatoHora = "HH:mm";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idReservaEditar"] != null)
            {
                if (!IsPostBack)
                {
                    cargarDatos();

                }
            }
            else Response.Redirect("/paginas/operador/reserva/lista-reservas.aspx");
        }

        private void cargarDatos()
        {
            Reservacion reserva = Reservacion.getReserva(Convert.ToInt32(Session["idReservaEditar"]));
            Session["reservaDatos"] = reserva;
            if (reserva.idReservacion != 0)
            {
                if (reserva.carta != null)
                {
                    string imagenDataUrl64 = "data:image/jpg;base64," + Convert.ToBase64String(reserva.carta);
                    Image.ImageUrl = imagenDataUrl64;
                }
                if (reserva.codigoQR != null)
                {
                    string imagenDataUrl64 = "data:image/jpg;base64," + Convert.ToBase64String(reserva.codigoQR);
                    ImageQR.ImageUrl = imagenDataUrl64;
                }

                DateTime fecha = Convert.ToDateTime(reserva.fecha);
                DateTime horaInicio = Convert.ToDateTime(reserva.horaInicio);
                DateTime horaFin = Convert.ToDateTime(reserva.horaFin);
                TimeSpan duracion = horaFin.Subtract(horaInicio);
                this.fecha.Text = fecha.ToString(FmtFechaTextBox);
                hora.Text = horaInicio.ToString("HH:mm");
                this.duracion.Text = duracion.ToString();
                periodo.SelectedValue = Convert.ToString(reserva.idPeriodoReserva);
                estado.Text = reserva.estadoReservaTxt;
                vigencia.SelectedValue = Convert.ToString(reserva.idVigenciaReserva);
                idInstructor.Text = Convert.ToString(reserva.idInstructor);
                instructor.Text = reserva.instructorTxt;
                operador.Text = reserva.administradorTxt;
                edificio.DataBind();
                edificio.SelectedValue = Convert.ToString(reserva.idEdificio);

                tipoActividad.SelectedValue = Convert.ToString(reserva.idTipoActividad);

                tituloSalon.Text = "Salon*(" + edificio.SelectedItem.Text + "): ";
                nombresSalones.SelectCommand = "SELECT idSalon, Salon FROM Salon WHERE idEdificio =" + reserva.idEdificio + "ORDER BY Salon";
                salon.DataBind();
                salon.SelectedValue = Convert.ToString(reserva.idSalon);
            }
        }

        protected void verSalones_Click(object sender, EventArgs e)
        {
            string idEdificio = edificio.SelectedValue;
            tituloSalon.Text = "Salon*(" + edificio.SelectedItem.Text + "): ";
            nombresSalones.SelectCommand = "SELECT idSalon, Salon FROM Salon WHERE idEdificio =" + idEdificio + "ORDER BY Salon";
            salon.DataBind();
        }

        private bool hayTraslapeEnTiempos(DateTime horaIncio1, DateTime horaFin1, DateTime horaIncio2, DateTime horaFin2)
        {//El metodo comprueba si hay interseccion entre los dos intervalos de hora, si hay interseccion debuelve true, sino false
            if ((horaIncio1.TimeOfDay < horaIncio2.TimeOfDay && horaFin1.TimeOfDay > horaIncio2.TimeOfDay) ||
                (horaIncio1.TimeOfDay < horaFin2.TimeOfDay && horaFin1.TimeOfDay > horaFin2.TimeOfDay) ||
                (horaIncio2.TimeOfDay < horaIncio1.TimeOfDay && horaFin2.TimeOfDay > horaIncio1.TimeOfDay) ||
                (horaIncio2.TimeOfDay < horaFin1.TimeOfDay && horaFin2.TimeOfDay > horaFin1.TimeOfDay) ||
                 (horaIncio1.TimeOfDay == horaIncio2.TimeOfDay && horaFin1.TimeOfDay == horaFin2.TimeOfDay))
            {
                return true;
            }
            else return false;
        }

        private bool existeFechaDiaria(DateTime FechaRegistrada, DateTime horaInicioRegistrado, DateTime horaFinRegistrado, DateTime FechaNueva, DateTime horaInicioNuevo, DateTime horaFinNuevo)
        {
            //FechaR = fecha registrada      FechaV = fecha a verificar para ingresar
            if (FechaNueva.Date >= FechaRegistrada.Date)
            {
                if (FechaNueva.DayOfWeek != DayOfWeek.Sunday && FechaNueva.DayOfWeek != DayOfWeek.Saturday)
                {
                    if (hayTraslapeEnTiempos(horaInicioRegistrado, horaFinRegistrado, horaInicioNuevo, horaFinNuevo))
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;

        }

        private bool existeFechaPeriodo(DateTime FechaR, DateTime horaInicioR, DateTime horaFinR, DateTime FechaNueva, DateTime horaInicioV, DateTime horaFinV, int periodoDias)
        {//Método que busca si hay traslapes entre fechas cuando la fecha registrada es por periodos
            bool existe = false;
            DateTime fechaRegistrada = FechaR;
            do
            {
                if (fechaRegistrada.Date == FechaNueva.Date)
                {
                    if (hayTraslapeEnTiempos(horaInicioR, horaFinR, horaInicioV, horaFinV))
                    {
                        existe = true;
                        break;
                    }
                }
                fechaRegistrada = fechaRegistrada.AddDays(periodoDias);

            } while (FechaNueva.Date >= fechaRegistrada.Date);
            return existe;
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (idInstructor.Text != "")
            {
                validarFechas();
            }
            else consola.Text = "Error: Id del Instructor vacía.";
        }

        public bool fechaRepetida(DateTime fecha, DateTime horaInicio, DateTime horaFin)
        {
            bool mismaFecha = false;
            Reservacion reserva = (Reservacion)Session["reservaDatos"];
            DateTime fechaOriginal = Convert.ToDateTime(reserva.fecha);
            DateTime horaInicioOriginal = Convert.ToDateTime(reserva.horaInicio);
            DateTime horaFinOriginal = Convert.ToDateTime(reserva.horaFin);

            if (fechaOriginal.Date == fecha.Date && horaInicioOriginal.TimeOfDay == horaInicio.TimeOfDay && horaFinOriginal.TimeOfDay == horaFin.TimeOfDay
                && reserva.idSalon == Convert.ToInt32(salon.SelectedValue))
            {
                mismaFecha = true;
            }

            return mismaFecha;
        }

        private void validarFechas()
        {
            DateTime fecha1 = Convert.ToDateTime(fecha.Text);
            DateTime horaInicio = Convert.ToDateTime(hora.Text);
            DateTime duracion1 = Convert.ToDateTime(duracion.Text);
            DateTime horaFinal = horaInicio.AddTicks(duracion1.Ticks);

            if (!fechaRepetida(fecha1, horaInicio, horaFinal))
            {
                if (fecha1.Date > DateTime.Now.Date)
                {
                    if (horaInicio.TimeOfDay >= Convert.ToDateTime(horaLimiteI).TimeOfDay && horaInicio.TimeOfDay <= Convert.ToDateTime(horaLimiteS).TimeOfDay)
                    {
                        if (duracion1.TimeOfDay <= Convert.ToDateTime(duracionLimite).TimeOfDay)
                        {
                            //sigue metodo para registrar reserva
                            validarFecha(fecha1, horaInicio, horaFinal);

                        }
                        else consola.Text = "Error: Duración fuera del límite permitido.";
                    }
                    else consola.Text = "Error: Hora de reserva fuera del intervalo permitido.";
                }
                else consola.Text = "Error: Fecha de reserva no permitida.";
            }
            else
            {
                //Actualizar reserva----------------------------------------------------------------------------------------------------------------------------------------------------
                ActualizarReserva(fecha1, horaInicio, horaFinal);
            }

        }

        private void validarFecha(DateTime FechaNueva, DateTime HoraInicioNuevo, DateTime HoraFinNuevo)
        {

            List<Reservacion> lista = Reservacion.getListReservacion(Convert.ToInt32(Session["idReservaEditar"]), Convert.ToInt32(salon.SelectedValue));
            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    DateTime fechaRegistrada;
                    DateTime horaInicioRegistrada;
                    DateTime horaFinRegistrada;
                    Reservacion reservaRegistrada = null; //Guardará la reserva que ya este registrada y que este en el mismo horario y fecha que la reserva a registrar                
                    bool flag = false;//para romper el foreach

                    foreach (Reservacion item in lista)
                    {
                        fechaRegistrada = Convert.ToDateTime(item.fecha);
                        horaInicioRegistrada = Convert.ToDateTime(item.horaInicio);
                        horaFinRegistrada = Convert.ToDateTime(item.horaFin);

                        switch (item.idPeriodoReserva)
                        {
                            case 1:
                                //Reserva Dia Único
                                switch (Convert.ToInt32(periodo.SelectedValue))
                                {
                                    case 1:
                                        //Fecha que se quiere reservar periodo día único
                                        if (fechaRegistrada.Date == FechaNueva.Date)
                                        {
                                            if (hayTraslapeEnTiempos(horaInicioRegistrada, horaFinRegistrada, HoraInicioNuevo, HoraFinNuevo))
                                            {
                                                flag = true;
                                                reservaRegistrada = item;
                                            }
                                        }
                                        break;

                                    case 2:
                                        //Fecha que se quiere reservar periodo Diario
                                        if (existeFechaDiaria(FechaNueva, HoraInicioNuevo, HoraFinNuevo, fechaRegistrada, horaInicioRegistrada, horaFinRegistrada))
                                        {
                                            flag = true;
                                            reservaRegistrada = item;
                                        }
                                        break;

                                    case 3:
                                        //Fecha que se quiere reservar periodo semanal
                                        if (existeFechaPeriodo(FechaNueva, HoraInicioNuevo, HoraFinNuevo, fechaRegistrada, horaInicioRegistrada, horaFinRegistrada, 7))
                                        {
                                            flag = true;
                                            reservaRegistrada = item;
                                        }
                                        break;

                                    case 4:
                                        //Fecha que se quiere reservar periodo quincenal
                                        if (existeFechaPeriodo(FechaNueva, HoraInicioNuevo, HoraFinNuevo, fechaRegistrada, horaInicioRegistrada, horaFinRegistrada, 14))
                                        {
                                            flag = true;
                                            reservaRegistrada = item;
                                        }
                                        break;
                                }
                                break;

                            case 2:
                                //Reserva Diaria                            

                                switch (Convert.ToInt32(periodo.SelectedValue))
                                {
                                    case 1:
                                        if (existeFechaDiaria(fechaRegistrada, horaInicioRegistrada, horaFinRegistrada, FechaNueva, HoraInicioNuevo, HoraFinNuevo))
                                        {
                                            flag = true;
                                            reservaRegistrada = item;
                                        }
                                        break;

                                    case 2:
                                        while (FechaNueva.Date < fechaRegistrada.Date)
                                        {
                                            FechaNueva = FechaNueva.AddDays(1);
                                            while (FechaNueva.DayOfWeek == DayOfWeek.Saturday || FechaNueva.DayOfWeek == DayOfWeek.Sunday)
                                            {
                                                FechaNueva = FechaNueva.AddDays(1);
                                            }
                                        }
                                        if (existeFechaDiaria(fechaRegistrada, horaInicioRegistrada, horaFinRegistrada, FechaNueva, HoraInicioNuevo, HoraFinNuevo))
                                        {
                                            flag = true;
                                            reservaRegistrada = item;
                                        }
                                        break;

                                    case 3:
                                        if (FechaNueva.DayOfWeek != DayOfWeek.Sunday && FechaNueva.DayOfWeek != DayOfWeek.Saturday)
                                        {
                                            while (FechaNueva.Date < fechaRegistrada.Date)
                                            {
                                                FechaNueva = FechaNueva.AddDays(7);
                                            }
                                            if (existeFechaDiaria(fechaRegistrada, horaInicioRegistrada, horaFinRegistrada, FechaNueva, HoraInicioNuevo, HoraFinNuevo))
                                            {
                                                flag = true;
                                                reservaRegistrada = item;
                                            }
                                        }
                                        break;

                                    case 4:
                                        if (FechaNueva.DayOfWeek != DayOfWeek.Sunday && FechaNueva.DayOfWeek != DayOfWeek.Saturday)
                                        {
                                            while (FechaNueva.Date < fechaRegistrada.Date)
                                            {
                                                FechaNueva = FechaNueva.AddDays(15);
                                            }
                                            if (existeFechaDiaria(fechaRegistrada, horaInicioRegistrada, horaFinRegistrada, FechaNueva, HoraInicioNuevo, HoraFinNuevo))
                                            {
                                                flag = true;
                                                reservaRegistrada = item;
                                            }
                                        }
                                        break;
                                }
                                break;

                            case 3:
                                //Reserva Semanal
                                switch (Convert.ToInt32(periodo.SelectedValue))
                                {
                                    case 2:
                                        if (fechaRegistrada.DayOfWeek != DayOfWeek.Saturday && fechaRegistrada.DayOfWeek != DayOfWeek.Sunday)
                                        {
                                            while (FechaNueva.Date < fechaRegistrada)
                                            {
                                                FechaNueva = FechaNueva.AddDays(1);
                                                while (FechaNueva.DayOfWeek == DayOfWeek.Saturday || FechaNueva.DayOfWeek == DayOfWeek.Sunday)
                                                {
                                                    FechaNueva = FechaNueva.AddDays(1);
                                                }
                                            }
                                        }
                                        break;
                                    case 3:
                                        while (FechaNueva.Date < fechaRegistrada.Date)
                                        {
                                            FechaNueva.AddDays(7);
                                        }
                                        break;
                                    case 4:
                                        while (FechaNueva.Date < fechaRegistrada.Date)
                                        {
                                            FechaNueva.AddDays(14);
                                        }
                                        break;
                                }
                                if (existeFechaPeriodo(fechaRegistrada, horaInicioRegistrada, horaFinRegistrada, FechaNueva, HoraInicioNuevo, HoraFinNuevo, 7))
                                {
                                    flag = true;
                                    reservaRegistrada = item;
                                }

                                break;

                            case 4:
                                //Reserva Quincenal 
                                switch (Convert.ToInt32(periodo.SelectedValue))
                                {
                                    case 2:
                                        if (fechaRegistrada.DayOfWeek != DayOfWeek.Saturday && fechaRegistrada.DayOfWeek != DayOfWeek.Sunday)
                                        {
                                            while (FechaNueva.Date < fechaRegistrada.Date)
                                            {
                                                FechaNueva = FechaNueva.AddDays(1);
                                                while (FechaNueva.DayOfWeek == DayOfWeek.Saturday || FechaNueva.DayOfWeek == DayOfWeek.Sunday)
                                                {
                                                    FechaNueva = FechaNueva.AddDays(1);
                                                }
                                            }
                                        }
                                        break;
                                    case 3:
                                        while (FechaNueva.Date < fechaRegistrada.Date)
                                        {
                                            FechaNueva.AddDays(7);
                                        }
                                        break;
                                    case 4:
                                        while (FechaNueva.Date < fechaRegistrada.Date)
                                        {
                                            FechaNueva.AddDays(14);
                                        }
                                        break;
                                }
                                if (existeFechaPeriodo(fechaRegistrada, horaInicioRegistrada, horaFinRegistrada, FechaNueva, HoraInicioNuevo, HoraFinNuevo, 14))
                                {
                                    flag = true;
                                    reservaRegistrada = item;
                                }

                                break;
                        }

                        if (flag)
                        {
                            break;
                        }
                    }
                    //codigo sigue aqui
                    if (reservaRegistrada == null)
                    {
                        //registrar aqui
                        ActualizarReserva(FechaNueva, HoraInicioNuevo, HoraFinNuevo);
                    }
                    else
                    {
                        consola.Text = "No es posible registrar la reserva, ya que hay reservas con la misma fecha y hora:\n" +
                                       "ID Reserva: " + reservaRegistrada.idReservacion + " \n" +
                                       "Fecha Reservada: " + reservaRegistrada.fecha + " \n" +
                                       "Hora de Incio: " + reservaRegistrada.horaInicio + " \n" +
                                       "Hora Final: " + reservaRegistrada.horaFin + "\n" +
                                       "Periodo de Reserva: " + reservaRegistrada.getPeriodoReserva();
                    }
                }
                else ActualizarReserva(FechaNueva, HoraInicioNuevo, HoraFinNuevo);
            }
            else consola.Text = "Ha ocurrido un error, intente mas tarde.";

        }

        private void ActualizarReserva(DateTime fecha, DateTime horaInicio, DateTime horaFin)
        {
            Reservacion reserva = new Reservacion();
            reserva.idReservacion = Convert.ToInt32(Session["idReservaEditar"]);
            reserva.fecha = fecha.ToString(formatoFecha);
            reserva.horaInicio = horaInicio.ToString(formatoHora);
            reserva.horaFin = horaFin.ToString(formatoHora);
            reserva.idPeriodoReserva = Convert.ToInt32(periodo.SelectedValue);
            reserva.idVigenciaReserva = Convert.ToInt32(vigencia.SelectedValue);
            reserva.idInstructor = Convert.ToInt32(idInstructor.Text);
            reserva.idSalon = Convert.ToInt32(salon.SelectedValue);
            reserva.idTipoActividad = Convert.ToInt32(tipoActividad.SelectedValue);
            reserva.idAdministrador = Convert.ToInt32(Session["idUsuario"]);
            if (reserva.Actualizar())
            {
                consola.Text = "Los datos se actualizaron correctamente.";
            }
            else consola.Text = "Error: El ID del instructor no existe o es erroneo.";
        }

        protected void subir_Click(object sender, EventArgs e)
        {
            //obteniendo imagen seleccionada
            try
            {
                int sizeImg = uploadImagen.PostedFile.ContentLength;
                byte[] imagenOriginal = new byte[sizeImg];
                uploadImagen.PostedFile.InputStream.Read(imagenOriginal, 0, sizeImg);
                Bitmap imagenOriginalBinaria = new Bitmap(uploadImagen.PostedFile.InputStream);

                //mostrando la imagen preview
                string imagenDataUrl64 = "data:image/jpg;base64," + Convert.ToBase64String(imagenOriginal);
                Image.ImageUrl = imagenDataUrl64;

                //insertando la imagen a la base de datos
                bool resultado = Reservacion.insertarimagen(Convert.ToInt32(Session["idReservaEditar"]), imagenOriginal, Reservacion.insertarCarta);
                if (resultado)
                {
                    consola.Text = "Carta cargada correctamente";
                    generarQR();
                }
                else consola.Text = "Error: La imagen no fue cargada, intente nuevamente";

            }
            catch { }


        }

        public void generarQR()
        {
            string url = "http://localhost:52277/paginas/estudiante/default.aspx?id=" + Convert.ToString(Session["idReservaEditar"]);
            QRCodeEncoder codificar = new QRCodeEncoder();
            Bitmap img = codificar.Encode(url);
            System.Drawing.Image QR = (System.Drawing.Image)img;

            using (MemoryStream ms = new MemoryStream())
            {
                QR.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] codigoQr = ms.ToArray();
                string imagenDataUrl64 = "data:image/jpg;base64," + Convert.ToBase64String(codigoQr);
                ImageQR.ImageUrl = imagenDataUrl64;
                Reservacion.insertarimagen(Convert.ToInt32(Session["idReservaEditar"]), codigoQr, Reservacion.insertarCodigoQR);
            }

        }
    }
}