using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlataformaWeb.clases;

namespace PlataformaWeb.paginas.administrador
{
    public partial class cargar_datos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void boton_Click(object sender, EventArgs e)
        {
            if (fileUpload.PostedFile.ContentType == "application/vnd.ms-excel" || 
                fileUpload.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                string fileName = "";
                try
                {
                    fileName = Path.Combine(Server.MapPath("~/import"), Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.PostedFile.FileName));
                    fileUpload.PostedFile.SaveAs(fileName);

                    string conString = "";
                    string ext = Path.GetExtension(fileUpload.PostedFile.FileName);
                    if (ext.ToLower() == ".xls")
                    {
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0; HDR=Yes; IMEX=2 \" ";

                    }
                    else if (ext.ToLower() == ".xlsx")
                    {
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0; HDR=Yes; IMEX=2 \" ";
                    }

                    string query = "Select [Nombre],[Carnet],[Fecha de Nacimiento],[Correo],[Telefono],[Usuario],[Clave],[Palabra Clave] From [Usuarios$]";


                    OleDbConnection con = new OleDbConnection(conString);

                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    con.Close();
                    con.Dispose();

                    //importando a la base de datos

                    string queryBD = "insert into Usuario (Nombre, Carnet, FechaNacimiento, Correo, Telefono, Usuario, Clave, PalabraClave, idRol) \n";

                    int cont = 0;
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        if (cont == 0)
                        {
                            queryBD += "values('" + fila["Nombre"].ToString() + "', '" + fila["Carnet"].ToString() + "', '" + fila["Fecha de Nacimiento"].ToString() + "', '" + fila["Correo"].ToString() + "', '" + fila["Telefono"].ToString() + "', '" + fila["Usuario"].ToString() + "', '" + fila["Clave"].ToString() + "', '" + fila["Palabra Clave"].ToString() + "', '4')";
                        }
                        else
                        {
                            if (fila["Nombre"].ToString() != "")
                            {
                                queryBD += ",\n";
                                queryBD += "      ('" + fila["Nombre"].ToString() + "', '" + fila["Carnet"].ToString() + "', '" + fila["Fecha de Nacimiento"].ToString() + "', '" + fila["Correo"].ToString() + "', '" + fila["Telefono"].ToString() + "', '" + fila["Usuario"].ToString() + "', '" + fila["Clave"].ToString() + "', '" + fila["Palabra Clave"].ToString() + "', '4')";
                            }                            
                        }
                        cont++;                        
                    }
                    //consola.Text = queryBD;
                    
                    if (Usuario.importarUsuarios(queryBD))
                        consola.Text += "\nDatos Importados Correctamente.";
                    else
                        consola.Text += "\nHa ocurrido un error, intente mas tarde.";
                }
                catch(Exception ex)
                {
                    consola.Text = ex.Message;
                }
                finally
                {
                    if (System.IO.File.Exists(fileName))  System.IO.File.Delete(fileName);
                }
            }
        }
    }
}