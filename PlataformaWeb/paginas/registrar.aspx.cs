using PlataformaWeb.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlataformaWeb.paginas
{
    public partial class registrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime fecha = new DateTime(1990, 1, 1);
                nacimiento.Text = fecha.ToString("yyyy-MM-dd");
            }

        }

        private bool hayCamposVacios()
        {
            bool camposVacios = false;

            
            if (nombre.Text == "" || carnet.Text == "" || nacimiento.Text == "" ||
                correo.Text == "" || usuario.Text == "" || clave.Text == "" || 
                clave2.Text == "" || palabraClave.Text == "" )
            {
                camposVacios = true;
            }
            return camposVacios;
        }

        private bool clavesIguales()
        {
            bool iguales = false;
            if (clave.Text == clave2.Text)  iguales = true;
            
            return iguales;
        }

        public bool existeUsuario() {

            string existe = Usuario.existeUsuario(usuario.Text);
            if (existe == "0") return false;            
            else return true;            
            
        }

        public void registrarUsuario()
        {
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario.rol = Usuario.estudiante;
            nuevoUsuario.nombre = nombre.Text;
            nuevoUsuario.nacimiento = nacimiento.Text;
            nuevoUsuario.correo = correo.Text;
            nuevoUsuario.usuario = usuario.Text;
            nuevoUsuario.password = clave.Text;
            nuevoUsuario.palabraClave = palabraClave.Text;

            try{nuevoUsuario.carnet = Convert.ToInt32(carnet.Text);}
            catch{ nuevoUsuario.carnet = 0; }

            try{nuevoUsuario.telefono = Convert.ToInt32(telefono.Text); }
            catch{ nuevoUsuario.telefono = 0; }
            
            if (nuevoUsuario.insertar()) Response.Redirect("/paginas/login.aspx");            
            else mensaje.Text = "Ha ocurrido un error, intente de nuevo.";
            
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                if (clavesIguales())
                {
                    if (!existeUsuario()) registrarUsuario();                    
                    else  mensaje.Text = "** El usuario ingresado ya existe."; 
                }
                else { mensaje.Text = "** Las contraseñas no coinciden.";  }
            }
            else { mensaje.Text = "** No se pudo registrar debido a que existen campos vacíos."; }
        }
    }
}