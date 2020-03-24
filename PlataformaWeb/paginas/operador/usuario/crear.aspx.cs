using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.operador.usuario
{
    public partial class crear : System.Web.UI.Page
    {
        //operador
        private DateTime fecha = new DateTime(1990, 1, 1);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                nacimiento.Text = fecha.ToString("yyyy-MM-dd");
            }
        }

        private bool hayCamposVacios()
        {
            bool camposVacios = false;


            if (nombre.Text == "" || carnet.Text == "" || nacimiento.Text == "" ||
                correo.Text == "" || usuario.Text == "" || clave.Text == "" ||
                clave2.Text == "" || palabraClave.Text == "")
            {
                camposVacios = true;
            }
            return camposVacios;
        }

        private bool clavesIguales()
        {
            bool iguales = false;

            if (clave.Text == clave2.Text) iguales = true;

            return iguales;
        }

        public bool existeUsuario()
        {
            string existe = Usuario.existeUsuario(usuario.Text);
            if (existe == "0") return false;
            else return true;
        }

        public void limpiarFormulario()
        {
            nombre.Text = "";
            carnet.Text = "";
            nacimiento.Text = fecha.ToString("yyyy-MM-dd");
            correo.Text = "";
            telefono.Text = "";
            usuario.Text = "";
            palabraClave.Text = "";

        }

        public void registrarUsuario()
        {
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario.rol = Convert.ToInt32(rol.SelectedItem.Value);
            nuevoUsuario.nombre = nombre.Text;
            nuevoUsuario.nacimiento = nacimiento.Text;
            nuevoUsuario.correo = correo.Text;
            nuevoUsuario.usuario = usuario.Text;
            nuevoUsuario.password = clave.Text;
            nuevoUsuario.palabraClave = palabraClave.Text;

            try { nuevoUsuario.carnet = Convert.ToInt32(carnet.Text); }
            catch { nuevoUsuario.carnet = 0; }

            try { nuevoUsuario.telefono = Convert.ToInt32(telefono.Text); }
            catch { nuevoUsuario.telefono = 0; }

            if (nuevoUsuario.insertar())
            {
                mensaje.Text = "** Registro realizado correctamente.";
                limpiarFormulario();
            }
            else mensaje.Text = "** Ha ocurrido un error, intente de nuevo.";
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                if (clavesIguales())
                {
                    if (!existeUsuario()) registrarUsuario();
                    else mensaje.Text = "** El usuario ingresado ya existe.";
                }
                else mensaje.Text = "** Las contraseñas no coinciden.";
            }
            else mensaje.Text = "** No se pudo registrar debido a que existen campos vacíos.";
        }
    }
}