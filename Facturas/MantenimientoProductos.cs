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
    public partial class MantenimientoProductos : Mantenimiento
    {
        public MantenimientoProductos()
        {
            InitializeComponent();
        }
        public override Boolean Guardar()
        {
            /*
             * procedimientos almacenados
             * ver: https://www.youtube.com/watch?v=oV1i7QnleCc&feature=youtu.be 
             */
            string sql = string.Format("EXEC ActualizaArticulos '{0}','{1}','{2}'", txtId.Text, txtNombre.Text, txtPrecio.Text);
            /*
             * control de erores
             * ver : https://www.youtube.com/watch?v=UncNMe6_OVM
             */
            if (ClassLibrary.Utilidades.ValidarFormulario(this, errorProvider1) == false)
            {
                try
                {
                    Utilidades.Ejecutar(sql);
                    MessageBox.Show("Se han guardado los datos.", "Guardado de datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtId.Text = "";
                    txtNombre.Text = "";
                    txtPrecio.Text = "";
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar articulo.\nSql: " + sql + "\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            } else
            {
                return false;
            }
        }


        public override void Eliminar()
        {
            string sql = string.Format("EXEC EliminarArticulos '{0}'",txtId.Text);
            try
            {
                Utilidades.Ejecutar(sql);
                MessageBox.Show("Articulo eliminado correctamente","Articulo Eliminado", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar articulos.\nSql: "+sql+"\n"+ex.Message,"Error Eliminacion",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            // cuando el texto cambie, borrar el mensaje de error
            errorProvider1.Clear();
        }
    }
}
