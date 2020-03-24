using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.operador.insumos
{
    public partial class editar_incidente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack && Request.Params["cod"] != null && Request.Params["nom"] != null)
            {
                string idIncidenteEditar = Request.Params["cod"].ToString();
                string responsable = Request.Params["nom"].ToString();

                IncidenteInsumo incidente = IncidenteInsumo.getIncidente(idIncidenteEditar);

                if (incidente != null)
                {
                    fecha.Text = Convert.ToDateTime(incidente.fechaIncidente).ToString("yyyy-MM-dd");
                    descripcion.Text = incidente.descripcion;
                    insumo.SelectedValue = incidente.idInsumo;
                    idUsuario.Text = incidente.idResponsble;
                    usuario.Text = responsable;
                    estado.SelectedValue = incidente.idEstadoIncidente;
                }
                else Response.Write("<script>alert('error');</script>");
            }            
        }

        private bool hayCamposVacios()
        {
            return (fecha.Text.Trim() == "" || descripcion.Text.Trim() == "" || idUsuario.Text.Trim() == "");
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                IncidenteInsumo nuevo = new IncidenteInsumo();
                nuevo.id = Request.Params["cod"].ToString();
                nuevo.fechaIncidente = fecha.Text;
                nuevo.descripcion = descripcion.Text;
                nuevo.idInsumo = insumo.SelectedValue;
                nuevo.idResponsble = idUsuario.Text;
                nuevo.idEstadoIncidente = estado.SelectedValue;

                if (nuevo.actualizar()) {
                    consola.Text = "Se ha actualizado la información.";
                }
                else consola.Text = "Ha ocurrido un error, revise que la informacion esté correcta.";
            }
            else consola.Text = "Llenar los campos obligatorios.";
        }
    }
}