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
    public partial class reportes : Form
    {
        public reportes()
        {
            InitializeComponent();
        }

        private void btnReporteVentas_Click(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("r"))
            {
                var frm = new ReporteVentas();
                frm.Show();
            }
            else
            {
                MessageBox.Show("No cuenta con permisos para acceder a reporte de ventas");
            }

        }

        private void btnReporteCobranza_Click(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("o"))
            {
                var frm = new reporteCuentasCobrar();
                frm.Show();
            }
            else
            {
                MessageBox.Show("No cuenta con permisos para acceder a reporte de cobranza");
            }

        }

        private void btnReimpresionNotaVenta_Click(object sender, EventArgs e)
        {
            var frm = new Ventas.reimpresionNotaVenta();
            frm.Show();
        }

        private void btnUtilidadBruta_Click(object sender, EventArgs e)
        {
            if (dComun.idEmpleado == 1)
            {
                var frm = new UtilidadBruta.utilidadBruta();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Solo el administrador puede ingresar a este reporte");
            }

        }

        private void btnUtilidadXFecha_Click(object sender, EventArgs e)
        {
            if (dComun.idEmpleado == 1)
            {
                var frm = new UtilidadBruta.utilidadBrutaGeneral();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Solo el administrador puede ingresar a este reporte");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("q"))
            {
                var frm = new reporteProductos();
                frm.Show();
            }
            else
            {
                MessageBox.Show("No cuenta con permisos para acceder a reporte de cobranza");
            }
        }
    }
}
