using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace PlataformaWeb.clases
{
    public class Correo
    {
        public static bool enviar(string EmailDestino, string mensaje)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string correo = "wilenrike@gmail.com";

            try
            {
                mail.From = new MailAddress(correo);
                mail.To.Add(new MailAddress(EmailDestino));
                mail.Body = mensaje;
                mail.Subject = "[IPC2]PLATAFORMA WEB - Recuperación de Contraseña";
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential(correo, "148willenrk&");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}