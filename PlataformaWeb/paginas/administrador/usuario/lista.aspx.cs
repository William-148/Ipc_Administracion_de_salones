﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas.usuario
{
    public partial class lista : System.Web.UI.Page
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
                GridViewRow row = GridView.Rows[index];

                //indicamos que queremos la primera celda de la fila y guardamos el contenido en la variable
                Session["idUsuarioEditar"] = row.Cells[0].Text;

                Response.Redirect("/paginas/administrador/usuario/editar.aspx");
                //Session["ProductoId"] = Convert.ToInt32(row.Cells[0].Text);
                //Session["NombreProducto"] = row.Cells[1].Text;
                //esponse.Redirect("/page/VerMateriaPrima.aspx");

            }
        }
    }
}