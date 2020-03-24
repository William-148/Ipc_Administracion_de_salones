using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas.estudiante.actividades
{
    public partial class actividades_matriculadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["msg"] != null && !IsPostBack)
            {
                string aux = Request.Params["msg"].ToString();

                switch (aux)
                {
                    case "1":
                        Response.Write("<script>alert('Se ha matriculado correctamente');</script>");
                        break;
                    case "2":
                        Response.Write("<script>alert('Se ha desmatriculado correctamente');</script>");
                        break;
                    case "3":
                        Response.Write("<script>alert('Se guardó su asistencia de la actividad');</script>");
                        break;
                    case "4":
                        Response.Write("<script>alert('Se guardó sus respuestas y su asistencia de la actividad');</script>");
                        break;
                    case "5":
                        Response.Write("<script>alert('No está matriculado en la actividad');</script>");
                        break;
                    case "6":
                        Response.Write("<script>alert('Ya ha confirmado su asistencia en la actividad anteriormente');</script>");
                        break;
                    case "7":
                        Response.Write("<script>alert('La actividad ya ha finalizado');</script>");
                        break;
                    case "8":
                        Response.Write("<script>alert('La actividad aún no se realiza');</script>");
                        break;
                    case "9":
                        Response.Write("<script>alert('La actividad no ha finalizado');</script>");
                        break;
                }               
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "detalle":
                    try
                    {
                        //Retorna el indice de la fila del boton clickeado
                        int index = Convert.ToInt32(e.CommandArgument);
                        //Retorna la fila completa indicandole el indice anterior
                        GridViewRow row = GridView1.Rows[index];
                        //indicamos que queremos la primera celda de la fila y guardamos el contenido en la variable
                        //Session["idReservaVer"] = row.Cells[0].Text;
                        //Session["reservaSalon"] = row.Cells[1].Text;
                        //Session["reservaEdificio"] = row.Cells[2].Text;
                        Response.Redirect("/paginas/estudiante/actividades/detalle.aspx?cod=" + row.Cells[0].Text);
                    }
                    catch { }
                    break;
            }
        }
    }
}