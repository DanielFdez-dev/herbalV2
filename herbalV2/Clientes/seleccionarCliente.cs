using Datos;
using herbalV2.Vendedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace herbalV2.Clientes
{
    public partial class seleccionarCliente : Form
    {
        public event EventHandler<ClienteSeleccionado> clienteSeleccionado;
        public seleccionarCliente()
        {
            InitializeComponent();
        }

        private void listarClientes()
        {
            try
            {
                var obj = new dClientes();
                dgvClientes.DataSource = obj.listarClientes();
                dgvClientes.Columns["idCliente"].Visible = false;
                dgvClientes.Columns["calle"].Visible = false;
                dgvClientes.Columns["telefono"].Visible = false;
                dgvClientes.Columns["municipio"].Visible = false;
                dgvClientes.Columns["codigoPostal"].Visible = false;
                dgvClientes.Columns["estado"].Visible = false;
                dgvClientes.Columns["rfc"].Visible = false;
                dgvClientes.Columns["idTipoPrecio"].Visible = false;
                dgvClientes.Columns["plazoPago"].Visible = false;
                dgvClientes.Columns["observaciones"].Visible = false;
                dgvClientes.Columns["fechaCreacion"].Visible = false;
                dgvClientes.Columns["fechaBaja"].Visible = false;
                dgvClientes.Columns["activo"].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarClientes(): " + e.Message);
            }
        }

        private void seleccionarCliente_()
        {
            clienteSeleccionado?.Invoke(this, new ClienteSeleccionado(Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value), dgvClientes.CurrentRow.Cells[1].Value.ToString(), dgvClientes.CurrentRow.Cells[9].Value.ToString()));
            this.Dispose();
        }

        private void buscarCliente()
        {
            if (txtBuscar.Text == "")
            {
                listarClientes();
            }
            else
            {
                dgvClientes.CurrentCell = null;
                foreach (DataGridViewRow row in dgvClientes.Rows)
                {
                    row.Visible = false;
                }
                foreach (DataGridViewRow row in dgvClientes.Rows)
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

        private void seleccionarCliente_Load(object sender, EventArgs e)
        {
            listarClientes();
            txtBuscar.Focus();
        }

        private void dgvClientes_DoubleClick(object sender, EventArgs e)
        {
            seleccionarCliente_();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscarCliente();
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
                    dgvClientes.Focus();
                }
                else if (dgvClientes.Focused)
                {
                    seleccionarCliente_();
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
    public class ClienteSeleccionado : EventArgs
    {
        public int IdClienteSeleccionado { get; set; }
        public string Nombre { get; set; }
        public string PlazoPago { get; set; }

        public ClienteSeleccionado(int idClienteSeleccionado, string nombre, string plazoPago)
        {
            IdClienteSeleccionado = idClienteSeleccionado;
            Nombre = nombre;
            PlazoPago = plazoPago;
        }
    }
}
