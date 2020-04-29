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


    public partial class frmCorteInventario : Form
    {


        string id_empresaPrincipal;
        List<Dictionary<string, object>> lista;
        List<Dictionary<string, object>> bodegaslista;
        List<Dictionary<string, object>> resultado;

        string id_bodega;
        string id_inventarioFiltro;

        string id_sucursal;

        public frmCorteInventario(string id_empresa)
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

            string bodegas = $"SELECT nombre_bodega, id_bodega FROM bodegas where id_sucursal={this.id_sucursal} and id_empresa={this.id_empresaPrincipal} ;";
            this.bodegaslista = globales.consulta(bodegas);
            if (bodegaslista.Count <= 0) return;
            foreach (var che in bodegaslista)
            {
                string nombre = Convert.ToString(che["nombre_bodega"]);
                comboBox2.Items.Add(nombre);
            }
            comboBox2.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string query = "SELECT DISTINCT(id_inventario) , descripcion, unidad_medida FROM inventario;";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            object[] aux1 = new object[resultado.Count];
            int contador1 = 0;

            foreach (var item in resultado)
            {
                string id_inventario = Convert.ToString(item["id_inventario"]);
                string descripcion = Convert.ToString(item["descripcion"]);
                string unidad_medida = Convert.ToString(item["unidad_medida"]);

                string corte = $"SELECT SUM (cantidad) AS cantidad  FROM ( SELECT COALESCE (SUM(cantidad), 0) AS cantidad	FROM control_movimientos WHERE	tipo_mov IN ('E', 'OC') AND id_inventario = {id_inventario} AND id_sucursal = {this.id_sucursal} UNION SELECT((COALESCE(SUM(cantidad), 0))*- 1) AS cantidad	FROM control_movimientos WHERE	tipo_mov IN ('S', 'V')AND id_inventario = {id_inventario} AND id_sucursal ={this.id_sucursal} AND id_empresa={this.id_empresaPrincipal} and ubicacion={this.id_bodega}) AS A1;";
                List<Dictionary<string, object>> res = globales.consulta(corte);
                double total = 0.00;
                if (res.Count >= 0)
                {

                    continue;
                }
                else
                {
                    total = Convert.ToDouble(res[0]["cantidad"]);
                }

                object[] tt1 = { id_inventario, descripcion, comboBox2.Text, total };

                aux1[contador1] = tt1;
                contador1++;
            }

            object[] parametros = { "sucursal" };
            object[] valor = { comboBox1.Text };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;





            ReportViewer reporte = globales.reportesParaPanel("ventas", "ventas", aux1, "", false, enviarParametros);
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in this.bodegaslista)
            {
                string nombre = Convert.ToString(item["nombre_bodega"]);

                if (nombre == comboBox2.Text)
                {
                    this.id_bodega = Convert.ToString(item["id_bodega"]);
                    continue;

                }
            }
        }



        private void button1_Click_2(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
              

                    string corte_inv = $"select distinct (id_control) as id_control,fecha from control_movimientos where tipo_mov='C'  and id_inventario={this.id_inventarioFiltro}   order by fecha desc limit 2";
                    List<Dictionary<string, object>> fechas = globales.consulta(corte_inv);
                    string id_control = string.Empty;
                    string id_control1 = string.Empty;
                    bool bandera;

                    if (fechas.Count >= 2) // será entre fechas
                    {
                        id_control = Convert.ToString(fechas[1]["id_control"]);
                        id_control1 = Convert.ToString(fechas[0]["id_control"]);


                        bandera = true;
                    }
                    else   // solo hay una ;
                    {

                        id_control = Convert.ToString(fechas[1]["id_control"]);

                        bandera = false;
                    }


                string query = string.Empty;

                    if (bandera == false)
                    {
                     query = $"SELECT a1.cantidad , a1.observaciones , a1.fecha , a1.hora ,a2.unidad_medida  FROM control_movimientos a1 left join inventario a2  on a1.id_inventario=a2.id_inventario where a1.id_inventario={this.id_inventarioFiltro}  and id_sucursal={this.id_sucursal} and id_control<{id_control}   ;" ;
                        //  Periodo = $"PRIMER FECHA {fecha1}";
                    }
                    else
                    {
                     query = $"SELECT a1.cantidad , a1.observaciones , a1.fecha , a1.hora ,a2.unidad_medida,a2.descripcion  FROM control_movimientos a1 left join inventario a2  on a1.id_inventario=a2.id_inventario where a1.id_inventario={this.id_inventarioFiltro}  and id_sucursal={this.id_sucursal} and id_control>{id_control}  and id_control <{id_control1} ;";
                    // Periodo = $"del {fecha2} al {fecha1}";
                }
                List<Dictionary<string, object>> corte1 = globales.consulta(query);
                    double total = 0.0;
                object[] aux1 = new object[corte1.Count];
                int contador = 0;

                foreach (var item in corte1)
                {
                    string descripcion = Convert.ToString(item["descripcion"]);
                    string cantidad = Convert.ToString(item["cantidad"]);
                    string unidad_medida = Convert.ToString(item["unidad_medida"]);
                    string observaciones = Convert.ToString(item["observaciones"]);
                    string fecha = Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.","");
                    string hora = Convert.ToString(item["hora"]).Replace(".416416+00","");


                    object[] tt1 = {  descripcion, cantidad, unidad_medida, observaciones, fecha,hora };


                    aux1[contador] = tt1;
                    contador++;
                }
                   
                

                object[] parametros = { "sucursal", "titulo" };
                object[] valor = { comboBox1.Text, $"REPORTE FÍSICO DE INVENTARIO CON CORTE DESGLOSADO" };
                object[][] enviarParametros = new object[2][];

                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;





                ReportViewer reporte = globales.reportesParaPanel("Corte_Desglosado", "corte_desglosado", aux1, "", false, enviarParametros);
                reporte.Dock = DockStyle.Fill;
                this.panel2.Controls.Clear();
                this.panel2.Controls.Add(reporte);


                this.Cursor = Cursors.Default;




            }
            else
            {
                string query = $"SELECT DISTINCT(a1.id_inventario) , descripcion, unidad_medida FROM inventario a1 RIGHT JOIN control_movimientos a2 on a1.id_inventario=a2.id_inventario  where a1.id_empresa ={this.id_empresaPrincipal} ";
                this.resultado = globales.consulta(query);


                object[] aux1 = new object[resultado.Count];
                int contador1 = 0;


                string Periodo = string.Empty;

                foreach (var item in resultado)
                {
                    string id_inventario = Convert.ToString(item["id_inventario"]);
                    string descripcion = Convert.ToString(item["descripcion"]);
                    string unidad_medida = Convert.ToString(item["unidad_medida"]);
                    string corte = string.Empty;


                    string corte_inv = $"select distinct (id_control) as id_control,fecha from control_movimientos where tipo_mov='C'  and id_inventario={id_inventario}   order by fecha desc limit 2";
                    List<Dictionary<string, object>> fechas = globales.consulta(corte_inv);
                    string id_control = string.Empty;
                    string id_control1 = string.Empty;
                    bool bandera;

                    if (fechas.Count >= 2) // será entre fechas
                    {
                        id_control = Convert.ToString(fechas[1]["id_control"]);
                        id_control1 = Convert.ToString(fechas[0]["id_control"]);


                        bandera = true;
                    }
                    else   // solo hay una ;
                    {

                        id_control = Convert.ToString(fechas[1]["id_control"]);

                        bandera = false;
                    }




                    if (bandera == false)
                    {
                        corte = $"SELECT SUM (cantidad) AS cantidad  FROM ( SELECT COALESCE (SUM(cantidad), 0) AS cantidad	FROM control_movimientos WHERE	tipo_mov IN ('E', 'OC','C') AND id_inventario = {id_inventario} AND id_sucursal = {this.id_sucursal}  and ubicacion='{this.id_bodega}' AND id_control<={id_control}  UNION SELECT((COALESCE(SUM(cantidad), 0))*- 1) AS cantidad	FROM control_movimientos WHERE	tipo_mov IN ('S', 'V','C')AND id_inventario = {id_inventario} AND id_sucursal ={this.id_sucursal} AND id_empresa={this.id_empresaPrincipal} and ubicacion='{this.id_bodega}' AND id_control<={id_control} ) AS A1;";
                        //  Periodo = $"PRIMER FECHA {fecha1}";
                    }
                    else
                    {
                        corte = $"SELECT SUM (cantidad) AS cantidad  FROM ( SELECT COALESCE (SUM(cantidad), 0) AS cantidad	FROM control_movimientos WHERE	tipo_mov IN ('E', 'OC','C') AND id_inventario = {id_inventario} AND id_sucursal = {this.id_sucursal}  and ubicacion='{this.id_bodega}'    and id_control >={id_control} and id_control<={id_control1}   UNION SELECT((COALESCE(SUM(cantidad), 0))*- 1) AS cantidad	FROM control_movimientos WHERE	tipo_mov IN ('S', 'V','C')AND id_inventario = {id_inventario} AND id_sucursal ={this.id_sucursal} AND id_empresa={this.id_empresaPrincipal} and ubicacion='{this.id_bodega}'      and id_control >={id_control} and id_control<={id_control1}  ) AS A1;";
                        // Periodo = $"del {fecha2} al {fecha1}";
                    }
                    List<Dictionary<string, object>> corte1 = globales.consulta(corte);
                    double total = 0.0;
                    if (corte1.Count <= 0)
                    {

                        continue;
                    }
                    else
                    {
                        total = Convert.ToDouble(corte1[0]["cantidad"]);
                    }

                    object[] tt1 = { id_inventario, descripcion, comboBox2.Text, total, unidad_medida };

                    aux1[contador1] = tt1;
                    contador1++;
                }

                object[] parametros = { "sucursal", "titulo" };
                object[] valor = { comboBox1.Text, $"REPORTE FÍSICO DE INVENTARIO CON CORTE {Periodo}" };
                object[][] enviarParametros = new object[2][];

                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;





                ReportViewer reporte = globales.reportesParaPanel("corteInventario", "corte_inv", aux1, "", false, enviarParametros);
                reporte.Dock = DockStyle.Fill;
                this.panel2.Controls.Clear();
                this.panel2.Controls.Add(reporte);


                this.Cursor = Cursors.Default;
            }

    

          
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {


            {

                char S;

                S = Char.ToUpper(e.KeyChar);

                e.KeyChar = S;

            }
          
        }

        private void comboProductoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = $"select * from inventario where descripcion ='{comboProductoFiltro.Text}' and id_empresa ={this.id_empresaPrincipal}";
            List<Dictionary<string, object>> res = globales.consulta(query);
            if (res.Count <= 0) return;
            this.id_inventarioFiltro = Convert.ToString(res[0]["id_inventario"]);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboProductoFiltro.Visible = true;
            }
            else
            {
                comboProductoFiltro.Visible = false;

            }
        }

        private void comboProductoFiltro_DropDown(object sender, EventArgs e)
        {
            comboProductoFiltro.Items.Clear();
            string query = $"SELECT * FROM inventario where descripcion like '%{comboProductoFiltro.Text}%' and id_empresa ={this.id_empresaPrincipal}; ";
            List<Dictionary<string, object>> result = globales.consulta(query);
            foreach (var item in result)
            {
                string nombre = Convert.ToString(item["descripcion"]);
                comboProductoFiltro.Items.Add(nombre);
            }
        }
    }
}