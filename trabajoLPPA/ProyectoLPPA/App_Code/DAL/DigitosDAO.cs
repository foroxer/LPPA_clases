using ProyectoLPPA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class DigitosDAO
{
    public DigitosDAO()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static void recalcularDigitoVertical(String tabla)
    {

        SqlConnection connection = ConexionSingleton.obtenerConexion();
        connection.Open();
        SqlTransaction tx = connection.BeginTransaction();
        String updateCommand = " UPDATE DIGITO_VERTICAL SET DV_DIGITO_CALCULADO = @HASH WHERE DV_NOMBRE_TABLA =  @NOMBRE_TABLA ";
        SqlCommand query = new SqlCommand("", connection, tx);
        StringBuilder sb = new StringBuilder();

        sb.Append(" SELECT dvh  FROM " + tabla);
        query.CommandText = sb.ToString();
        SqlDataReader reader;
        String sumaDeDVH = "";
        try
        {
            reader = query.ExecuteReader();

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    sumaDeDVH += reader.GetString(0);
                }
            }
            reader.Close();
            query.Parameters.Clear();
            query.CommandText = updateCommand;
            query.Parameters.Add(new SqlParameter("@HASH", System.Data.SqlDbType.VarChar)).Value = CryptoUtils.encriptarMD5(sumaDeDVH);
            query.Parameters.Add(new SqlParameter("@NOMBRE_TABLA", System.Data.SqlDbType.VarChar)).Value = tabla;

            query.ExecuteNonQuery();

            tx.Commit();
            connection.Close();
        }
        catch (Exception ex)
        {

            try
            {
                tx.Rollback();
            }
            catch (Exception ex2)
            {
                connection.Close();
                throw ex2;

            }
            connection.Close();
            throw ex;
        }
        finally
        {
            connection.Close();
        }

    }



    public static void verificarDigitosVerificadores()
    {

        SqlConnection connection = ConexionSingleton.obtenerConexion();
        if (connection.State == System.Data.ConnectionState.Open)
            connection.Close();
        connection.Open();
        SqlTransaction tr = connection.BeginTransaction();
        SqlDataReader reader = null;
        SqlCommand cmd = new SqlCommand("", connection, tr);
        Dictionary<String, String> digitoVerticalCalculado = new Dictionary<string, string>();

        StringBuilder stringParaDVH = new StringBuilder();
        StringBuilder builder = new StringBuilder();
        List<String> mensajesDeError = new List<string>(); // la lista en donde voy a cargar todos los mensajes de error
        String query = "SELECT id, fecha, mensaje, dvh FROM BITACORA ";

        cmd.CommandText = query;
        builder = new StringBuilder();

        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            builder.Clear();

            builder.Append(reader.GetValue(1).ToString());
            builder.Append(reader.GetValue(2).ToString());
            String md5 = CryptoUtils.encriptarMD5(builder.ToString());
            String patDVH = reader.GetString(3);

            if (!md5.Equals(patDVH))
            {
                int id = (int)reader.GetValue(0);
                mensajesDeError.Add("Falló la integridad de datos en bitácora en el id " + id.ToString());

            }
            stringParaDVH.Append(md5);
        }

        digitoVerticalCalculado.Add("BITACORA", stringParaDVH.ToString());
        reader.Close();
        stringParaDVH.Clear();


        query = " SELECT DV_NOMBRE_TABLA,DV_DIGITO_CALCULADO,dv_id FROM DIGITO_VERTICAL ";

        cmd.CommandText = query;

        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            builder.Clear();
            string tabla = reader.GetValue(0).ToString();
            string md5Base = reader.GetValue(1).ToString();
            string md5Calculado = digitoVerticalCalculado[tabla];

            md5Calculado = CryptoUtils.encriptarMD5(md5Calculado);

            if (!md5Base.Equals(md5Calculado))
            {
                long id = (long)reader.GetValue(2);
                mensajesDeError.Add("Falló la integridad de datos en digito vertical en el row " + id.ToString());
            }

        }
        reader.Close();
        connection.Close();

        if (mensajesDeError.Count > 0)
        {
            foreach (String item in mensajesDeError)
            {
                BitacoraDAO.grabarBitacora(0, item);
            }
            throw new Exception("Falló la integridad de datos.");
        }
    }

    public static string recalcularDigitoHorizontal(String[] campos)
    {
        StringBuilder builder = new StringBuilder();
        for (int x = 0; x < campos.Length; x++)
        {
            builder.Append(campos[x]);
        }
        return CryptoUtils.encriptarMD5(builder.ToString());
    }



}