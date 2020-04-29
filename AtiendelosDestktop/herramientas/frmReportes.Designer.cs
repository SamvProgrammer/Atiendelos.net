namespace AtiendelosDestktop.herramientas
{
    partial class frmReportes
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
            this.SuspendLayout();
            // 
            // frmReportes
            // 
            this.ClientSize = new System.Drawing.Size(354, 261);
            this.Name = "frmReportes";
            this.Load += new System.EventHandler(this.frmReportes_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ventasBindingSource;
        private reportes.tablas tablas;
    }
}