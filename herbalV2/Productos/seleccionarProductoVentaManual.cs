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

namespace herbalV2.Productos
{
    public partial class seleccionarProductoVentaManual : Form
    {
        public event EventHandler<ProductoSeleccionadoManual> productoSeleccionadoManual;
        DataTable tablaSeleccionado = new DataTable();
        public seleccionarProductoVentaManual()
        {
            InitializeComponent();
        }
        //private void listarProductos()
        //{
        //    try
        //    {
        //        var obj = new dProductos();
        //        dgvProductos.DataSource = obj.listarProductos();
        //        dgvProductos.Columns["idProducto"].Visible = false;
        //        dgvProductos.Columns["amecop"].Visible = false;
        //        dgvProductos.Columns["precioCosto"].Visible = false;
        //        dgvProductos.Columns["precioLab"].Visible = false;
        //        dgvProductos.Columns["precioDistribuidor"].Visible = false;
        //        dgvProductos.Columns["precioLista"].Visible = false;
        //        dgvProductos.Columns["precioMayoreo"].Visible = false;
        //        dgvProductos.Columns["iva"].Visible = false;
        //        dgvProductos.Columns["clasificacion"].Visible = false;
        //        dgvProductos.Columns["idClasificacion"].Visible = false;
        //        dgvProductos.Columns["idMarca"].Visible = false;
        //        dgvProductos.Columns["marca"].Visible = false;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Error mostrarTodos(): " + e.Message);
        //    }
        //}

        private void buscarProducto(string texto)
        {
            try
            {
                var obj = new dProductos();
                var productos = obj.seleccionarProducto(texto);
                dgvProductos.SuspendLayout();
                dgvProductos.DataSource = productos;
                dgvProductos.ResumeLayout();
                dgvProductos.Columns["idProducto"].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarProductos(): " + e.Message);
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

        private void seleccionarProductoVentaManual_Load(object sender, EventArgs e)
        {
            //listarProductos();
            txtBuscar.Focus();

            tablaSeleccionado.Columns.Add("codigo");
            tablaSeleccionado.Columns.Add("producto");
            tablaSeleccionado.Columns.Add("caducidad");
            tablaSeleccionado.Columns.Add("idLote");
            tablaSeleccionado.Columns.Add("lote");
            tablaSeleccionado.Columns.Add("cantidad");
            tablaSeleccionado.Columns.Add("precioMayoreo");
        }
        private void listarLote(int idProducto)
        {
            try
            {
                var obj = new dLotes();
                dgvLotes.DataSource = obj.listarLotes(idProducto);
                dgvLotes.Columns["idLote"].Visible = false;
                dgvLotes.Columns["descripcion"].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarLote(): " + e.Message);
            }
        }

        private void dgvProductos_Click(object sender, EventArgs e)
        {
            try
            {
                listarLote(Convert.ToInt32(dgvProductos.CurrentRow.Cells[0].Value));
            }
            catch 
            {
            }
            
        }
        private void seleccionarLote()
        {
            try
            {
                if (Convert.ToInt32(dgvLotes.CurrentRow.Cells[5].Value) >= cantidad.Value)
                {
                    DataRow row = tablaSeleccionado.NewRow();
                    row["codigo"] = dgvProductos.CurrentRow.Cells[1].Value;
                    row["producto"] = dgvProductos.CurrentRow.Cells[2].Value;
                    row["caducidad"] = dgvLotes.CurrentRow.Cells[4].Value;
                    row["idLote"] = dgvLotes.CurrentRow.Cells[0].Value;
                    row["lote"] = dgvLotes.CurrentRow.Cells[2].Value;
                    row["cantidad"] = cantidad.Value;
                    row["precioMayoreo"] = dgvProductos.CurrentRow.Cells[3].Value;

                    tablaSeleccionado.Rows.Add(row);

                    dgvSeleccionado.DataSource = tablaSeleccionado;
                    dgvSeleccionado.Columns["idLote"].Visible = false;
                }
                else
                {
                    MessageBox.Show("No hay suficiente stock");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error seleccionarLote(): " + e.Message);
            }
        }

        private void eliminarProducto()
        {
            try
            {
                if (dgvSeleccionado.RowCount != 0)
                {
                    if (MessageBox.Show("¿Desea eliminar el producto de la venta?", "Venta", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        tablaSeleccionado.Rows[dgvSeleccionado.CurrentRow.Index].Delete();
                        dgvSeleccionado.DataSource = null;
                        dgvSeleccionado.DataSource = tablaSeleccionado;
                    }
                }
                else MessageBox.Show("No hay datos para eliminar en la venta");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error eliminarProducto(): " + ex.Message);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.Escape)
            {
                this.Dispose();
                return true;
            }
            else if (keyData == Keys.Delete)
            {
                eliminarProducto();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                if (txtBuscar.Focused)
                {
                    dgvProductos.Focus();
                }
                else if (dgvProductos.Focused)
                {
                    try
                    {
                        listarLote(Convert.ToInt32(dgvProductos.CurrentRow.Cells[0].Value));
                    }
                    catch
                    {
                    }
                    dgvLotes.Focus();
                }
                else if (dgvLotes.Focused)
                {
                    cantidad.Focus();
                }
                else if (cantidad.Focused)
                {
                    seleccionarLote();
                }
                return true;
            }
            else if (keyData == Keys.F1)
            {
                enviarTabla();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        private void enviarTabla()
        {
            try
            {
                if(MessageBox.Show("¿Desea confirmar los datos seleccionados?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    productoSeleccionadoManual?.Invoke(this, new ProductoSeleccionadoManual(tablaSeleccionado));
                    this.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error enviarTabla(): " + e.Message);
            }
        }

        private void dgvLotes_DoubleClick(object sender, EventArgs e)
        {
            seleccionarLote();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            enviarTabla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminarProducto();
        }
    }
    public class ProductoSeleccionadoManual : EventArgs
    {
        public DataTable ProductosSeleccionados { get; set; }

        public ProductoSeleccionadoManual(DataTable productosSeleccionados)
        {
            ProductosSeleccionados = productosSeleccionados;
        }
    }
}
