using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas.estudiante.actividades
{
    public partial class actividades_asistidas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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