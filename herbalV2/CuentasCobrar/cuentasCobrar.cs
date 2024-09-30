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

namespace herbalV2.CuentasCobrar
{
    public partial class cuentasCobrar : Form
    {
        public cuentasCobrar()
        {
            InitializeComponent();
        }
        private void listarCuentas()
        {
            try
            {
                var obj = new dCuentasCobrar();
                dgvCuentas.DataSource = obj.listarCuentas();
                dgvCuentas.Columns["idCuentaCobrar"].Visible = false;
                dgvCuentas.Columns["adeudoTotal"].Visible = false;
                dgvCuentas.Columns["idCliente"].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarCuentas(): " + e.Message);
            }
        }
        private void buscar()
        {
            if (txtBuscar.Text == "")
            {
                listarCuentas();
            }
            else
            {
                dgvCuentas.CurrentCell = null;
                foreach (DataGridViewRow row in dgvCuentas.Rows)
                {
                    row.Visible = false;
                }
                foreach (DataGridViewRow row in dgvCuentas.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if ((cell.Value.ToString().ToUpper()).IndexOf(txtBuscar.Text.ToUpper()) == 0)
                        {
                            row.Visible = true;
                            break;
                        }
                    }
                }
            }
        }
        private void sumatoriaCliente()
        {
            try
            {
                lbAdeudoTotalCliente.Text = dgvCuentas.CurrentRow.Cells["nombre"].Value.ToString() + ":   $" + dgvCuentas.CurrentRow.Cells["adeudoTotal"].Value.ToString();

            }
            catch (Exception e)
            {
                MessageBox.Show("Error sumatoriaCliente(): " + e.Message);
            }
        }
        private void calculoAdeudoTotal()
        {
            try
            {
                decimal totalTodo = 0;
                for (int i = 0; i < dgvCuentas.Rows.Count; i++)
                {
                    totalTodo += Convert.ToDecimal(dgvCuentas["adeudo", i].Value);
                }
                lbTotal.Text = totalTodo.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error calculoAdeudoTotal(): " + e.Message);
            }

        }
        private void validacionesPendientes()
        {
            try
            {
                var obj = new validarAbonos();
                obj.ShowDialog();
                listarCuentas();
                calculoAdeudoTotal();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error validacionesPendientes(): " + e.Message);
            }
        }

        private void cuentasCobrar_Load(object sender, EventArgs e)
        {
            listarCuentas();
            calculoAdeudoTotal();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }

        private void dgvCuentas_Click(object sender, EventArgs e)
        {
            sumatoriaCliente();
        }

        private void dgvCuentas_DoubleClick(object sender, EventArgs e)
        {
            var obj = new abonos();
            obj.idCuentaCobrar = Convert.ToInt32(dgvCuentas.CurrentRow.Cells[0].Value);
            obj.idCliente = Convert.ToInt32(dgvCuentas.CurrentRow.Cells[2].Value);
            obj.adeudoTotal = Convert.ToDecimal(dgvCuentas.CurrentRow.Cells[4].Value);
            obj.ShowDialog();
            sumatoriaCliente();
            listarCuentas();
            calculoAdeudoTotal();
        }

        private void btnPendientesValidar_Click(object sender, EventArgs e)
        {
            validacionesPendientes();
        }

        private void dgvCuentas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvCuentas.Columns[e.ColumnIndex].Name == "fechaPago")
            {
                if (e.Value != null && e.Value is DateTime)
                {
                    DateTime fechaPago = (DateTime)e.Value;
                    DateTime hoy = DateTime.Now.Date;
                    DateTime dentroDeCincoDias = hoy.AddDays(5);

                    if (fechaPago < hoy)
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else if (fechaPago >= hoy && fechaPago <= dentroDeCincoDias)
                    {
                        e.CellStyle.BackColor = Color.Orange;
                        e.CellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.F1)
            {
                validacionesPendientes();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
