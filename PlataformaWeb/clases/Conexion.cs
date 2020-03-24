using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlataformaWeb.clases
{
    public class Conexion
    {
        private static SqlConnection conexion = new SqlConnection(
            "Data Source=VAIO; Initial Catalog=PlataformaWeb; Integrated Security=True;");


        //Obtener conexion de la base de datos
        public static SqlConnection getConexion()
        {
            return conexion;
        }

        public SqlCommand agregarComando(string query)
        {
            SqlCommand comando = new SqlCommand(query, conexion);
            return comando;
        }
    }
}