using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace LogicaNegocio
{
    public class Herramientas
    {
        public static DataTable IList_A_DataTable(IList iList)
        {
            DataTable oDataTableReturned = new DataTable();

            if (iList.Count > 0)
            {
                object _baseObj = iList[0];
                Type objectType = _baseObj.GetType();
                PropertyInfo[] properties = objectType.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    DataColumn oColumna;
                    oColumna = new DataColumn();
                    oColumna.ColumnName = property.Name;
                    try
                    {
                        oColumna.DataType = property.PropertyType;

                    }
                    catch (Exception)
                    {
                        //no hacer nada, quedará como string
                    }
                    oDataTableReturned.Columns.Add(oColumna);
                }

                foreach (object objItem in iList)
                {
                    DataRow oFila;
                    oFila = oDataTableReturned.NewRow();
                    foreach (PropertyInfo property in properties)
                    {
                        oFila[property.Name] = property.GetValue(objItem, null);
                    }
                    oDataTableReturned.Rows.Add(oFila);
                }
            }
            return oDataTableReturned;
        }

        public static string GenerarContraseña()
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return  result.ToString();
        }

        public static string EnviarCorreo(string host, int port, string login, string pass,
            string mailFrom, string[] mailTo, string Subject, string body)
        {

            string output = string.Empty;
            try
            {
                MailMessage email = new MailMessage();

                foreach (string word in mailTo)
                {  
                    email.To.Add(new MailAddress(word));
                }

                email.From = new MailAddress(mailFrom);
                email.Subject = Subject;

                email.Body = body;

                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = host;
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(login, pass);

                try
                {
                    smtp.Send(email);
                    email.Dispose();
                    output = "Correcto";
                }
                catch (SmtpException ex)
                {
                    output = "Error SMTP enviando correo electrónico: " + ex.Message + ex.StackTrace + ex.StatusCode.ToString() + ex.Source;
                }
                catch (IOException ex1)
                {
                    output = "Error IO enviando correo electrónico: " + ex1.Message + ex1.StackTrace + ex1.Source;
                }
                catch (Exception ex2)
                {
                    output = "Error general enviando correo electrónico: " + ex2.Message + ex2.StackTrace + ex2.Source;
                }

            }
            catch (SmtpException exc)
            {
                output = "Error SMTP al procesar la información del envío de correo: " + exc.Message + exc.StackTrace + exc.StatusCode.ToString() + exc.Source;
            }
            catch (IOException exc1)
            {
                output = "Error IO al procesar la información del envío de correo: " + exc1.Message + exc1.StackTrace + exc1.Source;
            }
            catch (Exception exc2)
            {
                output = "Error general al procesar la información del envío de correo: " + exc2.Message + exc2.StackTrace + exc2.Source;
            }

            return output;
        }

    }
}
