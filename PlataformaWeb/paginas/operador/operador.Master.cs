using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas.operador
{
    public partial class operador : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //nombreUsuario.Text = Session["nombre"].ToString();
            if (Session["nombre"] != null && Session["rol"] != null)
            {
                int rol = Convert.ToInt32(Session["rol"]);
                switch (rol)
                {
                    case 1:
                        Response.Redirect("/paginas/administrador/usuario/lista.aspx");
                        break;
                    case 2:                        
                        nombreUsuario.Text = Session["nombre"].ToString();
                        break;
                    case 3:
                        Response.Redirect("/paginas/instructor/home.aspx");
                        break;
                    case 4:
                        Response.Redirect("/paginas/estudiante/home.aspx");
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