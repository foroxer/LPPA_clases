using ProyectoLPPA;
using System;
using System.Data.SqlClient;

/// <summary>
/// Summary description for LoginDAO
/// </summary>
public class LoginDAO
{
    public LoginDAO()
    {
    }


    public static User login(String username, String password)
    {
        SqlConnection cn = ConexionSingleton.obtenerConexion();
        cn.Open();
        password = SeguridadUtiles.encriptarMD5(password);
        SqlTransaction tx = cn.BeginTransaction();
        SqlCommand cmd = new SqlCommand("SELECT * from Usuario where Alias = @id and Password = @password");
        cmd.Parameters.Add(new SqlParameter("id", username));
        cmd.Parameters.Add(new SqlParameter("password", password));
        cmd.Connection = cn;
        cmd.Transaction = tx;
        SqlDataReader reader = cmd.ExecuteReader();
        User user = null;


        if (reader.Read())
        {
            user = new User(reader.GetString(5),reader.GetString(4).Trim(), reader.GetInt32(0));
            
        }
        reader.Close();
        tx.Commit();
        cn.Close();
        return user;
    }
}