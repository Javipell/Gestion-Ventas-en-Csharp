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
    public partial class ConsultaProductos : Consultas
    {
        public ConsultaProductos()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuscar.Text.Trim()))
            {
                string sql = "SELECT * FROM articulo WHERE Nom_pro LIKE '%"
                    + txtBuscar.Text.Trim() + "%'";
                DataSet dataSet;
                try
                {
                    dataSet = Utilidades.Ejecutar(sql);
                    dataGridView1.DataSource = dataSet.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la busqueda de Articulos.\nSql: " + sql + "\n" + ex.Message, "Error de Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConsultaProductos_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LlenarDataGridView("articulo").Tables[0];
        }
    }
}
