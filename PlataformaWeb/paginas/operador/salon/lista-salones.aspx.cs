using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.operador.salon
{
    public partial class lista_salones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool hayCamposVacios()
        {
            if (salon.Text == "" || capacidad.Text=="") return true;
            else return false;
        }

        private void limpiar()
        {//Limpia el formulario
            edificio.SelectedIndex = 0;
            salon.Text = "";
            capacidad.Text = "";
            estado.SelectedIndex = 0;
            boton.Text = "Registrar";
            idtitulo.Text = "";
            tituloFormulario.Text = "Nuevo Salón";
        }

        private void registrarSalon()
        {
            Salon nuevo = new Salon();

            nuevo.salon = salon.Text;
            try {nuevo.capacidad = Convert.ToInt32(capacidad.Text); }
            catch { nuevo.capacidad = 0;  }
            nuevo.idEdificio = Convert.ToInt32(edificio.SelectedValue);
            nuevo.idEstadoSalon = Convert.ToInt32(estado.SelectedValue);

            if (nuevo.insertar()) {
                GridView1.DataBind();
                limpiar();
                Response.Write("<script>alert('El salon se ha registrado correctamente');</script>");
            }
            else Response.Write("<script>alert('El salon ya está registrado');</script>");
        }

        private void actualizarSalon()
        {
            Salon nuevo = new Salon();

            nuevo.id = Convert.ToInt32(Session["idSalonEditar"]);
            nuevo.salon = salon.Text;
            try { nuevo.capacidad = Convert.ToInt32(capacidad.Text); }
            catch { nuevo.capacidad = 0; }
            nuevo.idEdificio = Convert.ToInt32(edificio.SelectedValue);
            nuevo.idEstadoSalon = Convert.ToInt32(estado.SelectedValue);

            if (nuevo.actualizar()) {
                GridView1.DataBind();
                Response.Write("<script>alert('El salon se ha actualizado correctamente');</script>");
            }

            limpiar();
        }

        private void cargarDatos(Salon datosSalon)
        {//Carga los datos de un salon al formulario
            edificio.SelectedValue = datosSalon.idEdificio.ToString();
            salon.Text = datosSalon.salon.ToString();
            estado.SelectedValue = datosSalon.idEstadoSalon.ToString();
            boton.Text = "Actualizar";
            tituloFormulario.Text = "Editar Salón";
            idtitulo.Text = "Id. Salón: "+Session["idSalonEditar"];
            if (datosSalon.capacidad == 0) capacidad.Text = "";
            else capacidad.Text = datosSalon.capacidad.ToString();
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            //verifica si la accion es registrar o actualizar
            if (!hayCamposVacios()) {
                if (boton.Text == "Registrar") registrarSalon();//Registra el salon
                else actualizarSalon();//Actualiza el salon
            }
            else mensaje.Text = "Ingrese los datos solicitados.";
        }        

        protected void cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //Retorna el indice de la fila del boton clickeado
                int index = Convert.ToInt32(e.CommandArgument);
                //Retorna la fila completa indicandole el indice anterior
                GridViewRow row = GridView1.Rows[index];
                //indicamos que queremos la primera celda de la fila y guardamos el contenido en la variable
                Session["idSalonVer"] = row.Cells[0].Text;
                Session["idSalonEditar"] = row.Cells[0].Text;
                Session["edificioVer"] = row.Cells[1].Text;
                Session["salon"] = row.Cells[2].Text;
            }
            catch{ }

            if (e.CommandName == "editar")
            {
                int aux = Convert.ToInt32(Session["idSalonEditar"]);                
                cargarDatos(Salon.getSalon(aux));

            }
            else if(e.CommandName == "verReservas")
            {
                Response.Redirect("/paginas/operador/salon/ver-reservas.aspx");
            }

        }
    }
}