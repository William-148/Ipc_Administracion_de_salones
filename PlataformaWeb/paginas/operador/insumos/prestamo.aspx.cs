using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.operador.insumos
{
    public partial class prestamo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool hayCamposVacios()
        {
            return (idUsuario.Text.Trim() == "" || descripcion.Text.Trim() == "");
        }

        private void limpiar()
        {
            insumo.SelectedValue = "1";
            idUsuario.Text = "";
            descripcion.Text = "";
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                PrestamoInsumo nuevo = new PrestamoInsumo();

                nuevo.idInsumo = insumo.SelectedValue;
                nuevo.idUsuario = idUsuario.Text;
                nuevo.descripcion = descripcion.Text;

                if (nuevo.insertar())
                {
                    consola.Text = "La información se ha registrado correctamente.";
                    limpiar();
                }
                else consola.Text = "Ha ocurrido un error, revise si los datos estén correctos.";

            }
            else consola.Text = "Llene todos los campos solicitados.";
            
        }
    }
}