using AtiendelosDestktop.herramientas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


delegate void metodoEnviar( Dictionary<string, object> obj);

namespace AtiendelosDestktop.forms.catalogos
{
    public partial class frmCatProductos : Form
    {
        internal metodoEnviar enviar;
        string id_bodega;
        string id_empresaPrincipal;
        string id_sucursal;
        private List<Dictionary<string, object>> resultado = new List<Dictionary<string, object>>();
        private bool btnAceptarbool;
        private string folio;
        int c;
        int r;
        string id_inventario;




        public frmCatProductos(String id_bodegaMov, string id_empresaPrincipal , string id_sucursal)
        {
            InitializeComponent();
            this.id_bodega = id_bodegaMov;
            this.id_empresaPrincipal = id_empresaPrincipal;
            this.id_sucursal = id_sucursal;
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            if (resultado.Count == 0) return;
            if (string.IsNullOrWhiteSpace(this.id_inventario)) return;
            Close();
            this.btnAceptarbool = true;
            Dictionary<string, object> aux = null;
            try
            {
                string queruy = $"select * from inventario where id_inventario={this.id_inventario}";
                this.resultado = globales.consulta(queruy);
            }
            catch
            {

            }
         
            foreach (var item in resultado)
            {
              
                    aux = item;
                    
                
            }
            enviar(aux);
            this.Close();
        }

        private void frmCatProductos_Shown(object sender, EventArgs e)
        {
            string quey = $"select * from inventario where id_empresa={this.id_empresaPrincipal};";
            this.resultado = globales.consulta(quey);
            foreach(var item in this.resultado)
            {
                string nombre = Convert.ToString(item["descripcion"]);
                string unidad_medida = Convert.ToString(item["unidad_medida"]);
                string id = Convert.ToString(item["id_inventario"]);

                datos.Rows.Add(nombre, unidad_medida, id);
            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                datos.Focus();
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (e.KeyChar == 13)
                btnseleccionar_Click(null, null);
        }

        private void datos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                txtBusqueda.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnseleccionar_Click(null, null);
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string quey = $"select * from inventario where descripcion like '%{txtBusqueda.Text}%' AND  id_empresa={this.id_empresaPrincipal};";
            datos.Rows.Clear();
            this.resultado = globales.consulta(quey);
            foreach (var item in this.resultado)
            {
                string nombre = Convert.ToString(item["descripcion"]);
                string unidad_medida = Convert.ToString(item["unidad_medida"]);
                string id = Convert.ToString(item["id_inventario"]);

                datos.Rows.Add(nombre, unidad_medida, id);
            }
        }

        private void datos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            if (r == -1) return;
            DataGridViewRow row = datos.Rows[r];
            c = e.ColumnIndex;
        }

        private void datos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.id_inventario = Convert.ToString(datos.Rows[r].Cells[2].Value);
        }
    }
}
