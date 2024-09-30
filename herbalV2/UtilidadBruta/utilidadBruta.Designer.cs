namespace herbalV2.UtilidadBruta
{
    partial class utilidadBruta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.listaClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaDetalleUtilidadBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaVentaDetalleNotaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbTotalCosto = new System.Windows.Forms.Label();
            this.lbPorcentajeComision = new System.Windows.Forms.Label();
            this.lbPrecioComision = new System.Windows.Forms.Label();
            this.lbPrecioFlete = new System.Windows.Forms.Label();
            this.totalUtilidad = new System.Windows.Forms.Label();
            this.btnCargar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lbTotalVenta = new System.Windows.Forms.Label();
            this.btnEditarComision = new System.Windows.Forms.Button();
            this.txtComision = new System.Windows.Forms.TextBox();
            this.btnActualizarComision = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.listaClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaDetalleUtilidadBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaVentaDetalleNotaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // listaClienteBindingSource
            // 
            this.listaClienteBindingSource.DataSource = typeof(Datos.Listas.listaCliente);
            // 
            // listaDetalleUtilidadBindingSource
            // 
            this.listaDetalleUtilidadBindingSource.DataSource = typeof(Datos.Listas.listaDetalleUtilidad);
            // 
            // listaVentaDetalleNotaBindingSource
            // 
            this.listaVentaDetalleNotaBindingSource.DataSource = typeof(Datos.Listas.listaVentaDetalleNota);
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSetCliente";
            reportDataSource1.Value = this.listaClienteBindingSource;
            reportDataSource2.Name = "DataSetUtilidadGeneral";
            reportDataSource2.Value = this.listaDetalleUtilidadBindingSource;
            reportDataSource3.Name = "DataSetVentaDetalle";
            reportDataSource3.Value = this.listaVentaDetalleNotaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "herbalV2.RDLC.utilidad.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(577, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(589, 658);
            this.reportViewer1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(42, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Folio:";
            // 
            // txtFolio
            // 
            this.txtFolio.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolio.Location = new System.Drawing.Point(97, 89);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.Size = new System.Drawing.Size(90, 27);
            this.txtFolio.TabIndex = 8;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 30);
            this.label1.TabIndex = 10;
            this.label1.Text = "Utilidad de Ventas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Precio Costo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 274);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 19);
            this.label4.TabIndex = 12;
            this.label4.Text = "% Comisión:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 19);
            this.label5.TabIndex = 13;
            this.label5.Text = "Precio Comisión:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(54, 365);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 19);
            this.label6.TabIndex = 14;
            this.label6.Text = "Precio Flete:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 448);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 23);
            this.label7.TabIndex = 15;
            this.label7.Text = "Utilidad Bruta:";
            // 
            // lbTotalCosto
            // 
            this.lbTotalCosto.AutoSize = true;
            this.lbTotalCosto.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalCosto.Location = new System.Drawing.Point(161, 232);
            this.lbTotalCosto.Name = "lbTotalCosto";
            this.lbTotalCosto.Size = new System.Drawing.Size(41, 21);
            this.lbTotalCosto.TabIndex = 16;
            this.lbTotalCosto.Text = "0.00";
            // 
            // lbPorcentajeComision
            // 
            this.lbPorcentajeComision.AutoSize = true;
            this.lbPorcentajeComision.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPorcentajeComision.Location = new System.Drawing.Point(161, 274);
            this.lbPorcentajeComision.Name = "lbPorcentajeComision";
            this.lbPorcentajeComision.Size = new System.Drawing.Size(19, 21);
            this.lbPorcentajeComision.TabIndex = 17;
            this.lbPorcentajeComision.Text = "0";
            // 
            // lbPrecioComision
            // 
            this.lbPrecioComision.AutoSize = true;
            this.lbPrecioComision.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrecioComision.Location = new System.Drawing.Point(161, 317);
            this.lbPrecioComision.Name = "lbPrecioComision";
            this.lbPrecioComision.Size = new System.Drawing.Size(41, 21);
            this.lbPrecioComision.TabIndex = 18;
            this.lbPrecioComision.Text = "0.00";
            // 
            // lbPrecioFlete
            // 
            this.lbPrecioFlete.AutoSize = true;
            this.lbPrecioFlete.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrecioFlete.Location = new System.Drawing.Point(161, 365);
            this.lbPrecioFlete.Name = "lbPrecioFlete";
            this.lbPrecioFlete.Size = new System.Drawing.Size(41, 21);
            this.lbPrecioFlete.TabIndex = 19;
            this.lbPrecioFlete.Text = "0.00";
            // 
            // totalUtilidad
            // 
            this.totalUtilidad.AutoSize = true;
            this.totalUtilidad.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalUtilidad.Location = new System.Drawing.Point(161, 450);
            this.totalUtilidad.Name = "totalUtilidad";
            this.totalUtilidad.Size = new System.Drawing.Size(48, 23);
            this.totalUtilidad.TabIndex = 20;
            this.totalUtilidad.Text = "0.00";
            // 
            // btnCargar
            // 
            this.btnCargar.BackColor = System.Drawing.Color.Green;
            this.btnCargar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(102)))), ((int)(((byte)(78)))));
            this.btnCargar.FlatAppearance.BorderSize = 2;
            this.btnCargar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGreen;
            this.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargar.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.ForeColor = System.Drawing.Color.White;
            this.btnCargar.Location = new System.Drawing.Point(209, 83);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(166, 39);
            this.btnCargar.TabIndex = 21;
            this.btnCargar.Text = "Procesar Datos";
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(56, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 19);
            this.label8.TabIndex = 22;
            this.label8.Text = "Total Venta:";
            // 
            // lbTotalVenta
            // 
            this.lbTotalVenta.AutoSize = true;
            this.lbTotalVenta.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalVenta.Location = new System.Drawing.Point(161, 189);
            this.lbTotalVenta.Name = "lbTotalVenta";
            this.lbTotalVenta.Size = new System.Drawing.Size(41, 21);
            this.lbTotalVenta.TabIndex = 23;
            this.lbTotalVenta.Text = "0.00";
            // 
            // btnEditarComision
            // 
            this.btnEditarComision.BackColor = System.Drawing.Color.LightGray;
            this.btnEditarComision.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEditarComision.FlatAppearance.BorderSize = 2;
            this.btnEditarComision.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnEditarComision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarComision.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarComision.ForeColor = System.Drawing.Color.Black;
            this.btnEditarComision.Location = new System.Drawing.Point(381, 83);
            this.btnEditarComision.Name = "btnEditarComision";
            this.btnEditarComision.Size = new System.Drawing.Size(166, 39);
            this.btnEditarComision.TabIndex = 24;
            this.btnEditarComision.Text = "Editar Comisión";
            this.btnEditarComision.UseVisualStyleBackColor = false;
            this.btnEditarComision.Click += new System.EventHandler(this.btnEditarComision_Click);
            // 
            // txtComision
            // 
            this.txtComision.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComision.Location = new System.Drawing.Point(165, 271);
            this.txtComision.Name = "txtComision";
            this.txtComision.Size = new System.Drawing.Size(66, 27);
            this.txtComision.TabIndex = 25;
            this.txtComision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtComision.Visible = false;
            // 
            // btnActualizarComision
            // 
            this.btnActualizarComision.BackColor = System.Drawing.Color.Blue;
            this.btnActualizarComision.FlatAppearance.BorderColor = System.Drawing.Color.MediumBlue;
            this.btnActualizarComision.FlatAppearance.BorderSize = 2;
            this.btnActualizarComision.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnActualizarComision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarComision.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarComision.ForeColor = System.Drawing.Color.White;
            this.btnActualizarComision.Location = new System.Drawing.Point(234, 271);
            this.btnActualizarComision.Margin = new System.Windows.Forms.Padding(0);
            this.btnActualizarComision.Name = "btnActualizarComision";
            this.btnActualizarComision.Size = new System.Drawing.Size(93, 27);
            this.btnActualizarComision.TabIndex = 26;
            this.btnActualizarComision.Text = "Actualizar";
            this.btnActualizarComision.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnActualizarComision.UseVisualStyleBackColor = false;
            this.btnActualizarComision.Visible = false;
            this.btnActualizarComision.Click += new System.EventHandler(this.btnActualizarComision_Click);
            // 
            // utilidadBruta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 682);
            this.Controls.Add(this.btnActualizarComision);
            this.Controls.Add(this.txtComision);
            this.Controls.Add(this.btnEditarComision);
            this.Controls.Add(this.lbTotalVenta);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.totalUtilidad);
            this.Controls.Add(this.lbPrecioFlete);
            this.Controls.Add(this.lbPrecioComision);
            this.Controls.Add(this.lbPorcentajeComision);
            this.Controls.Add(this.lbTotalCosto);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFolio);
            this.Controls.Add(this.reportViewer1);
            this.Name = "utilidadBruta";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.utilidadBruta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listaClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaDetalleUtilidadBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaVentaDetalleNotaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbTotalCosto;
        private System.Windows.Forms.Label lbPorcentajeComision;
        private System.Windows.Forms.Label lbPrecioComision;
        private System.Windows.Forms.Label lbPrecioFlete;
        private System.Windows.Forms.Label totalUtilidad;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbTotalVenta;
        private System.Windows.Forms.BindingSource listaClienteBindingSource;
        private System.Windows.Forms.BindingSource listaDetalleUtilidadBindingSource;
        private System.Windows.Forms.BindingSource listaVentaDetalleNotaBindingSource;
        private System.Windows.Forms.Button btnEditarComision;
        private System.Windows.Forms.TextBox txtComision;
        private System.Windows.Forms.Button btnActualizarComision;
    }
}