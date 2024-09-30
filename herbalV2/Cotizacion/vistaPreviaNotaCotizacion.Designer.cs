namespace herbalV2.Cotizacion
{
    partial class vistaPreviaNotaCotizacion
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.listaClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaVentaDetalleNotaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaVentaGeneralNotaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.listaClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaVentaDetalleNotaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaVentaGeneralNotaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetCliente";
            reportDataSource1.Value = this.listaClienteBindingSource;
            reportDataSource2.Name = "DataSetVentaDetalle";
            reportDataSource2.Value = this.listaVentaDetalleNotaBindingSource;
            reportDataSource3.Name = "DataSetVentaGeneral";
            reportDataSource3.Value = this.listaVentaGeneralNotaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "herbalV2.RDLC.notaVenta.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(738, 425);
            this.reportViewer1.TabIndex = 0;
            // 
            // listaClienteBindingSource
            // 
            this.listaClienteBindingSource.DataSource = typeof(Datos.Listas.listaCliente);
            // 
            // listaVentaDetalleNotaBindingSource
            // 
            this.listaVentaDetalleNotaBindingSource.DataSource = typeof(Datos.Listas.listaVentaDetalleNota);
            // 
            // listaVentaGeneralNotaBindingSource
            // 
            this.listaVentaGeneralNotaBindingSource.DataSource = typeof(Datos.Listas.listaVentaGeneralNota);
            // 
            // vistaPreviaNotaCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 425);
            this.Controls.Add(this.reportViewer1);
            this.Name = "vistaPreviaNotaCotizacion";
            this.Text = "vistaPreviaNotaCotizacion";
            this.Load += new System.EventHandler(this.vistaPreviaNotaCotizacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listaClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaVentaDetalleNotaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaVentaGeneralNotaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource listaClienteBindingSource;
        private System.Windows.Forms.BindingSource listaVentaDetalleNotaBindingSource;
        private System.Windows.Forms.BindingSource listaVentaGeneralNotaBindingSource;
    }
}