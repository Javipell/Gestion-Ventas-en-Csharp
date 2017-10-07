using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ClassLibrary
{
    public class Utilidades
    {
        public static DataSet Ejecutar ( string sql)
        {
            string cadenaConexion = "Data Source=DESKTOP-NJLRLMD;Initial Catalog=BD_DataSet;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conexion = new SqlConnection(cadenaConexion);
            DataSet datos = new DataSet();

            try
            {
                conexion.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                adaptador.Fill(datos);
                conexion.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error de conexion.\n" + cadenaConexion);
            }

            return datos;

        }
    }
}
