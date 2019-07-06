using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ProyectoLPPA
{
    public class ConexionSingleton
    {

        private static SqlConnection _conexion;

        private static SqlConnection constructor()
        {
            
            return new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString);
        }

        public static SqlConnection obtenerConexion()
        {
            if (_conexion == null)
            {
                _conexion = constructor();
            }
            return _conexion;
        }




    }
}