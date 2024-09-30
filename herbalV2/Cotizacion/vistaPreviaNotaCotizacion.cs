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

namespace herbalV2.Cotizacion
{
    public partial class vistaPreviaNotaCotizacion : Form
    {
        public int folioCotizacion { get; set; }
        public vistaPreviaNotaCotizacion()
        {
            InitializeComponent();
        }

        private void vistaPreviaNotaCotizacion_Load(object sender, EventArgs e)
        {
            var objCliente = new dClientes();
            var objCotizacion = new dCotizacion();
            listaClienteBindingSource.DataSource = objCliente.listaClienteNotaVenta(folioCotizacion);
            listaVentaDetalleNotaBindingSource.DataSource = objCotizacion.cotizacionDetalleNota(folioCotizacion);
            listaVentaGeneralNotaBindingSource.DataSource = objCotizacion.cotizacionGeneralNota(folioCotizacion);
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
