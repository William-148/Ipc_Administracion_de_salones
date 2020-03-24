using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PlataformaWeb.clases;
using System.Data;

namespace PlataformaWeb.clases
{
    public class Salon
    {
        public int id { set; get; }
        public string salon { set; get; }
        public int capacidad { set; get; }
        public int idEdificio { set; get; }
        public int idEstadoSalon { set; get; }

        public Salon()
        {
            id = 0;
            salon = "";
            capacidad = 0;
            idEdificio = 0;
            idEstadoSalon = 0;
        }


        public bool insertar()
        {
           
            bool exito = false;

            SqlConnection conexion = Conexion.getConexion();

            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("insertar_salon", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros                
                cmd.Parameters.AddWithValue("@Salon", salon);
                if (capacidad != 0) { cmd.Parameters.AddWithValue("@capacidad", capacidad); }
                cmd.Parameters.AddWithValue("@idEdificio", idEdificio);
                cmd.Parameters.AddWithValue("@idEstadoSalon", idEstadoSalon);

                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos las filas afectadas en una variable
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas > 0) exito = true;

            }
            catch {exito = false; }
            finally{ conexion.Close(); }

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
                SqlCommand cmd = new SqlCommand("actualizar_salon", conexion);

                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idSalon", id);
                cmd.Parameters.AddWithValue("@Salon", salon);
                if (capacidad != 0) { cmd.Parameters.AddWithValue("@capacidad", capacidad); }
                cmd.Parameters.AddWithValue("@idEdificio", idEdificio);
                cmd.Parameters.AddWithValue("@idEstadoSalon", idEstadoSalon);

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

        public static Salon getSalon(int idSalon)
        {
            Salon salon = new Salon();

            SqlConnection conexion = Conexion.getConexion();

            try
            {
                //Creamos un objeto tipo SqlCommand y le pasamos como parametros el
                //nombre de un procedimiento o un instruccion sql
                SqlCommand cmd = new SqlCommand("obtener_salon_id", conexion);
                //indicamos el tipo de comando que se va enviar
                cmd.CommandType = CommandType.StoredProcedure;
                //Mandamos los parametros
                cmd.Parameters.AddWithValue("@idSalon", idSalon);
                //Abrimos la conexion
                conexion.Open();
                //Ejecutamos el comando y guardamos el resultado en un objeto SqlDataReader
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    try
                    {
                        salon.id = Convert.ToInt32(leer["idSalon"].ToString());
                        salon.salon = leer["Salon"].ToString();
                        try { salon.capacidad = Convert.ToInt32(leer["Capacidad"].ToString()); }
                        catch { salon.capacidad = 0; }                        
                        salon.idEdificio = Convert.ToInt32(leer["idEdificio"].ToString());
                        salon.idEstadoSalon = Convert.ToInt32(leer["idEstadoSalon"].ToString());
                    }
                    catch { salon.id = 0; }
                }
            }
            catch{salon.id = 0; }
            finally{ conexion.Close(); }

            return salon;

        }

    }
}