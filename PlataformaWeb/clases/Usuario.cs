using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace PlataformaWeb.clases
{
    public class Usuario
    {
        //Roles que puede tener un usuario
        public static int administrador = 1;
        public static int operador = 2;
        public static int instructor = 3;
        public static int estudiante = 4;

        //Campos de clase
        public int rol { set; get; }
        public string rolTxt { set; get; }
        public int idUsuario { set; get; }
        public string nombre { set; get; }
        public int carnet { set; get; }
        public string nacimiento { set; get; }
        public string correo { set; get; }
        public int telefono { set; get; }
        public string usuario { set; get; }
        public string password { set; get; }
        public string palabraClave { set; get; }

        public Usuario()
        {
            idUsuario = -1;
            nombre = "";
            carnet = 0;
            nacimiento = "";
            correo = "";
            telefono = 0;
            usuario = "";
            password = "";
            palabraClave = "";
            rolTxt = "";
        }

        public static string existeUsuario(string usuario)
        {
            //Método para verificar si un usuario existe en la base de datos
            string exist = "1";

            SqlConnection conexion = Conexion.getConexion();

            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("existe_usuario", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@usuario", usuario);
                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos el resultado en un objeto SqlDataReader
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    exist = leer["Existe"].ToString();

                }
            }
            catch
            {
                exist = "1";
            }
            finally
            {
                conexion.Close();
            }

            return exist;
            
        }

        public bool insertar()
        {
            //Método para insertar usuario en la base de datos
            bool exito = false;

            SqlConnection conexion = Conexion.getConexion();

            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("insertar_usuario", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@carnet", carnet);
                cmd.Parameters.AddWithValue("@fecha", nacimiento);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@clave", password);
                cmd.Parameters.AddWithValue("@palabraClave", palabraClave);
                cmd.Parameters.AddWithValue("@idRol",rol);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas > 0) exito = true;
                
            }
            catch
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
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
                SqlCommand cmd = new SqlCommand("actualizar_usuario", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@carnet", carnet);
                cmd.Parameters.AddWithValue("@fecha", nacimiento);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@clave", password);
                cmd.Parameters.AddWithValue("@palabraClave", palabraClave);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas > 0) exito = true;

            }
            catch
            {
                exito = false;
            }
            finally
            {
                conexion.Close();
            }
            return exito;


        }

        public static Usuario getUsuario(string usuario, string password)
        {
            Usuario user = new Usuario();

            SqlConnection conexion = Conexion.getConexion();

            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("inicio_sesion", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@clave", password);
                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos el resultado en un objeto SqlDataReader
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    try
                    {
                        user.idUsuario = Convert.ToInt32(leer["idUsuario"].ToString());
                        user.nombre = leer["Nombre"].ToString();
                        user.usuario = leer["Usuario"].ToString();
                        user.rol = Convert.ToInt32(leer["idRol"].ToString());
                    }
                    catch
                    {
                        user.idUsuario = 0;
                    }                   
                }
            }
            catch
            {
                user.idUsuario = 0;
            }
            finally
            {
                conexion.Close();
            }

            return user;
        }

        public static Usuario getUsuario(int id)
        {
            Usuario user = new Usuario();

            SqlConnection conexion = Conexion.getConexion();

            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("usuario_por_id", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idUsuario", id);
                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos el resultado en un objeto SqlDataReader
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    try
                    {
                        user.idUsuario = Convert.ToInt32(leer["idUsuario"].ToString());
                        user.nombre = leer["Nombre"].ToString();
                        user.usuario = leer["Usuario"].ToString();
                        user.carnet = Convert.ToInt32(leer["Carnet"].ToString());
                        user.nacimiento = leer["FechaNacimiento"].ToString();
                        user.telefono = Convert.ToInt32(leer["Telefono"].ToString());
                        user.correo = leer["Correo"].ToString();
                        user.rolTxt = leer["Rol"].ToString();
                    }
                    catch
                    {
                        user.idUsuario = 0;
                    }
                }
            }
            catch
            {
                user.idUsuario = 0;
            }
            finally
            {
                conexion.Close();
            }

            return user;
        }

        public static bool importarUsuarios(string query)
        {
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                SqlCommand cmd = new SqlCommand(query,conexion);
                cmd.CommandType = CommandType.Text;
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas > 0) exito = true;
            }
            catch   {exito = false; }
            finally { conexion.Close(); }
            return exito;
        }

        public static Usuario recuperarCuenta(string correo)
        {
            SqlConnection conexion = Conexion.getConexion();
            Usuario user = new Usuario();
            try
            {
                
                string query = "SELECT Nombre, Usuario, PalabraClave, Clave FROM Usuario WHERE Correo = @correo";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@correo", correo);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    try
                    {
                        user.nombre = leer["Nombre"].ToString();
                        user.usuario = leer["Usuario"].ToString();
                        user.palabraClave = leer["PalabraClave"].ToString();
                        user.password = leer["Clave"].ToString();
                    }
                    catch {   user.palabraClave = "";  }
                }
            }
            catch {  user.palabraClave = "";  }
            finally  {  conexion.Close();  }
            return user;
        }
    }
}