
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace ProyectoLPPA
{
    


    public static class SeguridadUtiles
    {

        public static void cambiarPassword(String user, String password)
        {
            password = CryptoUtils.encriptarMD5(password);
            SqlConnection cn = ConexionSingleton.obtenerConexion();
            cn.Open();
            SqlTransaction tx = cn.BeginTransaction();
            SqlCommand cmd = new SqlCommand("UPDATE USUARIO SET password = @password, intentos = 0 WHERE ALIAS = @ALIAS");
            cmd.Parameters.Add(new SqlParameter("ALIAS", user));
            cmd.Parameters.Add(new SqlParameter("password", password));
            cmd.Connection = cn;
            cmd.Transaction = tx;
            cmd.ExecuteNonQuery();
            tx.Commit();
            cn.Close();
        }

      
    }
}