using AtiendelosDestktop.forms;
using AtiendelosDestktop.herramientas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtiendelosDestktop
{
    public partial class login : Form
    {
        string id;
        bool cierra;

        public login()
        {
            InitializeComponent();

        }

        private void login_Load(object sender, EventArgs e)
        {
            string query = "SELECT nombre,id FROM empresas;";
            List<Dictionary<string, object>> res = globales.consulta(query);
            foreach(var item in res)
            {
                string nombre_empresa = Convert.ToString(item["nombre"]);
                comboBox1.Items.Add(nombre_empresa);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string login = $"SELECT * FROM users where login='{txtUsr.Text}' and password='{txtPasword.Text}' and id_empresa={this.id};";
            List<Dictionary<string, object>> resultado = globales.consulta(login);
            if (resultado.Count <= 0) return;



            frmMenu menu = new frmMenu(this.id);
            menu.Show();


            globales.menuPrincipal = menu;

            this.Hide();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valida = $"select id from empresas where nombre='{comboBox1.Text}'";
            List<Dictionary<string, object>> valid = globales.consulta(valida);
                this.id = string.Empty;
            id = Convert.ToString(valid[0]["id"]);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtUsr.Text) || string.IsNullOrWhiteSpace(txtPasword.Text) || string.IsNullOrWhiteSpace(comboBox1.Text)) return;
                globales.usuario = txtUsr.Text;
                globales.password = txtPasword.Text;
                globales.id_sucursal = Convert.ToInt32( this.id) ;

                    string ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"/credenciales.txt";
                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    string guardar = $"usuario:{globales.usuario.Trim()}|password:{globales.password.Trim()}|id_sucursal:{globales.id_sucursal}|";
                    StreamWriter escribir = new StreamWriter(ruta);
                    escribir.WriteLine(guardar);
                    escribir.Close();

                
            }
        }

        private void login_Shown(object sender, EventArgs e)
        {

            if (globales.acceso == true) return;
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"/credenciales.txt";
            if (File.Exists(ruta))
            {
                try
                {
                    StreamReader leer = new StreamReader(ruta);
                    string texto = leer.ReadToEnd();
                    leer.Close();
                    string[] split = texto.Split('|');
                    string usuario = split[0].Split(':')[1];
                    string pass = split[1].Split(':')[1];
                    string id_sucursal = split[2].Split(':')[1];


                    txtUsr.Text = usuario;
                    txtPasword.Text = pass;
                    this.id = id_sucursal;
                    pictureBox2_Click(null, null);
                }
                catch (Exception ee) {

                }
            }
        }
    }
}
