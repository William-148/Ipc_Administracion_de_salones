using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.operador.salon
{
    public partial class editar_incidente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idIncidenteEditar"] != null && Session["IncidenteResponsable"] != null)
            {
                if (!IsPostBack)   cargarDatos();
            }
            else Response.Redirect("/paginas/operador/salon/lista-incidentes.aspx",true);
        }

        private void cargarDatos()
        {
            IncidenteSalon inc = IncidenteSalon.getIncidente(Convert.ToInt32(Session["idIncidenteEditar"]));

            fecha.Text = Convert.ToDateTime(inc.fechaIncidente).ToString("yyyy-MM-dd");
            idUsuario.Text = inc.idUsuario;
            nombre.Text = Convert.ToString(Session["IncidenteResponsable"]);
            descripcion.Text = inc.descripcion;
            edificio.DataBind();
            edificio.SelectedValue = inc.idEdificio;

            nombresSalones.SelectCommand = "SELECT [idSalon], [Salon] FROM Salon WHERE idEdificio = " + inc.idEdificio + " ORDER BY Salon";
            tituloSalon.Text = "Salon*(" + edificio.SelectedItem.Text + "):";
            salon.DataBind();
            salon.SelectedValue = inc.idSalon;

            estadoIncidente.DataBind();
            estadoIncidente.SelectedValue = inc.idEstadoIncidente;
        }

        private bool hayCamposVacios() => (fecha.Text.Trim() == "" || idUsuario.Text.Trim() == "" || descripcion.Text.Trim()== "");

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                IncidenteSalon inc = new IncidenteSalon();
                inc.idIncidenteSalon = Convert.ToString(Session["idIncidenteEditar"]);
                inc.fechaIncidente = fecha.Text;
                inc.descripcion = descripcion.Text;
                inc.idUsuario = idUsuario.Text;
                inc.idSalon = salon.SelectedValue;
                inc.idEstadoIncidente = estadoIncidente.SelectedValue;
                if (inc.actualizar()) consola.Text = "La información se actualizó correctamente.";
                else consola.Text = "Error: id de usuario no existe o es errónea.";
            }
            else consola.Text = "Llene todos los campos antes de continuar.";
        }

        protected void verSalones_Click(object sender, EventArgs e)
        {
            nombresSalones.SelectCommand = "SELECT [idSalon], [Salon] FROM Salon WHERE idEdificio = " + edificio.SelectedValue + " ORDER BY Salon";

            tituloSalon.Text = "Salon*(" + edificio.SelectedItem.Text + "):";

            salon.DataBind();

        }
    }
}