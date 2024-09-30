using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace herbalV2.Usuarios
{
    public partial class usuarios : Form
    {
        public usuarios()
        {
            InitializeComponent();
        }

        private void limpiarForm()//Metodo para limpiar las cajas de texto
        {
            txtNombre.Text = null;
            txtUsuario.Text = null;
            txtPass.Text = null;
            deseleccionarTodo();
            label2.Text = "Agregar";
            idEmpleado.Text = "0";
        }

        private void listarUsuarios()//Metodo para mostrar los usuarios activos
        {
            try
            {
                var obj_user = new dUsuarios();
                dgvUsuarios.DataSource = obj_user.listarUsuarios();
                dgvUsuarios.Columns["idEmpleado"].Visible = false;
                dgvUsuarios.Columns["pass"].Visible = false;
                dgvUsuarios.Columns["permisos"].Visible = false;
                dgvUsuarios.Columns["fechaBaja"].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error listarUsuarios(): " + e.Message);
            }
        }

        private void agregarUsuario()//Metodo para agregar usuario nuevo
        {
            if (dComun.permisosEmpleado.Contains("j"))
            {
                var obj_usuario = new dUsuarios();
                string permisos_ = permisos();
                //Validacion de campos vacios
                if (!string.IsNullOrEmpty(txtUsuario.Text))
                {
                    if (!string.IsNullOrEmpty(txtPass.Text))
                    {
                        if (!string.IsNullOrEmpty(txtNombre.Text))
                        {
                            if (MessageBox.Show("¿Desea agregar el nuevo usuario?", "Nuevo Usuario", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    obj_usuario.agregarUsuario(txtUsuario.Text, txtNombre.Text, txtPass.Text, permisos_);
                                    MessageBox.Show("Usuario agregado correctamente");
                                    listarUsuarios();
                                    limpiarForm();
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show("Error agregarUsuario(): " + e.Message);
                                }
                            }
                        }
                        else MessageBox.Show("El campo 'Nombre' no puede estar vacio", "ERROR", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("El campo 'Contraseña' no puede estar vacío", "ERROR", MessageBoxButtons.OK);
                }
                else MessageBox.Show("El campo 'Usuario' no puede estar vacio", "ERROR", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("No tiene permisos para agregar un nuevo usuario");
            }


        }

        private void modificarUsuario()
        {
            string permissionFinal = permisos();
            if (!string.IsNullOrEmpty(txtUsuario.Text))
            {
                if (!string.IsNullOrEmpty(txtPass.Text))
                {
                    if (!string.IsNullOrEmpty(txtNombre.Text))
                    {
                        if (MessageBox.Show("¿Desea Modificar el usuario?", "Usuario", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            try
                            {
                                //MessageBox.Show(permissionFinal);
                                var obj_user = new dUsuarios();
                                obj_user.modificarUsuario(Convert.ToInt32(idEmpleado.Text), txtUsuario.Text, txtNombre.Text, txtPass.Text, permissionFinal);
                                MessageBox.Show("Se modifico correctamente el usuario");
                                limpiarForm();
                                listarUsuarios();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Fallo en: " + e.Message);
                            }
                        }

                    }
                    else MessageBox.Show("El campo 'Nombre' no puede estar vacio", "ERROR", MessageBoxButtons.OK);
                }
                else MessageBox.Show("El campo 'Contraseña' no puede estar vacío", "ERROR", MessageBoxButtons.OK);
            }
            else MessageBox.Show("El campo 'Usuario' no puede estar vacio", "ERROR", MessageBoxButtons.OK);
        }

        private void eliminarUsuario()//Metodo para borrar un usuario
        {
            if (dComun.permisosEmpleado.Contains("i"))
            {
                if (dgvUsuarios.CurrentRow.Cells[0].Value.ToString() != "1")
                {
                    if (MessageBox.Show("¿Desea borrar al usuario?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            var obj_user = new dUsuarios();
                            obj_user.eliminarUsuario(Convert.ToInt32(dgvUsuarios.CurrentRow.Cells[0].Value));
                            MessageBox.Show("Usuario eliminado correctamente");
                            listarUsuarios();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error en: " + e.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El usuario Administrador no se puede eliminar");
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos para eliminar usuarios");
            }

        }

        private void permisosCheck(string permisos)
        {
            if (permisos.Contains("a")) ch_productAdjust.Checked = true;

            if (permisos.Contains("b")) ch_productDelete.Checked = true;

            if (permisos.Contains("c")) ch_productIn.Checked = true;

            if (permisos.Contains("d")) ch_productModify.Checked = true;

            if (permisos.Contains("e")) ch_productNew.Checked = true;

            if (permisos.Contains("f")) ch_clientDelete.Checked = true;

            if (permisos.Contains("g")) ch_clientModify.Checked = true;

            if (permisos.Contains("h")) ch_clientNew.Checked = true;

            if (permisos.Contains("i")) ch_userDelete.Checked = true;

            if (permisos.Contains("j")) ch_userNew.Checked = true;

            if (permisos.Contains("k")) ch_userModify.Checked = true;


            if (permisos.Contains("l")) ch_sellerDelete.Checked = true;

            if (permisos.Contains("m")) ch_sellerModify.Checked = true;

            if (permisos.Contains("n")) ch_sellerNew.Checked = true;

            if (permisos.Contains("o")) ch_reportAccounts.Checked = true;

            if (permisos.Contains("p")) ch_reportClient.Checked = true;

            if (permisos.Contains("q")) ch_reportProduct.Checked = true;

            if (permisos.Contains("r")) ch_reportSale.Checked = true;


            if (permisos.Contains("s")) ch_validation1.Checked = true;

            if (permisos.Contains("t")) ch_validation2.Checked = true;

            if (permisos.Contains("u")) ch_sale.Checked = true;

            if (permisos.Contains("v")) ch_accounts.Checked = true;

            if (permisos.Contains("w")) chSeleccionManual.Checked = true;
        }

        private void usuarios_Load(object sender, EventArgs e)
        {
            listarUsuarios();
        }
        private void seleccionarTodo()
        {
            ch_accounts.Checked = true;
            ch_client.Checked = true;
            ch_clientDelete.Checked = true;
            ch_clientModify.Checked = true;
            ch_clientNew.Checked = true;
            ch_product.Checked = true;
            ch_productAdjust.Checked = true;
            ch_productDelete.Checked = true;
            ch_productIn.Checked = true;
            ch_productModify.Checked = true;
            ch_productNew.Checked = true;
            ch_reportAccounts.Checked = true;
            ch_reportProduct.Checked = true;
            ch_reportSale.Checked = true;
            ch_reportClient.Checked = true;
            ch_sale.Checked = true;
            ch_seller.Checked = true;
            ch_sellerDelete.Checked = true;
            ch_sellerModify.Checked = true;
            ch_sellerNew.Checked = true;
            ch_user.Checked = true;
            ch_userDelete.Checked = true;
            ch_userNew.Checked = true;
            ch_userModify.Checked = true;
            ch_validation1.Checked = true;
            ch_validation2.Checked = true;
            chSeleccionManual.Checked = true;
        }
        private void deseleccionarTodo()
        {
            ch_accounts.Checked = false;
            ch_client.Checked = false;
            ch_clientDelete.Checked = false;
            ch_clientModify.Checked = false;
            ch_clientNew.Checked = false;
            ch_product.Checked = false;
            ch_productAdjust.Checked = false;
            ch_productDelete.Checked = false;
            ch_productIn.Checked = false;
            ch_productModify.Checked = false;
            ch_productNew.Checked = false;
            ch_reportAccounts.Checked = false;
            ch_reportProduct.Checked = false;
            ch_reportSale.Checked = false;
            ch_reportClient.Checked = false;
            ch_sale.Checked = false;
            ch_seller.Checked = false;
            ch_sellerDelete.Checked = false;
            ch_sellerModify.Checked = false;
            ch_sellerNew.Checked = false;
            ch_user.Checked = false;
            ch_userDelete.Checked = false;
            ch_userNew.Checked = false;
            ch_userModify.Checked = false;
            ch_validation1.Checked = false;
            ch_validation2.Checked = false;
            chSeleccionManual.Checked = false;
        }

        private void btnSeleccionarTodo_Click(object sender, EventArgs e)
        {
            seleccionarTodo();
        }

        private void btnDeseleccionar_Click(object sender, EventArgs e)
        {
            deseleccionarTodo();
        }
        private string permisos()
        {
            string permissions = "";
            if (ch_productAdjust.Checked) permissions += "a";
            if (ch_productDelete.Checked) permissions += "b";
            if (ch_productIn.Checked) permissions += "c";
            if (ch_productModify.Checked) permissions += "d";
            if (ch_productNew.Checked) permissions += "e";

            if (ch_clientDelete.Checked) permissions += "f";
            if (ch_clientModify.Checked) permissions += "g";
            if (ch_clientNew.Checked) permissions += "h";

            if (ch_userDelete.Checked) permissions += "i";
            if (ch_userNew.Checked) permissions += "j";
            if (ch_userModify.Checked) permissions += "k";

            if (ch_sellerDelete.Checked) permissions += "l";
            if (ch_sellerModify.Checked) permissions += "m";
            if (ch_sellerNew.Checked) permissions += "n";

            if (ch_reportAccounts.Checked) permissions += "o";
            if (ch_reportClient.Checked) permissions += "p";
            if (ch_reportProduct.Checked) permissions += "q";
            if (ch_reportSale.Checked) permissions += "r";

            if (ch_validation1.Checked) permissions += "s";
            if (ch_validation2.Checked) permissions += "t";

            if (ch_sale.Checked) permissions += "u";
            if (ch_accounts.Checked) permissions += "v";
            if (chSeleccionManual.Checked) permissions += "w";

            return permissions;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//Asigna telcas a botones de formulario
        {
            if (keyData == Keys.Delete)
            {
                if (dgvUsuarios.Focused)
                {
                    eliminarUsuario();
                    return true;
                }
                else return false;
            }
            if (keyData == Keys.Enter)
            {
                if (label2.Text == "Agregar")
                {
                    agregarUsuario();
                }
                else
                {
                    modificarUsuario();
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void ch_product_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_product.Checked)
            {
                ch_productAdjust.Checked = true;
                ch_productDelete.Checked = true;
                ch_productIn.Checked = true;
                ch_productNew.Checked = true;
                ch_productModify.Checked = true;
            }
            else
            {
                ch_productAdjust.Checked = false;
                ch_productDelete.Checked = false;
                ch_productIn.Checked = false;
                ch_productNew.Checked = false;
                ch_productModify.Checked = false;
            }
        }

        private void ch_client_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_client.Checked)
            {
                ch_clientDelete.Checked = true;
                ch_clientModify.Checked = true;
                ch_clientNew.Checked = true;
            }
            else
            {
                ch_clientDelete.Checked = false;
                ch_clientModify.Checked = false;
                ch_clientNew.Checked = false;
            }
        }

        private void ch_user_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_user.Checked)
            {
                ch_userDelete.Checked = true;
                ch_userModify.Checked = true;
                ch_userNew.Checked = true;
            }
            else
            {
                ch_userDelete.Checked = false;
                ch_userModify.Checked = false;
                ch_userNew.Checked = false;
            }
        }

        private void ch_seller_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_seller.Checked)
            {
                ch_sellerDelete.Checked = true;
                ch_sellerModify.Checked = true;
                ch_sellerNew.Checked = true;
            }
            else
            {
                ch_sellerDelete.Checked = false;
                ch_sellerModify.Checked = false;
                ch_sellerNew.Checked = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (label2.Text == "Agregar")
            {
                agregarUsuario();
            }
            else
            {
                modificarUsuario();
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminarUsuario();
        }

        private void dgvUsuarios_DoubleClick(object sender, EventArgs e)
        {
            if (dComun.permisosEmpleado.Contains("k"))
            {
                label2.Text = "Modificar";
                idEmpleado.Text = dgvUsuarios.CurrentRow.Cells[0].Value.ToString();
                txtNombre.Text = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
                txtUsuario.Text = dgvUsuarios.CurrentRow.Cells[2].Value.ToString();
                txtPass.Text = dgvUsuarios.CurrentRow.Cells[3].Value.ToString();
                deseleccionarTodo();
                permisosCheck(dgvUsuarios.CurrentRow.Cells[7].Value.ToString());
            }
            else
            {
                MessageBox.Show("No tiene permisos para modificar información de usuarios");
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                listarUsuarios();
            }
            else
            {
                dgvUsuarios.CurrentCell = null;
                foreach (DataGridViewRow row in dgvUsuarios.Rows)
                {
                    row.Visible = false;
                }
                foreach (DataGridViewRow row in dgvUsuarios.Rows)
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
    }
}
