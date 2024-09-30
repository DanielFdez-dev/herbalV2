
using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace herbalV2.Principal
{
    public partial class Principal : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessaege(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public Principal()
        {
            InitializeComponent();
            panelContenedor.SizeChanged += panelContenedor_SizeChanged;
        }

        private Dictionary<Type, Size> initialSizes = new Dictionary<Type, Size>();
        private void OpenForm<MyForm>() where MyForm : Form, new()
        {
            Form formulario;
            formulario = panelContenedor.Controls.OfType<MyForm>().FirstOrDefault();//Busca en la coleccion el formulario
            //si el formulario no existe
            if (formulario == null)
            {
                formulario = new MyForm();
                formulario.TopLevel = false;
                formulario.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(formulario);
                panelContenedor.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                formulario.MaximizeBox = false;
                formulario.MinimizeBox = false;
                //formulario.WindowState = FormWindowState.Maximized;
                formulario.Focus();

                // Almacena el tamaño inicial del formulario hijo
                initialSizes[typeof(MyForm)] = formulario.Size;
            }
            else
            {
                formulario.BringToFront();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            OpenForm<Usuarios.usuarios>();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            OpenForm<Clientes.clientes>();
        }

        private void btnVendedores_Click(object sender, EventArgs e)
        {
            OpenForm<Vendedores.vendedores>();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            OpenForm<Productos.productos>();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("u"))
            {
                OpenForm<Ventas.ventas>();
            }
        }

        private void btnCotizacion_Click(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("u"))
            {
                OpenForm<Cotizacion.cotizacion>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para ingresar");
            }
        }

        private void btnCuentasCobrar_Click(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("v"))
            {
                OpenForm<CuentasCobrar.cuentasCobrar>();
            }
            else
            {
                MessageBox.Show("No tiene permisos para ingresar");
            }
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            lbNombreEmpleado.Text = dComun.nombreEmpleado;
            MostrarVersion();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessaege(this.Handle, 0x112, 0xf012, 0);
        }

        private void pbMenu_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 250)
            {
                panelMenu.Width = 63;
            }
            else if (panelMenu.Width == 63)
            {
                panelMenu.Width = 250;
            }
        }

        private void panelContenedor_SizeChanged(object sender, EventArgs e)
        {
            if (panelMenu.Width == 250)
            {
                foreach (var form in panelContenedor.Controls.OfType<Form>())
                {
                    form.Width = panelContenedor.Width;
                }
            }
            else if (panelMenu.Width == 63)
            {
                foreach (var form in panelContenedor.Controls.OfType<Form>())
                {
                    form.Width = panelContenedor.Width;
                }
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            OpenForm<Reportes.reportes>();
        }
        private void MostrarVersion()
        {
            // Obtener la versión de la aplicación
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string versionTexto = $"V. {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";

            // Asignar la versión al label
            lbVersion.Text = versionTexto;
        }

        private void btnVentasPendientes_Click(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("u"))
            {
                var frm = new VentasPendientes.ventasPendientes();
                frm.ShowDialog();
            }
        }
    }
}
