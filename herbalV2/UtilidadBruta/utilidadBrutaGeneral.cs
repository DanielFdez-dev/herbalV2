using Datos;
using Datos.Listas;
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
    public partial class utilidadBrutaGeneral : Form
    {
        public decimal totalUtilidadPorFecha;
        public utilidadBrutaGeneral()
        {
            InitializeComponent();
        }
        public void procesarDatos()
        {
            var obj = new dUtilidadBruta();
            List<listaDetalleUtilidad> lista = obj.utilidadPorFecha(fecha1.Value, fecha2.Value);

            listaDetalleUtilidadBindingSource.DataSource = lista;

            this.reportViewer1.RefreshReport();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            procesarDatos();
        }

        private void utilidadBrutaGeneral_Load(object sender, EventArgs e)
        {
        }
    }
}
