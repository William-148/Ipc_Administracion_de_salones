using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas.operador.insumos
{
    public partial class lista_prestamo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                //Retorna el indice de la fila del boton clickeado
                int index = Convert.ToInt32(e.CommandArgument);
                //Retorna la fila completa indicandole el indice anterior
                GridViewRow row = GridView1.Rows[index];

                //indicamos que queremos la primera celda de la fila y guardamos el contenido en la variable

                Response.Redirect("/paginas/operador/insumos/editar-prestamo.aspx?cod="+ row.Cells[0].Text+ "&nom="+ row.Cells[3].Text);

            }
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            switch (filtrar.SelectedValue)
            {
                case "1"://todos
                    DatosPrestamos.SelectCommand = "SELECT I.idInsumoPrestado, I.Fecha, I.Descripcion, U.Nombre, N.Nombre AS Insumo, E.Estado, I.FechaDevuelto FROM InsumoPrestado I, Usuario U, Insumo N, EstadoPrestamo E WHERE I.idUsuario = U.idUsuario AND I.idInsumo = N.idInsumo AND I.idEstadoPrestamo = E.idEstadoPrestamo  ORDER BY I.Fecha desc";
                    GridView1.DataBind();
                    break;

                case "2"://NO Devueltos
                    DatosPrestamos.SelectCommand = "SELECT I.idInsumoPrestado, I.Fecha, I.Descripcion, U.Nombre, N.Nombre AS Insumo, E.Estado, I.FechaDevuelto FROM InsumoPrestado I, Usuario U, Insumo N, EstadoPrestamo E WHERE I.idUsuario = U.idUsuario AND I.idInsumo = N.idInsumo AND I.idEstadoPrestamo = E.idEstadoPrestamo AND I.idEstadoPrestamo = 1 ORDER BY I.Fecha desc";
                    GridView1.DataBind();
                    break;

                case "3"://Devueltos
                    DatosPrestamos.SelectCommand = "SELECT I.idInsumoPrestado, I.Fecha, I.Descripcion, U.Nombre, N.Nombre AS Insumo, E.Estado, I.FechaDevuelto FROM InsumoPrestado I, Usuario U, Insumo N, EstadoPrestamo E WHERE I.idUsuario = U.idUsuario AND I.idInsumo = N.idInsumo AND I.idEstadoPrestamo = E.idEstadoPrestamo AND I.idEstadoPrestamo = 2 ORDER BY I.Fecha desc";
                    GridView1.DataBind();
                    break;
            }
        }
    }
}