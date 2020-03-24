using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.estudiante.actividades
{
    public partial class detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["cod"] != null )
            {
                if (Actividad.asistioEnActividad(Session["idUsuario"].ToString(), Request.Params["cod"].ToString()))
                    asistencia.Visible = true;
                
                Actividad actividad = Actividad.getDetalleActividad(Request.Params["cod"].ToString());
                DateTime date = Convert.ToDateTime(actividad.fecha);
                if (DateTime.Now.Date >= date.Date) descargar.Visible = true;

                if (actividad != null)
                {
                    idActividad1.Text = actividad.id;
                    nombre.Text = actividad.nombre;
                    edificio.Text = actividad.edificio;
                    salon.Text = actividad.salon;
                    hora.Text = actividad.hora;
                    tipoActividad.Text = actividad.tipoActividadTxt;
                    instructor.Text = actividad.instructor;
                    descripcion.Text = actividad.descripcion;
                    fecha.Text = date.ToString("dd/MM/yyyy");
                }


                else Response.Redirect("<script>alert('Ha ocurrido un error, id no reconocido');</script>");
            }
        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (Actividad.desmatricularse(Session["idUsuario"].ToString(), idActividad1.Text))
                Response.Redirect("/paginas/estudiante/actividades/actividades-matriculadas.aspx?msg=2");
            else Response.Write("<script>alert('Ha ocurrido un error, intente más tarde.');</script>");
        }

        protected void descargar_Click(object sender, EventArgs e)
        {
            try
            {
                Actividad a = Actividad.getPresentacion(idActividad1.Text);
                Response.Clear();
                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "presentacion" + a.extensionPresentacion));
                string extens = extensiones(a.extensionPresentacion);
                Response.ContentType = extens;
                Response.BinaryWrite(a.presentacion);
                Response.End();
            }
            catch { }
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
    }
}