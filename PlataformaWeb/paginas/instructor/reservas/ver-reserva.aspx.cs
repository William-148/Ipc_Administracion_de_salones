using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.instructor.reservas
{
    public partial class ver_reserva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["idReservaVer"] != null && Session["reservaSalon"] != null && Session["reservaEdificio"] != null)
            {
                if (!IsPostBack)
                {
                    cargarDatos();
                }
            }
            else Response.Redirect("/paginas/instructor/reservas/lista-reservas.aspx");
        }

        private string getPeriodo(int idPeriodo)
        {
            switch (idPeriodo)
            {
                case 1: return "Día único";
                case 2: return "Diario";
                case 3: return "Semanal";
                case 4: return "Quincenal";
                default: return "No definido";
            }
        }

        private string getVigencia(int idVigencia)
        {
            switch (idVigencia)
            {
                case 1: return "Vigente";
                case 2: return "Finalizado";
                default: return "No definido";
            }
        }


        private void cargarDatos()
        {
            Reservacion reserva = Reservacion.getReserva(Convert.ToInt32(Session["idReservaVer"]));
            Session["reservaDatos"] = reserva;
            if (reserva.idReservacion != 0)
            {
                if (reserva.carta != null)
                {
                    string imagenDataUrl64 = "data:image/jpg;base64," + Convert.ToBase64String(reserva.carta);
                    imagenCarta.ImageUrl = imagenDataUrl64;
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

                this.fecha.Text = fecha.ToString("dd/MM/yyyy");
                hora.Text = horaInicio.ToString("HH:mm");
                this.duracion.Text = duracion.ToString();
                periodoReserva.Text = getPeriodo(reserva.idPeriodoReserva);
                vigencia1.Text = getVigencia(reserva.idVigenciaReserva);
                idInstructor.Text = Convert.ToString(reserva.idInstructor);
                instructor.Text = reserva.instructorTxt;
                operador.Text = reserva.administradorTxt;
                salon1.Text = Session["reservaSalon"].ToString();
                edificio1.Text = Session["reservaEdificio"].ToString();
            }
        }

        protected void descargar_Click(object sender, EventArgs e)
        {
            Reservacion reserva =(Reservacion) Session["reservaDatos"];
            byte[] codigoQR = reserva.codigoQR;
            if (codigoQR != null)
            {
                Response.Clear();
                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "CodigoQr.jpg"));
                Response.ContentType = "image/jpg";
                Response.BinaryWrite(codigoQR);
                Response.End();
            }
        }
    }
}