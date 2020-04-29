using AtiendelosDestktop.forms.reportes;
using AtiendelosDestktop.herramientas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtiendelosDestktop.forms
{
   
    public partial class frmMenu : Form
    {
        string id_obtenido;
        bool bandera =true;
        public frmMenu(string id)
        {
            InitializeComponent();
            this.id_obtenido = id;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btninicio_Click(null, e);
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {

            SubmenuReportes.Visible = true;
            this.MenuVertical.Size = new System.Drawing.Size(304, 612);
            this.btninicio.Size = new System.Drawing.Size(304, 102);

        }

        private void btnrptventa_Click_1(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;

            AbrirFormEnPanel(new Ventas(this.id_obtenido));

        }

        private void btnrptcompra_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
        }

        private void btnrptpagos_Click_1(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
            AbrirFormEnPanel(new frmGastos(this.id_obtenido));

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void AbrirFormEnPanel(object formhija)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formhija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
            this.MenuVertical.Size = new System.Drawing.Size(217, 612);
            this.btninicio.Size = new System.Drawing.Size(220, 102);

        }

        private void btnproductos_Click(object sender, EventArgs e)
        {
            SubMenuProductos.Visible = true;
            this.MenuVertical.Size = new System.Drawing.Size(304, 612);
            this.btninicio.Size = new System.Drawing.Size(304, 102);
        }

        private void btninicio_Click(object sender, EventArgs e)
        {
           // AbrirFormEnPanel(new inicio());
        }

        private void frmMenu_Shown(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmPortada());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            globales.acceso = bandera;
            login inicio = new login();
            inicio.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
            AbrirFormEnPanel(new frmCorteInventario(this.id_obtenido));
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
            AbrirFormEnPanel(new frmCorteInventario(this.id_obtenido));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SubMenuProductos.Visible = false;
            AbrirFormEnPanel(new frmCategorias(this.id_obtenido));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SubMenuProductos.Visible = false;
            AbrirFormEnPanel(new frmProductos(this.id_obtenido));

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SubMenuProductos.Visible = false;
                
        }

        private void btnrptcompra_Click_1(object sender, EventArgs e)
        {

        }

        private void MenuVertical_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmInventarios(this.id_obtenido));

        }
    }
    
}