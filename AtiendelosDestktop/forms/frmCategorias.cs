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

namespace AtiendelosDestktop.forms
{
    public partial class frmCategorias : Form
    {

        string id_empresaPrincipal;
        int r;
        int c;
        int rs;
        int cs;
        private bool teclaEnter;
        string id_categ;

        public frmCategorias(string id_empresa)
        {
            InitializeComponent();
            this.id_empresaPrincipal = id_empresa;

        }

        private void frmCategorias_Shown(object sender, EventArgs e)
        {
            RellenaGridCateg();
        }

        private void RellenaGridCateg()
        {
            dataCategorias.Rows.Clear();
            string query = $"SELECT * FROM categoria where id_empresa={this.id_empresaPrincipal} order by id desc;";
            List<Dictionary<string, object>> categorias = globales.consulta(query);
            if (categorias.Count <= 0) return;
            foreach (var item in categorias)
            {
                string nombre = Convert.ToString(item["nombre"]);
                string descripcion = Convert.ToString(item["descripcion"]);
                string submenu = Convert.ToString(item["submenu"]);
                string boton = string.Empty;
                if (submenu == "True")
                {
                    boton = "✔";
                }
                else
                {
                    boton = "✘";

                }
                string id = Convert.ToString(item["id"]);

                dataCategorias.Rows.Add(nombre, descripcion, boton, id);
            }
        }

        private void dataCategorias_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            if (r == -1) return;
            DataGridViewRow row = dataCategorias.Rows[r];
            c = e.ColumnIndex;
        }

        private void dataCategorias_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.r + 1;
                var y = dataCategorias.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }
            string id = string.Empty; ;
            string subcateg = string.Empty;
            string nombre = string.Empty;
            string descripcion = string.Empty;
            try
            {
                nombre = Convert.ToString(dataCategorias.Rows[r].Cells[0].Value);
                descripcion = Convert.ToString(dataCategorias.Rows[r].Cells[1].Value);
                subcateg = Convert.ToString(dataCategorias.Rows[r].Cells[2].Value);
                id = Convert.ToString(dataCategorias.Rows[r].Cells[3].Value);

                   
            }
            catch
            {

            }
            if (string.IsNullOrWhiteSpace(id)) return;
            if(subcateg== "✔" || subcateg == "✘")
            {
                dataSubCateg.Visible = true;
                label3.Visible = true;
               
            }

            //Actualiza

            if (string.IsNullOrWhiteSpace(id)) return;

            string query = $"update  categoria set nombre='{nombre}' , descripcion='{descripcion}'  where id={id}; ";
            globales.consulta(query);


        }

        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void dataCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string subcateg = string.Empty;

            if (c == 2)
            {
                subcateg = Convert.ToString(dataCategorias.Rows[r].Cells[2].Value);
                this.id_categ = Convert.ToString(dataCategorias.Rows[r].Cells[3].Value);
                if (string.IsNullOrWhiteSpace(subcateg)) return;
                if (subcateg == "✔" )
                {

                        dataSubCateg.Visible = true;
                        label3.Visible = true;
                        pictureBox1.Visible = true;
                        llenaSubCateg();   
                }

                if (subcateg == "✘")
                {
                    DialogResult dialogo = globales.MessageBoxQuestion("¿DESEA AGREGAR UNA SUBCATEGORÍA?", "AVISO", globales.menuPrincipal);
                    if (dialogo==DialogResult.Yes)
                    {
                        dataSubCateg.Rows.Clear();
                        dataSubCateg.Visible = true;
                        label3.Visible = true;
                        pictureBox1.Visible = true;
                        string id = Convert.ToString(dataCategorias.Rows[r].Cells[3].Value);

                        string query = $"update  categoria set submenu='t'  where id={id} and id_empresa={this.id_empresaPrincipal}; ";
                        globales.consulta(query);
                        dataCategorias.Rows[r].Cells[2].Value = "✔";
                        dataSubCateg.Focus();

                    }
                }

            }
        }

        private void llenaSubCateg()
        {
            dataSubCateg.Rows.Clear();

            string query = $"select * from subcategoria where id_categoria={this.id_categ} order by id desc";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
           foreach(var item in resultado)
            {
                string nombre = Convert.ToString(item["nombre"]);
                string id = Convert.ToString(item["id"]);

                dataSubCateg.Rows.Add(nombre, id);
            }

        }

        private void dataCategorias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Insert)
            {
                string boleano = string.Empty;
                DialogResult dialogo = globales.MessageBoxQuestion("¿LA CATEGORÍA CONTENDRÁ SUBCATEGORÍAS?", "AVISO", globales.menuPrincipal);
                if (dialogo==DialogResult.Yes)
                {
                    boleano = "t";
                }
                else
                {
                    boleano = "f";
                }

                string query = $"insert into categoria (nombre,descripcion,submenu,id_empresa) values('NUEVO', 'NUEVO' ,'{boleano}' ,{this.id_empresaPrincipal});";
                globales.consulta(query,true);
                RellenaGridCateg();
                dataCategorias.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);


            }

            if (e.KeyCode==Keys.Delete)
            {
               
                DialogResult dialogo = globales.MessageBoxQuestion("¿DESEA BORRAR EL REGISTRO?", "AVISO", globales.menuPrincipal);
                if (dialogo==DialogResult.Yes)
                    {
                    string id_sub = Convert.ToString(dataCategorias.Rows[r].Cells[3].Value);
                    string query = $"delete from categoria where id={id_sub}";
                    globales.consulta(query);
                    RellenaGridCateg();

                }

            }
        }

        private void frmCategorias_KeyPress(object sender, KeyPressEventArgs e)
        {
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;

            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void dataCategorias_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);

        }

        private void dataSubCateg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Insert)
            {
                string query = $"insert into subcategoria(nombre , id_categoria) values ('NUEVO',{this.id_categ})" ;
                globales.consulta(query);
                llenaSubCateg();
                dataSubCateg.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);

            }

            if (e.KeyCode==Keys.Delete)
            {
                DialogResult dialogo = globales.MessageBoxQuestion("¿DESEA ELIMINAR EL REGISTRO?", "AVISO", globales.menuPrincipal);
                if (dialogo == DialogResult.No) return;
                string id = Convert.ToString(dataSubCateg.Rows[r].Cells[1].Value);
                string query = $"delete from subcategoria where id={id}";
                globales.consulta(query);
                llenaSubCateg();
            }
        }

        private void dataSubCateg_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string nombre = Convert.ToString(dataSubCateg.Rows[rs].Cells[0].Value);
            string id = Convert.ToString(dataSubCateg.Rows[rs].Cells[1].Value);
            string query = $"update subcategoria set nombre='{nombre}'where id={id}";
            globales.consulta(query);

        }

        private void dataSubCateg_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            rs = e.RowIndex;
            if (rs == -1) return;
            DataGridViewRow row = dataSubCateg.Rows[rs];
            cs = e.ColumnIndex;
        }
    }
}
