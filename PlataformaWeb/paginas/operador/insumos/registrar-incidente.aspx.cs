using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.operador.insumos
{
    public partial class editar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private bool hayCamposVacios()
        {
            return (fecha.Text.Trim() == "" || descripcion.Text.Trim() == "" || idUsuario.Text.Trim() == "");
        }

        private void limpiar()
        {
            fecha.Text = "";
            descripcion.Text = "";
            insumo.SelectedValue = "1";
            idUsuario.Text = "";
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                IncidenteInsumo nuevo = new IncidenteInsumo();
                nuevo.fechaIncidente = fecha.Text;
                nuevo.descripcion = descripcion.Text;
                nuevo.idInsumo = insumo.SelectedValue;
                nuevo.idResponsble = idUsuario.Text;

                if (nuevo.insertar()) {
                    consola.Text = "Se ha registrado la información.";
                    limpiar();
                }
                else consola.Text = "Ha ocurrido un error, revise que la informacion esté correcta.";
            }
            else consola.Text = "Llenar los campos obligatorios.";
        }
    }
}