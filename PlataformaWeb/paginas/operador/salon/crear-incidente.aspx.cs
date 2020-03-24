using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.operador.salon
{
    public partial class crear_incidente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)            
                fecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private bool hayCamposVacios() => (fecha.Text.Trim() == "" || idUsuario.Text.Trim() == "" || descripcion.Text.Trim() == "");

        private void limpiar()
        {
            fecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            idUsuario.Text = "";
            descripcion.Text = "";
        }
                
        private void registrarIncidente()
        {
            IncidenteSalon incidente = new IncidenteSalon();
            incidente.fechaIncidente = fecha.Text;
            incidente.idUsuario = idUsuario.Text;
            incidente.descripcion = descripcion.Text;
            incidente.idSalon = salon.SelectedValue;

            if (incidente.insertar()) {
                consola.Text = "El registro se ha guardado correctamente.";
                limpiar();
            }
            else consola.Text = "Error: El id del usuario es erroneo.";

        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
                registrarIncidente();
            else
            {
                if (idUsuario.Text == "")
                    consola.Text = "Error: Ingrese el Id del Usuario Responsable.";
                else if (descripcion.Text == "")
                    consola.Text = "Error: Ingrese la descripción del incidente";
                else
                    consola.Text = "Error: Ingrese la fecha del incidente.";
            }    
        }

        protected void verSalones_Click(object sender, EventArgs e)
        {
            nombresSalones.SelectCommand = "SELECT [idSalon], [Salon] FROM Salon WHERE idEdificio = "+edificio.SelectedValue+" ORDER BY Salon";

            tituloSalon.Text = "Salon*("+edificio.SelectedItem.Text+"):";

            salon.DataBind();
        }
    }
}