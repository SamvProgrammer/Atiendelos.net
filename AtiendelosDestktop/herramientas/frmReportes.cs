using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtiendelosDestktop.herramientas
{
    public partial class frmReportes : Form
    {
        private bool imprimir;
        private string mensaje;

        public frmReportes(string nombreReporte,string nombreTabla)
        {
            InitializeComponent();

            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tablas = new AtiendelosDestktop.reportes.tablas();
            this.ventasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tablas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ventasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ventasBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = $"AtiendelosDestktop.reportes.{nombreReporte}.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(559, 387);
            this.reportViewer1.TabIndex = 0;
            // 
            // tablas
            // 
            this.tablas.DataSetName = "tablas";
            this.tablas.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ventasBindingSource
            // 
            this.ventasBindingSource.DataMember = nombreTabla;
            this.ventasBindingSource.DataSource = this.tablas;
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 387);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReportes";
            this.Text = "frmReportes";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ventasBindingSource)).EndInit();
            this.ResumeLayout(false);
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }



        internal void cargarDatos(string tablaNombre, object[] objeto, string mensaje, bool imprimir = false, object[] parametros = null)
        {

            DataTable tabla = tablas.Tables[tablaNombre];

            this.mensaje = mensaje;

            if (tabla != null)
            {

                foreach (var item in objeto)
                {
                    tabla.Rows.Add((object[])item);
                }
            }

            if (parametros != null)
            {
                object[][] elemento = (object[][])parametros;
                ReportParameter[] auxParametros = new ReportParameter[elemento[0].Length];
                for (int x = 0; x < elemento[0].Length; x++)
                {
                    ReportParameter p1 = new ReportParameter((string)elemento[0][x], (string)elemento[1][x]);
                    auxParametros[x] = p1;
                }
                reportViewer1.LocalReport.SetParameters(auxParametros);
            }
            this.reportViewer1.RefreshReport();
            this.imprimir = imprimir;
        }

        private void frmReportes_Load_1(object sender, EventArgs e)
        {

        }
    }
}
