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
    public partial class frmGastos : Form
    {

        string id_empresaPrincipal;
        List<Dictionary<string, object>> lista;
        string id_sucursal;

        public frmGastos(string id_empresa)
        {
            InitializeComponent();
            this.id_empresaPrincipal = id_empresa;

        }
        private void Ventas_Load(object sender, EventArgs e)
        {
            string query = $"SELECT nombre,id FROM sucursales WHERE id_empresa={this.id_empresaPrincipal}";
            this.lista = globales.consulta(query);
            foreach (var item in lista)
            {
                string nombre = Convert.ToString(item["nombre"]);
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
            string query = $"select a1.nombre as gasto, a1.total ,a1.id_cortecaja as folio,a3.login  from gastos a1 join corte_caja a2 ON a1.id_cortecaja =a2.id  join users a3 ON a1.id_usuario=a3.id where (a1.id_sucursal= {this.id_sucursal} and a1.id_empresa= {this.id_empresaPrincipal})and  a2.fecha between '{fecha1.Text}' and '{fecha2.Text}' order by a1.id_cortecaja";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            object[] aux1 = new object[resultado.Count];
            int contador1 = 0;

            foreach (var item in resultado)
            {
                string gasto = Convert.ToString(item["gasto"]);
                string total = Convert.ToString(item["total"]);
                string folio = Convert.ToString(item["folio"]);
                string login = Convert.ToString(item["login"]);


                object[] tt1 = { folio, total, folio, login };

                aux1[contador1] = tt1;
                contador1++;
            }

            object[] parametros = { "sucursal", "titulo" };
            object[] valor = { comboBox1.Text, "Periodo: " + fecha1.Text + " al " + fecha2.Text };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;





            ReportViewer reporte = globales.reportesParaPanel("reporte_gastos", "gastos", aux1, "", false, enviarParametros);
            reporte.Dock = DockStyle.Fill;
            this.panel2.Controls.Clear();
            this.panel2.Controls.Add(reporte);


            this.Cursor = Cursors.Default;
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

        private void button1_Click_2(object sender, EventArgs e)
        {

            DateTime f1 = Convert.ToDateTime(fecha1.Value);
            DateTime f2 = Convert.ToDateTime(fecha2.Value);

            string fecha1r = String.Format("{0:yyyy-MM-dd}", f1);
            string fecha2r = String.Format("{0:yyyy-MM-dd}", f2);


            string query = $"select a1.nombre as gasto, a1.total ,a1.id_cortecaja as folio,a3.login  from gastos a1 join corte_caja a2 ON a1.id_cortecaja =a2.id  join users a3 ON a1.id_usuario=a3.id where (a1.id_sucursal= {this.id_sucursal} and a1.id_empresa= {this.id_empresaPrincipal})and  a2.fecha between '{fecha1.Text}' and '{fecha2.Text}' order by a1.id_cortecaja";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            object[] aux1 = new object[resultado.Count];
            int contador1 = 0;

            foreach (var item in resultado)
            {
                string gasto = Convert.ToString(item["gasto"]);
                string total = Convert.ToString(item["total"]);
                string folio = Convert.ToString(item["folio"]);
                string login = Convert.ToString(item["login"]);


                object[] tt1 = { folio, total, folio, login };

                aux1[contador1] = tt1;
                contador1++;
            }

            object[] parametros = { "sucursal", "titulo" };
            object[] valor = { comboBox1.Text, "Periodo: " + fecha1.Text + " al " + fecha2.Text };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;





            ReportViewer reporte = globales.reportesParaPanel("reporte_gastos", "gastos", aux1, "", false, enviarParametros);
            reporte.Dock = DockStyle.Fill;
            this.panel2.Controls.Clear();
            this.panel2.Controls.Add(reporte);


            this.Cursor = Cursors.Default;
        }
    

    }
}
