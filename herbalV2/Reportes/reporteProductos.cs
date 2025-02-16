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

namespace herbalV2.Reportes
{
    public partial class reporteProductos : Form
    {
        private int idCliente;
        private int idVendedor;
        private string columnaSumada;
        public reporteProductos()
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

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new dReportes();
                if (cbTipoReporte.SelectedIndex == 0)//Existencia General
                {
                    dgvReporte.DataSource = obj.existenciaProductos();
                    dgvReporte.Columns["idClasificacion"].Visible = false;
                    dgvReporte.Columns["idMarca"].Visible = false;
                    calcularPiezasTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ProcesarInformacion(): " + ex.Message);
            }
        }

        private void cbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbTipoReporte.SelectedIndex == 1)
            //{

            //}
        }

        private void reporteProductos_Load(object sender, EventArgs e)
        {
            cbTipoReporte.SelectedIndex = 0;
        }
        private void calcularPiezasTotal()
        {
            try
            {
                int totalDatos = 0;
                decimal totalImporte = 0;

                foreach (DataGridViewRow fila in dgvReporte.Rows)
                {
                    if (!fila.IsNewRow) // Para evitar contar la fila nueva al final del DataGridView.
                    {
                        totalDatos += Convert.ToInt32(fila.Cells["STOCK"].Value);
                        totalImporte += Convert.ToDecimal(fila.Cells["IMPORTE"].Value);
                    }
                }
                lbImporteTotal.Text = totalImporte.ToString("N0");
            }
            catch (Exception e)
            {

                MessageBox.Show("Error calcularPiezasTotal(): " + e.Message);
            }
        }
    }
}
