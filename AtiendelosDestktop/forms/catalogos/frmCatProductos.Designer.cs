namespace AtiendelosDestktop.forms.catalogos
{
    partial class frmCatProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCatProductos));
            this.BUSQUEDA = new System.Windows.Forms.Label();
            this.datos = new System.Windows.Forms.DataGridView();
            this.Colum1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnseleccionar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btncerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BUSQUEDA
            // 
            this.BUSQUEDA.AutoSize = true;
            this.BUSQUEDA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BUSQUEDA.Location = new System.Drawing.Point(9, 66);
            this.BUSQUEDA.Name = "BUSQUEDA";
            this.BUSQUEDA.Size = new System.Drawing.Size(177, 16);
            this.BUSQUEDA.TabIndex = 14;
            this.BUSQUEDA.Text = "Busqueda (PRODUCTO)";
            // 
            // datos
            // 
            this.datos.AllowUserToAddRows = false;
            this.datos.AllowUserToDeleteRows = false;
            this.datos.AllowUserToResizeColumns = false;
            this.datos.AllowUserToResizeRows = false;
            this.datos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.datos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.datos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.datos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Colum1,
            this.Column1,
            this.Column2});
            this.datos.Location = new System.Drawing.Point(9, 135);
            this.datos.MultiSelect = false;
            this.datos.Name = "datos";
            this.datos.ReadOnly = true;
            this.datos.Size = new System.Drawing.Size(363, 263);
            this.datos.TabIndex = 13;
            this.datos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datos_CellClick);
            this.datos.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.datos_CellEnter);
            this.datos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datos_KeyDown);
            // 
            // Colum1
            // 
            this.Colum1.HeaderText = "NOMBRE";
            this.Colum1.Name = "Colum1";
            this.Colum1.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(290, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBusqueda.Location = new System.Drawing.Point(9, 85);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(366, 20);
            this.txtBusqueda.TabIndex = 11;
            this.txtBusqueda.TextChanged += new System.EventHandler(this.txtBusqueda_TextChanged);
            this.txtBusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBusqueda_KeyDown);
            this.txtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusqueda_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "CATÁLOGO DE BUSQUEDA";
            // 
            // btnseleccionar
            // 
            this.btnseleccionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnseleccionar.Location = new System.Drawing.Point(86, 18);
            this.btnseleccionar.Name = "btnseleccionar";
            this.btnseleccionar.Size = new System.Drawing.Size(120, 27);
            this.btnseleccionar.TabIndex = 2;
            this.btnseleccionar.Text = "SELECCIONAR ";
            this.btnseleccionar.UseVisualStyleBackColor = true;
            this.btnseleccionar.Click += new System.EventHandler(this.btnseleccionar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Menu;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btncerrar);
            this.panel2.Controls.Add(this.btnseleccionar);
            this.panel2.Location = new System.Drawing.Point(9, 424);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(366, 64);
            this.panel2.TabIndex = 15;
            // 
            // btncerrar
            // 
            this.btncerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncerrar.Location = new System.Drawing.Point(221, 18);
            this.btncerrar.Name = "btncerrar";
            this.btncerrar.Size = new System.Drawing.Size(115, 27);
            this.btncerrar.TabIndex = 3;
            this.btncerrar.Text = "CERRAR";
            this.btncerrar.UseVisualStyleBackColor = true;
            // 
            // frmCatProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 492);
            this.Controls.Add(this.BUSQUEDA);
            this.Controls.Add(this.datos);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtBusqueda);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCatProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCatPorductos";
            this.Shown += new System.EventHandler(this.frmCatProductos_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BUSQUEDA;
        private System.Windows.Forms.DataGridView datos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colum1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnseleccionar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btncerrar;
    }
}