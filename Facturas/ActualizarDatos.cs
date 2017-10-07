﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;

namespace Facturas
{
    public partial class ActualizarDatos : FormBase
    {
        public ActualizarDatos()
        {
            InitializeComponent();
        }

        /*
         * metodo btnGuardaar_Click
         * Comprueba si es valida la contraseña anterior
         * comprueba si la contraseña nueva es igual a la verificacion
         * y actualiza la bases de datos modificadola 
         */
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtAnterior.Text == Acceso.login.Tables[0].Rows[0]["password"].ToString())
            {
                if ( (txtNueva.Text == txtRepetir.Text) && (txtNueva.Text != txtAnterior.Text) )
                {
                    string id = Acceso.login.Tables[0].Rows[0]["id_usuario"].ToString();
                    string sql = string.Format("UPDATE usuarios SET password='{0}' WHERE Id_usuario='{1}';", txtNueva.Text, id);
                    DataSet datos = Utilidades.Ejecutar(sql);
                    try
                    {
                        string prueba = datos.Tables[0].Rows[0]["Num_usu"].ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fallo en la modificacion.\nSQL: "+sql+"\n"+ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseña nueva y la de verificación no coinciden.","Contraseña Incorrecta", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe introducir la contraseña anterior.","Contraseña Incorrecta", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           
        }
    }
}