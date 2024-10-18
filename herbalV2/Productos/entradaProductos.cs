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
    public partial class entradaProductos : Form
    {
        public int idProducto { get; set; }
        public entradaProductos()
        {
            InitializeComponent();
        }
        private void listarLotes()
        {
            try
            {
                var obj = new dLotes();
                dgvLotes.DataSource = obj.listarLotes(idProducto);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarLotes(): " + e.Message);
            }
        }
        private void limpiarControles()
        {
            txtLote.Text = string.Empty;
            txtStock.Text = string.Empty;
        }
        private void agregarLote()
        {
            try
            {
                if (string.IsNullOrEmpty(txtLote.Text) || string.IsNullOrEmpty(txtStock.Text))
                {
                    MessageBox.Show("No puede haber campos vacios");
                }
                else
                {
                    if (MessageBox.Show("¿Desea ingresar un nuevo lote?", "Lote", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var obj= new dLotes();
                        obj.agregarLote(txtLote.Text, dtCaducidad.Value, Convert.ToInt32(txtStock.Text), idProducto);
                        MessageBox.Show("Lote ingresado correctamente");
                        listarLotes();
                        limpiarControles();
                        txtLote.Focus();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error agregarLote(): " + e.Message);
            }
        }

        private void seleccionarProducto()
        {
            try
            {
                var obj = new seleccionarOtroProducto();
                obj.productoSeleccionado += seleccionarOtroProducto_idProductoSeleccionado;
                obj.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error seleccionarProducto(): " + e.Message);
            }
        }
        private void eliminarLote()
        {
            try
            {
                if(MessageBox.Show("¿Seguro que desea eliminar el lote: " + dgvLotes.CurrentRow.Cells[2].Value.ToString(), "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var obj = new dLotes();
                    obj.eliminarLote(Convert.ToInt32(dgvLotes.CurrentRow.Cells[0].Value), idProducto);
                    limpiarControles();
                    listarLotes();
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Error eliminarLote(): " + e.Message);
            }
        }

        private void seleccionarOtroProducto_idProductoSeleccionado(object sender, ProductoSeleccionado e)
        {
            this.idProducto = e.IdProductoSeleccionado;
            lbDescripcionProducto.Text = e.Descripcion;
            listarLotes();
        }

        private void entradaProductos_Load(object sender, EventArgs e)
        {
            listarLotes();
            dtCaducidad.Format = DateTimePickerFormat.Custom;
            dtCaducidad.CustomFormat = "MMMM yyyy"; // Mostrar mes y año
            dtCaducidad.ShowUpDown = true; // Para mostrar el control como un selector de mes/año

        }

        private void linkSeleccionarProducto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            seleccionarProducto();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            agregarLote();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.Delete)
            {
                if (dgvLotes.Focused)
                {
                    eliminarLote();
                    return true;
                }
                else return false;
            }
            else if (keyData == Keys.Enter)
            {
                agregarLote();
                return true;
            }
            else if (keyData == Keys.F1)
            {

                seleccionarProducto();
                return true;

            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminarLote();
        }
    }
}
