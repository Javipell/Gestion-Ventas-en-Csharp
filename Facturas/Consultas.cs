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
    public partial class Consultas : FormBase
    {
        public Consultas()
        {
            InitializeComponent();
        }

        public DataSet LlenarDataGridView(string tabla)
        {
            DataSet dataSet = new DataSet();
            /*
             * de esta forma no funciona !!
             * string sql = string.Format("SELECT * FROM '{0}'", tabla);
             */
            string sql = string.Format("SELECT * FROM " + tabla);
            dataSet = Utilidades.Ejecutar(sql);

            return dataSet;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
