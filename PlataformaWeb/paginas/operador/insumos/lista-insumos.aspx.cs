using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.operador.insumos
{
    public partial class lista_insumos : System.Web.UI.Page
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
                    Session["idInsumoEditar"] = row.Cells[0].Text;
                    tituloFormulario.Text = "Editar Insumo";
                    boton.Text = "Actualizar";
                    idtitulo.Text = "Id. Insumo: " + row.Cells[0].Text;
                    nombre.Text = row.Cells[1].Text;
                    descripcion.Text = row.Cells[2].Text;
                    
                    for (int i = 0; i< tipoInsumo.Items.Count; i++)
                    {
                        if (tipoInsumo.Items[i].Text == row.Cells[3].Text)
                        {
                            tipoInsumo.SelectedIndex = i;
                            break;
                        }                            
                    }
                }
                catch { }
            }
        }

        public bool hayCamposVacios()
        {
            return (nombre.Text.Trim() == "" || descripcion.Text.Trim() == "");
        }

        public void limpiar()
        {
            nombre.Text = "";
            descripcion.Text = "";
            idtitulo.Text = "";
            boton.Text = "Registrar";            
            tituloFormulario.Text = "Nuevo Insumo";
        }

        public void registrar()
        {
            Insumo nuevo = new Insumo();
            nuevo.nombre = nombre.Text;
            nuevo.descripcion = descripcion.Text;
            nuevo.tipo = tipoInsumo.SelectedValue;

            if (nuevo.insertar())
            {
                GridView1.DataBind();
                limpiar();
                Response.Write("<script>alert('El salon se ha registrado correctamente');</script>");
            }
            else Response.Write("<script>alert('Ha ocurrido un error, intente nuevamente');</script>");
        }

        public void actualizar()
        {
            Insumo nuevo = new Insumo();
            nuevo.idInsumo = Session["idInsumoEditar"].ToString();
            nuevo.nombre = nombre.Text;
            nuevo.descripcion = descripcion.Text;
            nuevo.tipo = tipoInsumo.SelectedValue;

            if (nuevo.actualizar())
            {
                GridView1.DataBind();
                Response.Write("<script>alert('El salon se ha actualizado correctamente');</script>");
            }
            limpiar();
        }


        protected void boton_Click(object sender, EventArgs e)
        {
            //verifica si la accion es registrar o actualizar
            if (!hayCamposVacios())
            {
                if (boton.Text == "Registrar") registrar();//Registra el insumo
                else actualizar();//Actualiza el insumo
            }
            else mensaje.Text = "Ingrese los datos solicitados.";
        }

        protected void cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}