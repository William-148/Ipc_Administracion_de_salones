using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas.operador.insumos
{
    public partial class lista_incidentes : System.Web.UI.Page
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
                Response.Redirect("/paginas/operador/insumos/editar-incidente.aspx?cod="+ row.Cells[0].Text+"&nom="+ row.Cells[2].Text);
            }
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            switch (filtrar.SelectedValue)
            {
                case "1"://todos
                    DatosIncidentes.SelectCommand = "SELECT [idIncidenteInsumo], [FechaIncidente], U.[Nombre], N.[Nombre] As [Insumo], EI.[Estado], [FechaCreacion]  FROM [IncidenteInsumo] I, [Usuario] U, [Insumo] N, [EstadoIncidente] EI WHERE I.idUsuario = U.idUsuario AND I.idInsumo = N.idInsumo AND I.idEstadoIncidente = EI.idEstadoIncidente ORDER BY I.FechaCreacion desc";
                    GridView1.DataBind();
                    break;

                case "2"://NO RESUELTOS
                    DatosIncidentes.SelectCommand = "SELECT[idIncidenteInsumo], [FechaIncidente], U.[Nombre], N.[Nombre] As[Insumo], EI.[Estado], [FechaCreacion] FROM[IncidenteInsumo] I, [Usuario] U, [Insumo] N, [EstadoIncidente] EI WHERE I.idEstadoIncidente = 1 AND I.idUsuario = U.idUsuario AND I.idInsumo = N.idInsumo AND I.idEstadoIncidente = EI.idEstadoIncidente ORDER BY I.FechaCreacion desc";
                    GridView1.DataBind();
                    break;

                case "3"://RESUELTOS
                    DatosIncidentes.SelectCommand = "SELECT[idIncidenteInsumo], [FechaIncidente], U.[Nombre], N.[Nombre] As [Insumo], EI.[Estado], [FechaCreacion]  FROM [IncidenteInsumo] I, [Usuario] U, [Insumo] N, [EstadoIncidente] EI WHERE I.idEstadoIncidente = 2 AND I.idUsuario = U.idUsuario AND I.idInsumo = N.idInsumo AND I.idEstadoIncidente = EI.idEstadoIncidente ORDER BY I.FechaCreacion desc";
                    GridView1.DataBind();
                    break;
            }
        }
    }
}