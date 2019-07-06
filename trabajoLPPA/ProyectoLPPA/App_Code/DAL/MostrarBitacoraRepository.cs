using ProyectoLPPA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de MostrarBitacoraRepository
/// </summary>
public class MostrarBitacoraRepository
{
    public MostrarBitacoraRepository()
    {
    }

    public static List<Bitacora> execute(String desde, String hasta)
    {
        SqlConnection cn = ConexionSingleton.obtenerConexion();
        if (cn.State == System.Data.ConnectionState.Open)
            cn.Close();
        cn.Open();
        SqlTransaction tx = cn.BeginTransaction();
        StringBuilder sb = new StringBuilder();
        SqlCommand cmd = new SqlCommand();
        List<Bitacora> registros = new List<Bitacora>();

        sb.Append(" SELECT * from vistaBitacora ");
        sb.Append(desde != null || hasta != null ? " where " : "");
        if(desde != null)
        {
            sb.Append(" fecha >= @desde");
            cmd.Parameters.Add(new SqlParameter("desde", desde));
        }
        if (desde != null && hasta != null )
        {
            sb.Append(" and ");
        }

        if (hasta != null)
        {
            sb.Append(" fecha =< @hasta");
            cmd.Parameters.Add(new SqlParameter("hasta", hasta));
        }
        cmd.CommandText = sb.ToString();
        cmd.Connection = cn;
        cmd.Transaction = tx;
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            registros.Add(new Bitacora(reader.GetDateTime(0), reader.GetString(2), reader.GetString(1)));
        }
        reader.Close();
        tx.Commit();
        cn.Close();
        return registros;
    }
}