using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturas
{
    public partial class VentanaAdmin : FormBase
    {
        public VentanaAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VentanaAdmin_Load(object sender, EventArgs e)
        {
            txtAdmin.Text = Acceso.login.Tables[0].Rows[0]["Nom_usu"].ToString();
            txtUsuario.Text = Acceso.login.Tables[0].Rows[0]["account"].ToString();
            txtCodigo.Text = Acceso.login.Tables[0].Rows[0]["id_usuario"].ToString();
            string imagen = Acceso.login.Tables[0].Rows[0]["Foto"].ToString();
            pictureBox1.Image = Image.FromFile(imagen);

        }

        private void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            ActualizarDatos frmDatos = new ActualizarDatos();
            frmDatos.ShowDialog();
        }
    }
}
