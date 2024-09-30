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
    public partial class vistaPreviaNotaVenta : Form
    {
        public int folioVenta { get; set; }
        public vistaPreviaNotaVenta()
        {
            InitializeComponent();
        }

        private void vistaPreviaNotaVenta_Load(object sender, EventArgs e)
        {
            var objCliente = new dClientes();
            var objVenta = new dVentas();
            listaClienteBindingSource.DataSource = objCliente.listaClienteNotaVenta(folioVenta);
            listaVentaDetalleNotaBindingSource.DataSource = objVenta.ventaDetalleNota(folioVenta);
            listaVentaGeneralNotaBindingSource.DataSource = objVenta.ventaGeneralNota(folioVenta);
            this.reportViewer1.RefreshReport();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {

            if (keyData == Keys.Escape)
            {
                this.Dispose();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
