using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SyncTAT
{
    class EnviarEmail
    {
        
        public Boolean SendMail(List<string> emailTo, string puerto, bool SSL, string smtp, string usuario, string contra, bool prueba, string file, bool error,string catalogo)
        {
            try
            {
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                string codehtmlini = "<html><head><title>Bienvenido</title>" +
                "<meta http-equiv=\"Content-Type\" content=\"text/html;charset=iso-8859-1\"></head><body>" +
                "<div align=\"center\">" +
                //"<img src=\"http://www.kelloggs.com.mx/content/LatinAmerica/kelloggs_mx/es_MX/jcr:content/par/commoncontentroot/headerGrid/par/responsiveimage.img.png/1515789576193.png\" />" +                
                "<p style=\"font-family: Arial, Helvetica Neue, Helvetica, sans-serif; color: #000000; font-size: 16px;" +
                "'line-height: 18px; text-align: center;  margin: 30px 10px 10px 10px;\">";
                string codehtmlfin = "<p style=\"font-family: Arial, Helvetica Neue, Helvetica, sans-serif; color: #8F8F8F; font-size: 13px;" +
                "padding-top: 30px; line-height: 18px; text-align: center;\">" +
                "Este es un mensaje enviado de manera automática. <br> Favor de no responder a esta dirección." +
                "</p></div></body></html>";

                //string[] split = datosEmail[2].Split(new Char[] { '@' });
                //switch(split[1]){
                //    case "hotmail.com":
                //            smtp = "smtp.live.com";
                //        break;
                //        case "yahoo.com":
                //            smtp = "smtp.mail.yahoo.com";
                //        break;
                //        case "gmail.com":
                //        smtp = "smtp.gmail.com";
                //        break;
                //    default:
                //        smtp = datosEmail[4].ToString();
                //        break;
                //}
                //c.correoAsunto, c.correoCuerpo, c.email, c.emailPass, e.SMTPAdd, e.puerto, e.SSLOpt from configuracion as c inner join email as e on c.emailDatos = e.sufijo where idConfig = 'Activo';
                SmtpClient SmtpServer = new SmtpClient(smtp);
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress(usuario, "Sincronización de Catálogos", Encoding.UTF8);
                //Aquí ponemos el asunto del correo
                //Aquí ponemos el mensaje que incluirá el correo
                if (prueba)
                {
                    mail.Subject = "Prueba de reporte de sincronización automática";
                    mail.Body = codehtmlini + "Mensaje de Prueba de Sincronización automática" + codehtmlfin;
                }
                else
                {
                    if (error)
                    {
                        mail.Subject = "TAT2: Error en Sincronización";
                        mail.Body = codehtmlini + "<p style=\"font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 16px;line-height: 2; text-align: left;\"> Estimado usuario</p>"+
                            "<p style=\"font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 15px;line-height: 2; text-align: justify;\">Se te informa que la ejecucíon de sincronización  del día " + DateTime.Today.ToString("dd/MM/yyyy") + " ha terminado con error para los siguientes catálogos. </p>"+
                            "<ul style=\"font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 14px;line-height: 2; text-align: left;\"><li>" + catalogo+"</li></ul>"+
                            "<p style=\"font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 15px;line-height: 2; text-align: justify;\">Favor de revisar y/o ejecutar manualmente.</p>" + codehtmlfin;
                    }
                    else
                    {
                        mail.Subject = "TAT2: Reporte de Sincronización";
                        mail.Body = codehtmlini + "<p style=\"font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 16px;line-height: 2; text-align: left;\">Estimado usuario</p>"+
                            "<p style=\"font-family: Arial, Helvetica Neue, Helvetica, sans-serif; font-size: 15px;line-height: 2; text-align: justify;\">Se te informa que la ejecucíon de sincronización  del día " + DateTime.Today.ToString("dd/MM/yyyy") + " ha terminado con éxito. </p>" + codehtmlfin;
                    }

                }
                mail.IsBodyHtml = true;
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                //mail.To.Add("eric.iori_zcr@hotmail.com");
                //mail.To.Add("ljesuscastrog@outlook.com, eric.iori_zcr@hotmail.com");
                string mailToComplete = "";
                for (int i = 0; i < emailTo.Count; i++)
                {
                    mailToComplete += emailTo[i];
                    if ((emailTo.Count - 1) != i)
                    {
                        mailToComplete += ",";
                    }
                }
                mail.To.Add(mailToComplete);
                //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                if (File.Exists(file))
                {
                    mail.Attachments.Add(new Attachment(file));
                }
                
                //if (File.Exists(excel))
                //{
                //    mail.Attachments.Add(new Attachment(excel));
                    
                //}
                //Configuracion del SMTP
                //SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                SmtpServer.Port = int.Parse(puerto); //Puerto que utiliza Gmail para sus servicios
                //Especificamos las credenciales con las que enviaremos el mail                
                SmtpServer.Credentials = new System.Net.NetworkCredential(usuario, contra);                
                SmtpServer.EnableSsl = SSL;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
