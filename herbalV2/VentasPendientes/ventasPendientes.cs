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

namespace herbalV2.VentasPendientes
{
    public partial class ventasPendientes : Form
    {
        public ventasPendientes()
        {
            InitializeComponent();
        }

        private void listarVentasPendientes()
        {
            try
            {
                var obj = new dVentas();
                dgvVenta.DataSource = obj.listarVentasPendientes();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error listarVentasPendientes(): " + ex.Message);
            }
        }

        private void ventasPendientes_Load(object sender, EventArgs e)
        {
            listarVentasPendientes();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.Enter)
            {
                if (dgvVenta.Focused)
                {
                    seleccionarProducto();
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void dgvVenta_DoubleClick(object sender, EventArgs e)
        {
            seleccionarProducto();
        }
        private void seleccionarProducto()
        {
            var frm = new comisionFlete();
            frm.folio = Convert.ToInt32(dgvVenta.CurrentRow.Cells[0].Value);
            frm.total = Convert.ToDecimal(dgvVenta.CurrentRow.Cells[5].Value);
            frm.ShowDialog();
            listarVentasPendientes();
        }
    }
}
