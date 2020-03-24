using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas.estudiante
{
    public partial class masterEstudiante : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombre"] != null && Session["rol"] != null)
            {
                int rol = Convert.ToInt32(Session["rol"]);
                switch (rol)
                {
                    case 1:
                        Response.Redirect("/paginas/administrador/usuario/lista.aspx");
                        break;
                    case 2:
                        Response.Redirect("/paginas/operador/usuario/lista.aspx");
                        break;
                    case 3:
                        Response.Redirect("/paginas/instructor/home.aspx");
                        break;
                    case 4:
                        nombreUsuario.Text = Session["nombre"].ToString();
                        break;
                    default:
                        Response.Redirect("/paginas/login.aspx");
                        break;
                }
            }
            else Response.Redirect("/paginas/login.aspx?user=none");
        }
    }
}