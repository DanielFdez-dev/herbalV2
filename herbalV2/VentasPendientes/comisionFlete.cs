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
    public partial class comisionFlete : Form
    {
        public int folio { get; set; }
        public decimal total { get; set; }
        private decimal resultado;
        public comisionFlete()
        {
            InitializeComponent();
        }

        private void comisionFlete_Load(object sender, EventArgs e)
        {
            lbFolio.Text = folio.ToString();
            lbTotal.Text = total.ToString();
        }
        private void calculoComision()
        {
            decimal porcentaje = Convert.ToDecimal(txtPorcentajeComision.Text);
            resultado = Math.Round(total * (porcentaje / 100), 2);
            txtPrecioComision.Text = resultado.ToString();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.Enter)
            {
                if (txtPorcentajeComision.Focused)
                {
                    calculoComision();
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCostoFlete.Text) || !string.IsNullOrEmpty(txtPorcentajeComision.Text) || !string.IsNullOrEmpty(txtPrecioComision.Text))
            {
                try
                {
                    if (MessageBox.Show("Verifique que la información sea correcta \n\n¿Desea Guardar?", "Guardar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var obj = new dVentas();
                        if (obj.cerrarVenta(Convert.ToInt32(lbFolio.Text), Convert.ToDecimal(txtCostoFlete.Text), Convert.ToInt32(txtPorcentajeComision.Text), Convert.ToDecimal(txtPrecioComision.Text)))
                        {
                            MessageBox.Show("Información guardada correctamente");
                            this.Dispose();
                        }
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error btnGuardar(): " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No puede haber campos vacios");
            }
        }
    }
}
