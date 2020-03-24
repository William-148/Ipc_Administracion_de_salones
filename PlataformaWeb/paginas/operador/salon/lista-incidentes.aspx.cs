using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas.operador.salon
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

                //indicamos que queremos la primera celda de la fila y guardamos el contenido en la variable
                Session["idIncidenteEditar"] = row.Cells[0].Text;
                Session["IncidenteResponsable"] = row.Cells[2].Text;

                Response.Redirect("/paginas/operador/salon/editar-incidente.aspx");

            }
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            switch (filtrar.SelectedValue)
            {
                case "1"://todos
                    DatosIncidentes.SelectCommand = "SELECT [idIncidenteSalon], [FechaIncidente], U.[Nombre],  E.[Nombre] AS [Edificio], S.[Salon], EI.[Estado], [FechaCreacion]  FROM [IncidenteSalon] I, [Usuario] U, [Salon] S, [Edificio] E, [EstadoIncidente] EI WHERE I.idUsuario = U.idUsuario AND I.idSalon = S.idSalon AND S.idEdificio = E.idEdificio AND  I.idEstadoIncidente = EI.idEstadoIncidente ORDER BY I.FechaCreacion desc";
                    GridView1.DataBind();
                    break;

                case "2"://NO RESUELTOS
                    DatosIncidentes.SelectCommand = "SELECT [idIncidenteSalon], [FechaIncidente], U.[Nombre],  E.[Nombre] AS [Edificio], S.[Salon], EI.[Estado], [FechaCreacion] FROM[IncidenteSalon] I, [Usuario] U, [Salon] S, [Edificio] E, [EstadoIncidente] EI WHERE I.idEstadoIncidente = 1 AND I.idUsuario = U.idUsuario AND I.idSalon = S.idSalon AND S.idEdificio = E.idEdificio AND  I.idEstadoIncidente = EI.idEstadoIncidente ORDER BY I.FechaCreacion desc";
                    GridView1.DataBind();
                    break;

                case "3"://RESUELTOS
                    DatosIncidentes.SelectCommand = "SELECT [idIncidenteSalon], [FechaIncidente], U.[Nombre],  E.[Nombre] AS [Edificio], S.[Salon], EI.[Estado], [FechaCreacion] FROM[IncidenteSalon] I, [Usuario] U, [Salon] S, [Edificio] E, [EstadoIncidente] EI  WHERE I.idEstadoIncidente = 2 AND I.idUsuario = U.idUsuario AND I.idSalon = S.idSalon AND S.idEdificio = E.idEdificio AND  I.idEstadoIncidente = EI.idEstadoIncidente ORDER BY I.FechaCreacion desc";
                    GridView1.DataBind();
                    break;
            }
        }
    }
}