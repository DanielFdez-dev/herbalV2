using Datos;
using Datos.Listas;
using herbalV2.Clientes;
using herbalV2.Productos;
using herbalV2.Vendedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace herbalV2.Cotizacion
{
    public partial class cotizacion : Form
    {
        private int idProductoVenta;
        DataTable tablaCotizacion = new DataTable();
        public cotizacion()
        {
            InitializeComponent();
        }
        private void limpiarControles()
        {
            lbNombreCliente.Text = "-";
            lbNombreVendedor.Text = "-";
            txtPlazoPago.Text = "0";
            txtIdCliente.Text = string.Empty;
            txtIdVendedor.Text = string.Empty;
            cbTipoPrecio.SelectedIndex = 0;
            lbFechaPago.Text = "-";
            dgvVenta.DataSource = null;
            tablaCotizacion.Reset();

            txtCodigo.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            lbStockProducto.Text = "-";

            txtDescuentoAd.Text = "0";
            txtCantidadTotal.Text = "0";
            txtSubtotal.Text = "0.00";
            txtPrecioDescuento.Text = "0.00";
            txtTotal.Text = "0.00";
        }
        private void listarTipoPrecio()
        {
            try
            {
                var obj = new dTipoPrecio();
                cbTipoPrecio.DataSource = obj.listarTipoPrecio();
                cbTipoPrecio.DisplayMember = "descripcionCompleta";
                cbTipoPrecio.ValueMember = "idTipoPrecio";
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarTipoPrecio(): " + e.Message);
            }
        }
        private void buscarCliente(int idCliente)
        {
            try
            {
                var obj = new dClientes();
                string nombreCliente, plazoPago;
                int idTipoPrecio;
                obj.buscarCliente(idCliente, out nombreCliente, out idTipoPrecio, out plazoPago);
                lbNombreCliente.Text = nombreCliente;
                cbTipoPrecio.SelectedValue = idTipoPrecio;
                txtPlazoPago.Text = plazoPago;
                calcularFechaPago(Convert.ToInt32(string.IsNullOrEmpty(txtPlazoPago.Text) ? "0" : txtPlazoPago.Text));
            }
            catch (Exception e)
            {
                MessageBox.Show("Error buscarCliente(): " + e.Message);
            }
        }
        private void buscarVendedor(int idVendedor)
        {
            try
            {
                var obj = new dVendedores();
                string nombreVendedor;
                obj.buscarVendedor(idVendedor, out nombreVendedor);
                lbNombreVendedor.Text = nombreVendedor;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error buscarVendedor(): " + e.Message);
            }
        }

        private void calcularFechaPago(int plazoPago)
        {
            try
            {
                lbFechaPago.Text = DateTime.Now.AddDays(plazoPago).ToString("dd-MMM-yy");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error calcularFechaPago" + e.Message);
            }
        }
        private void abrirVentanaVendedores()
        {
            try
            {
                var obj = new Vendedores.seleccionarVendedor();
                obj.vendedorSeleccionado += seleccionarVendedor_idVendedorSeleccionado;
                obj.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error abrirVentanaVendedores(): " + e.Message);
            }
        }
        private void abrirVentanaClientes()
        {
            try
            {
                var obj = new Clientes.seleccionarCliente();
                obj.clienteSeleccionado += seleccionarCliente_idClienteSeleccionado;
                obj.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error abrirVentanaVendedores(): " + e.Message);
            }
        }
        private void abrirVentanaProductos()
        {
            try
            {
                var obj = new Productos.seleccionarProductoVenta();
                obj.productoSeleccionadoVenta += seleccionarProductoVenta_idProductoSeleccionado;
                obj.ShowDialog();
                txtCantidad.Focus();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error abrirVentanaVendedores(): " + e.Message);
            }
        }
        private void seleccionarProductoVenta_idProductoSeleccionado(object sender, ProductoSeleccionadoVenta e)
        {
            idProductoVenta = e.IdProducto;
            txtCodigo.Text = e.Codigo;
            txtProducto.Text = e.Descripcion;
            txtPrecio.Text = e.Precio.ToString();
            lbStockProducto.Text = e.Stock.ToString();
        }
        private void seleccionarCliente_idClienteSeleccionado(object sender, ClienteSeleccionado e)
        {
            txtIdCliente.Text = e.IdClienteSeleccionado.ToString();
            lbNombreCliente.Text = e.Nombre;
            txtPlazoPago.Text = e.PlazoPago;
            calcularFechaPago(Convert.ToInt32(string.IsNullOrEmpty(txtPlazoPago.Text) ? "0" : txtPlazoPago.Text));
        }
        private void seleccionarVendedor_idVendedorSeleccionado(object sender, VendedorSeleccionado e)
        {
            txtIdVendedor.Text = e.IdVendedorSeleccionado.ToString();
            lbNombreVendedor.Text = e.Nombre;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToString("dd-MMMM-yyyy/HH:mm:ss");
        }

        private void cotizacion_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            listarTipoPrecio();
            calcularFechaPago(Convert.ToInt32(string.IsNullOrEmpty(txtPlazoPago.Text) ? "0" : txtPlazoPago.Text));
            tablaCotizacion.Columns.Add("idProducto");
            tablaCotizacion.Columns.Add("Codigo");
            tablaCotizacion.Columns.Add("Producto");
            tablaCotizacion.Columns.Add("Cantidad");
            tablaCotizacion.Columns.Add("Precio");
            tablaCotizacion.Columns.Add("Importe");
            tablaCotizacion.Columns.Add("TP");
            tablaCotizacion.Columns.Add("idTipoPrecio");
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.Delete)
            {
                eliminarProducto();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                if (txtIdVendedor.Focused)
                {
                    buscarVendedor(Convert.ToInt32(string.IsNullOrEmpty(txtIdVendedor.Text) ? "0" : txtIdVendedor.Text));
                    txtIdCliente.Focus();
                }
                else if (txtIdCliente.Focused)
                {
                    buscarCliente(Convert.ToInt32(string.IsNullOrEmpty(txtIdCliente.Text) ? "0" : txtIdCliente.Text));
                    cbTipoPrecio.Focus();
                }
                else if (cbTipoPrecio.Focused)
                {
                    dtFechaEntrega.Focus();
                }
                else if (dtFechaEntrega.Focused)
                {
                    txtPlazoPago.Focus();
                }
                else if (txtPlazoPago.Focused)
                {
                    calcularFechaPago(Convert.ToInt32(string.IsNullOrEmpty(txtPlazoPago.Text) ? "0" : txtPlazoPago.Text));
                    txtCodigo.Focus();
                }
                else if (txtCodigo.Focused)
                {
                    txtCantidad.Focus();
                }
                else if (txtCantidad.Focused)
                {
                    if (lbStockProducto.Text == "-")
                    {
                        MessageBox.Show("Seleccione un producto");
                    }
                    else if (string.IsNullOrWhiteSpace(txtCantidad.Text))
                    {
                        MessageBox.Show("Ingresa una cantidad");
                    }
                    else if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(lbStockProducto.Text))
                    {
                        MessageBox.Show("Está excediendo la cantidad de venta con el stock existente");
                    }
                    else
                    {
                        registrarProducto();

                        txtCodigo.Text = string.Empty;
                        txtProducto.Text = string.Empty;
                        txtCantidad.Text = string.Empty;
                        txtPrecio.Text = string.Empty;
                        lbStockProducto.Text = "-";
                    }
                }
                return true;
            }
            else if (keyData == Keys.F1)
            {
                abrirVentanaVendedores();
                return true;
            }
            else if (keyData == Keys.F2)
            {
                abrirVentanaClientes();
                return true;
            }
            else if (keyData == Keys.F3)
            {
                abrirVentanaProductos();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        private decimal obtenerPrecioDescuento(decimal precioMayoreo)
        {
            string input = cbTipoPrecio.Text;
            string pattern = @"-(\d+)%";
            decimal descuento = 0, precio = 0;
            try
            {
                Match match = Regex.Match(input, pattern);
                if (match.Success)
                {
                    string numero = match.Groups[1].Value;
                    descuento = Convert.ToDecimal(numero) / 100;
                }
                precio = precioMayoreo - (precioMayoreo * descuento);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error obtenerPrecioDescuento()" + e.Message);
                precio = 0;
            }

            return precio;
        }
        private void calcularSubtotal()
        {
            decimal suma = 0m;

            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (row.Cells["Importe"].Value != null && decimal.TryParse(row.Cells["Importe"].Value.ToString(), out decimal importe))
                {
                    suma += importe;
                }
            }

            txtSubtotal.Text = $"{suma:N2}";
        }
        private void calcularTotal(decimal descuento)
        {
            try
            {
                if (txtSubtotal.Text != "0.00")
                {
                    descuento = descuento / 100;

                    decimal subtotal = Convert.ToDecimal(txtSubtotal.Text);
                    decimal precioDescuento = Math.Round((subtotal * descuento), 2);
                    decimal total = Math.Round((subtotal - precioDescuento), 2);
                    txtTotal.Text = total.ToString();
                    txtPrecioDescuento.Text = precioDescuento.ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error calcularTotal" + e.Message);
            }

        }
        private void conteoPiezas()
        {
            int suma = 0;

            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (row.Cells["Cantidad"].Value != null && int.TryParse(row.Cells["Cantidad"].Value.ToString(), out int cantidad))
                {
                    suma += cantidad;
                }
            }

            txtCantidadTotal.Text = suma.ToString();
        }

        private void eliminarProducto()
        {
            try
            {
                if (dgvVenta.RowCount != 0)
                {
                    if (MessageBox.Show("¿Desea eliminar el producto de la venta?", "Venta", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Obtener el índice de la fila actual
                        int rowIndex = dgvVenta.CurrentRow.Index;

                        // Eliminar la fila de la tabla de datos
                        tablaCotizacion.Rows[rowIndex].Delete();

                        // Aceptar los cambios en la tabla de datos
                        tablaCotizacion.AcceptChanges();

                        // Actualizar el DataGridView para reflejar los cambios
                        dgvVenta.DataSource = null;
                        dgvVenta.DataSource = tablaCotizacion;
                        dgvVenta.Columns["idProducto"].Visible = false;
                        dgvVenta.Columns["idTipoPrecio"].Visible = false;
                        calcularSubtotal();
                        calcularTotal(Convert.ToDecimal(txtDescuentoAd.Text));
                        conteoPiezas();
                    }
                }
                else MessageBox.Show("No hay datos para eliminar en la venta");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error eliminarProducto(): " + ex.Message);
            }
        }

        private void txtDescuentoAd_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescuentoAd.Text))
            {
                calcularTotal(Convert.ToDecimal(txtDescuentoAd.Text));
            }
            else
            {
                calcularTotal(0);
            }
        }

        private void btnCotizacion_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("¿Desea generar la cotización?", "Cotización", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    List<listaVentaDetalle> listaVenta = new List<listaVentaDetalle>();
                    foreach (DataGridViewRow row in dgvVenta.Rows)
                    {
                        var lista = new listaVentaDetalle()
                        {
                            idLote = Convert.ToInt32(row.Cells["idProducto"].Value),
                            cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value),
                            idTipoPrecio = Convert.ToInt32(row.Cells["idTipoPrecio"].Value),
                            idVenta = 0,
                            precioIva = 0,
                            precioUnitario = Convert.ToDecimal(row.Cells["Precio"].Value),
                            total = Convert.ToDecimal(row.Cells["Importe"].Value)
                        };
                        listaVenta.Add(lista);
                    }
                    var obj = new dCotizacion();
                    int folio = obj.ultimoFolioCotizacion();
                    if (obj.agregarCotizacion(folio, DateTime.Now, dtFechaEntrega.Value, Convert.ToInt32(txtPlazoPago.Text), Convert.ToInt32(txtCantidadTotal.Text), Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtTotal.Text), 0,
                        Convert.ToInt32(txtDescuentoAd.Text), Convert.ToDecimal(txtPrecioDescuento.Text), Convert.ToInt32(lbIdUsuario.Text), Convert.ToInt32(txtIdVendedor.Text), Convert.ToInt32(txtIdCliente.Text), listaVenta))
                    {
                        var objNota = new vistaPreviaNotaCotizacion();
                        objNota.folioCotizacion = folio;
                        objNota.ShowDialog();
                        MessageBox.Show("Cotización generada con éxito");
                        limpiarControles();
                        txtIdVendedor.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Evento GenerarVenta: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarControles();
        }

        private void registrarProducto()
        {
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            decimal precio = 0;
            try
            {
                precio = obtenerPrecioDescuento(Convert.ToDecimal(txtPrecio.Text));
                DataRow row = tablaCotizacion.NewRow();
                row["idProducto"] = idProductoVenta;
                row["Codigo"] = txtCodigo.Text;
                row["Producto"] = txtProducto.Text;
                row["Cantidad"] = txtCantidad.Text;
                row["Precio"] = Math.Round(precio, 2);
                row["Importe"] = Math.Round(precio * Convert.ToDecimal(txtCantidad.Text), 2);
                row["TP"] = cbTipoPrecio.Text;
                row["idTipoPrecio"] = cbTipoPrecio.SelectedValue;
                tablaCotizacion.Rows.Add(row);

                dgvVenta.DataSource = tablaCotizacion;
                dgvVenta.Columns["idProducto"].Visible = false;
                dgvVenta.Columns["idTipoPrecio"].Visible = false;
                calcularSubtotal();
                calcularTotal(Convert.ToDecimal(txtDescuentoAd.Text));
                conteoPiezas();
            }
            catch (Exception ex )
            {
                MessageBox.Show("Error RegistrarProducto Evento: " + ex.Message);
            }
        }
    }
}
