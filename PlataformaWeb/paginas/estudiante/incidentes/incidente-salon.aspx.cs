using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas.estudiante.incidentes
{
    public partial class incidente_salon2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void boton_Click(object sender, EventArgs e)
        {
            string query;
            switch (boton.Text)
            {
                case "Incidentes Resueltos":
                    query = "SELECT I.idIncidenteSalon, I.FechaIncidente, E.Nombre ,S.Salon, I.Descripcion FROM IncidenteSalon I, Salon S, Edificio E WHERE I.idUsuario = @idUsuario AND I.idSalon = S.idSalon AND S.idEdificio = E.idEdificio AND I.idEstadoIncidente = 2 ORDER BY I.FechaIncidente DESC";
                    ListadoInsumos.SelectCommand = query;
                    boton.Text = title.Text = "Incidentes No Resueltos";
                    GridView1.DataBind();
                    break;
                case "Incidentes No Resueltos":
                    query = "SELECT I.idIncidenteSalon, I.FechaIncidente, E.Nombre ,S.Salon, I.Descripcion FROM IncidenteSalon I, Salon S, Edificio E WHERE I.idUsuario = @idUsuario AND I.idSalon = S.idSalon AND S.idEdificio = E.idEdificio AND I.idEstadoIncidente = 1 ORDER BY I.FechaIncidente DESC";
                    ListadoInsumos.SelectCommand = query;
                    boton.Text = title.Text = "Incidentes Resueltos";
                    GridView1.DataBind();
                    break;
            }
        }
    }
}