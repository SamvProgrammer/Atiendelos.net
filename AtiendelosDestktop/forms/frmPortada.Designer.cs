namespace AtiendelosDestktop.forms
{
    partial class frmPortada
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
            this.txtHora = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtHora
            // 
            this.txtHora.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtHora.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.txtHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHora.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.txtHora.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.txtHora.Location = new System.Drawing.Point(0, 0);
            this.txtHora.Name = "txtHora";
            this.txtHora.Size = new System.Drawing.Size(1064, 108);
            this.txtHora.TabIndex = 1;
            this.txtHora.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmPortada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AtiendelosDestktop.Properties.Resources.logoresutaurante;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1064, 573);
            this.Controls.Add(this.txtHora);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPortada";
            this.Text = "frmPortada";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label txtHora;
        private System.Windows.Forms.Timer timer1;
    }
}