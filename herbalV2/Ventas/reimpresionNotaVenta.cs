using Datos;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace herbalV2.Ventas
{
    public partial class reimpresionNotaVenta : Form
    {
        public reimpresionNotaVenta()
        {
            InitializeComponent();
        }
        private void cargarDatos()
        {
            try
            {
                var objCliente = new dClientes();
                var objVenta = new dVentas();
                int folio = Convert.ToInt32(txtFolioVenta.Text);

                dgvVenta.DataSource = objVenta.ventaDetalleNota(folio);

                listaClienteBindingSource.DataSource = objCliente.listaClienteNotaVenta(folio);
                listaVentaDetalleNotaBindingSource.DataSource = objVenta.ventaDetalleNota(folio);
                listaVentaGeneralNotaBindingSource.DataSource = objVenta.ventaGeneralNota(folio);
                // Configura el modo de visualización a diseño de impresión
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

                // Configura el zoom a "Ancho de página"
                reportViewer1.ZoomMode = ZoomMode.PageWidth;
                this.reportViewer1.RefreshReport();
            }
            catch
            {

                MessageBox.Show("No se encontró el folio");
            }
        }

        private void reimpresionNotaVenta_Load(object sender, EventArgs e)
        {
        }

        private void btnCargarDatos_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtFolioVenta.Text))
            {
                cargarDatos();
            }
            else
            {
                MessageBox.Show("Ingresa un folio de venta");
            }
        }
    }
}
