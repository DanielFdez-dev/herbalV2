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
    public partial class validarAbonos : Form
    {
        public validarAbonos()
        {
            InitializeComponent();
        }
        private void listarValidacionesPendientes()
        {
            try
            {
                var obj = new dCuentasCobrar();
                dgvValidaciones.DataSource = obj.listarValidacionesPendientes();
                dgvValidaciones.Columns["idCuentaCobrarDetalle"].Visible = false;
                dgvValidaciones.Columns["nombreDocumento"].Visible = false;
                dgvValidaciones.Columns["extensionDocumento"].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarValidacionesPendientes(): " + e.Message);
            }
        }
        private void validacion1()
        {
            try
            {
                if (Convert.ToBoolean(dgvValidaciones.CurrentRow.Cells[7].Value) == false)
                {
                    if (MessageBox.Show("¿Desea validar el abono de:" + dgvValidaciones.CurrentRow.Cells[1].Value.ToString() + "\n por la cantidad de: $" +
                    dgvValidaciones.CurrentRow.Cells[6].Value.ToString(), "1ra Validacion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var obj = new dCuentasCobrar();
                        obj.validacion1(Convert.ToInt32(dgvValidaciones.CurrentRow.Cells[0].Value));
                    }
                }
                else
                {
                    MessageBox.Show("La primera validación ya se había realizado con anterioridad");
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Error validacion1(): " + e.Message);
            }
        }
        private void validacion2()
        {
            try
            {
                if (Convert.ToBoolean(dgvValidaciones.CurrentRow.Cells[7].Value) == true)
                {
                    if (MessageBox.Show("¿Desea validar el abono de:" + dgvValidaciones.CurrentRow.Cells[1].Value.ToString() + "\n por la cantidad de: " +
                        dgvValidaciones.CurrentRow.Cells[6].Value.ToString(), "2da Validacion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var obj = new dCuentasCobrar();
                        obj.validacion2(Convert.ToInt32(dgvValidaciones.CurrentRow.Cells[0].Value));
                    }
                }
                else
                {
                    MessageBox.Show("Se requiere realizar la 1ra validación para completar la 2da validación");
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Error validacion2(): " + e.Message);
            }
        }

        private void validarAbonos_Load(object sender, EventArgs e)
        {
            listarValidacionesPendientes();
        }

        private void btnValidacion1_Click(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("s"))
            {
                validacion1();
                listarValidacionesPendientes();
            }
            else
            {
                MessageBox.Show("No tiene permisos para generar la primera validación");
            }

        }

        private void btnValidacion2_Click(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("j"))
            {
                validacion2();
                listarValidacionesPendientes();
            }
            else
            {
                MessageBox.Show("No tiene permisos para generar la segunda validación");
            }

        }
        public string SaveFileToDownloads(byte[] fileBytes, string fileName)
        {
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            string filePath = Path.Combine(downloadsPath, fileName);

            File.WriteAllBytes(filePath, fileBytes);

            return filePath;
        }
        private void btnArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new dCuentasCobrar();
                int idCuentaCobrar = Convert.ToInt32(dgvValidaciones.CurrentRow.Cells[0].Value);
                byte[] fileBytes = obj.descargaArchivo(idCuentaCobrar);

                if (fileBytes != null)
                {
                    string fileName = dgvValidaciones.CurrentRow.Cells[3].Value.ToString() + "-" + dgvValidaciones.CurrentRow.Cells[1].Value.ToString() + "-" + dgvValidaciones.CurrentRow.Cells[10].Value.ToString();
                    string filePath = SaveFileToDownloads(fileBytes, fileName);
                    MessageBox.Show($"Archivo guardado en Descargas");
                }
                else
                {
                    MessageBox.Show("No se registró evidencia para ese abono.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error en Descarga de Archivo: " + ex.Message);
            }
            
        }
        private void buscar()
        {
            if (txtBuscar.Text == "")
            {
                listarValidacionesPendientes();
            }
            else
            {
                dgvValidaciones.CurrentCell = null;
                foreach (DataGridViewRow row in dgvValidaciones.Rows)
                {
                    row.Visible = false;
                }
                foreach (DataGridViewRow row in dgvValidaciones.Rows)
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }
    }
}
