using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LogicaNegocio;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for Tools
/// </summary>
public class Tools
{
	public Tools()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static string CuerpoCorreo(string NombreCompleto, string cedula, string clave,string tipoCorreo)
    {
        string cuerpo = string.Empty;
        string plantilla ="";

        switch (tipoCorreo)
            {
                case "NuevoUsuario":
                   plantilla = @"\CorreoNuevoUsuario.html";
                    break;

                case "OlvidoClave":
                    plantilla = @"\CorreoNuevaContraseña.html";
                    break;

            }

        string[] st = File.ReadAllLines(ConfigurationManager.AppSettings["PathPlantillas"] + plantilla);

        foreach (var item in st)
            cuerpo += item;


        cuerpo = cuerpo.Replace("%NombreCompleto%", NombreCompleto);
        cuerpo = cuerpo.Replace("%NumeroCedula%", cedula);
        cuerpo = cuerpo.Replace("%ClaveAsignada%", clave);

        return cuerpo;
    }

    public static string EnviarCorreo(string correoCliente, string NombreCliente, string cedula, string claveAsignada, string TipoCorreo)
    {
        string host = "";
        int port = 0;
        string login = "";
        string pass = "";
        string mailFrom = "";
        string mailTo = "";

        string subject = "";
        string body = string.Empty;

        string respuestaEnvio="";

        try
        {
            // AppSettingsCorreo
            host = ConfigurationManager.AppSettings["SmtpServer"];
            port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            login = ConfigurationManager.AppSettings["SmtpLogin"];
            pass = ConfigurationManager.AppSettings["SmtpPass"];
            mailFrom = ConfigurationManager.AppSettings["FromEmail"];


            mailTo = correoCliente;

            switch (TipoCorreo)
            {
                case "NuevoUsuario":
                    body = CuerpoCorreo(NombreCliente, cedula, claveAsignada, TipoCorreo);
                    subject = "Notificación de creación de cuenta.";
                    break;

                case "OlvidoClave":
                    body = CuerpoCorreo(NombreCliente, cedula, claveAsignada, TipoCorreo);
                    subject = "Notificación de asignación de nueva contraseña.";
                    break;

                case "NuevaConstruct":
                    body = CuerpoCorreo(NombreCliente, cedula, claveAsignada, TipoCorreo);
                    subject = "Notificación de creación de cuenta.";
                    break;


            }
        }

        catch (Exception excp)
        {
               
        }

        string[] correos = correosRegistrados(mailTo);

        respuestaEnvio = LogicaNegocio.Herramientas.EnviarCorreo(host, port, login, pass, mailFrom, correos, subject, body);

        return respuestaEnvio;
    }

    public static string[] correosRegistrados(string correos)
    {
        string[] separators = { ",", ";"};
        string value = correos.Replace(" ", "");
        string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //foreach (var word in words)
        //    Console.WriteLine(word);
        return words;
    }

    public static DataTable ConvertirADatatable(GridView GridView1)
    {
        DataTable dt = new DataTable();
        for (int i = 0; i < GridView1.Columns.Count; i++)
        {
            dt.Columns.Add("column" + i.ToString());
        }
        foreach (GridViewRow row in GridView1.Rows)
        {
            DataRow dr = dt.NewRow();
            for (int j = 0; j < GridView1.Columns.Count; j++)
            {
                dr["column" + j.ToString()] = row.Cells[j].Text;
            }

            dt.Rows.Add(dr);
        }

        return dt;
    }

    public static string EncriptarUsuario(string texto)
    {
        string key = "EPPSOFTRecolector";
        //arreglo de bytes donde guardaremos la llave
        byte[] keyArray;
        //arreglo de bytes donde guardaremos el texto
        //que vamos a encriptar
        byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

        //se utilizan las clases de encriptación
        //provistas por el Framework
        //Algoritmo MD5
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        //se guarda la llave para que se le realice
        //hashing
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

        hashmd5.Clear();

        //Algoritmo 3DAS
        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        //se empieza con la transformación de la cadena
        ICryptoTransform cTransform = tdes.CreateEncryptor();

        //arreglo de bytes donde se guarda la
        //cadena cifrada
        byte[] ArrayResultado =
        cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

        tdes.Clear();

        //se regresa el resultado en forma de una cadena
        return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
    }

    public static string DesencriptarUsuario(string textoEncriptado)
    {
        string key = "EPPSOFTRecolector";
        byte[] keyArray;
        //convierte el texto en una secuencia de bytes
        byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

        //se llama a las clases que tienen los algoritmos
        //de encriptación se le aplica hashing
        //algoritmo MD5
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();

        byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

        tdes.Clear();
        //se regresa en forma de cadena
        return UTF8Encoding.UTF8.GetString(resultArray);
    }


}