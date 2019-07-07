using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using ProyectoLPPA;

/// <summary>
/// Descripción breve de MailService
/// </summary>
public class MailService
{
    public MailService()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static void restaurarPasswordMail(String user)
    {
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        String newPass = StringUtilities.RandomString(8);
        String toEncript = user + "," + newPass;
        toEncript = SeguridadUtiles.encriptarAES(toEncript);
        toEncript = HttpUtility.UrlEncode(toEncript);

        try
        {
            TextWriter sw = new StreamWriter(filePath + @"\\PASSWORD.txt");
            sw.WriteLine("Para restaurar su password ingrese al siguiente link");
            sw.WriteLine("http://localhost:50311/UI/RestaurarPassword.aspx?data=" + toEncript);
            sw.WriteLine("");
            sw.WriteLine("su nueva password sera : " + newPass);
            sw.Flush();
            sw.Close();
            
        }
        catch(Exception ex)
        {
            SeguridadUtiles.grabarBitacora(0, "error en enviode mail de restauracion de password");
        }

    }
}