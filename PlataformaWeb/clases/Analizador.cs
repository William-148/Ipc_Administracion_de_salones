using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlataformaWeb.clases
{
    public class Analizador
    {

        private static List<string> analizador(string fecha)
        {
            //analiza un texto dd/mm/yyy y lo separa en dia, mes y año
            List<string> numeros = new List<string>();
            char aux;
            string item = "";
            for (int i = 0; i < fecha.Length; i++)
            { //      01/10/2030 0000
                if (numeros.Count == 3) break;

                aux = fecha[i];
                switch (aux)
                {
                    case '/':
                    case ' ':
                        numeros.Add(item);
                        item = "";
                        break;
                    default:
                        item += aux;
                        break;
                }
            }
            return numeros;
        }

        public static DateTime getFecha(string fecha)
        {//retorna una objeto DateTime con la fecha introducida dd/mm/yyy
            List<string> tokens = analizador(fecha);
            int year = 2018;
            int month = 10;
            int day = 10;

            if (tokens.Count > 0)
            {
                try
                {
                    year = Convert.ToInt32(tokens[2]);
                    month = Convert.ToInt32(tokens[1]);
                    day = Convert.ToInt32(tokens[0]);
                }
                catch
                {
                    year = 2018;
                    month = 10;
                    day = 10;
                }
            }
            DateTime date = new DateTime(year, month, day);
            return date;
        }
    }
}