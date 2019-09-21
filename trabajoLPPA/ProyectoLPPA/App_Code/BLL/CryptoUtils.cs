using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de CryptoUtils
/// </summary>
public class CryptoUtils
{
    private static String key = "820197F22E4E16897823C52B8F3C08DB";

    public CryptoUtils()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static String encriptarMD5(String stringAEncriptar)
    {
        String hash = "";
        if (!String.IsNullOrEmpty(stringAEncriptar))
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(stringAEncriptar);
            byte[] datoEncodeado = md5.ComputeHash(bytes);
            hash = BitConverter.ToString(datoEncodeado).Replace("-", "");

        }


        return hash;
    }

    public static String encriptarAES(String stringAEncriptar)
    {
        //instancio aes
        Aes aesProvider = Aes.Create();

        aesProvider.Key = Encoding.UTF8.GetBytes(key);
        aesProvider.GenerateIV();
        aesProvider.Mode = CipherMode.CBC;
        aesProvider.Padding = PaddingMode.PKCS7;
        //creo los streams que voy a necesitar
        MemoryStream ms = new MemoryStream();
        ICryptoTransform encriptador = aesProvider.CreateEncryptor();
        CryptoStream cs = new CryptoStream(ms, encriptador, CryptoStreamMode.Write);
        StreamWriter sw = new StreamWriter(cs);
        //escribo el dato que se va a encriptar
        sw.Write(stringAEncriptar);
        sw.Close();
        cs.Close();
        ms.Close();
        //obtengo la data encriptada
        byte[] dataEncriptada = ms.ToArray();
        byte[] resultadoCombinado = new byte[dataEncriptada.Length + aesProvider.IV.Length];
        //agrego el iv generado al resultado para poder desencriptarlo despues
        Array.Copy(aesProvider.IV, 0, resultadoCombinado, 0, aesProvider.IV.Length);
        Array.Copy(dataEncriptada, 0, resultadoCombinado, aesProvider.IV.Length, dataEncriptada.Length);


        return Convert.ToBase64String(resultadoCombinado);
    }
    public static String desencriptarAES(String stringADesencriptar)
    {
        String resultado = "";

        try
        {
            byte[] bytesADesencriptar = Convert.FromBase64String(stringADesencriptar);
            Aes aesProvider = Aes.Create();
            //obtengo el block size y inicializo los arrays
            byte[] IvGuardado = new byte[aesProvider.BlockSize / 8];
            byte[] datosPuros = new byte[bytesADesencriptar.Length - IvGuardado.Length];
            Array.Copy(bytesADesencriptar, IvGuardado, IvGuardado.Length);
            Array.Copy(bytesADesencriptar, IvGuardado.Length, datosPuros, 0, datosPuros.Length);

            aesProvider.Key = Encoding.UTF8.GetBytes(key);
            aesProvider.Mode = CipherMode.CBC;
            aesProvider.Padding = PaddingMode.PKCS7;

            aesProvider.IV = IvGuardado;

            //desencripto
            MemoryStream ms = new MemoryStream(datosPuros);
            ICryptoTransform decryptor = aesProvider.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            StreamReader sw = new StreamReader(cs);
            resultado = sw.ReadToEnd();
            sw.Close();
            cs.Close();
            ms.Close();

        }
        catch (Exception ex)
        {
            resultado = "#ERROR";

        }
        //reconvierte desde base 64
        return resultado;
    }
}