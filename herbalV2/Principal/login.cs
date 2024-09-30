using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace herbalV2.Principal
{
    public partial class login : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessaege(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public login()
        {
            InitializeComponent();
        }

        private void pbCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void logeo()
        {
            if (string.IsNullOrWhiteSpace(txtEmpleado.Text))
            {
                MessageBox.Show("No puede estar el campo Usuario vacío");
                txtEmpleado.Focus();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    MessageBox.Show("No puede estar el campo Contraseña vacío");
                    txtPass.Focus();
                }
                else
                {
                    var obj = new dUsuarios();
                    int idEmpleado = 0, identificador=0;
                    string nombre = string.Empty, permisos = string.Empty;
                    if (obj.login(txtEmpleado.Text, txtPass.Text, out idEmpleado, out nombre, out permisos, out identificador))
                    {
                        if (identificador == 1)
                        {
                            dComun.idEmpleado = idEmpleado;
                            dComun.nombreEmpleado = nombre;
                            dComun.permisosEmpleado = permisos;

                            var form = new Principal();
                            form.FormClosed += Logout;
                            form.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error de lógica");
                        }
                    }
                    else
                    {
                        if (identificador == 0)
                        {
                            MessageBox.Show("El usuario y/o contraseña son incorrectos, intente nuevamente");
                            txtPass.Text = string.Empty;
                            txtEmpleado.Focus();
                            txtEmpleado.SelectAll();
                        }
                        else if (identificador == 2)
                        {
                            MessageBox.Show("Ocurrio un error con la conexión a la base de datos");
                        }
                    }
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            logeo();
        }

        private void login_Load(object sender, EventArgs e)
        {
            txtEmpleado.Focus();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {

            if (keyData == Keys.Escape)
            {
                this.Dispose();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                if (txtEmpleado.Focused)
                {
                    txtPass.Focus();
                }
                else if (txtPass.Focus())
                {
                    logeo();
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        private void Logout(object sender, FormClosedEventArgs e) //metodo para logout
        {
            this.Show();
            txtEmpleado.Focus();
            txtPass.Text = string.Empty;

        }

        private void login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessaege(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
