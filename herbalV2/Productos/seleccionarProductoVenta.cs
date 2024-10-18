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
    public partial class seleccionarProductoVenta : Form
    {
        public event EventHandler<ProductoSeleccionadoVenta> productoSeleccionadoVenta;
        public seleccionarProductoVenta()
        {
            InitializeComponent();
        }
        private void seleccionarProducto()
        {
            productoSeleccionadoVenta?.Invoke(this, new ProductoSeleccionadoVenta(Convert.ToInt32(dgvProductos.CurrentRow.Cells[0].Value), dgvProductos.CurrentRow.Cells[1].Value.ToString(), dgvProductos.CurrentRow.Cells[2].Value.ToString(),
                Convert.ToDecimal(dgvProductos.CurrentRow.Cells[3].Value), Convert.ToInt32(dgvProductos.CurrentRow.Cells[4].Value)));
            this.Dispose();
        }
        //private void listarProductos()
        //{
        //    try
        //    {
        //        var obj = new dProductos();
        //        dgvProductos.DataSource = obj.listarProductos();
        //        dgvProductos.Columns["idProducto"].Visible = false;
        //        dgvProductos.Columns["precioCosto"].Visible = false;
        //        dgvProductos.Columns["precioLab"].Visible = false;
        //        dgvProductos.Columns["precioDistribuidor"].Visible = false;
        //        dgvProductos.Columns["precioLista"].Visible = false;
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

        private void seleccionarProductoVenta_Load(object sender, EventArgs e)
        {
            //listarProductos();
            txtBuscar.Focus();
        }

        private void dgvProductos_DoubleClick(object sender, EventArgs e)
        {
            seleccionarProducto();
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
                    dgvProductos.Focus();
                }
                else if (dgvProductos.Focused)
                {
                    seleccionarProducto();
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
    public class ProductoSeleccionadoVenta : EventArgs
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public ProductoSeleccionadoVenta(int idProductoSeleccionado, string codigo, string descripcion, decimal precio, int stock)
        {
            IdProducto = idProductoSeleccionado;
            Codigo = codigo;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
        }
    }
}
