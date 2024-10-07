namespace herbalV2.UtilidadBruta
{
    partial class utilidadBrutaGeneral
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fecha2 = new System.Windows.Forms.DateTimePicker();
            this.fecha1 = new System.Windows.Forms.DateTimePicker();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.listaDetalleUtilidadBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.listaDetalleUtilidadBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 30);
            this.label1.TabIndex = 11;
            this.label1.Text = "Utilidad de Ventas";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 27);
            this.label3.TabIndex = 25;
            this.label3.Text = "Fecha Final:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 27);
            this.label2.TabIndex = 24;
            this.label2.Text = "Fecha Inicial:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fecha2
            // 
            this.fecha2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fecha2.Location = new System.Drawing.Point(153, 97);
            this.fecha2.Name = "fecha2";
            this.fecha2.Size = new System.Drawing.Size(131, 27);
            this.fecha2.TabIndex = 23;
            // 
            // fecha1
            // 
            this.fecha1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fecha1.Location = new System.Drawing.Point(153, 60);
            this.fecha1.Name = "fecha1";
            this.fecha1.Size = new System.Drawing.Size(131, 27);
            this.fecha1.TabIndex = 22;
            // 
            // btnProcesar
            // 
            this.btnProcesar.BackColor = System.Drawing.Color.SkyBlue;
            this.btnProcesar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnProcesar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcesar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesar.Location = new System.Drawing.Point(33, 145);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(114, 34);
            this.btnProcesar.TabIndex = 26;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = false;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Right;
            this.reportViewer1.DocumentMapWidth = 63;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.listaDetalleUtilidadBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "herbalV2.RDLC.utilidadGeneral.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(398, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.ShowBackButton = false;
            this.reportViewer1.ShowContextMenu = false;
            this.reportViewer1.ShowCredentialPrompts = false;
            this.reportViewer1.ShowDocumentMapButton = false;
            this.reportViewer1.ShowFindControls = false;
            this.reportViewer1.ShowPageNavigationControls = false;
            this.reportViewer1.ShowParameterPrompts = false;
            this.reportViewer1.Size = new System.Drawing.Size(488, 486);
            this.reportViewer1.TabIndex = 29;
            // 
            // listaDetalleUtilidadBindingSource
            // 
            this.listaDetalleUtilidadBindingSource.DataSource = typeof(Datos.Listas.listaDetalleUtilidad);
            // 
            // utilidadBrutaGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 486);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fecha2);
            this.Controls.Add(this.fecha1);
            this.Controls.Add(this.label1);
            this.Name = "utilidadBrutaGeneral";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.utilidadBrutaGeneral_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listaDetalleUtilidadBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker fecha2;
        private System.Windows.Forms.DateTimePicker fecha1;
        private System.Windows.Forms.Button btnProcesar;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource listaDetalleUtilidadBindingSource;
    }
}