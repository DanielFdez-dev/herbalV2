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

namespace herbalV2.Ventas
{
    public partial class ventas : Form
    {
        private int idProductoVenta;
        DataTable tablaVenta = new DataTable();
        public ventas()
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

            tablaVenta.Clear();

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

            txtCotización.Text = string.Empty;
            lbIdCotizacion.Text = "0";
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
        private void eliminarProducto()
        {
            try
            {
                if (dgvVenta.RowCount != 0)
                {
                    if (MessageBox.Show("¿Desea eliminar el producto de la venta?", "Venta", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        tablaVenta.Rows[dgvVenta.CurrentRow.Index].Delete();
                        dgvVenta.DataSource = null;
                        dgvVenta.DataSource = tablaVenta;
                        dgvVenta.Columns["idLote"].Visible = false;
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
                MessageBox.Show("Error abrirVentanaClientes(): " + e.Message);
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
                MessageBox.Show("Error abrirVentanaProductos(): " + e.Message);
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
            buscarCliente(e.IdClienteSeleccionado);
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

        private void ventas_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            listarTipoPrecio();
            calcularFechaPago(Convert.ToInt32(string.IsNullOrEmpty(txtPlazoPago.Text) ? "0" : txtPlazoPago.Text));
            tablaVenta.Columns.Add("idLote");
            tablaVenta.Columns.Add("Codigo");
            tablaVenta.Columns.Add("Lote");
            tablaVenta.Columns.Add("Producto");
            tablaVenta.Columns.Add("Cantidad");
            tablaVenta.Columns.Add("Precio");
            tablaVenta.Columns.Add("Importe");
            tablaVenta.Columns.Add("TP");
            tablaVenta.Columns.Add("idTipoPrecio");
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
                        int cantidad = Convert.ToInt32(txtCantidad.Text);
                        calcularLotes(idProductoVenta, cantidad);

                        txtCodigo.Text = string.Empty;
                        txtProducto.Text = string.Empty;
                        txtCantidad.Text = string.Empty;
                        txtPrecio.Text = string.Empty;
                        lbStockProducto.Text = "-";
                    }
                }
                else if (txtCotización.Focused)
                {
                    cargarCotización();
                }
                return true;
            }
            else if (keyData == Keys.F1)
            {
                if (txtIdVendedor.Focused)
                {
                    abrirVentanaVendedores();
                }
                else if (txtIdCliente.Focused)
                {
                    abrirVentanaClientes();
                }
                else if(txtCodigo.Focused)
                {
                    abrirVentanaProductos();
                }
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

            return Math.Round(precio, 2);
        }
        private void calcularLotes(int idProducto, int cantidad)
        {
            int stockTemp = 0;
            int result = 0;
            var obj = new dProductos();
            DataTable tablaLotes = obj.buscarProducto(idProducto);
            DataTable tablaTemp = new DataTable();

            tablaTemp.Columns.Add("idLote");
            tablaTemp.Columns.Add("lote");
            tablaTemp.Columns.Add("cantidad");
            tablaTemp.Columns.Add("precioMayoreo");


            try
            {
                for (int i = 0; i <= tablaLotes.Rows.Count; i++)
                {
                    stockTemp = Convert.ToInt32(tablaLotes.Rows[i]["Stock"]);
                    if (cantidad <= stockTemp)
                    {
                        DataRow row = tablaTemp.NewRow();
                        row["idLote"] = tablaLotes.Rows[i]["idLote"];
                        row["lote"] = tablaLotes.Rows[i]["lote"];
                        row["cantidad"] = cantidad;
                        row["precioMayoreo"] = tablaLotes.Rows[i]["precioMayoreo"];
                        tablaTemp.Rows.Add(row);
                        break;
                    }
                    else
                    {
                        cantidad = cantidad - stockTemp;
                        result = result + stockTemp;
                        DataRow row = tablaTemp.NewRow();
                        row["idLote"] = tablaLotes.Rows[i]["idLote"];
                        row["lote"] = tablaLotes.Rows[i]["lote"];
                        row["cantidad"] = stockTemp;
                        row["precioMayoreo"] = tablaLotes.Rows[i]["precioMayoreo"];
                        tablaTemp.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error calculo de lote" + e.Message);
            }
            try
            {
                decimal precio;
                for (int i = 0; i < tablaTemp.Rows.Count; i++)
                {
                    precio = obtenerPrecioDescuento(Convert.ToDecimal(tablaTemp.Rows[i]["precioMayoreo"]));
                    DataRow row = tablaVenta.NewRow();
                    row["idLote"] = tablaTemp.Rows[i]["idLote"];
                    row["Lote"] = tablaTemp.Rows[i]["lote"];
                    row["Codigo"] = txtCodigo.Text;
                    row["Producto"] = txtProducto.Text;
                    row["Cantidad"] = tablaTemp.Rows[i]["cantidad"];
                    row["Precio"] = Math.Round(precio, 2);
                    row["Importe"] = Math.Round(precio * Convert.ToDecimal(tablaTemp.Rows[i]["cantidad"]), 2);
                    row["TP"] = cbTipoPrecio.Text;
                    row["idTipoPrecio"] = cbTipoPrecio.SelectedValue;
                    tablaVenta.Rows.Add(row);
                }

                dgvVenta.DataSource = tablaVenta;
                dgvVenta.Columns["idLote"].Visible = false;
                dgvVenta.Columns["idTipoPrecio"].Visible = false;
                calcularSubtotal();
                calcularTotal(Convert.ToDecimal(txtDescuentoAd.Text));
                conteoPiezas();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error calculo de lote 2" + e.Message);
            }
        }
        private void calcularLotesCotizacion(string codigoProducto, string descripcion, int idProducto, int cantidad, decimal precioUnitario, int idTipoPrecio)
        {
            int stockTemp = 0;
            int result = 0;
            var obj = new dProductos();
            DataTable tablaLotes = obj.buscarProducto(idProducto);
            DataTable tablaTemp = new DataTable();

            tablaTemp.Columns.Add("idLote");
            tablaTemp.Columns.Add("lote");
            tablaTemp.Columns.Add("cantidad");
            tablaTemp.Columns.Add("precioMayoreo");


            try
            {
                for (int i = 0; i <= tablaLotes.Rows.Count; i++)
                {
                    stockTemp = Convert.ToInt32(tablaLotes.Rows[i]["Stock"]);
                    if (cantidad <= stockTemp)
                    {
                        DataRow row = tablaTemp.NewRow();
                        row["idLote"] = tablaLotes.Rows[i]["idLote"];
                        row["lote"] = tablaLotes.Rows[i]["lote"];
                        row["cantidad"] = cantidad;
                        row["precioMayoreo"] = precioUnitario;
                        tablaTemp.Rows.Add(row);
                        break;
                    }
                    else
                    {
                        cantidad = cantidad - stockTemp;
                        result = result + stockTemp;
                        DataRow row = tablaTemp.NewRow();
                        row["idLote"] = tablaLotes.Rows[i]["idLote"];
                        row["lote"] = tablaLotes.Rows[i]["lote"];
                        row["cantidad"] = stockTemp;
                        row["precioMayoreo"] = precioUnitario;
                        tablaTemp.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error calculo de lote" + e.Message);
            }
            try
            {
                for (int i = 0; i < tablaTemp.Rows.Count; i++)
                {
                    cbTipoPrecio.SelectedValue = idTipoPrecio;
                    DataRow row = tablaVenta.NewRow();
                    row["idLote"] = tablaTemp.Rows[i]["idLote"];
                    row["Lote"] = tablaTemp.Rows[i]["lote"];
                    row["Codigo"] = codigoProducto;
                    row["Producto"] = descripcion;
                    row["Cantidad"] = tablaTemp.Rows[i]["cantidad"];
                    row["Precio"] = precioUnitario;
                    row["Importe"] = Math.Round(precioUnitario * Convert.ToDecimal(tablaTemp.Rows[i]["cantidad"]), 2);
                    row["TP"] = cbTipoPrecio.Text;
                    row["idTipoPrecio"] = cbTipoPrecio.SelectedValue;
                    tablaVenta.Rows.Add(row);
                }

                dgvVenta.DataSource = tablaVenta;
                dgvVenta.Columns["idLote"].Visible = false;
                dgvVenta.Columns["idTipoPrecio"].Visible = false;
                calcularSubtotal();
                calcularTotal(Convert.ToDecimal(txtDescuentoAd.Text));
                conteoPiezas();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error calculo de lote 2" + e.Message);
            }
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
        private void cargarCotización()
        {
            try
            {
                string mensaje = "";
                var obj = new dCotizacion();
                DataTable tabla = obj.buscarCotización(Convert.ToInt32(txtCotización.Text), out mensaje);

                if (mensaje == "EXPIRADA")
                {
                    MessageBox.Show("La cotización expiró");
                }
                else if (mensaje != "")
                {
                    MessageBox.Show(mensaje);
                }
                else
                {
                    buscarCliente(Convert.ToInt32(tabla.Rows[0]["idCliente"]));
                    buscarVendedor(Convert.ToInt32(tabla.Rows[0]["idVendedor"]));
                    txtIdCliente.Text = tabla.Rows[0]["idCliente"].ToString();
                    txtIdVendedor.Text = tabla.Rows[0]["idVendedor"].ToString();

                    lbIdCotizacion.Text = tabla.Rows[0]["idCotizacion"].ToString();
                    var objProductos = new dProductos();
                    foreach (DataRow row in tabla.Rows)
                    {
                        int cantidad = Convert.ToInt32(row["cantidad"]);
                        int stockAlmacen = objProductos.validarStock(Convert.ToInt32(row["idProducto"]));
                        if (stockAlmacen <= cantidad)//Valida si hay stock suficiente
                        {//Si no hay
                            if (MessageBox.Show("No hay stock suficiente para el producto: " + row["descripcion"].ToString() + "\n, ¿Desea continuar con lo existente?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                cantidad = stockAlmacen;
                            }
                            else
                            {
                                limpiarControles();
                                break;
                            }
                        }
                        txtDescuentoAd.Text = row["descuento"].ToString();

                        calcularLotesCotizacion(row["codigo"].ToString(), row["descripcion"].ToString(), Convert.ToInt32(row["idProducto"]), cantidad, Convert.ToDecimal(row["precioUnitario"])
                            , Convert.ToInt32(row["idTipoPrecio"]));
                    }
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Error cargarCotizacion(): " + e.Message);
            }
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea generar la venta?", "Venta", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    List<listaVentaDetalle> listaVenta = new List<listaVentaDetalle>();
                    foreach (DataGridViewRow row in dgvVenta.Rows)
                    {
                        var lista = new listaVentaDetalle()
                        {
                            cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value),
                            idLote = Convert.ToInt32(row.Cells["idLote"].Value),
                            idTipoPrecio = Convert.ToInt32(row.Cells["idTipoPrecio"].Value),
                            idVenta = 0,
                            precioIva = 0,
                            precioUnitario = Convert.ToDecimal(row.Cells["Precio"].Value),
                            total = Convert.ToDecimal(row.Cells["Importe"].Value)
                        };
                        listaVenta.Add(lista);
                    }
                    var obj = new dVentas();
                    int folio = obj.ultimoFolio();

                    string tipoVenta = (rbCredito.Checked) ? "CREDITO" : "CONTADO";

                    if (obj.agregarVenta(folio, Convert.ToDateTime(lbFechaPago.Text), dtFechaEntrega.Value, Convert.ToInt32(txtPlazoPago.Text), Convert.ToInt32(txtCantidadTotal.Text), Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtTotal.Text), 0,
                        Convert.ToInt32(txtDescuentoAd.Text), Convert.ToDecimal(txtPrecioDescuento.Text), Convert.ToInt32(lbIdUsuario.Text), Convert.ToInt32(txtIdVendedor.Text), Convert.ToInt32(txtIdCliente.Text), listaVenta, Convert.ToInt32(lbIdCotizacion.Text), tipoVenta))
                    {
                        var objNotaVenta = new vistaPreviaNotaVenta();
                        objNotaVenta.folioVenta = folio;
                        objNotaVenta.ShowDialog();

                        MessageBox.Show("Venta generada con éxito");
                        limpiarControles();
                        txtIdVendedor.Focus();
                        try
                        {
                            var objValidacion = new dLotes();
                            objValidacion.validarStocks();
                        }
                        catch (Exception ex2)
                        {
                            MessageBox.Show("Error al validar stocks" + ex2.Message);
                        }
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

        private void seleccionManual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            abrirVentanaProductosSeleccionManual();
        }

        private void abrirVentanaProductosSeleccionManual()
        {
            if (dComun.permisosEmpleado.Contains("w"))
            {
                try
                {
                    var obj = new Productos.seleccionarProductoVentaManual();
                    obj.productoSeleccionadoManual += seleccionarProductosManual_idProductoSeleccionado;
                    obj.ShowDialog();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error abrirVentanaProductosSeleccionManual(): " + e.Message);
                }
            }
            else
            {
                var obj = new logeoSeleccionManual();
                obj.accessoSeleccionManual += accesoSeleccionManual_acceso;
                obj.ShowDialog();
            }
        }
        private void accesoSeleccionManual_acceso(object sender, AccesoSeleccionManual e)
        {
            if (e.Acceso)
            {
                var obj = new Productos.seleccionarProductoVentaManual();
                obj.productoSeleccionadoManual += seleccionarProductosManual_idProductoSeleccionado;
                obj.ShowDialog();
            }
        }
        private void seleccionarProductosManual_idProductoSeleccionado(object sender, ProductoSeleccionadoManual e)
        {

            DataTable tab = e.ProductosSeleccionados;

            if (tab.Rows.Count > 0)
            {
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    decimal precioUnitario = obtenerPrecioDescuento(Convert.ToDecimal(tab.Rows[i]["precioMayoreo"]));
                    DataRow row = tablaVenta.NewRow();
                    row["idLote"] = tab.Rows[i]["idLote"];
                    row["Lote"] = tab.Rows[i]["lote"];
                    row["Codigo"] = tab.Rows[i]["codigo"];
                    row["Producto"] = tab.Rows[i]["producto"];
                    row["Cantidad"] = tab.Rows[i]["cantidad"];
                    row["Precio"] = precioUnitario;
                    row["Importe"] = row["Importe"] = Math.Round(precioUnitario * Convert.ToDecimal(tab.Rows[i]["cantidad"]), 2);
                    row["TP"] = cbTipoPrecio.Text;
                    row["idTipoPrecio"] = cbTipoPrecio.SelectedValue;
                    tablaVenta.Rows.Add(row);
                }
                dgvVenta.DataSource = tablaVenta;
                dgvVenta.Columns["idLote"].Visible = false;
                dgvVenta.Columns["idTipoPrecio"].Visible = false;
                calcularSubtotal();
                calcularTotal(Convert.ToDecimal(txtDescuentoAd.Text));
                conteoPiezas();
            }
        }
    }
}
