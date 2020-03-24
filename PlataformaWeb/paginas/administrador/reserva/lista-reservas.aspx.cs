using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas.administrador.Reserva
{
    public partial class lista_reservas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                try
                {
                    //Retorna el indice de la fila del boton clickeado
                    int index = Convert.ToInt32(e.CommandArgument);
                    //Retorna la fila completa indicandole el indice anterior
                    GridViewRow row = GridView1.Rows[index];
                    //indicamos que queremos la primera celda de la fila y guardamos el contenido en la variable
                    Session["idReservaEditar"] = row.Cells[0].Text;
                }
                catch { }

                Response.Redirect("/paginas/administrador/reserva/ver-reserva.aspx");

            }
        }
    }
}