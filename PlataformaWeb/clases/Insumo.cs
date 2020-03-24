using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlataformaWeb.clases
{
    public class Insumo
    {
        public string idInsumo { set; get; }
        public string nombre { set; get; }
        public string descripcion { set; get; }
        public string tipo { set; get; }

        public bool insertar()
        {
            //Método para insertar usuario en la base de datos
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                string query = "INSERT INTO Insumo (Nombre, Descripcion, Cantidad, idTipoInsumo) VALUES(@nombre, @descripcion, 1, @tipoInsumo)";
                SqlCommand cmd = new SqlCommand(query, conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@tipoInsumo", tipo);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas > 0) exito = true;

            }
            catch { exito = false;}
            finally  { conexion.Close(); }
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
                string query = "UPDATE Insumo SET Nombre = @nombre, Descripcion = @descripcion, idTipoInsumo = @tipo WHERE idInsumo = @id";
                SqlCommand cmd = new SqlCommand(query, conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@id", idInsumo);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@tipo", tipo);

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