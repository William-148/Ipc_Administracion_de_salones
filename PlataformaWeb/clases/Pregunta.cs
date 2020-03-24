using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlataformaWeb.clases
{
    public class Pregunta
    {
        public string id { set; get; }
        public string pregunta { set; get; }
        public string respuesta { set; get; }
        public string idActividad { set; get; }

        public void insertar()
        {
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                string query = "INSERT INTO Pregunta (Pregunta, Respuesta, idActividad) VALUES(@pregunta, @respuesta, @idActividad)";
                SqlCommand cmd = new SqlCommand(query, conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@pregunta", pregunta);
                cmd.Parameters.AddWithValue("@respuesta", respuesta);
                cmd.Parameters.AddWithValue("@idActividad", idActividad);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                cmd.ExecuteNonQuery();

            }
            catch {  }
            finally { conexion.Close(); }

        }

        public void actualizar()
        {
            //Método para insertar usuario en la base de datos
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                string query = "UPDATE Pregunta SET Pregunta = @pregunta, Respuesta = @respuesta WHERE idPregunta = @idPregunta";
                SqlCommand cmd = new SqlCommand(query, conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idPregunta", id);
                cmd.Parameters.AddWithValue("@pregunta", pregunta);
                cmd.Parameters.AddWithValue("@respuesta", respuesta);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                cmd.ExecuteNonQuery();
            }
            catch {  }
            finally { conexion.Close(); }
        }

        public static void eliminar(string idPregunta)
        {
            //Método para insertar usuario en la base de datos
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                string query = "DELETE Pregunta WHERE idPregunta = @idPregunta";
                SqlCommand cmd = new SqlCommand(query, conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idPregunta", idPregunta);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally { conexion.Close(); }
        }

        public static bool hayCuestionario(string idActividad)
        {
            SqlConnection conexion = Conexion.getConexion();
            int count = 0;
            try
            {
                string query = "select count(*) as Preguntas from Pregunta where idActividad = @idActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idActividad", idActividad);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    count = Convert.ToInt32(leer["Preguntas"].ToString());
                }
            }
            catch {  }
            finally { conexion.Close(); }
            return (count > 0);
        }

        public static bool registrarPreguntas(string query)
        {
            //Método para insertar usuario en la base de datos
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand(query, conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros

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
    }
}