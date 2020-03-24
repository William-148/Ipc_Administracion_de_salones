using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.operador.reserva
{
    public partial class crear : System.Web.UI.Page
    {

        private const string horaLimiteI = "06:00";
        private const string horaLimiteS = "21:00";
        private const string duracionLimite = "10:00";
        private const string formatoFecha = "dd/MM/yyyy";
        private const string formatoHora = "HH:mm";



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatos();

            }
        }

        private void cargarDatos()
        {
            DateTime fecha1 = DateTime.Now.Date;
            DateTime hora1 = Convert.ToDateTime("07:00");
            DateTime duracion1 = Convert.ToDateTime("01:00");

            fecha.Text = fecha1.ToString("yyyy-MM-dd");
            hora.Text = hora1.ToString(formatoHora);
            duracion.Text = duracion1.ToString(formatoHora);
        }        

        private void validarFechas()
        {
            DateTime fecha1 = Convert.ToDateTime(fecha.Text);
            DateTime horaInicio = Convert.ToDateTime(hora.Text);
            DateTime duracion1 = Convert.ToDateTime(duracion.Text);
            DateTime horaFinal = horaInicio.AddTicks(duracion1.Ticks);

            if (fecha1.Date > DateTime.Now.Date)
            {
                if (horaInicio.TimeOfDay >= Convert.ToDateTime(horaLimiteI).TimeOfDay && horaInicio.TimeOfDay <= Convert.ToDateTime(horaLimiteS).TimeOfDay)
                {
                    if (duracion1.TimeOfDay <= Convert.ToDateTime(duracionLimite).TimeOfDay)
                    {
                        //sigue metodo para registrar reserva
                        /*mensaje.Text = "Fecha: " + fecha1.ToString(formatoFecha) + "   Hora Inicio: " + horaInicio.ToString(formatoHora)
                        + "     Hora Final: " + horaFinal.ToString(formatoHora);*/
                        validarFecha(fecha1, horaInicio, horaFinal);

                    }
                    else consola.Text = "Error: Duración fuera del límite permitido.";
                }
                else consola.Text = "Error: Hora de reserva fuera del intervalo permitido.";
            }
            else consola.Text = "Error: Fecha de reserva no permitida.";
        }

        private bool hayTraslapeEnTiempos(DateTime horaIncio1, DateTime horaFin1, DateTime horaIncio2, DateTime horaFin2)
        {//El metodo comprueba si hay interseccion entre los dos intervalos de hora, si hay interseccion debuelve true, sino false
            if ((horaIncio1.TimeOfDay < horaIncio2.TimeOfDay && horaFin1.TimeOfDay > horaIncio2.TimeOfDay) ||
                (horaIncio1.TimeOfDay < horaFin2.TimeOfDay && horaFin1.TimeOfDay > horaFin2.TimeOfDay) ||
                (horaIncio2.TimeOfDay < horaIncio1.TimeOfDay && horaFin2.TimeOfDay > horaIncio1.TimeOfDay) ||
                (horaIncio2.TimeOfDay < horaFin1.TimeOfDay && horaFin2.TimeOfDay > horaFin1.TimeOfDay)||
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

        private void validarFecha(DateTime FechaNueva, DateTime HoraInicioNuevo, DateTime HoraFinNuevo)
        {

            List<Reservacion> lista = Reservacion.getListReservacion(Convert.ToInt32(salon.SelectedValue));
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
                                        while (FechaNueva.Date < fechaRegistrada.Date )
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
                        registrarReserva(FechaNueva, HoraInicioNuevo, HoraFinNuevo);
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
                else registrarReserva(FechaNueva, HoraInicioNuevo, HoraFinNuevo);
            }
            else consola.Text = "Ha ocurrido un error, intente mas tarde.";
            
        }

        private void registrarReserva(DateTime fecha, DateTime horaInicio, DateTime horaFin)
        {
            Reservacion reserva = new Reservacion();
            reserva.fecha = fecha.ToString(formatoFecha);
            reserva.horaInicio = horaInicio.ToString(formatoHora);
            reserva.horaFin = horaFin.ToString(formatoHora);
            reserva.idPeriodoReserva = Convert.ToInt32(periodo.SelectedValue);
            reserva.idInstructor = Convert.ToInt32(idInstructor.Text);
            reserva.idTipoActividad = Convert.ToInt32(tipoActividad.SelectedValue);
            reserva.idSalon = Convert.ToInt32(salon.SelectedValue);
            reserva.idAdministrador = Convert.ToInt32(Session["idUsuario"]);
            if (reserva.insertar())
            {
                Response.Redirect("/paginas/operador/reserva/lista-reservas.aspx");
            }
            else consola.Text = "Error: El ID del instructor no existe o es erroneo.";
        }

        protected void verSalones_Click(object sender, EventArgs e)
        {
            string idEdificio = edificio.SelectedValue;
            tituloSalon.Text = "Salon*("+edificio.SelectedItem.Text+"): ";
            nombresSalones.SelectCommand = "SELECT idSalon, Salon FROM Salon WHERE idEdificio =" + idEdificio + "ORDER BY Salon";
            salon.DataBind();
        }
    }
}