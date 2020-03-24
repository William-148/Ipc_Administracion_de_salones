using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PlataformaWeb.clases
{
    public class IncidenteInsumo
    {
        public string id { set; get; }
        public string fechaIncidente { set; get; }
        public string fechaCreacion { set; get; }
        public string descripcion { set; get; }
        public string idResponsble { set; get; }
        public string idInsumo { set; get; }
        public string idEstadoIncidente { set; get; }
        public string responsable { set; get; }
        public string insumo { set; get; }

        public bool insertar()
        {
            //Método para insertar usuario en la base de datos
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql

                SqlCommand cmd = new SqlCommand("incidenteInsumo_Insertar", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@fechaIncidente", fechaIncidente);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@idUsuario", idResponsble);
                cmd.Parameters.AddWithValue("@idInsumo", idInsumo);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas > 0) exito = true;

            }
            catch { exito = false; }
            finally { conexion.Close(); }
            return exito;
        }

        public bool actualizar()
        {
            //Método para insertar usuario en la base de datos
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("IncidenteInsumo_Actualizar", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idIncidente", id);
                cmd.Parameters.AddWithValue("@fechaIncidente", fechaIncidente);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@idUsuario", idResponsble);
                cmd.Parameters.AddWithValue("@idInsumo", idInsumo);
                cmd.Parameters.AddWithValue("@idEstadoIncidente", idEstadoIncidente);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas > 0) exito = true;

            }
            catch { exito = false; }
            finally { conexion.Close(); }
            return exito;
        }

        public static IncidenteInsumo getIncidente(string idIncidente)
        {
            SqlConnection conexion = Conexion.getConexion();
            IncidenteInsumo incidente = new IncidenteInsumo();
            try
            {
                string query = "SELECT * FROM IncidenteInsumo WHERE idIncidenteInsumo = @idIncidenteInsumo";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idIncidenteInsumo", idIncidente);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    incidente.fechaIncidente = leer["FechaIncidente"].ToString();
                    incidente.descripcion = leer["Descripcion"].ToString();
                    incidente.idResponsble = leer["idUsuario"].ToString();
                    incidente.idInsumo = leer["idInsumo"].ToString();
                    incidente.idEstadoIncidente = leer["idEstadoIncidente"].ToString();
                }
            }
            catch { incidente = null; ; }
            finally { conexion.Close(); }
            return incidente;
        }


    }
}