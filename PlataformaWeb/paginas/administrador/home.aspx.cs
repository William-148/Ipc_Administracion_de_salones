using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.administrador
{
    public partial class home : System.Web.UI.Page
    {
        private List<string[]> reservas;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            string query = "select R.idReservaSalon, Ed.Nombre as Edificio, S.Salon, R.HoraInicio, R.HoraFin, U.Nombre as Instructor, E.Estado from ReservaSalon R, EstadoReserva E, Usuario U, Salon S, Edificio Ed where R.idEstadoReserva = E.idEstadoReserva and R.idInstructor = u.idUsuario and R.idSalon = S.idSalon and S.idEdificio = Ed.idEdificio and R.Fecha = '" + Calendar1.SelectedDate.ToShortDateString() + "' order by HoraInicio asc";
            eventosReservas.SelectCommand = query;
            DataList1.DataBind();
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.Day == 1 && !e.Day.IsOtherMonth)
            {//El if hará que solo se haga la consulta una vez

                //Obtiene un listado de reservas que pertenezcan al mes que muestra el calendario
                //y lo guardará en la variable reservas
                reservas = Reservacion.getListaReservacion(e.Day.Date.Month, e.Day.Date.Year);
            }

            if (reservas != null && !e.Day.IsOtherMonth)
            {
                foreach (string[] item in reservas)
                {
                    DateTime aux = Convert.ToDateTime(item[0]);
                    if (e.Day.Date.Day == aux.Date.Day)
                    {
                        if (item[1] == "1") e.Cell.Controls.Add(new LiteralControl("<span class=\"eventoPrevio icon-bookmarks\"></span> "));
                        else e.Cell.Controls.Add(new LiteralControl("<span class=\"eventoFinalizado  icon-bookmark\"></span> "));
                    }
                }
            }
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ver")
            {
                Session["idReservaEditar"] = e.CommandArgument.ToString();
                Response.Redirect("/paginas/administrador/reserva/ver-reserva.aspx");
            }
        }
    }
}