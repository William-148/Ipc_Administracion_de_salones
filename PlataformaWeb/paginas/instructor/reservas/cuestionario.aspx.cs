using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.instructor.reservas
{
    public partial class cuestionario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Params["cod"] != null && !IsPostBack)
            {
                Session["idActividad"] = Request.Params["cod"].ToString();
                //SqlDataSource1.SelectCommand = "SELECT * FROM [Pregunta] WHERE idActividad = " + Request.Params["cod"].ToString();
            }

        }

        private bool hayCamposVacios() {
            return (respuesta.Text.Trim() =="" || pregunta.Text.Trim() =="");
        }        

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                Pregunta p = new Pregunta();
                p.pregunta = pregunta.Text;
                p.respuesta = respuesta.Text;
                p.idActividad = Request.Params["cod"].ToString();
                p.insertar();
                pregunta.Text = "";
                respuesta.Text = "";
                DataList1.DataBind();
            }
            else consola.Text = "Llene todos los campos.";
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    DataList1.EditItemIndex = e.Item.ItemIndex;
                    DataList1.DataBind();

                    break;

                case "eliminar":
                    Pregunta.eliminar(e.CommandArgument.ToString());
                    DataList1.EditItemIndex = -1;
                    DataList1.DataBind();
                    break;

                case "actualizar":
                    Pregunta p = new Pregunta();
                    p.id = e.CommandArgument.ToString();
                    p.pregunta = ((TextBox)e.Item.FindControl("TextBox1")).Text;
                    p.respuesta = ((TextBox)e.Item.FindControl("TextBox2")).Text;
                    p.actualizar();
                    DataList1.EditItemIndex = -1;
                    DataList1.DataBind();

                    break;

                case "cancelar":
                    DataList1.EditItemIndex = -1;
                    DataList1.DataBind();
                    break;
            }
        }
    }
}