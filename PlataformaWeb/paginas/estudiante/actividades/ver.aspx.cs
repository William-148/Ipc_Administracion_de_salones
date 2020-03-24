using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.estudiante.actividades
{
    public partial class ver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["cod"] != null )
            {
                

                Actividad actividad = Actividad.getDetalleActividad(Request.Params["cod"].ToString());

                if (Actividad.estaMatriculado(Session["idUsuario"].ToString(), Request.Params["cod"].ToString()) || !Actividad.hayCupo(idActividad1.Text))
                    boton.Visible = false;

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
                    fecha.Text = Convert.ToDateTime(actividad.fecha).ToString("dd/MM/yyyy");
                }
                else Response.Redirect("<script>alert('Ha ocurrido un error, id no reconocido');</script>");
            }
        }
               
        protected void boton_Click(object sender, EventArgs e)
        {                    
            if (Actividad.matricularse(Session["idUsuario"].ToString(), idActividad1.Text))
                Response.Redirect("/paginas/estudiante/actividades/actividades-matriculadas.aspx?msg=1");
            else Response.Write("<script>alert('Ha ocurrido un error, intente más tarde.');</script>");          
            
        }
        
        
    }
}