using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas
{
    public partial class recuperar_cuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool hayCamposVacios() => (correo.Text.Trim() == "" || palabraClave.Text.Trim() == "");

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                Usuario user = Usuario.recuperarCuenta(correo.Text);

                if (user.palabraClave != "")
                {
                    if (user.palabraClave == palabraClave.Text)
                    {
                        string mensaje = "Saludos "+user.nombre+", ésta es la información de su cuenta: \nNombre de Usuario: "+user.usuario+"\nContraseña: "+user.password;
                        if (Correo.enviar(correo.Text, mensaje))
                        {
                            palabraClave.Text = "";
                            correo.Text = "";
                            Response.Write("<script>alert('La contraseña fue enviada a su correo.');</script>");
                        }
                        else this.mensaje.Text = "Ha ocurrido un error, intente más tarde.";

                    } else Response.Write("<script>alert('La palabra clave es incorrecta.');</script>");
                }
                else mensaje.Text = "Error: La información no existe en el sistema.";                
            }
            else mensaje.Text = "Error: Llene todos los campos.";

        }
    }
}