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

namespace herbalV2.Clientes
{
    public partial class clientes : Form
    {
        public clientes()
        {
            InitializeComponent();
        }

        private void limpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtEmpresa.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            cbMunicipio.DataSource = null;
            txtRFC.Text = string.Empty;
            txtPlazoPago.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            txtAdeudo.Text = "0.0";
            txtCodigoPostal.Text = string.Empty;

            label2.Text = "Agregar";
            idCliente.Text = "0";
            obtenerUltimoId();
        }
        private void listarEstados()
        {
            try
            {
                var obj = new dUbicaciones();
                cbEstado.DataSource = obj.listarEstado();
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
        private void listarMunicipios()
        {
            try
            {
                int id = Convert.ToInt32(cbEstado.SelectedValue);
                var obj = new dUbicaciones();
                cbMunicipio.DataSource = obj.listarMunicipio(id);
                cbMunicipio.DisplayMember = "descripcion";
                cbMunicipio.ValueMember = "idMunicipio";
                cbMunicipio.AutoCompleteMode = AutoCompleteMode.Suggest;
                cbMunicipio.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception)
            {
            }
        }
        private void cargarTipoPrecio()
        {
            try
            {
                var obj = new dTipoPrecio();
                cbTipoPrecio.DataSource = obj.listarTipoPrecio();
                cbTipoPrecio.DisplayMember = "descripcion";
                cbTipoPrecio.ValueMember = "idTipoPrecio";
                cbTipoPrecio.AutoCompleteMode = AutoCompleteMode.Suggest;
                cbTipoPrecio.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error cargarTipoPrecio(): " + e.Message);
            }
        }

        private void listarClientes()
        {
            try
            {
                var obj = new dClientes();
                dgvClientes.DataSource = obj.listarClientes();
                dgvClientes.Columns["telefono"].Visible=false;
                dgvClientes.Columns["calle"].Visible = false;
                dgvClientes.Columns["codigoPostal"].Visible = false;
                dgvClientes.Columns["municipio"].Visible = false;
                dgvClientes.Columns["RFC"].Visible= false;
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

        private void eliminarCliente()
        {
            if (dComun.permisosEmpleado.Contains("f"))
            {
                try
                {
                    if (MessageBox.Show("¿Desea borrar el Cliente?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            var obj_user = new dClientes();
                            obj_user.eliminarCliente(Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value));
                            MessageBox.Show("Cliente eliminado correctamente");
                            listarClientes();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error en: " + e.Message);
                        }
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Error eliminarCliente(): " + e.Message);
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para ingresar");
            }

        }

        private void agregarCliente()
        {
            if (dComun.permisosEmpleado.Contains("h"))
            {
                try
                {
                    if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtEmpresa.Text) || string.IsNullOrEmpty(txtCalle.Text) || string.IsNullOrEmpty(txtCodigoPostal.Text) || string.IsNullOrEmpty(txtTelefono.Text)
                        || string.IsNullOrEmpty(cbMunicipio.Text) || string.IsNullOrEmpty(txtRFC.Text) || string.IsNullOrEmpty(txtPlazoPago.Text)
                        || string.IsNullOrEmpty(txtObservaciones.Text) || string.IsNullOrEmpty(txtAdeudo.Text))
                    {
                        MessageBox.Show("No puede haber campos vacíos");
                    }
                    else
                    {
                        var obj = new dClientes();
                        obj.agregarCliente(txtNombre.Text, txtEmpresa.Text, txtCalle.Text, txtCodigoPostal.Text, txtTelefono.Text, cbMunicipio.Text, cbEstado.Text, txtRFC.Text, Convert.ToInt32(cbTipoPrecio.SelectedValue.ToString()),
                            Convert.ToInt32(txtPlazoPago.Text), txtObservaciones.Text, Convert.ToDecimal(txtAdeudo.Text));
                        MessageBox.Show("Cliente agregado correctamente");
                        listarClientes();
                        limpiarControles();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error agregarCliente(): " + e.Message);
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para agregar un nuevo cliente");
            }

        }
        private void modificarCliente()
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtEmpresa.Text) || string.IsNullOrEmpty(txtCalle.Text) || string.IsNullOrEmpty(txtCodigoPostal.Text) || string.IsNullOrEmpty(txtTelefono.Text)
                    || string.IsNullOrEmpty(cbMunicipio.Text) || string.IsNullOrEmpty(cbEstado.Text) || string.IsNullOrEmpty(txtRFC.Text) || string.IsNullOrEmpty(txtPlazoPago.Text)
                    || string.IsNullOrEmpty(txtObservaciones.Text) || string.IsNullOrEmpty(txtAdeudo.Text))
                {
                    MessageBox.Show("No puede haber campos vacíos");
                }
                else
                {
                    var obj = new dClientes();
                    obj.modificarClientes(Convert.ToInt32(idCliente.Text), txtNombre.Text, txtEmpresa.Text, txtCalle.Text,txtCodigoPostal.Text, txtTelefono.Text, cbMunicipio.Text, cbEstado.Text, txtRFC.Text, Convert.ToInt32(cbTipoPrecio.SelectedValue.ToString()),
                        Convert.ToInt32(txtPlazoPago.Text), txtObservaciones.Text, Convert.ToDecimal(txtAdeudo.Text));
                    MessageBox.Show("Cliente modificado correctamente");
                    listarClientes();
                    limpiarControles();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error modificarCliente(): " + e.Message);
            }
        }
        private void obtenerUltimoId()
        {
            try
            {
                var obj = new dClientes();
                txtIdCliente.Text = obj.ultimoId().ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error obtenerUltimoId(): " + e.Message);
            }
        }

        private void clientes_Load(object sender, EventArgs e)
        {
            obtenerUltimoId();
            listarClientes();
            cargarTipoPrecio();
            listarEstados();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (label2.Text == "Agregar")
            {
                agregarCliente();
            }
            else
            {
                modificarCliente();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminarCliente();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.Delete)
            {
                if (dgvClientes.Focused)
                {
                    eliminarCliente();
                    return true;
                }
                else return false;
            }
            if (keyData == Keys.Enter)
            {
                if (label2.Text == "Agregar")
                {
                    agregarCliente();
                }
                else
                {
                    modificarCliente();
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void dgvClientes_DoubleClick(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("g"))
            {
                label2.Text = "Modificar";
                txtIdCliente.Text = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                idCliente.Text = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                txtNombre.Text = dgvClientes.CurrentRow.Cells[1].Value.ToString();
                txtEmpresa.Text = dgvClientes.CurrentRow.Cells[2].Value.ToString();
                txtCalle.Text = dgvClientes.CurrentRow.Cells[3].Value.ToString();
                txtTelefono.Text = dgvClientes.CurrentRow.Cells[4].Value.ToString();
                cbMunicipio.Text = dgvClientes.CurrentRow.Cells[5].Value.ToString();
                cbEstado.Text = dgvClientes.CurrentRow.Cells[6].Value.ToString();
                txtRFC.Text = dgvClientes.CurrentRow.Cells[7].Value.ToString();
                cbTipoPrecio.SelectedValue = dgvClientes.CurrentRow.Cells[8].Value;
                txtPlazoPago.Text = dgvClientes.CurrentRow.Cells[9].Value.ToString();
                txtObservaciones.Text = dgvClientes.CurrentRow.Cells[10].Value.ToString();
                txtAdeudo.Text = dgvClientes.CurrentRow.Cells[11].Value.ToString();
                txtCodigoPostal.Text = dgvClientes.CurrentRow.Cells[15].Value.ToString();
            }
            else
            {
                MessageBox.Show("No tiene permisos para modificar información de clientes");
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarControles();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
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

        private void cbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            var estadoSeleccionado = cbEstado.SelectedItem;
            if (estadoSeleccionado != null)
            {
                listarMunicipios();
            }
        }
    }
}
