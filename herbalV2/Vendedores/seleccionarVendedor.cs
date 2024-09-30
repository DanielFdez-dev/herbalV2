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

namespace herbalV2.Vendedores
{
    public partial class seleccionarVendedor : Form
    {
        public event EventHandler<VendedorSeleccionado> vendedorSeleccionado;
        
        public seleccionarVendedor()
        {
            InitializeComponent();
        }

        private void listarVendedores()
        {
            try
            {
                var obj = new dVendedores();
                dgvVendedores.DataSource = obj.listarVendedores();
                dgvVendedores.Columns["idVendedor"].Visible = false;
                dgvVendedores.Columns["comision"].Visible = false;
                dgvVendedores.Columns["idEstado"].Visible = false;
                //dgvVendedores.Columns["idRegion"].Visible = false;
                dgvVendedores.Columns["fechaCreacion"].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarVendedores(): " + e.Message);
            }
        }

        private void buscarVendedor()
        {
            if (txtBuscar.Text == "")
            {
                listarVendedores();
            }
            else
            {
                dgvVendedores.CurrentCell = null;
                foreach (DataGridViewRow row in dgvVendedores.Rows)
                {
                    row.Visible = false;
                }
                foreach (DataGridViewRow row in dgvVendedores.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if ((cell.Value.ToString().ToUpper()).IndexOf(txtBuscar.Text.ToUpper()) == 0)
                        {
                            row.Visible = true;
                            break;
                        }
                    }
                }
            }
        }
        private void seleccionarVendedor_()
        {
            vendedorSeleccionado?.Invoke(this, new VendedorSeleccionado(Convert.ToInt32(dgvVendedores.CurrentRow.Cells[0].Value), dgvVendedores.CurrentRow.Cells[1].Value.ToString()));
            this.Dispose();
        }

        private void seleccionarVendedor_Load(object sender, EventArgs e)
        {
            listarVendedores();
            txtBuscar.Focus();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscarVendedor();
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
                if (txtBuscar.Focused)
                {
                    dgvVendedores.Focus();
                }
                else if(dgvVendedores.Focused)
                {
                    seleccionarVendedor_();
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void dgvVendedores_DoubleClick(object sender, EventArgs e)
        {
            seleccionarVendedor_();
        }
    }
    public class VendedorSeleccionado : EventArgs
    {
        public int IdVendedorSeleccionado { get; set; }
        public string Nombre { get; set; }

        public VendedorSeleccionado(int idVendedorSeleccionado, string nombre)
        {
            IdVendedorSeleccionado = idVendedorSeleccionado;
            Nombre = nombre;
        }
    }
}
