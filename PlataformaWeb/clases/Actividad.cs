using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlataformaWeb.clases
{
    public class Actividad
    {
        public string id { set; get; }
        public string nombre { set; get; }
        public string descripcion { set; get; }
        public string idTipoActividad { set; get; }
        public string tipoActividadTxt { set; get; }
        public string extensionPresentacion { set; get; }
        public byte[] presentacion { set; get; }

        public string fecha { set; get; }
        public string hora { set; get; }
        public string edificio { set; get; }
        public string salon { set; get; }
        public string instructor { set; get; }
        public string inicio { set; get; }
        public string fin { set; get; }




        public static bool subirPresentacion(string idActividad, byte[] presentacion, string extension)
        {
            //Método para insertar usuario en la base de datos
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                string query = "UPDATE Actividad SET Presentacion = @presentacion, ExtensionPresentacion = @extension WHERE idActividad = @idActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idActividad", idActividad);
                cmd.Parameters.AddWithValue("@presentacion", presentacion);
                cmd.Parameters.AddWithValue("@extension", extension);

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

        public static Actividad getPresentacion(string idActividad)
        {
            SqlConnection conexion = Conexion.getConexion();
            Actividad actividad = new Actividad();
            try
            {
                string query = "SELECT Presentacion, ExtensionPresentacion FROM Actividad WHERE idActividad = @idActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idActividad", idActividad);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    actividad.extensionPresentacion = leer["ExtensionPresentacion"].ToString();                  
                    actividad.presentacion = (byte[])leer["Presentacion"]; 
                    
                }
            }
            catch { actividad = null; ; }
            finally { conexion.Close(); }
            return actividad;
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
                string query = "UPDATE Actividad SET Nombre = @nombre, Descripcion = @descripcion WHERE idActividad = @idActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idActividad", id);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);

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

        public static Actividad getActividad(string idActividad)
        {
            SqlConnection conexion = Conexion.getConexion();
            Actividad actividad = new Actividad();
            try
            {
                string query = "SELECT idActividad, A.Nombre, Descripcion, T.Nombre As Tipo, ExtensionPresentacion, Presentacion FROM Actividad A, TipoActividad T WHERE A.idActividad = @id and A.idTipoActividad = T.idTipoActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", idActividad);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    actividad.id = leer["idActividad"].ToString();
                    actividad.tipoActividadTxt = leer["Tipo"].ToString();
                    try
                    {
                        actividad.nombre = leer["Nombre"].ToString();
                        actividad.descripcion = leer["Descripcion"].ToString();
                        actividad.extensionPresentacion = leer["ExtensionPresentacion"].ToString();
                    }
                    catch { }
                    try { actividad.presentacion = (byte[])leer["Presentacion"]; }
                    catch { actividad.presentacion = null; }
                }
            }
            catch { actividad = null; ; }
            finally { conexion.Close(); }
            return actividad;
        }

        public static Actividad getDetalleActividad(string idActividad)
        {
            SqlConnection conexion = Conexion.getConexion();
            Actividad actividad = new Actividad();
            try
            {
                string query = "SELECT A.idActividad, A.Nombre, R.Fecha, E.Nombre AS Edificio, S.Salon, R.HoraInicio, R.HoraFin, T.Nombre AS Tipo, U.Nombre AS Instructor, A.Descripcion FROM Actividad A, ReservaSalon R, Usuario U, TipoActividad T, Salon S, Edificio E WHERE A.idActividad = R.idActividad AND R.idInstructor = U.idUsuario AND A.idTipoActividad = T.idTipoActividad AND R.idSalon = S.idSalon AND S.idEdificio = E.idEdificio AND A.idActividad = @idActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idActividad", idActividad);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    actividad.id = leer["idActividad"].ToString();
                    actividad.nombre = leer["Nombre"].ToString();
                    actividad.fecha = leer["Fecha"].ToString();
                    actividad.edificio = leer["Edificio"].ToString();
                    actividad.salon = leer["Salon"].ToString();
                    actividad.hora = leer["HoraInicio"].ToString()+" - "+leer["HoraFin"].ToString();
                    actividad.tipoActividadTxt = leer["Tipo"].ToString();
                    actividad.instructor = leer["Instructor"].ToString();
                    actividad.descripcion = leer["Descripcion"].ToString();
                    actividad.inicio = leer["HoraInicio"].ToString();
                    actividad.fin = leer["HoraFin"].ToString();
                }
            }
            catch { actividad = null; ; }
            finally { conexion.Close(); }
            return actividad;
        }

        public static bool estaMatriculado(string idUsuario, string idActividad)
        {
            string matriculado = "0";
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                string query = "select count(*) as Existe from Matriculacion where idUsuario = @idUsuario and idActividad = @idActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@idActividad", idActividad);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    matriculado = leer["Existe"].ToString();
                }
            }
            catch { }
            finally { conexion.Close(); }

            if (matriculado == "0") return false;
            else return true;
            
        }

        public static bool asistioEnActividad(string idUsuario, string idActividad)
        {
            string asistio = "0";
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                string query = "select count(*) as Existe from Asistencia where idUsuario = @idUsuario and idActividad = @idActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@idActividad", idActividad);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    asistio = leer["Existe"].ToString();
                }
            }
            catch { }
            finally { conexion.Close(); }

            if (asistio == "0") return false;
            else return true;

        }

        public static bool hayCupo(string idActividad)
        {
            int cantidad = 0;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                string query = "SELECT COUNT(*) AS Matriculados FROM Matriculacion WHERE idActividad = @idActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);
                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idActividad", idActividad);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    cantidad = Convert.ToInt32(leer["Matriculados"].ToString());
                }
            }
            catch { }
            finally { conexion.Close(); }

            return (cantidad < 25);

        }

        public static bool matricularse(string idUsuario, string idActividad)
        {
            //Método para matricular un usuario a una actividad
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                string query = "INSERT INTO Matriculacion (idUsuario, idActividad) VALUES(@idUsuario, @idActividad)";
                SqlCommand cmd = new SqlCommand(query, conexion);
                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@idActividad", idActividad);

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

        public static bool desmatricularse(string idUsuario, string idActividad)
        {
            //Método para matricular un usuario a una actividad
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                string query = "delete Matriculacion where idUsuario = @idUsuario AND idActividad = @idActividad";
                SqlCommand cmd = new SqlCommand(query, conexion);
                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@idActividad", idActividad);

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

        public static bool registrarAsistencia(string idActividad, string idUsuario)
        {
            //Método para insertar usuario en la base de datos
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("asistencia_registrar", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idActividad", idActividad);
                cmd.Parameters.AddWithValue("@idEstudiante", idUsuario);

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

        public static List<string[]> getListaMatriculadas(int mes, int year, string idUsuario)
        {
            List<string[]> lista = new List<string[]>();
            string txtMes;
            if (mes < 10) txtMes = "0" + mes;
            else txtMes = "" + mes;
            //solo traer las reservas que estan finalizadas y vigentes. idvigencia = 1  idEstadoReserva = 2
            //traer reservas que pertenecen al salon que se elija en el registro

            SqlConnection conexion = Conexion.getConexion();

            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                string query = "SELECT R.Fecha, R.idActividad from ReservaSalon R , Matriculacion M where format(Fecha, 'MM/yyyy') = '" + txtMes + "/" + year + "'  AND  M.idUsuario = @idUsuario AND R.idActividad = M.idActividad ";
                SqlCommand cmd = new SqlCommand(query, conexion);
                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable

                //Ejecutamos el comando y guardamos el resultado en un objeto SqlDataReader
                SqlDataReader leer = cmd.ExecuteReader();

                while (leer.Read())
                {
                    try
                    {
                        string[] nuevo = new string[2];
                        nuevo[0] = leer["Fecha"].ToString();
                        nuevo[1] = leer["idActividad"].ToString();
                        lista.Add(nuevo);
                    }
                    catch { }
                }

            }
            catch { lista = null; }
            finally { conexion.Close(); }

            return lista;
        }

    }
}