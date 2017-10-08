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
    public partial class Acceso : FormBase
    {
        public static DataSet login;

        public Acceso()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = string.Format("SELECT * FROM usuarios WHERE account='{0}' AND password='{1}';", txtUsuario.Text.Trim(), txtContraseña.Text.Trim() );
            if ( txtUsuario.Text != "")
            {
                DataSet datos = Utilidades.Ejecutar(sql);

                try
                {
                    string usuario = datos.Tables[0].Rows[0]["account"].ToString().Trim();
                    string password = datos.Tables[0].Rows[0]["password"].ToString().Trim();

                    if (usuario == txtUsuario.Text.Trim() && password == txtContraseña.Text.Trim())
                    {
                        login = datos;

                        if (Convert.ToBoolean( datos.Tables[0].Rows[0]["Status_admin"]))
                        {
                            VentanaAdmin frmAdmin = new VentanaAdmin();
                            frmAdmin.ShowDialog();
                        }
                        else
                        {
                            VentanaUser frmUser = new VentanaUser();
                            frmUser.ShowDialog();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datos incorrectos!!.\nSQL: "+sql+"\n"+ex.Message);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Acceso_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            txtUsuario.Text = "javier";
            txtContraseña.Text = "123456";
        }
    }
}
