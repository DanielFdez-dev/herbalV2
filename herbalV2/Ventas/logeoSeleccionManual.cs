using Datos;
using herbalV2.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace herbalV2.Ventas
{
    public partial class logeoSeleccionManual : Form
    {
        public event EventHandler<AccesoSeleccionManual> accessoSeleccionManual;
        public logeoSeleccionManual()
        {
            InitializeComponent();
        }
        private void datosEmpleado()
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
                    int idEmpleado = 0, identificador = 0;
                    string nombre = string.Empty, permisos = string.Empty;
                    if (obj.login(txtEmpleado.Text, txtPass.Text, out idEmpleado, out nombre, out permisos, out identificador))
                    {
                        if (permisos.Contains("w"))
                        {
                            accessoSeleccionManual?.Invoke(this, new AccesoSeleccionManual(true));
                            this.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("El usuario no cuenta con permisos de Selección manual de lotes");
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
            datosEmpleado();
        }
    }
    public class AccesoSeleccionManual : EventArgs
    {
        public bool Acceso { get; set; }

        public AccesoSeleccionManual(bool acceso)
        {
            Acceso = acceso;
        }
    }
}
