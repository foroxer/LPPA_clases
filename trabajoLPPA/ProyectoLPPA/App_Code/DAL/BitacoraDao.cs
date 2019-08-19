using ProyectoLPPA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de MostrarBitacoraRepository
/// </summary>
public class BitacoraDAO
{
    public BitacoraDAO()
    {
    }
    public static List<Bitacora> execute(String desde, String hasta)
    {
        SqlConnection cn = ConexionSingleton.obtenerConexion();

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
            sb.Append(" fecha <= @hasta");
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

    public static void grabarBitacora(int usuId, String mensaje)
    {
        SqlConnection connection = ConexionSingleton.obtenerConexion();
        connection.Open();
        SqlTransaction tx = connection.BeginTransaction();
        StringBuilder builder = new StringBuilder(" INSERT INTO BITACORA  (");
        builder.Append("idUsuario,");
        builder.Append("mensaje,");
        builder.Append("dvh )");

        builder.Append(" VALUES (");
        builder.Append(" @USUARIO,");
        builder.Append(" @MENSAJE,");
        builder.Append(" @DVH");
        builder.Append(" ) ");
        SqlCommand cmd = new SqlCommand(builder.ToString(), connection, tx);
        DateTime fecha = DateTime.Now;
        cmd.Parameters.Add(new SqlParameter("@MENSAJE", System.Data.SqlDbType.Text)).Value = mensaje;
        cmd.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.VarChar)).Value = DigitosDAO.recalcularDigitoHorizontal(new string[] { fecha.ToString(), mensaje });
        cmd.Parameters.Add(new SqlParameter("@USUARIO", System.Data.SqlDbType.BigInt)).Value = usuId;

        try
        {
            cmd.ExecuteNonQuery();
            tx.Commit();
            connection.Close();
            DigitosDAO.recalcularDigitoVertical("BITACORA");
        }
        catch (Exception ex)
        {
            try
            {
                tx.Rollback();
            }
            catch (Exception)
            {


            }
            connection.Close();
            throw ex;
        }


    }
}
