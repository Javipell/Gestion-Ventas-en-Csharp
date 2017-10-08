using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
                Console.WriteLine("Error de conexion.\n" + cadenaConexion+"\n"+ex.Message);
            }

            return datos;

        }

        /*
         * Metodo ValidarFormulario
         * ver : https://www.youtube.com/watch?v=UncNMe6_OVM
         * 1 añadir using System.Windows.Forms
         */
        public static Boolean ValidarFormulario(Control Objeto, ErrorProvider errorProvider)
        {
            Boolean HayErrores = false;
            
            foreach (Control Item in Objeto.Controls)
            {
                
                if (Item is ErrorTxtBox)
                {
                    ErrorTxtBox Obj = (ErrorTxtBox)Item;
                    // valida que no esten vacios los campos
                    // solo se ejecuta si la propiedad Validar del texbox estra a true
                    if ( Obj.Validar == true )
                    {
                        if ( string.IsNullOrEmpty ( Obj.Text.Trim() ) )
                        {
                            errorProvider.SetError(Obj, "No puede estar vacio");
                            HayErrores = true;
                        }
                    }
                    // valida que solo haya numeros y no letras
                    // solo se ejecuta si la propiedad SoloNumeros del textbox esta a true
                    if ( Obj.SoloNumeros == true )
                    {
                        int contador = 0;
                        int letrasEncontradas = 0;

                        /*foreach ( Char letra in Obj.Text.Trim() )
                        {
                            if ( char.IsLetter( Obj.Text.Trim(), contador ) )
                            {
                                letrasEncontradas++;
                            }
                            contador++;
                        }
                        if ( letrasEncontradas != 0 )
                        {
                            HayErrores = true;
                            errorProvider.SetError(Obj, "Solo se admiten números.");
                        }*/
                        foreach ( Char letra in Obj.Text.Trim() )
                        {
                            if ( char.IsLetter( Obj.Text.Trim(), contador ) )
                            {
                                HayErrores = true;
                                errorProvider.SetError(Obj, "Solo se admiten números.");
                                break;
                            }
                            contador++;
                        }
                    }
                }
            }
            return HayErrores;
        }
    }
}
