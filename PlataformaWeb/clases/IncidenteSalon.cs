using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlataformaWeb.clases
{
    public class IncidenteSalon
    {
        public string idIncidenteSalon { set; get; }
        public string fechaIncidente { set; get; }
        public string descripcion { set; get; }
        public string idUsuario { set; get; }
        public string idSalon { set; get; }
        public string idEstadoIncidente { set; get; }
        public string fechaCreacion { set; get; }

        public string idEdificio { set; get; }


        public bool insertar()
        {
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {                
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("IncidenteSalon_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@fechaIncidente", fechaIncidente);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@idSalon", idSalon);
                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable                
                if (cmd.ExecuteNonQuery() > 0) exito = true;
            }
            catch  {exito = false;}
            finally {conexion.Close();}

            return exito;
        }

        public bool actualizar()
        {
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("IncidenteSalon_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idIncidenteSalon", idIncidenteSalon);
                cmd.Parameters.AddWithValue("@fechaIncidente", fechaIncidente);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@idSalon", idSalon);
                cmd.Parameters.AddWithValue("@idEstadoIncidente", idEstadoIncidente);
                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable                
                if (cmd.ExecuteNonQuery() > 0) exito = true;
            }
            catch { exito = false; }
            finally { conexion.Close(); }

            return exito;
        }

        public static IncidenteSalon getIncidente(int idIncidente)
        {
            IncidenteSalon inc = new IncidenteSalon();
            SqlConnection conexion = Conexion.getConexion();

            try
            {
                SqlCommand cmd = new SqlCommand("IncidenteSalon_GetPorSuId", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idIncidenteSalon", idIncidente);
                conexion.Open();
                //Ejecutamos el comando y guardamos el resultado en un objeto SqlDataReader
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    try
                    {
                        inc.fechaIncidente = leer["FechaIncidente"].ToString();
                        inc.descripcion = leer["Descripcion"].ToString();
                        inc.idUsuario = leer["idUsuario"].ToString();
                        inc.idSalon = leer["idSalon"].ToString();
                        inc.idEdificio = leer["idEdificio"].ToString();
                        inc.idEstadoIncidente = leer["idEstadoIncidente"].ToString();
                    }
                    catch { inc.idUsuario = ""; }
                }
            }
            catch  {inc.idUsuario = "";  }
            finally  {  conexion.Close();  }

            return inc;
        }
    }
}