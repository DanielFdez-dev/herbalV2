using Datos;
using herbalV2.Clientes;
using herbalV2.Vendedores;
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
    public partial class reporteCuentasCobrar : Form
    {
        private int idCliente;
        private int idVendedor;
        private string columnaSumada;
        public reporteCuentasCobrar()
        {
            InitializeComponent();
        }
        private void ocultarRadioButtons()
        {
            rbDia.Visible = false;
            rdMes.Visible = false;
            rbAño.Visible = false;
            rbFechaEspecifica.Visible = false;

            label2.Visible = false;
            label3.Visible = false;
            fecha1.Visible = false;
            fecha2.Visible = false;
        }
        private void mostrarRadioButtons()
        {
            rbDia.Visible = true;
            rdMes.Visible = true;
            rbAño.Visible = true;
            rbFechaEspecifica.Visible = true;


        }
        private void procesarReporte()
        {
            int indexSeleccionado = cbTipoReporte.SelectedIndex;
            dgvReporte.DataSource = null;
            try
            {
                lbImporteTotal.Text = "0.0";
                DateTime fechaInicial = DateTime.Now, fechaFinal = DateTime.Now;
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

                if (indexSeleccionado == 0)
                {
                    dgvReporte.DataSource = obj.cuentasPorCobrar();
                    columnaSumada = "adeudos";
                    calcularTotales();
                }
                else if (indexSeleccionado == 1)
                {
                    dgvReporte.DataSource = obj.adeudosGlobalesPorCliente();
                    columnaSumada = "adeudos";
                    calcularTotales();
                }
                else if (indexSeleccionado == 2)
                {
                    dgvReporte.DataSource = obj.movimientosPorClienteEspecifico(fechaInicial, fechaFinal, idCliente);
                    lbImporteTotal.Text = "";
                }
                else if (indexSeleccionado == 3)
                {
                    dgvReporte.DataSource = obj.adeudosGlobalesPorVendedor();
                    columnaSumada = "adeudos";
                    calcularTotales();
                }
                else if (indexSeleccionado == 4)
                {
                    dgvReporte.DataSource = obj.adeudosPorVendedorEspecifico(idVendedor);
                    columnaSumada = "adeudos";
                    calcularTotales();
                }
                else if (indexSeleccionado == 5)
                {
                    dgvReporte.DataSource = obj.abonosRecibidos(fechaInicial, fechaFinal);
                    columnaSumada = "abono";
                    calcularTotales();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error procesarReporte(): " + ex.Message);
            }
        }

        private void reporteCuentasCobrar_Load(object sender, EventArgs e)
        {
            cbTipoReporte.SelectedIndex = 0;
        }

        private void cbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipoReporte.SelectedIndex == 2)
            {
                abrirVentanaClientes();
                mostrarRadioButtons();
            }
            else if (cbTipoReporte.SelectedIndex == 5)
            {
                mostrarRadioButtons();
            }
            else if (cbTipoReporte.SelectedIndex == 4)
            {
                abrirVentanaVendedores();
                ocultarRadioButtons();
            }
            else{
                
                ocultarRadioButtons();
            }
        }

        private void rbDia_CheckedChanged(object sender, EventArgs e)
        {
            fecha1.Visible = true;
            fecha2.Visible = false;
            label2.Text = "Fecha:";
            label3.Visible = false;
            label2.Visible = true;
            fecha1.Format = DateTimePickerFormat.Custom;
            fecha1.CustomFormat = "dd/MM/yyyy";
        }

        private void rdMes_CheckedChanged(object sender, EventArgs e)
        {
            fecha1.Visible = true;
            fecha2.Visible = false;
            label2.Text = "Fecha:";
            label3.Visible = false;
            label2.Visible = true;

            fecha1.Format = DateTimePickerFormat.Custom;
            fecha1.CustomFormat = "MM/yyyy";
        }

        private void rbAño_CheckedChanged(object sender, EventArgs e)
        {
            fecha1.Visible = true;
            fecha2.Visible = false;
            label2.Text = "Fecha:";
            label3.Visible = false;
            label2.Visible = true;

            fecha1.Format = DateTimePickerFormat.Custom;
            fecha1.CustomFormat = "yyyy";
        }

        private void rbFechaEspecifica_CheckedChanged(object sender, EventArgs e)
        {
            fecha1.Visible = true;
            fecha2.Visible = true;
            label3.Visible = true;
            label2.Text = "Fecha Inicial:";
            label2.Visible = true;

            fecha1.Format = DateTimePickerFormat.Custom;
            fecha1.CustomFormat = "dd/MM/yyyy";
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
        private void calcularTotales()
        {
            try
            {
                decimal suma = 0;

                foreach (DataGridViewRow fila in dgvReporte.Rows)
                {
                    if (fila.Cells[columnaSumada].Value != null) // Asegurarse de que no sea nulo
                    {
                        // Tratar de convertir el valor a decimal y sumarlo
                        suma += Convert.ToDecimal(fila.Cells[columnaSumada].Value);
                    }
                }
                lbImporteTotal.Text = suma.ToString();
            }
            catch (Exception ex)
            {

                lbImporteTotal.Text = "0.0";
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            procesarReporte();
        }
    }
}
