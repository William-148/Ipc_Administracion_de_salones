using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.estudiante
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] != null )
            {
                if (!IsPostBack)
                {
                    validarInformacion();
                    if (Pregunta.hayCuestionario(Request.Params["id"].ToString()))
                        SqlDataSource1.SelectCommand = "SELECT idPregunta, Pregunta FROM [Pregunta] WHERE idActividad = " + Request.Params["id"].ToString();
                    else
                    {
                        if(Actividad.registrarAsistencia(Request.Params["id"].ToString(), Session["idUsuario"].ToString()))
                            Response.Redirect("/paginas/estudiante/actividades/actividades-matriculadas.aspx?msg=3");
                    }
                }                
            }
            else Response.Redirect("/paginas/estudiante/home.aspx");
        }

        private void validarInformacion()
        {
            Actividad actividad = Actividad.getDetalleActividad(Request.Params["id"].ToString());
            DateTime date = Convert.ToDateTime(actividad.fecha);
            DateTime horafin = Convert.ToDateTime(actividad.fin);
            if (date.Date < DateTime.Now.Date)
                Response.Redirect("/paginas/estudiante/actividades/actividades-matriculadas.aspx?msg=7");
            else if(date.Date > DateTime.Now.Date)
                Response.Redirect("/paginas/estudiante/actividades/actividades-matriculadas.aspx?msg=8");
            else if (date.Date == DateTime.Now.Date)
            {
                if(DateTime.Now.TimeOfDay < horafin.TimeOfDay)
                    Response.Redirect("/paginas/estudiante/actividades/actividades-matriculadas.aspx?msg=9");
            }     

            if (!Actividad.estaMatriculado(Session["idUsuario"].ToString(), Request.Params["id"].ToString()))
                Response.Redirect("/paginas/estudiante/actividades/actividades-matriculadas.aspx?msg=5");
            if(Actividad.asistioEnActividad(Session["idUsuario"].ToString(), Request.Params["id"].ToString()))
                Response.Redirect("/paginas/estudiante/actividades/actividades-matriculadas.aspx?msg=6");

        }

        protected void boton_Click(object sender, EventArgs e)
        {
            bool flag = true;
            string txt = "INSERT INTO Respuesta (idPregunta, Respuesta, idUsuario)";
            string idUsuario = Session["idUsuario"].ToString();
                            
            for (int i = 0; i < DataList1.Items.Count; i++)
            {                
                string idPregunta = ((Label)DataList1.Items[i].FindControl("idPregunta")).Text;
                string respuesta = ((TextBox)DataList1.Items[i].FindControl("respuesta")).Text.Trim();

                if (!(respuesta == ""))
                {
                    if (i == 0) txt += " VALUES ( '" + idPregunta + "', '" + respuesta + "', '" + idUsuario + "' )";
                    else txt += ", ('" + idPregunta + "' , '" + respuesta + "' , '" + idUsuario + "') ";
                }
                else
                {
                    titulo.Text = "Cuestionario - Responder todas las Preguntas";
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                if (Pregunta.registrarPreguntas(txt))
                {
                    Actividad.registrarAsistencia(Request.Params["id"].ToString(), Session["idUsuario"].ToString());
                    Response.Redirect("/paginas/estudiante/actividades/actividades-matriculadas.aspx?msg=4");
                }
            }
        }
    }
}