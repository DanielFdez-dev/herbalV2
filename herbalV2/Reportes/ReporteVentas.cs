using Datos;
using herbalV2.Clientes;
using herbalV2.Productos;
using herbalV2.Vendedores;
using herbalV2.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace herbalV2.Reportes
{
    public partial class ReporteVentas : Form
    {
        private int idCliente;
        private int idVendedor;
        private int idProducto;
        public ReporteVentas()
        {
            InitializeComponent();
        }
        private void limpiarControles()
        {
            lbNombre.Text = string.Empty;
            lbImporteTotal.Text = "0.0";
            lbPiezas.Text = "0";
        }
        private void procesarInformacion()
        {
            try
            {
                limpiarControles();
                DateTime fechaInicial = DateTime.Now, fechaFinal = DateTime.Now;
                dgvReporte.DataSource = null;
                if (rbDia.Checked)
                {
                    fechaInicial = new DateTime(fecha1.Value.Year, fecha1.Value.Month, fecha1.Value.Day, 0, 0, 0);
                    fechaFinal = new DateTime(fecha1.Value.Year, fecha1.Value.Month, fecha1.Value.Day, 23, 59, 59);
                }
                else if (rdMes.Checked)
                {
                    fechaInicial = new DateTime(fecha1.Value.Year, fecha1.Value.Month, 1);

                    DateTime primerDiaDelMesSiguiente = new DateTime(fecha1.Value.Year, fecha1.Value.Month, 1).AddMonths(1);

                    fechaFinal = primerDiaDelMesSiguiente.AddDays(-1);
                }
                else if (rbAño.Checked)
                {
                    fechaInicial = new DateTime(fecha1.Value.Year, 1, 1);
                    fechaFinal = new DateTime(fecha1.Value.Year, 12, 31);
                }
                else if (rbFechaEspecifica.Checked)
                {
                    fechaInicial = new DateTime(fecha1.Value.Year, fecha1.Value.Month, fecha1.Value.Day, 0, 0, 0);
                    fechaFinal = new DateTime(fecha2.Value.Year, fecha2.Value.Month, fecha2.Value.Day, 23, 59, 59);
                }
                var obj = new dReportes();
                if (cbTipoReporte.SelectedIndex == 0)
                {
                    dgvReporte.DataSource = obj.ventasGlobalesPorFolio(fechaInicial, fechaFinal);
                }
                else if (cbTipoReporte.SelectedIndex == 1)
                {
                    dgvReporte.DataSource = obj.ventasGlobalesPorCliente(fechaInicial, fechaFinal);
                }
                else if (cbTipoReporte.SelectedIndex == 2)
                {
                    dgvReporte.DataSource = obj.ventasPorClienteEspecifico(fechaInicial, fechaFinal, idCliente);
                }
                else if (cbTipoReporte.SelectedIndex == 3)
                {
                    dgvReporte.DataSource = obj.ventasGlobalesPorVendedor(fechaInicial, fechaFinal);
                }
                else if (cbTipoReporte.SelectedIndex == 4)
                {
                    dgvReporte.DataSource = obj.ventasPorVendedorEspecifico(fechaInicial, fechaFinal, idVendedor);
                }
                else if (cbTipoReporte.SelectedIndex == 5)
                {
                    dgvReporte.DataSource = obj.ventasGlobalesPorProducto(fechaInicial, fechaFinal);
                    calcularCantidades();
                }
                else if (cbTipoReporte.SelectedIndex == 6)
                {
                    dgvReporte.DataSource = obj.ventasPorProductoEspecifico(fechaInicial, fechaFinal, idProducto);
                    calcularCantidades();
                }
                calcularTotales();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error procesarInformacion(): " + ex.Message);
            }
        }
        private void calcularTotales()
        {
            try
            {
                decimal suma = 0;

                foreach (DataGridViewRow fila in dgvReporte.Rows)
                {
                    if (fila.Cells["total"].Value != null) // Asegurarse de que no sea nulo
                    {
                        // Tratar de convertir el valor a decimal y sumarlo
                        suma += Convert.ToDecimal(fila.Cells["Total"].Value);
                    }
                }
                lbImporteTotal.Text = suma.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error calcularTotales(): " + ex.Message);
            }
        }
        private void calcularCantidades()
        {
            try
            {
                decimal suma = 0;

                foreach (DataGridViewRow fila in dgvReporte.Rows)
                {
                    if (fila.Cells["cantidad"].Value != null) // Asegurarse de que no sea nulo
                    {
                        // Tratar de convertir el valor a decimal y sumarlo
                        suma += Convert.ToDecimal(fila.Cells["cantidad"].Value);
                    }
                }
                lbPiezas.Text = suma.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error calcularTotales(): " + ex.Message);
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
        private void abrirVentanaProductos()
        {
            try
            {
                var obj = new Productos.seleccionarProductoVenta();
                obj.productoSeleccionadoVenta += seleccionarProductoVenta_idProductoSeleccionado;
                obj.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error abrirVentanaVendedores(): " + e.Message);
            }
        }
        private void seleccionarCliente_idClienteSeleccionado(object sender, ClienteSeleccionado e)
        {
            idCliente = e.IdClienteSeleccionado;
            lbNombre.Text = "Cliente: " + e.Nombre;
        }
        private void seleccionarVendedor_idVendedorSeleccionado(object sender, VendedorSeleccionado e)
        {
            idVendedor = e.IdVendedorSeleccionado;
            lbNombre.Text = "Vendedor: " + e.Nombre;
        }
        private void seleccionarProductoVenta_idProductoSeleccionado(object sender, ProductoSeleccionadoVenta e)
        {
            idProducto = e.IdProducto;
            lbNombre.Text = "Producto: " + e.Descripcion;
        }
        private void rbDia_CheckedChanged(object sender, EventArgs e)
        {
            fecha1.Visible = true;
            fecha2.Visible = false;
            label2.Text = "Fecha:";
            label3.Visible = false;

            fecha1.Format = DateTimePickerFormat.Custom;
            fecha1.CustomFormat = "dd/MM/yyyy";
        }

        private void rdMes_CheckedChanged(object sender, EventArgs e)
        {
            fecha1.Visible = true;
            fecha2.Visible = false;
            label2.Text = "Fecha:";
            label3.Visible = false;

            fecha1.Format = DateTimePickerFormat.Custom;
            fecha1.CustomFormat = "MM/yyyy";
        }

        private void rbAño_CheckedChanged(object sender, EventArgs e)
        {
            fecha1.Visible = true;
            fecha2.Visible = false;
            label2.Text = "Fecha:";
            label3.Visible = false;

            fecha1.Format = DateTimePickerFormat.Custom;
            fecha1.CustomFormat = "yyyy";
        }

        private void rbFechaEspecifica_CheckedChanged(object sender, EventArgs e)
        {
            fecha1.Visible = true;
            fecha2.Visible = true;
            label3.Visible = true;
            label2.Text = "Fecha Inicial:";

            fecha1.Format = DateTimePickerFormat.Custom;
            fecha1.CustomFormat = "dd/MM/yyyy";
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            procesarInformacion();
        }

        private void cbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbTipoReporte.SelectedIndex == 2)
            {
                abrirVentanaClientes();
            }
            else if(cbTipoReporte.SelectedIndex == 4)
            {
                abrirVentanaVendedores();
            }
            else if (cbTipoReporte.SelectedIndex == 6)
            {
                abrirVentanaProductos();
            }
        }

        private void ReporteVentas_Load(object sender, EventArgs e)
        {
            cbTipoReporte.SelectedIndex = 0;
        }
    }
}
