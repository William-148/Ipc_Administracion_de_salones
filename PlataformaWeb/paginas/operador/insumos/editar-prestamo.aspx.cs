using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.operador.insumos
{
    public partial class editar_prestamo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["cod"] != null && Request.Params["nom"] != null && !IsPostBack)
            {
                string idPrestamoEditar = Request.Params["cod"].ToString();
                string responsable = Request.Params["nom"].ToString();

                PrestamoInsumo pr = PrestamoInsumo.getPrestramo(idPrestamoEditar);

                if (pr != null)
                {
                    nombre.Text = responsable;
                    insumoLista.SelectedValue = pr.idInsumo;
                    idUsuario.Text = pr.idUsuario;
                    descripcion.Text = pr.descripcion;
                    estado.SelectedValue = pr.idEstadoPrestamo;
                }
                else Response.Write("<script>alert('error');</script>");
            }
        }

        private bool hayCamposVacios()
        {
            return (idUsuario.Text.Trim() == "" || descripcion.Text.Trim() == "");
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                PrestamoInsumo nuevo = new PrestamoInsumo();
                nuevo.id = Request.Params["cod"].ToString();
                nuevo.descripcion = descripcion.Text;
                nuevo.idUsuario = idUsuario.Text;
                nuevo.idInsumo = insumoLista.SelectedValue;
                nuevo.idEstadoPrestamo = estado.SelectedValue;
                
                if (nuevo.actualizar()) consola.Text = "La información se ha actualizado correctamente.";
                else consola.Text = "Ha ocurrido un error, revise si los datos estén correctos.";
            }
            else consola.Text = "Llene todos los campos solicitados.";
            
        }
    }
}