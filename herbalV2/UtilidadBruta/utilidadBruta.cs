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

namespace herbalV2.UtilidadBruta
{
    public partial class utilidadBruta : Form
    {
        private int folioVenta = 0;
        public utilidadBruta()
        {
            InitializeComponent();
        }

        private void utilidadBruta_Load(object sender, EventArgs e)
        {

        }
        private void cargarDatos()
        {
            if (!string.IsNullOrEmpty(txtFolio.Text))
            {
                try
                {
                    folioVenta = Convert.ToInt32(txtFolio.Text);
                    var obj = new dUtilidadBruta();
                    var objCliente = new dClientes();
                    var listaDetalle = obj.utilidadDetalle(folioVenta);
                    decimal totalCosto = 0;
                    if (listaDetalle.Count > 0)
                    {
                        foreach (var item in listaDetalle)
                        {
                            totalCosto += item.importe;
                        }
                        var listaGeneral = obj.utilidadGeneral(folioVenta, totalCosto);

                        foreach (var item in listaGeneral)
                        {
                            item.totalUtilidad = item.totalVenta - item.precioFlete - item.precioComision - item.totalVentaCosto;

                            lbTotalVenta.Text = item.totalVenta.ToString();
                            lbTotalCosto.Text = item.totalVentaCosto.ToString();
                            lbPorcentajeComision.Text = item.porcentajeComision.ToString();
                            lbPrecioComision.Text = item.precioComision.ToString();
                            lbPrecioFlete.Text = item.precioFlete.ToString();
                            totalUtilidad.Text = item.totalUtilidad.ToString();
                        }
                        listaClienteBindingSource.DataSource = objCliente.listaClienteNotaVenta(folioVenta);
                        listaVentaDetalleNotaBindingSource.DataSource = listaDetalle;
                        listaDetalleUtilidadBindingSource.DataSource = listaGeneral;
                        this.reportViewer1.RefreshReport();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la venta");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error cargarDatos(): " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No puede estar vacío el campo del folio");
            }
        }
        private void modificarComision()
        {
            if (MessageBox.Show("¿Desea cambiar el porcentaje de comisión de la venta con folio: " + folioVenta.ToString() + "?\n\nPorcentaje anterior: " + lbPorcentajeComision.Text + "%\nPorcentaje nuevo: " + txtComision.Text + "%", "Comisión", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var obj = new dVentas();
                decimal costoComision = Math.Round(Convert.ToDecimal(lbTotalVenta.Text) * (Convert.ToDecimal(txtComision.Text) / 100), 2);
                obj.modificarComision(folioVenta, Convert.ToInt32(txtComision.Text), costoComision);

                cargarDatos();
            }
            else
            {
                txtComision = null;
                txtComision.Visible = false;
                btnActualizarComision.Visible = false;
            }
        }



        private void btnCargar_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {

            if (keyData == Keys.Enter)
            {
                if (txtFolio.Focused)
                {
                    cargarDatos();
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void btnEditarComision_Click(object sender, EventArgs e)
        {
            if (folioVenta == 0)
            {
                MessageBox.Show("Para editar la comisión se requiere cargar una venta");
            }
            else
            {
                if (txtComision.Visible == false)
                {
                    txtComision.Visible = true;
                    btnActualizarComision.Visible = true;
                }
                else
                {
                    txtComision.Visible = false;
                    btnActualizarComision.Visible = false;
                }
            }
        }

        private void btnActualizarComision_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtComision.Text))
            {
                modificarComision();

                txtComision.Text = null;
                txtComision.Visible = false;
                btnActualizarComision.Visible = false;
            }
            else
            {
                MessageBox.Show("Ingresa un porcentaje de comisión");
                txtComision.Focus();
            }
        }
    }
}
