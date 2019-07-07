using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace ProyectoLPPA
{


    public static class SeguridadUtiles
    {

        private static String dbName = "lppa";

        public static string recalcularDigitoHorizontal(String[] campos)
        {
            StringBuilder builder = new StringBuilder();
            for (int x = 0; x < campos.Length; x++)
            {
                builder.Append(campos[x]);
            }
            return SeguridadUtiles.encriptarMD5(builder.ToString());
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
                query.Parameters.Add(new SqlParameter("@HASH", System.Data.SqlDbType.VarChar)).Value = SeguridadUtiles.encriptarMD5(sumaDeDVH);
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
        public static void realizarBackup(int partes, String directorio)
        {
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            StringBuilder queryText = new StringBuilder();
            directorio = directorio.Replace("//", "\\");
            queryText.Append(" USE MASTER ");
            queryText.Append(" BACKUP DATABASE " + dbName);

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
                grabarBitacora(0, "Se realizó un backup");

            }
            catch (Exception e)
            {
                connection.Close();
                grabarBitacora(0, "Falló un backup");
                throw e;
            }
        }

        public static void realizarRestore(String directorio)

        {
            
            SqlConnection connection =  ConexionSingleton.obtenerConexion();
            connection.Open();
            directorio = directorio.Replace("//", "\\");
            StringBuilder queryText = new StringBuilder();
            queryText.Append(" USE MASTER ");

            queryText.Append(" alter database  " + dbName);
            queryText.Append(" set offline with rollback immediate ");
            queryText.Append(" RESTORE DATABASE  " + dbName);
            queryText.Append(" FROM  DISK = '" + directorio + "'");
            queryText.Append(" WITH REPLACE ");
            queryText.Append(" alter database  " + dbName);
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
                grabarBitacora(0, "Falló un restore");
                throw e;
            }


            grabarBitacora(0, "Se realizó un restore");

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
            cmd.Parameters.Add(new SqlParameter("@DVH", System.Data.SqlDbType.VarChar)).Value = recalcularDigitoHorizontal(new string[] { fecha.ToString(), mensaje });
            cmd.Parameters.Add(new SqlParameter("@USUARIO", System.Data.SqlDbType.BigInt)).Value = usuId;

            try
            {
                cmd.ExecuteNonQuery();
                tx.Commit();
                connection.Close();
                recalcularDigitoVertical("BITACORA");
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


        public static String listarBitacora(String filtro, String valor, String orden)
        {
            List<Bitacora> result = new List<Bitacora>();
            SqlConnection connection = ConexionSingleton.obtenerConexion();
            connection.Open();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            SqlTransaction tx = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("", connection, tx);
            String query = " SELECT * FROM vistaBitacora order by fecha desc ";
            cmd.CommandText = query;
            try
            {
                DateTime dateTime;
                String nombre;
                String mensaje;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.NextResult())
                {
                    dateTime = reader.GetDateTime(0);
                    nombre = reader.GetString(1);
                    mensaje = reader.GetString(2);
                    result.Add(new Bitacora(dateTime, nombre, mensaje));
                }
                reader.Close();
                tx.Commit();
                connection.Close();

            }
            catch (Exception)
            {
                tx.Rollback();
                connection.Close();
            }
            return serializer.Serialize(result);
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
                String md5 = SeguridadUtiles.encriptarMD5(builder.ToString());
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

                md5Calculado = SeguridadUtiles.encriptarMD5(md5Calculado);

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
                    grabarBitacora(0, item);
                }
                throw new Exception("Falló la integridad de datos.");
            }
        }

    }
}