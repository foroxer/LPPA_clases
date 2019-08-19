using ProyectoLPPA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de BackupDAO
/// </summary>
public class BackupDAO
{
    public BackupDAO()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static void realizarBackup(int partes, String directorio)
    {
        SqlConnection connection = ConexionSingleton.obtenerConexion();
        connection.Open();
        StringBuilder queryText = new StringBuilder();
        directorio = directorio.Replace("//", "\\");
        queryText.Append(" USE MASTER ");
        queryText.Append(" BACKUP DATABASE " + ConexionSingleton.dbName);

        for (int i = 0; i < partes; i++)
        {
            if (i == 0)
            {
                queryText.Append(" TO DISK = '" + directorio + (i + 1) + ".bak '");
            }
            else
            {
                queryText.Append(" , DISK = '" + directorio + (i + 1) + ".bak '");
            }
        }

        queryText.Append(" WITH init ");
        SqlCommand query = new SqlCommand(queryText.ToString(), connection);
        try
        {
            query.ExecuteNonQuery();
            connection.Close();
            BitacoraDAO.grabarBitacora(0, "Se realizó un backup");

        }
        catch (Exception e)
        {
            connection.Close();
            BitacoraDAO.grabarBitacora(0, "Falló un backup");
            throw e;
        }
    }

    public static void realizarRestore(String directorio)

    {

        SqlConnection connection = ConexionSingleton.obtenerConexion();
        connection.Open();
        directorio = directorio.Replace("//", "\\");
        StringBuilder queryText = new StringBuilder();
        queryText.Append(" USE MASTER ");

        queryText.Append(" alter database  " + ConexionSingleton.dbName);
        queryText.Append(" set offline with rollback immediate ");
        queryText.Append(" RESTORE DATABASE  " + ConexionSingleton.dbName);
        queryText.Append(" FROM  DISK = '" + directorio + "'");
        queryText.Append(" WITH REPLACE ");
        queryText.Append(" alter database  " + ConexionSingleton.dbName);
        queryText.Append(" set online with rollback immediate ");
        SqlCommand query = new SqlCommand(queryText.ToString(), connection);
        try
        {
            query.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {


            connection.Close();
            BitacoraDAO.grabarBitacora(0, "Falló un restore");
            throw e;
        }


        BitacoraDAO.grabarBitacora(0, "Se realizó un restore");

    }
}