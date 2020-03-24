using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PlataformaWeb.clases
{
    public class PrestamoInsumo
    {
        public string id { set; get; }
        public string fecha { set; get; }
        public string fechaDevuelto { set; get; }
        public string descripcion { set; get; }
        public string idUsuario { set; get; }
        public string idInsumo { set; get; }
        public string idEstadoPrestamo { set; get; }

        public bool insertar()
        {
            //Método para insertar usuario en la base de datos
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("insumo_prestar", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
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

                SqlCommand cmd = new SqlCommand("insumo_actualizarPrestamo", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idInsumoPrestado", id);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@idInsumo", idInsumo);
                cmd.Parameters.AddWithValue("@idEstadoPrestamo", idEstadoPrestamo);

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

        public static PrestamoInsumo getPrestramo(string idPrestamo)
        {
            SqlConnection conexion = Conexion.getConexion();
            PrestamoInsumo prestamo = new PrestamoInsumo();
            try
            {
                string query = "SELECT Fecha, Descripcion,idUsuario,idInsumo,idEstadoPrestamo FROM InsumoPrestado WHERE idInsumoPrestado = @IdInsumoPrestado";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdInsumoPrestado", idPrestamo);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    prestamo.fecha = leer["Fecha"].ToString();
                    prestamo.descripcion = leer["Descripcion"].ToString();
                    prestamo.idUsuario = leer["idUsuario"].ToString();
                    prestamo.idInsumo = leer["idInsumo"].ToString();
                    prestamo.idEstadoPrestamo = leer["idEstadoPrestamo"].ToString();
                }

            }
            catch { prestamo = null; ; }
            finally { conexion.Close(); }
            return prestamo;
        }
    }
}