using AtiendelosDestktop.herramientas;
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

namespace AtiendelosDestktop.forms.reportes
{
    public partial class Ventas : Form
    {
        string id_empresaPrincipal;
        List<Dictionary<string, object>> lista;
        string id_sucursal;


        public Ventas(string id_empresa)
        {
            InitializeComponent();
            this.id_empresaPrincipal = id_empresa;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
           string query = $"SELECT nombre,id FROM sucursales WHERE id_empresa={this.id_empresaPrincipal}";
            this.lista = globales.consulta(query);
            foreach(var item in lista)
            {
                string nombre= Convert.ToString(item["nombre"]);
                string id = Convert.ToString(item["id"]);
                comboBox1.Items.Add(nombre);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
   

         

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

       
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
   
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            foreach (var item in this.lista)
            {
                string nombre = Convert.ToString(item["nombre"]);

                if (nombre == comboBox1.Text)
                {
                    this.id_sucursal = Convert.ToString(item["id"]);
                    continue;

                }
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
           
    }

        private void button1_Click_2(object sender, EventArgs e)
        {
            DateTime f1 = Convert.ToDateTime(fecha1.Value);
            DateTime f2 = Convert.ToDateTime(fecha2.Value);

            string fecha1r = String.Format("{0:yyyy-MM-dd}", f1);
            string fecha2r = String.Format("{0:yyyy-MM-dd}", f2);


            string query = $"SELECT id_folio, a1.nombre as mesa, tipo_pago, total, id_user, a2.nombre FROM historico_tickets a1 JOIN  users a2 ON a1.id_user = a2. id WHERE  (a1.fecha between '{fecha1r}' and '{fecha2r}') AND a1.cancelado = FALSE AND a1.id_sucursal = {this.id_sucursal} AND a1.id_empresa = {this.id_empresaPrincipal};";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            object[] aux1 = new object[resultado.Count];
            int contador1 = 0;

            foreach (var item in resultado)
            {
                string folio = Convert.ToString(item["id_folio"]);
                string mesa = Convert.ToString(item["mesa"]);
                string tipo_pago = Convert.ToString(item["tipo_pago"]);
                if (tipo_pago == "E") tipo_pago = "EFECTIVO";
                if (tipo_pago == "T") tipo_pago = "TARJETA";
                if (tipo_pago == "O") tipo_pago = "OTROS";
                string total = Convert.ToString(item["total"]);
                string nombre = Convert.ToString(item["nombre"]);


                object[] tt1 = { folio, nombre, mesa, tipo_pago, total };

                aux1[contador1] = tt1;
                contador1++;
            }

            object[] parametros = { "sucursal", "titulo" };
            object[] valor = { comboBox1.Text, "Periodo: " + fecha1.Text + " al " + fecha2.Text };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;





            ReportViewer reporte = globales.reportesParaPanel("ventas", "ventas", aux1, "", false, enviarParametros);
            reporte.Dock = DockStyle.Fill;
            this.panel2.Controls.Clear();
            this.panel2.Controls.Add(reporte);


            this.Cursor = Cursors.Default;
        }
    }
    }
