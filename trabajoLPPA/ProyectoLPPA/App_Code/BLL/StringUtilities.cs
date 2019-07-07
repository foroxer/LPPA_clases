using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de StringUtilities
/// </summary>
public class StringUtilities
{
    public StringUtilities()
    {  }

    private static Random random = new Random();
    public static string RandomString(int longitud)
    {
        const string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(abc, longitud)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

}