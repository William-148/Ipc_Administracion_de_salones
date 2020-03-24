using PlataformaWeb.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["nombre"] = null;
            Session["rol"] = null;
            if (Request.Params["user"] != null && !IsPostBack)
            {
                string parametro = Request.Params["user"].ToString();
                if (parametro == "none") Response.Write("<script>alert('Inicie Sesión Para Acceder a la Plataforma');</script>");
            }
        }

        private bool hayCamposVacios()
        {
            bool campoVacio = false;
            if (usuario.Text == "" || clave.Text =="")  campoVacio = true;            

            return campoVacio;
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                Usuario user = Usuario.getUsuario(usuario.Text, clave.Text);

                if (user.idUsuario > 0)
                {
                    Session["rol"] = user.rol;
                    Session["idUsuario"] = user.idUsuario;
                    Session["nombre"] = user.nombre;
                    Session["usuario"] = user.usuario;

                    switch (user.rol)
                    {
                        case 1:
                            //administrador
                            Response.Redirect("/paginas/administrador/home.aspx");
                            break;
                        case 2:
                            //operador
                            Response.Redirect("/paginas/operador/home.aspx");
                            break;
                        case 3:
                            //instructor
                            Response.Redirect("/paginas/instructor/home.aspx");
                            break;
                        case 4:
                            //estudiante
                            Response.Redirect("/paginas/estudiante/home.aspx");
                            break;
                    }
                }
                else mensaje.Text = "Error: Datos ingresados no existen en el sistema.";
                
            }
            else mensaje.Text = "Error: Llene todos los campos.";
            
        }
    }
}