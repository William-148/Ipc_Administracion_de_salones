using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.usuario
{
    public partial class editar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuarioEditar"] != null)
            {
                if (!IsPostBack)
                {

                    int idUsuario = Convert.ToInt32(Session["idUsuarioEditar"]);
                    Usuario user = Usuario.getUsuario(idUsuario);
                    Session["UsuarioEditar"] = user.usuario;
                    cargarDatos(user);
                }
            }
            else Response.Redirect("/paginas/administrador/usuario/lista.aspx");

        }

        private void cargarDatos(Usuario user)
        {
            rol.Text += user.rolTxt;
            nombre.Text = user.nombre;
            carnet.Text = user.carnet.ToString();            
            //nacimiento.Text = DateTime.Today.ToString("yyyy-MM-dd");
            nacimiento.Text = Analizador.getFecha(user.nacimiento).ToString("yyyy-MM-dd");
            correo.Text = user.correo;
            telefono.Text = user.telefono.ToString();
            usuario.Text = user.usuario;
        }
        
        private void ActualizarUsuario()
        {
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario.idUsuario = Convert.ToInt32(Session["idUsuarioEditar"]);
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
            
            if (nuevoUsuario.actualizar())
            {
                mensaje.Text = "** La actualización se realizó correctamente.";
                Session["UsuarioEditar"] = nuevoUsuario.usuario;
            }
            else  mensaje.Text = "** Ha ocurrido un error, intente de nuevo.";
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                bool updateUsuario = false;
                string aux = (string)Session["UsuarioEditar"];
                if (!aux.Equals(usuario.Text))updateUsuario = true;                

                if (clavesIguales())
                {
                    if (updateUsuario)
                    {
                        if (!existeUsuario())ActualizarUsuario();
                        else mensaje.Text = "** El usuario ingresado ya existe.";
                        
                    }
                    else ActualizarUsuario();
                }
                else mensaje.Text = "** Las contraseñas no coinciden.";
            }
            else mensaje.Text = "** No se pudo registrar debido a que existen campos vacíos.";     
        }

        private bool hayCamposVacios()
        {
            bool camposVacios = false;


            if (nombre.Text == "" || carnet.Text == "" || nacimiento.Text == "" ||
                correo.Text == "" || usuario.Text == "" )
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
            if (existe == "0")  return false;            
            else  return true;
        }

        protected void salir_Click(object sender, EventArgs e)
        {
            Session["idUsuarioEditar"] = null;
            Response.Redirect("/paginas/administrador/usuario/lista.aspx");
        }
    }
}