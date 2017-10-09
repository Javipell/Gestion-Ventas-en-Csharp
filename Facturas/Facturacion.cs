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
    public partial class Facturacion : Procesos
    {
        public Facturacion()
        {
            InitializeComponent();
        }

        private void Facturacion_Load(object sender, EventArgs e)
        {
            txtAtiende.Text = Acceso.login.Tables[0].Rows[0]["Nom_usu"].ToString();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int valorBuscar = 0;
            string cadenaBuscar = "";
            string sql = "";
            DataSet dataSet;

            if (!string.IsNullOrEmpty(txtCodigo.Text.Trim()))
            {
                // determina si ha introducido un numero o un texto 
                try
                {
                    // si se puede convertir a numero ...
                    valorBuscar = Convert.ToInt32(txtCodigo.Text);
                    sql = string.Format("SELECT * FROM cliente WHERE id_clientes='{0}'",valorBuscar);
                }
                catch (Exception)
                {
                    // si no es un texto...
                    cadenaBuscar = txtCodigo.Text.Trim();
                    sql = string.Format("SELECT * FROM cliente WHERE Nom_cli LIKE '%{0}%';",cadenaBuscar);
                }

                //MessageBox.Show("a buscar "+ txtCodigo.Text + "\nsql: "+sql);

                try
                {
                    // vacio el combobox
                    cmbNomCliente.Items.Clear();
                    // ejecuto la consulta
                    dataSet =  Utilidades.Ejecutar(sql);
                    // si se buscaba un numero, solo ha podido encontrar uno ...
                    if (valorBuscar != 0)
                    {
                        // añade el nombre del cliente al combo 
                        cmbNomCliente.Items.Add(dataSet.Tables[0].Rows[0]["Nom_Cli"].ToString());
                        // selecciona el unico elemento
                        cmbNomCliente.SelectedIndex = 0;
                        // pone  el foco en el siguiente campo
                        txtCodArticulo.Focus();
                    }
                    else // si se buscaba un texto ...
                    {
                        // determina cuantas filas devuelve el dataset
                        int filas =  ( dataSet.Tables[0].Rows.Count );
                        // si solo hay una fila
                        if (filas>1)
                        {
                            cmbNomCliente.Items.Add("Resultados...");
                        }
                        // recorre las filas y añade los nombres al combobox
                        for (int i = 0; i < filas; i++)
                        {
                            cmbNomCliente.Items.Add(dataSet.Tables[0].Rows[i]["Nom_Cli"].ToString());
                            cmbNomCliente.SelectedIndex = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Valor no encontrado.\nSql. " + sql + "\n" + ex.Message, "Error en la busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        public static int contador_filas = 0;

        public static double total = 0.0;

        private void btnColocar_Click(object sender, EventArgs e)
        {
            if (Utilidades.ValidarFormulario(this, errorProvider1) == false )
            {
                Boolean exite_articulo = false;
                int num_fila = 0;
                if (contador_filas == 0)
                {
                    // añado los datos al dataGridView
                    dataGridView1.Rows.Add(txtCodArticulo.Text, txtDescripcion.Text, txtPrecio.Text, txtCantidad.Text);
                    double importe = 0.0;
                    // una manera ...
                    importe = Convert.ToDouble(dataGridView1.Rows[contador_filas].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[contador_filas].Cells[3].Value);
                    // otra manera ...
                    importe = Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text);
                    // añado el importe al dataGridView
                    dataGridView1.Rows[contador_filas].Cells[4].Value = importe;
                    contador_filas++;
                    total += importe;
                }
                else
                {
                    // recorro fila por fila el dataGridView
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        
                        // comparo el valor de la columna cero con el valor del codigo introducido
                        if (item.Cells[0].Value.ToString() == txtCodArticulo.Text)
                        {
                            exite_articulo = true;
                            // guardo el numero de la fila
                            num_fila = item.Index;
                        }
                    }
                    if (exite_articulo)
                    {
                        double importeAnterior = Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[3].Value);
                        dataGridView1.Rows[num_fila].Cells[3].Value = 
                            ( Convert.ToDouble( txtCantidad.Text ) 
                            + Convert.ToDouble( dataGridView1.Rows[num_fila].Cells[3].Value) ).ToString(); // lo convierto string
                        exite_articulo = false;
                        double importe = Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[num_fila].Cells[3].Value);
                        // añado el importe al dataGridView
                        dataGridView1.Rows[num_fila].Cells[4].Value = importe;
                        total = total - importeAnterior + importe;
                    }
                    else
                    {
                        // añado los datos al dataGridView
                        dataGridView1.Rows.Add(txtCodArticulo.Text, txtDescripcion.Text, txtPrecio.Text, txtCantidad.Text);
                        double importe = 0.0;
                        // una manera ...
                        importe = Convert.ToDouble(dataGridView1.Rows[contador_filas].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[contador_filas].Cells[3].Value);
                        // otra manera ...
                        importe = Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text);
                        // añado el importe al dataGridView
                        dataGridView1.Rows[contador_filas].Cells[4].Value = importe;
                        contador_filas++;
                        total += importe;
                    }
                }
                lblTotal.Text = Convert.ToString(total);                
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (contador_filas > 0 )
            {
                int fila =0;
                // guarda la fila seleccionada
                fila= Convert.ToInt16( dataGridView1.CurrentRow.Index);
                double import = 0.0;
                // guarda el importe de la fila seleccionada
                import = Convert.ToDouble( dataGridView1.Rows[fila].Cells[4].Value );
                total -= import;
                lblTotal.Text = Convert.ToString(total);
                // elimina la fila seleccionada en el datagridview
                dataGridView1.Rows.RemoveAt(fila);
                // resta una fila al contador de filas
                contador_filas--;
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ConsultarCliente frmCli = new ConsultarCliente();
            frmCli.ShowDialog();
            if (frmCli.DialogResult == DialogResult.OK)
            {
                // fila seleccionada en el datagridview del formulario ConsultarCliente
                int fila = frmCli.dataGridView1.CurrentRow.Index;
                txtCodigo.Text = frmCli.dataGridView1.Rows[fila].Cells[0].Value.ToString();
                // borra el combobox y lo actualiza 
                cmbNomCliente.Items.Clear();
                string nombre = frmCli.dataGridView1.Rows[fila].Cells[1].Value.ToString();
                cmbNomCliente.Items.Add(nombre);
                // selecciona el primer valor
                cmbNomCliente.SelectedIndex = 0;
                txtCodArticulo.Focus();
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            ConsultaProductos frmPro = new ConsultaProductos();
            frmPro.ShowDialog();
            if (frmPro.DialogResult == DialogResult.OK)
            {
                // fila seleccionada en el datagridview del formulario ConsultarCliente
                int fila = frmPro.dataGridView1.CurrentRow.Index;
                txtCodArticulo.Text = frmPro.dataGridView1.Rows[fila].Cells[0].Value.ToString();
                txtDescripcion.Text = frmPro.dataGridView1.Rows[fila].Cells[1].Value.ToString();
                txtPrecio.Text = frmPro.dataGridView1.Rows[fila].Cells[2].Value.ToString();
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            // cuando el texto cambie, borrar el mensaje de error
            errorProvider1.Clear();
        }

        private void txtCodArticulo_TextChanged(object sender, EventArgs e)
        {
            // cuando el texto cambie, borrar el mensaje de error
            errorProvider1.Clear();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            // cuando el texto cambie, borrar el mensaje de error
            errorProvider1.Clear();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            // cuando el texto cambie, borrar el mensaje de error
            errorProvider1.Clear();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            // cuando el texto cambie, borrar el mensaje de error
            errorProvider1.Clear();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        public override void Nuevo()
        {
            txtCodigo.Text = "";
            cmbNomCliente.Items.Clear();
            cmbNomCliente.Text = "";
            txtCodArticulo.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            lblTotal.Text = "";
            contador_filas = 0;
            dataGridView1.Rows.Clear();
            total = 0;
            txtCodigo.Focus();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (contador_filas > 0)
            {
                DataSet dataSet;
                string sql = "" ;

                try
                {
                    // ejecuta el procedimiento almacenado de actualizar factura
                    sql = "EXEC ActualizaFacturas " + txtCodigo.Text.Trim();
                    dataSet = Utilidades.Ejecutar(sql);
                    // guarda el numero de factura traido por la consulta
                    string numeroFactura = dataSet.Tables[0].Rows[0]["NumeroFactura"].ToString().Trim();
                    // recorre cada una de las filas del dataGridView1
                    foreach (DataGridViewRow fila in dataGridView1.Rows)
                    {
                        /*
                         * Orden de las columnas del dataGridView1
                         * 0 codigo producto, 1 descripcion, 2 precio, 3 cantidad, 4 importe
                         */
                        sql = string.Format( "EXEC ActualizaDetalles '{0}','{1}','{2}','{3}'", 
                                numeroFactura, 
                                fila.Cells[0].Value.ToString(), // codigo producto
                                fila.Cells[2].Value.ToString(), // precio
                                fila.Cells[3].Value.ToString()  // camtidad 
                                );
                        dataSet = Utilidades.Ejecutar(sql);
                    }
                    // ejecuta el procedimiento Datos factura, donde guarda el numero, fecha, cliente
                    sql = "EXEC DatosFactura " + numeroFactura;
                    dataSet = Utilidades.Ejecutar(sql);
                    /*
                     * VENTANA REPORTE
                     */
                    Reportes reportes = new Reportes();
                    reportes.reportViewer1.LocalReport.DataSources[0].Value = dataSet.Tables[0];
                    reportes.ShowDialog();
                    Nuevo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al facturar. \nSql: "+sql+"\n"+ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }
    }
}
