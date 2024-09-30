using Datos;
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
    public partial class vendedores : Form
    {
        public vendedores()
        {
            InitializeComponent();
        }

        private void limpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtComision.Text = string.Empty;
            cbEstado.SelectedIndex= 0;
            label2.Text = "Agregar";
            idVendedor.Text = "0";
        }

        private void agregarVendedor()
        {
            if (dComun.permisosEmpleado.Contains("n"))
            {
                try
                {
                    if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtComision.Text))
                    {
                        MessageBox.Show("No pueden haber campos vacios");
                    }
                    else
                    {
                        var obj = new dVendedores();
                        obj.agregarVendedor(txtNombre.Text, Convert.ToInt32(txtComision.Text), Convert.ToInt32(cbEstado.SelectedValue));
                        MessageBox.Show("Vendedor agregado correctamente");
                        listarVendedores();
                        limpiarControles();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error agregarVendedor(): " + e.Message);
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para agregar un nuevo vendedor");
            }

        }
        private void eliminarVendedor()
        {
            if (dComun.permisosEmpleado.Contains("l"))
            {
                try
                {
                    var obj = new dVendedores();
                    obj.eliminarVendedor(Convert.ToInt32(dgvVendedores.CurrentRow.Cells[0].Value));
                    MessageBox.Show("Vendedor eliminado correctamente");
                    listarVendedores();
                    limpiarControles();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error eliminarVendedor(): " + e.Message);
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para eliminar información");
            }

        }

        private void modificarVendedor()
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtComision.Text))
                {
                    MessageBox.Show("No pueden haber campos vacios");
                }
                else
                {
                    var obj = new dVendedores();
                    obj.modificarVendedor(Convert.ToInt32(idVendedor.Text), txtNombre.Text, Convert.ToInt32(txtComision.Text), Convert.ToInt32(cbEstado.SelectedValue));
                    MessageBox.Show("Vendedor modificado correctamente");
                    listarVendedores();
                    limpiarControles();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error modificarVendedor(): " + e.Message);
            }
        }
        private void listarVendedores()
        {
            try
            {
                var obj = new dVendedores();
                dgvVendedores.DataSource = obj.listarVendedores();
                dgvVendedores.Columns["idVendedor"].Visible = false;
                dgvVendedores.Columns["idEstado"].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarVendedores(): " + e.Message);
            }
        }
        private void listarEstados()
        {
            try
            {
                var obj = new dRegiones();
                cbEstado.DataSource = obj.listarEstados();
                cbEstado.DisplayMember = "descripcion";
                cbEstado.ValueMember = "idEstado";
                cbEstado.AutoCompleteMode = AutoCompleteMode.Suggest;
                cbEstado.AutoCompleteSource = AutoCompleteSource.ListItems;
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarEstados(): " + e.Message);
            }
        }

        private void vendedores_Load(object sender, EventArgs e)
        {
            listarEstados();
            listarVendedores();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (label2.Text == "Agregar")
            {
                agregarVendedor();
            }
            else
            {
                modificarVendedor();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.Delete)
            {
                if (dgvVendedores.Focused)
                {
                    eliminarVendedor();
                    return true;
                }
                else return false;
            }
            if (keyData == Keys.Enter)
            {
                if (label2.Text == "Agregar")
                {
                    agregarVendedor();
                }
                else
                {
                    modificarVendedor();
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
            if (dComun.permisosEmpleado.Contains("m"))
            {
                label2.Text = "Modificar";
                idVendedor.Text = dgvVendedores.CurrentRow.Cells[0].Value.ToString();
                txtNombre.Text = dgvVendedores.CurrentRow.Cells[1].Value.ToString();
                txtComision.Text = dgvVendedores.CurrentRow.Cells[2].Value.ToString();
                cbEstado.SelectedValue = dgvVendedores.CurrentRow.Cells[4].Value;
            }
            else
            {
                MessageBox.Show("No tiene permisos para modificar información de vendedores");
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarControles();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminarVendedor();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
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
    }
}
