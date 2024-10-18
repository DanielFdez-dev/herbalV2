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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace herbalV2.Productos
{
    public partial class seleccionarOtroProducto : Form
    {
        public event EventHandler<ProductoSeleccionado> productoSeleccionado;
        public seleccionarOtroProducto()
        {
            InitializeComponent();
        }

        private void seleccionarProducto()
        {
            productoSeleccionado?.Invoke(this, new ProductoSeleccionado(Convert.ToInt32(dgvProductos.CurrentRow.Cells[0].Value), dgvProductos.CurrentRow.Cells[2].Value.ToString()));
            this.Dispose();
        }

        //private void listarProductos()
        //{
        //    try
        //    {
        //        var obj = new dProductos();
        //        dgvProductos.DataSource = obj.listarProductos();
        //        dgvProductos.Columns["idProducto"].Visible = false;
        //        dgvProductos.Columns["stock"].Visible = false;
        //        dgvProductos.Columns["precioCosto"].Visible = false;
        //        dgvProductos.Columns["precioLab"].Visible = false;
        //        dgvProductos.Columns["precioDistribuidor"].Visible = false;
        //        dgvProductos.Columns["precioMayoreo"].Visible = false;
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
                dgvProductos.Columns["Precio"].Visible = false;
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

        private void seleccionarOtroProducto_Load(object sender, EventArgs e)
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

    public class ProductoSeleccionado : EventArgs
    {
        public int IdProductoSeleccionado { get; set; }
        public string Descripcion { get; set; }

        public ProductoSeleccionado(int idProductoSeleccionado, string descripcion)
        {
            IdProductoSeleccionado = idProductoSeleccionado;
            Descripcion = descripcion;
        }
    }
}
