using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.instructor.reservas
{
    public partial class actividad : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Params["cod"] != null )
            {
                Actividad actividad = Actividad.getActividad(Request.Params["cod"].ToString());

                if (actividad != null)
                {
                    if (Actividad.asistioEnActividad(Session["idUsuario"].ToString(), Request.Params["cod"].ToString()))
                        asistencia.Visible = true;

                    idActividad.Text = actividad.id;                    
                    tipoActividad.Text = actividad.tipoActividadTxt;
                    if (actividad.nombre != "" && actividad.descripcion != "")
                    {
                        nombre.Text = actividad.nombre;
                        descripcion.Text = actividad.descripcion;
                    }
                    if (actividad.extensionPresentacion == "") hayPresentacion.Text = "Debe subir su presentación.";
                    else {
                        hayPresentacion.Text = "Presentación cargada.";
                        Session["presentacion"] = actividad.presentacion;
                        Session["extension"] = actividad.extensionPresentacion;
                    }
                }
                else Response.Redirect("<script>alert('Ha ocurrido un error, id no reconocido');</script>");
            }
        }

        private bool hayCamposVacios()
        {
            return (nombre.Text.Trim() == "" || descripcion.Text.Trim() == "");
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (!hayCamposVacios())
            {
                Actividad actividad = new Actividad();
                actividad.id = idActividad.Text;
                actividad.nombre = nombre.Text;
                actividad.descripcion = descripcion.Text;
                if (actividad.actualizar()) consola.Text = "Información guardada correctamente.";
                else consola.Text = "Ha ocurrido un error, revise los datos.";
            }
            else consola.Text = "Llene los campos solicitados.";            
        }

        protected void subir_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtenemos el nombre del archivo con su extensión
                string extension = System.IO.Path.GetExtension(upload.FileName);

                //Obtenemos la matriz de bytes del archivo
                int sizeArch = upload.PostedFile.ContentLength; //tamaño del archivo
                byte[] archivo = new byte[sizeArch];//Creamos el array de bytes para guardar el archivo
                //upload.PostedFile.InputStream.Read(archivo, 0, sizeArch);//guardamos el archivo en el array
                archivo = upload.FileBytes;
                //Llamamos al metodo para subir la presentacion
                if (sizeArch == 0) return;
                bool exito = Actividad.subirPresentacion(idActividad.Text, archivo, extension);
                if (exito) {
                    consola.Text = "Se ha guardado el archivo correctamente.";
                    hayPresentacion.Text = "Presentación cargada.";
                }
                else consola.Text = "Ha ocurrido un error, intente nuevamente";
            }
            catch(Exception ex)
            {
                consola.Text = ex.Message;
            }
        }

        protected void descargar_Click(object sender, EventArgs e)
        {
            
            if (Session["presentacion"] != null)
            {
                try
                {
                    Response.Clear();
                    Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "presentacion" + Session["extension"].ToString()));
                    string extens = extensiones(Session["extension"].ToString());
                    Response.ContentType = extens;
                    Response.BinaryWrite((byte[])Session["presentacion"]);
                    Response.End();
                }
                catch (Exception ex)
                {
                    consola.Text = ex.Message;
                }                
            }
        }

        private string extensiones(string ext)
        {
            switch (ext)
            {
                case ".jpg":
                case ".gif":
                case ".png":
                    return "image/" + ext.Substring(1);
                default:
                    return "application/" + ext.Substring(1);
            }
        }

        protected void cuestionario_Click(object sender, EventArgs e)
        {
            if (Request.Params["cod"] != null)
            {
                Response.Redirect("/paginas/instructor/reservas/cuestionario.aspx?cod=" + Request.Params["cod"].ToString());
            }
            else consola.Text="Ha ocurrido un error, recargue la página.";
            
        }
    }
}