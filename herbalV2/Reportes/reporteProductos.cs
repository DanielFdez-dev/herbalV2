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
    }
}
