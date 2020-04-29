using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtiendelosDestktop.forms
{
    public partial class frmPortada : Form
    {

        string id_empresaPrincipal;

        public frmPortada()
        {
            InitializeComponent();
            timer1.Enabled = true;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            timer1.Enabled = true;
        }

    

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtHora.Text = DateTime.Now.ToString("hh:mm:ss");

        }
    }
}
