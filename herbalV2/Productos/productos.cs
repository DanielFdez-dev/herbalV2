using Datos;
using Datos.Listas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace herbalV2.Productos
{
    public partial class productos : Form
    {
        public productos()
        {
            InitializeComponent();
            dgvProductos.VirtualMode = true;
            
        }

        private void limpiarControles()
        {
            txtCodigo.Text = string.Empty;
            txtAmecop.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPCosto.Text = string.Empty;
            txtPrecioDistribuidor.Text = string.Empty;
            txtPMayoreo.Text = string.Empty;
            txtPLab.Text = string.Empty;
            txtStock.Text = string.Empty;
            chIva.Checked = false;
            cbClasificacion.SelectedIndex = 0;
            cbMarca.SelectedIndex = 0;
            label2.Text = "Agregar";
            idProducto.Text = "0";
            txtPLista.Text = string.Empty;
            txtStock.Text = "0.0";
        }
        //private  void listarProductos()
        //{
        //    try
        //    {
        //        var obj = new dProductos();
        //        var productos = obj.listarProductos();
        //        dgvProductos.SuspendLayout();
        //        dgvProductos.DataSource = productos;
        //        dgvProductos.ResumeLayout();
        //        dgvProductos.Columns["idProducto"].Visible = false;
        //        dgvProductos.Columns["idClasificacion"].Visible = false;
        //        dgvProductos.Columns["idMarca"].Visible = false;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Error listarProductos(): " + e.Message);
        //    }
        //}
        private async void listarClasificaciones()
        {
            try
            {
                var obj = new dClasificaciones();
                cbClasificacion.DataSource = await Task.Run(() => obj.listarClasificaciones());
                cbClasificacion.DisplayMember = "descripcion";
                cbClasificacion.ValueMember = "idClasificacion";
                cbClasificacion.AutoCompleteMode = AutoCompleteMode.Suggest;
                cbClasificacion.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarClasificaciones(): " + e.Message);
            }
        }
        private async void listarMarcas()
        {
            try
            {
                var obj = new dMarca();
                cbMarca.DataSource = await Task.Run(() => obj.listarMarcas());
                cbMarca.DisplayMember = "descripcion";
                cbMarca.ValueMember = "idMarca";
                cbMarca.AutoCompleteMode = AutoCompleteMode.Suggest;
                cbMarca.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarMarcas(): " + e.Message);
            }
        }
        private void agregarProducto()
        {
            if (dComun.permisosEmpleado.Contains("e"))
            {
                try
                {
                    if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtAmecop.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtPMayoreo.Text) || string.IsNullOrEmpty(txtPCosto.Text)
                        || string.IsNullOrEmpty(txtPLab.Text) || string.IsNullOrEmpty(txtPrecioDistribuidor.Text))
                    {
                        MessageBox.Show("No pueden haber campos vacíos");
                    }
                    else
                    {
                        var obj = new dProductos();
                        obj.agregarProducto(txtCodigo.Text, txtAmecop.Text, txtDescripcion.Text, 0, Convert.ToDecimal(txtPCosto.Text), Convert.ToDecimal(txtPLab.Text), Convert.ToDecimal(txtPrecioDistribuidor.Text),
                            Convert.ToDecimal(txtPMayoreo.Text), Convert.ToDecimal(txtPLista.Text), Convert.ToInt32(chIva.Checked), Convert.ToInt32(cbClasificacion.SelectedValue), Convert.ToInt32(cbMarca.SelectedValue));
                        MessageBox.Show("Producto agregado correctamente");
                        limpiarControles();
                        //listarProductos();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error agregarProducto(): " + e.Message);
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para agregar productos nuevos");
            }

        }
        private void modificarProducto()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtAmecop.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtPMayoreo.Text) || string.IsNullOrEmpty(txtPCosto.Text)
                    || string.IsNullOrEmpty(txtPLab.Text) || string.IsNullOrEmpty(txtStock.Text) || string.IsNullOrEmpty(txtPrecioDistribuidor.Text))
                {
                    MessageBox.Show("No pueden haber campos vacíos");
                }
                else
                {
                    var obj = new dProductos();
                    obj.modificarProducto(Convert.ToInt32(idProducto.Text), txtCodigo.Text, txtAmecop.Text, txtDescripcion.Text, Convert.ToInt32(txtStock.Text), 
                        Convert.ToDecimal(txtPCosto.Text), Convert.ToDecimal(txtPLab.Text), Convert.ToDecimal(txtPrecioDistribuidor.Text), Convert.ToDecimal(txtPMayoreo.Text), 
                        Convert.ToDecimal(txtPLista.Text), Convert.ToInt32(chIva.Checked), Convert.ToInt32(cbClasificacion.SelectedValue), Convert.ToInt32(cbMarca.SelectedValue));
                    MessageBox.Show("Producto modificado correctamente");
                    limpiarControles();
                    //listarProductos();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error agregarProducto(): " + e.Message);
            }
        }
        private void eliminarProducto()
        {
            if (dComun.permisosEmpleado.Contains("b"))
            {
                try
                {
                    if (MessageBox.Show("¿Desea eliminar el producto?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var obj = new dProductos();
                        obj.eliminarProducto(Convert.ToInt32(dgvProductos.CurrentRow.Cells[0].Value));
                        MessageBox.Show("Producto eliminado correctamente");
                        //listarProductos();
                        limpiarControles();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error eliminarProducto(): " + e.Message);
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para eliminar productos");
            }

        }
        private void calcularPrecios()
        {
            try
            {
                decimal precioMayoreo = Convert.ToDecimal(txtPMayoreo.Text);
                // Calcular los demás precios
                decimal precioLab = precioMayoreo * .70m;
                decimal precioDistribuidor = precioMayoreo * 0.80m;
                decimal precioLista = precioMayoreo / 0.60m;

                txtPLab.Text = Math.Round(precioLab, 2).ToString();
                txtPrecioDistribuidor.Text = Math.Round(precioDistribuidor, 2).ToString();
                txtPLista.Text = Math.Round(precioLista, 2).ToString();
            }
            catch (Exception e)
            {

                MessageBox.Show("Error calcularPrecio(): " + e.Message);
            }
        }
        private void abrirLotes()
        {
            try
            {
                var obj = new entradaProductos();
                obj.idProducto = Convert.ToInt32(0);
                obj.lbDescripcionProducto.Text = "SELECCIONE UN PRODUCTO";
                obj.ShowDialog();
                //listarProductos();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error abrirLotes(): " + e.Message);
            }
        }

        private void productos_Load(object sender, EventArgs e)
        {
            listarClasificaciones();
            listarMarcas();
            //listarProductos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (label2.Text == "Agregar")
            {
                agregarProducto();
            }
            else
            {
                modificarProducto();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarControles();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminarProducto();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.Delete)
            {
                if (dgvProductos.Focused)
                {
                    eliminarProducto();
                    return true;
                }
                else return false;
            }
            else if (keyData == Keys.Enter)
            {
                if (txtPMayoreo.Focused)
                {
                    calcularPrecios();

                }
                else if (label2.Text == "Agregar")
                {
                    agregarProducto();
                }
                else
                {
                    modificarProducto();
                }
                return true;
            }
            else if (keyData == Keys.F1)
            {

                abrirLotes();
                return true;

            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void dgvProductos_DoubleClick(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("d"))
            {
                label2.Text = "Modificar";
                idProducto.Text = dgvProductos.CurrentRow.Cells[0].Value.ToString();
                txtCodigo.Text = dgvProductos.CurrentRow.Cells[1].Value.ToString();
                txtAmecop.Text = dgvProductos.CurrentRow.Cells[2].Value.ToString();
                txtDescripcion.Text = dgvProductos.CurrentRow.Cells[3].Value.ToString();
                txtStock.Text = dgvProductos.CurrentRow.Cells[4].Value.ToString();
                txtPCosto.Text = dgvProductos.CurrentRow.Cells[5].Value.ToString();
                txtPLab.Text = dgvProductos.CurrentRow.Cells[6].Value.ToString();
                txtPrecioDistribuidor.Text = dgvProductos.CurrentRow.Cells[7].Value.ToString();
                txtPMayoreo.Text = dgvProductos.CurrentRow.Cells[8].Value.ToString();
                txtPLista.Text = dgvProductos.CurrentRow.Cells[9].Value.ToString();
                chIva.Checked = Convert.ToBoolean(dgvProductos.CurrentRow.Cells[10].Value);
                cbClasificacion.SelectedValue = dgvProductos.CurrentRow.Cells[11].Value;
                cbMarca.SelectedValue = dgvProductos.CurrentRow.Cells[13].Value;
            }
            else
            {
                MessageBox.Show("No tiene permisos para modificar información de productos");
            }

        }

        private void btnLotes_Click(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("c"))
            {
                abrirLotes();
                
            }
            else
            {
                MessageBox.Show("No tiene permisos para generar entradas de producto");
            }

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.TextLength >= 3)
            {
                buscarProducto(txtBuscar.Text);
            }
            else
            {
                dgvProductos.DataSource = null;
            }
        }
        private void buscarProducto(string texto)
        {
            try
            {
                var obj = new dProductos();
                var productos = obj.listarProductos(texto);
                dgvProductos.SuspendLayout();
                dgvProductos.DataSource = productos;
                dgvProductos.ResumeLayout();
                dgvProductos.Columns["idProducto"].Visible = false;
                dgvProductos.Columns["idClasificacion"].Visible = false;
                dgvProductos.Columns["idMarca"].Visible = false;
                dgvProductos.Columns["iva"].Visible = false;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarProductos(): " + e.Message);
            }
        }
    }
}
