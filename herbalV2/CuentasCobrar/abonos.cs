using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace herbalV2.CuentasCobrar
{
    public partial class abonos : Form
    {
        private byte[] fileBytes;
        private string nombreArchivo, extensionArchivo;
        public int idCuentaCobrar { get; set; }
        public int idCliente { get; set; }
        public decimal adeudoTotal { get; set; }
        public abonos()
        {
            InitializeComponent();
        }
        public void listarAbonos()
        {
            try
            {
                var obj = new dCuentasCobrar();
                dgvAbonos.DataSource = obj.listarCuentasCobrarDetalle(idCuentaCobrar);
                dgvAbonos.Columns["idCuentaCobrarDetalle"].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarAbonos(): " + e.Message);
            }
        }
        private void listarTipoPago()
        {
            try
            {
                var obj = new dTipoPago();
                cbTipoPago.DataSource = obj.listarTipoPago();
                cbTipoPago.DisplayMember = "descripcion";
                cbTipoPago.ValueMember = "idTipoPago";
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarTipoPago(): " + e.Message);
            }
        }
        private void listarBancos()
        {
            try
            {
                var obj = new dTipoPago();
                cbBanco.DataSource = obj.listarBancos();
                cbBanco.DisplayMember = "descripcion";
                cbBanco.ValueMember = "idBanco";
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarTipoPago(): " + e.Message);
            }
        }

        private void limpiarControles()
        {
            fileBytes = null;
            txtCantidad.Text = string.Empty;
            txtComentario.Text = string.Empty;
        }
        private void agregarAbono()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCantidad.Text) || string.IsNullOrEmpty(txtComentario.Text))
                {
                    MessageBox.Show("No puede haber ningun campo vacío");
                }
                else if (Convert.ToDecimal(txtCantidad.Text) > Convert.ToDecimal(txtAdeudo.Text))
                {
                    MessageBox.Show("No se puede abonar una cantidad que exceda a la deuda");
                }
                else
                {
                    decimal cantidad = Convert.ToDecimal(txtCantidad.Text);
                    decimal adeudoTotal = Convert.ToDecimal(txtAdeudo.Text);
                    string tipoAbono = (cantidad == adeudoTotal) ? "P.TOTAL" : "P.PARCIAL";
                    if (MessageBox.Show("¿Desea agregar un abono a la cuenta?", "Abono", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var obj = new dCuentasCobrar();
                        if (obj.agregarAbono(idCuentaCobrar, cantidad, Convert.ToInt32(cbTipoPago.SelectedValue), txtComentario.Text, idCliente, fileBytes, nombreArchivo, extensionArchivo, Convert.ToInt32(cbBanco.SelectedValue),tipoAbono))
                        {
                            MessageBox.Show("Abono generado exitosamente");

                            listarAbonos();
                            cantidadAbonos();
                            adeudoTotal = adeudoTotal - Convert.ToDecimal(txtCantidad.Text);
                            txtAdeudo.Text = adeudoTotal.ToString();
                            txtCantidad.Text = adeudoTotal.ToString();
                            limpiarControles();
                        }
                        else
                        {
                            MessageBox.Show("No se generó el abono");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error agregarAbono(): " + e.Message);
            }
        }

        private void cantidadAbonos()
        {
            try
            {
                int cantidad = dgvAbonos.RowCount;
                txtCantidadAbonos.Text = cantidad.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error cantidadAbonos(): " + e.Message);
            }
        } 

        private void abonos_Load(object sender, EventArgs e)
        {
            listarTipoPago();
            listarBancos();
            listarAbonos();
            cantidadAbonos();
            txtAdeudo.Text = adeudoTotal.ToString();
            txtCantidad.Text = adeudoTotal.ToString();
        }


        private void btnArchivo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    fileBytes = File.ReadAllBytes(filePath);
                    nombreArchivo = openFileDialog.FileName;
                    extensionArchivo= Path.GetExtension(filePath).ToLower();
                }
            }
        }

        private void btnGuardarAbono_Click(object sender, EventArgs e)
        {
            agregarAbono();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.Enter)
            {
                if (txtCantidad.Focused)
                {
                    cbTipoPago.Focus();
                }
                else if (cbTipoPago.Focused)
                {
                    txtComentario.Focus();
                }
                else if (txtComentario.Focused)
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "All files (*.*)|*.*";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = openFileDialog.FileName;
                            fileBytes = File.ReadAllBytes(filePath);
                        }
                    }
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
