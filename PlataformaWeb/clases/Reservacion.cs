using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PlataformaWeb.clases
{
    public class Reservacion
    {
        public static int insertarCarta = 1;
        public static int insertarCodigoQR = 2;

        public int idReservacion { set; get; }
        public string fecha { set; get; }
        public string horaInicio { set; get; }
        public string horaFin { set; get; }
        public byte[] carta { set; get; }
        public byte[] codigoQR { set; get; }
        public int idPeriodoReserva { set; get; }
        public int idEstadoReserva { set; get; }
        public int idVigenciaReserva { set; get; }
        public int idActividad { set; get; }
        public int idTipoActividad { set; get; }
        public int idInstructor { set; get; }
        public int idAdministrador { set; get; }
        public int idSalon { set; get; }
        public string fechaCreacion {set; get;}
        public string estadoReservaTxt { set; get; }
        public int idEdificio { set; get; }
        public string instructorTxt { set; get; }
        public string administradorTxt { set; get; }

        public Reservacion()
        {
            idReservacion = 0;
            fecha = "";
            horaInicio = "";
            horaFin = "";
            estadoReservaTxt = "";
            idEdificio = 0;
            fechaCreacion = "";
            instructorTxt = "";
            administradorTxt = "";
            idPeriodoReserva = 0;
            idEstadoReserva = 0;
            idVigenciaReserva = 0;
            idActividad = 0;
            idInstructor = 0;
            idAdministrador = 0;
            idSalon = 0;
        }

        public string getPeriodoReserva()
        {
            switch (idPeriodoReserva)
            {
                case 1: return "Día Unico";
                case 2: return "Diario";
                case 3: return "Semanal";
                case 4: return "Quincenal";
                default: return "";
            }
        }

        public static byte[] getCodigoQR(int idReservacion)
        {
            SqlConnection conexion = Conexion.getConexion();
            byte[] codigoQR = null;
            try
            {
                SqlCommand cmd = new SqlCommand("obtener_codigoQR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idReservacion", idReservacion);
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    codigoQR = (byte[])leer["CodigoQr"];
                }

            }
            catch { codigoQR = null; ; }
            finally { conexion.Close(); }
            return codigoQR;
        }

        public static List<Reservacion> getListReservacion(int idSalon)
        {
            List<Reservacion> lista = new List<Reservacion>();

            //solo traer las reservas que estan finalizadas y vigentes. idvigencia = 1  idEstadoReserva = 2
            //traer reservas que pertenecen al salon que se elija en el registro

            SqlConnection conexion = Conexion.getConexion();

            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("lista_reservas_porSalon", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                //cmd.Parameters.AddWithValue("@idReserva", idReservacion);
                cmd.Parameters.AddWithValue("@idSalon", idSalon);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable

                //Ejecutamos el comando y guardamos el resultado en un objeto SqlDataReader
                SqlDataReader leer = cmd.ExecuteReader();

                while (leer.Read())
                {
                    try
                    {
                        Reservacion nuevo = new Reservacion();
                        nuevo.idReservacion = Convert.ToInt32(leer["idReservaSalon"].ToString());
                        nuevo.fecha = leer["Fecha"].ToString();
                        nuevo.horaInicio = leer["HoraInicio"].ToString();
                        nuevo.horaFin = leer["HoraFin"].ToString();
                        nuevo.idPeriodoReserva = Convert.ToInt32(leer["idPeriodoReserva"].ToString());

                        lista.Add(nuevo);
                    }
                    catch { }
                }

            }
            catch {  lista = null;}
            finally { conexion.Close();}
            
            return lista;
        }

        public static List<string[]> getListaReservacion(int mes, int year)
        {
            List<string[]> lista = new List<string[]>();
            List<string> fechas = new List<string>();
            string txtMes;
            if (mes < 10) txtMes = "0" + mes;
            else txtMes = ""+mes;
            //solo traer las reservas que estan finalizadas y vigentes. idvigencia = 1  idEstadoReserva = 2
            //traer reservas que pertenecen al salon que se elija en el registro

            SqlConnection conexion = Conexion.getConexion();

            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                string query = "select Fecha, idEstadoReserva, idInstructor from ReservaSalon where format(Fecha, 'MM/yyyy') = '"+ txtMes + "/"+year+ "' order by idEstadoReserva asc";
                SqlCommand cmd = new SqlCommand(query, conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable

                //Ejecutamos el comando y guardamos el resultado en un objeto SqlDataReader
                SqlDataReader leer = cmd.ExecuteReader();

                while (leer.Read())
                {
                    try
                    {
                        string[] nuevo = new string[3];
                        nuevo[0] = leer["Fecha"].ToString();                        
                        nuevo[1] = leer["idEstadoReserva"].ToString();
                        nuevo[2] = leer["idInstructor"].ToString();

                        if (!fechas.Contains(nuevo[0]))
                        {
                            fechas.Add(nuevo[0]);
                            lista.Add(nuevo);
                        }
                        
                    }
                    catch { }
                }

            }
            catch { lista = null; }
            finally { conexion.Close(); }

            return lista;
        }

        public static List<string[]> getListaReservacion(int mes, int year, string idInstructor)
        {
            List<string[]> lista = new List<string[]>();
            List<string> fechas = new List<string>();
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
                string query = "select Fecha, idEstadoReserva, idInstructor from ReservaSalon where format(Fecha, 'MM/yyyy') = '" + txtMes + "/" + year + "' order by idEstadoReserva asc";
                SqlCommand cmd = new SqlCommand(query, conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.Text;

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable

                //Ejecutamos el comando y guardamos el resultado en un objeto SqlDataReader
                SqlDataReader leer = cmd.ExecuteReader();

                while (leer.Read())
                {
                    try
                    {
                        string[] nuevo = new string[3];
                        nuevo[0] = leer["Fecha"].ToString();
                        nuevo[1] = leer["idEstadoReserva"].ToString();
                        nuevo[2] = leer["idInstructor"].ToString();

                        if (!fechas.Contains(nuevo[0]) && nuevo[2] == idInstructor)
                        {
                            fechas.Add(nuevo[0]);
                            lista.Add(nuevo);
                        }

                    }
                    catch { }
                }

            }
            catch { lista = null; }
            finally { conexion.Close(); }

            return lista;
        }

        public static List<Reservacion> getListReservacion(int idReservacion, int idSalon)
        {
            List<Reservacion> lista = new List<Reservacion>();
            SqlConnection conexion = Conexion.getConexion();

            try
            {
                SqlCommand cmd = new SqlCommand("lista_reservas_porSalonIdReserva", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idReserva", idReservacion);
                cmd.Parameters.AddWithValue("@idSalon", idSalon);

                //Abrimos la conexion
                conexion.Open();
                SqlDataReader leer = cmd.ExecuteReader();

                while (leer.Read())
                {
                    try
                    {
                        Reservacion nuevo = new Reservacion();
                        nuevo.idReservacion = Convert.ToInt32(leer["idReservaSalon"].ToString());
                        nuevo.fecha = leer["Fecha"].ToString();
                        nuevo.horaInicio = leer["HoraInicio"].ToString();
                        nuevo.horaFin = leer["HoraFin"].ToString();
                        nuevo.idPeriodoReserva = Convert.ToInt32(leer["idPeriodoReserva"].ToString());

                        lista.Add(nuevo);
                    }
                    catch { }
                }

            }
            catch { lista = null; }
            finally { conexion.Close(); }
            return lista;
        }


        public static bool insertarimagen(int idReserva, byte[] imagen, int carta_codigoQR)
        {
            bool exito = false;
            int carta = 1;

            SqlConnection conexion = Conexion.getConexion();
            SqlCommand cmd = null;
            try
            {
                if (carta_codigoQR == carta)
                {
                    cmd = new SqlCommand("insertar_carta", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idReservacion", idReserva);
                    cmd.Parameters.AddWithValue("@carta", imagen);

                }
                else 
                {
                    cmd = new SqlCommand("insertar_codigo_QR", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idReservacion", idReserva);
                    cmd.Parameters.AddWithValue("@qr", imagen);
                }
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

        public bool insertar()
        {
            //Método para insertar usuario en la base de datos
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("insertar_reservacion", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                //cmd.Parameters.AddWithValue("@idReserva", idReservacion);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@horaInicio", horaInicio);
                cmd.Parameters.AddWithValue("@horaFin", horaFin);
                cmd.Parameters.AddWithValue("@idPeriodoReserva", idPeriodoReserva);
                cmd.Parameters.AddWithValue("@idInstructor", idInstructor);
                cmd.Parameters.AddWithValue("@idAdministrador", idAdministrador);
                cmd.Parameters.AddWithValue("@idSalon", idSalon);
                cmd.Parameters.AddWithValue("@idTipoActividad", idTipoActividad);

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

        public bool Actualizar()
        {
            //Método para insertar usuario en la base de datos
            bool exito = false;
            SqlConnection conexion = Conexion.getConexion();
            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("actualizar_reservacion", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                //cmd.Parameters.AddWithValue("@idReserva", idReservacion);
                cmd.Parameters.AddWithValue("@idReserva", idReservacion);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@horaInicio", horaInicio);
                cmd.Parameters.AddWithValue("@horaFin", horaFin);
                cmd.Parameters.AddWithValue("@idPeriodoReserva", idPeriodoReserva);
                cmd.Parameters.AddWithValue("@idVigenciaReserva", idVigenciaReserva);
                cmd.Parameters.AddWithValue("@idInstructor", idInstructor);
                cmd.Parameters.AddWithValue("@idAdministrador", idAdministrador);
                cmd.Parameters.AddWithValue("@idSalon", idSalon);
                cmd.Parameters.AddWithValue("@idTipoActividad", idTipoActividad);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas > 0) exito = true;

            }
            catch { exito = false;}
            finally {  conexion.Close();  }
            return exito;

        }

        public static Reservacion getReserva(int idReserva)
        {
            Reservacion reserva = new Reservacion();
            SqlConnection conexion = Conexion.getConexion();

            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("reserva_por_id", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idReservacion", idReserva);
                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos el resultado en un objeto SqlDataReader
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    try
                    {
                        reserva.idReservacion = idReserva;
                        reserva.fecha = leer["Fecha"].ToString();
                        reserva.horaInicio = leer["HoraInicio"].ToString();
                        reserva.horaFin = leer["HoraFin"].ToString();
                        reserva.fechaCreacion = leer["FechaCreacion"].ToString();                        
                        reserva.idPeriodoReserva = Convert.ToInt32(leer["idPeriodoReserva"].ToString());
                        reserva.estadoReservaTxt = leer["Estado"].ToString();
                        reserva.idVigenciaReserva = Convert.ToInt32(leer["idVigenciaReserva"].ToString());
                        reserva.idInstructor = Convert.ToInt32(leer["idInstructor"].ToString());
                        reserva.instructorTxt = leer["Instructor"].ToString();
                        reserva.idAdministrador = Convert.ToInt32(leer["idAdministrador"].ToString());
                        reserva.administradorTxt = leer["Administrador"].ToString();
                        reserva.idSalon = Convert.ToInt32(leer["idSalon"].ToString());
                        reserva.idEdificio = Convert.ToInt32(leer["idEdificio"].ToString());
                        reserva.idTipoActividad = Convert.ToInt32(leer["idTipoActividad"].ToString());
                        
                        try { reserva.carta = (byte[])leer["CartaSolicitud"]; }
                        catch { reserva.carta = null; }

                        try { reserva.codigoQR = (byte[])leer["CodigoQR"]; }
                        catch { reserva.codigoQR = null; }
                    }
                    catch
                    {
                        reserva.idReservacion = 0;
                    }
                }
            }
            catch
            {
                reserva.idReservacion = 0;
            }
            finally
            {
                conexion.Close();
            }
            return reserva;
        }
    }
}