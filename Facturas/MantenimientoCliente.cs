using System;
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
    public partial class MantenimientoCliente : Mantenimiento
    {
        public MantenimientoCliente()
        {
            InitializeComponent();
        }
        public override bool Guardar()
        {
            string sql = string.Format("EXEC ActualizaClientes '{0}','{1}','{2}'", txtId.Text,txtNombre.Text, txtApellido.Text);

            if ( Utilidades.ValidarFormulario( this, errorProvider1 ) == false )
            {
                try
                {
                    Utilidades.Ejecutar(sql);
                    MessageBox.Show("Cliente actualizado correctamente.", "Actualizacion Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtId.Text = "";
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al Insertar Clientes.\nSql: " + sql + "\n" + ex.Message, "Error en Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            // cuando el texto cambie, borrar el mensaje de error
            errorProvider1.Clear();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            // cuando el texto cambie, borrar el mensaje de error
            errorProvider1.Clear();
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            // cuando el texto cambie, borrar el mensaje de error
            errorProvider1.Clear();
        }
    }
}
