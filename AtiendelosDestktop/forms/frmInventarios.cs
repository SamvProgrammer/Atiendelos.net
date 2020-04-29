using AtiendelosDestktop.forms.catalogos;
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
    public partial class frmInventarios : Form
    {
        string id_empresaPrincipal;
        int c;
        int r;
        private bool teclaEnter;
        private List<Dictionary<string, object>> resulta;
        string id_sucursal;
        private List<Dictionary<string, object>> listaId;
        private List<Dictionary<string, object>> listaBodega;
        string id_bodegaMov;
        private List<Dictionary<string, object>> BodegasLista;
        int ObtenIdBodega;
        string id_bodega_orden;
        int folio_orden;
        string sucursalOrden;


        public frmInventarios(string id)
        {
            InitializeComponent();
            this.id_empresaPrincipal = id;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {

            }
            if (tabControl1.SelectedIndex == 1)
            {

            }
            if (tabControl1.SelectedIndex == 2)
            {
                MessageBox.Show("Recetas");
            }
            if (tabControl1.SelectedIndex == 3)
            {
                IniciaBodega();
            }
            if (tabControl1.SelectedIndex == 4)
            {
                IniciaProveedor();
            }
            if (tabControl1.SelectedIndex == 5)
            {

                medidas();

            }

            if (tabControl1.SelectedIndex == 6)
            {
                IniciaOrden();
            }
        }

        private void frmInventarios_Shown(object sender, EventArgs e)
        {
            rellenaGridInv();
        }

        public void rellenaGridInv()
        {
            string query = $"SELECT * FROM inventario where id_empresa={this.id_empresaPrincipal};";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO HAY PRODUCTOS ASOCIADOS", "AVISO", globales.menuPrincipal);
                DialogResult dialogo1 = globales.MessageBoxInformation("AÑADA PARÁMETROS A BODEGAS , PROVEEDORES Y UNIDADES DE MEDIDA PARA COMENZAR", "INICIEMOS", globales.menuPrincipal);
                return;
            }

            dataInventario.Rows.Clear();
            foreach (var item in resultado)
            {
                string nombre = Convert.ToString(item["descripcion"]);
                string id_inventario = Convert.ToString(item["id_inventario"]);
                dataInventario.Rows.Add(nombre, id_inventario);
            }
        }


        private void dataInventario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            nuevoInv();
            try
            
            {
                btnOk.Text = "ACTUALIZAR";
                string id_inventario = Convert.ToString(dataInventario.Rows[r].Cells[1].Value);
                if (string.IsNullOrWhiteSpace(id_inventario)) return;
                string query = $"select * from inventario where id_empresa={this.id_empresaPrincipal} and id_inventario={id_inventario}";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (var item in resultado)
                {
                    txtCodigo.Text = Convert.ToString(item["codigo"]);
                    txtNombre.Text = Convert.ToString(item["descripcion"]);
                    comboMedida.Text = Convert.ToString(item["unidad_medida"]);
                    string id_proveedor = Convert.ToString(item["id_proveedor"]);
                    seleccionaProveedor(id_proveedor);
                    string ubicacion = Convert.ToString(item["ubicacion"]);
                    seleccionaBodega(ubicacion);
                    comboClasificación.Text = Convert.ToString(item["categoria"]);
                    int cost = Convert.ToInt32(item["costo_unitario"]);
                    txtCosto.Text = cost.ToString("C");
                    txtMinimo.Text = Convert.ToString(item["minimo"]);

                }
            }
            catch
            {

            }

        }


        private void seleccionaProveedor(string id_proveedor)
        {
            if (string.IsNullOrWhiteSpace(id_proveedor)) return;

            string query = $"select nombre from proveedores where id_empresa={this.id_empresaPrincipal} and id_proveedor ={id_proveedor}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            comboProveedor.Text = Convert.ToString(resultado[0]["nombre"]);

        }

        private void seleccionaBodega(string ubicacion)
        {
            if (string.IsNullOrWhiteSpace(ubicacion)) return;
            string query = $"select nombre_bodega from bodegas where id_empresa={this.id_empresaPrincipal} and id_bodega={ubicacion}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            comboBodega.Text = Convert.ToString(resultado[0]["nombre_bodega"]);


        }


        private void dataInventario_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            if (r == -1) return;
            DataGridViewRow row = dataInventario.Rows[r];
            c = e.ColumnIndex;
        }

        private void dataInventario_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.r + 1;
                var y = dataInventario.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }


        }

        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void dataInventario_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);

        }

        private void frmInventarios_KeyPress(object sender, KeyPressEventArgs e)
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

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
           
        }


        private void nuevoInv()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            comboMedida.Items.Clear();
            comboProveedor.Items.Clear();
            txtMinimo.Text = "";
            comboBodega.Items.Clear();
            txtCosto.Text = "";
            comboProveedor.Items.Clear();
            comboMedida.Items.Clear();
            comboBodega.Items.Clear();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

          
        }

        private void dataInventario_KeyDown(object sender, KeyEventArgs e)
        {

            if (tabControl1.SelectedIndex == 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    string id_inventario = Convert.ToString(dataInventario.Rows[r].Cells[1].Value);
                    if (string.IsNullOrWhiteSpace(id_inventario)) return;
                    string query = $"delete from inventario where id_inventario={id_inventario} ";
                    DialogResult dialogo = globales.MessageBoxQuestion("¿DESEA ELIMINAR EL PRODUCTO INVENTARIADO?", "AVISO", globales.menuPrincipal);
                    if (dialogo == DialogResult.Yes)
                    {
                        globales.consulta(query);
                        rellenaGridInv();
                        nuevoInv();
                        btnOk.Text = "-";
                    }
                }




            }




        }


        public void medidas()
        {
            string query = $"select * from medidas where id_empresa={this.id_empresaPrincipal} order by id desc";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO EXISTEN REGISTROS DE MÉDIDAS", "AVISO", globales.menuPrincipal);
                return;
            }
            dataMedidas.Rows.Clear();

            foreach (var item in resultado)
            {
                string nombre = Convert.ToString(item["nombre"]);
                string id = Convert.ToString(item["id"]);

                dataMedidas.Rows.Add(nombre, id);
            }
        }

        private void comboMedida_Enter(object sender, EventArgs e)
        {
            string medidas = $"select nombre from medidas where id_empresa={this.id_empresaPrincipal}";
            List<Dictionary<string, object>> res = globales.consulta(medidas);
            if (res.Count <= 0) return;

            foreach (var item in res)
            {
                string nombre = Convert.ToString(item["nombre"]);
                comboMedida.Items.Add(nombre);
            }
        }

        private void comboProveedor_Enter(object sender, EventArgs e)
        {
            string proveedor = $"select nombre from proveedores where id_empresa={this.id_empresaPrincipal}";
            List<Dictionary<string, object>> resultado = globales.consulta(proveedor);
            if (resultado.Count <= 0) return;
            foreach (var item in resultado)
            {
                string nombre = Convert.ToString(item["nombre"]);
                comboProveedor.Items.Add(nombre);
            }

        }

        private void dataMedidas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            if (r == -1) return;
            DataGridViewRow row = dataInventario.Rows[r];
            c = e.ColumnIndex;
        }

        private void dataMedidas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.r + 1;
                var y = dataInventario.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }
            string nombre = Convert.ToString(dataMedidas.Rows[r].Cells[c].Value);
            string id = Convert.ToString(dataMedidas.Rows[r].Cells[1].Value);
            string actualiza = $"update medidas set nombre='{nombre}' where id={id} and id_empresa={this.id_empresaPrincipal}";
            globales.consulta(actualiza);
            dataMedidas.Rows.Clear();
            medidas();

        }

        private void dataMedidas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                string inserta = $"insert into medidas (nombre,id_empresa) values ('NUEVO',{this.id_empresaPrincipal})";
                globales.consulta(inserta);
                medidas();
                dataMedidas.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
            }

        }

        private void IniciaProveedor()
        {
            string query = $"select * from proveedores where id_empresa={this.id_empresaPrincipal}";
            List<Dictionary<string, object>> resu = globales.consulta(query);
            if (resu.Count <= 0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO EXISTEN PROVEEDORES REGISTRADOS", "AVISO", globales.menuPrincipal);
                return;
            }
            dataProveedores.Rows.Clear();
            foreach (var item in resu)
            {
                string nombre = Convert.ToString(item["nombre"]);
                string descripcion = Convert.ToString(item["descripcion"]);
                string domicilio = Convert.ToString(item["domicilio"]);
                string rfc = Convert.ToString(item["rfc"]);
                string correo = Convert.ToString(item["correo"]);
                string telefono = Convert.ToString(item["telefono"]);
                string id_proveedor = Convert.ToString(item["id_proveedor"]);

                dataProveedores.Rows.Add(nombre, descripcion, domicilio, rfc, correo, telefono, id_proveedor);
            }
        }

        private void dataProveedores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string id_proveedor = Convert.ToString(dataProveedores.Rows[r].Cells[6].Value);
                if (string.IsNullOrWhiteSpace(id_proveedor)) return;
                string query = $"delete from proveedores where id_proveedor={id_proveedor}";
                globales.consulta(query);
            }

            if (e.KeyCode == Keys.Insert)
            {

                try
                {

                    string inserta = $"insert into proveedores (nombre,descripcion,domicilio,rfc,correo,telefono,id_empresa) values ('NUEVO','-','-','-','-','-',{this.id_empresaPrincipal})";
                    globales.consulta(inserta);
                    dataProveedores.Rows.Clear();
                    IniciaProveedor();
                }
                catch
                {

                }

            }
        }

        private void dataProveedores_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            if (r == -1) return;
            DataGridViewRow row = dataInventario.Rows[r];
            c = e.ColumnIndex;
        }

        private void dataProveedores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.r + 1;
                var y = dataInventario.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }

            try
            {
                string nombre = Convert.ToString(dataProveedores.Rows[r].Cells[0].Value);
                string descripcion = Convert.ToString(dataProveedores.Rows[r].Cells[1].Value);
                string domicilio = Convert.ToString(dataProveedores.Rows[r].Cells[2].Value);
                string rfc = Convert.ToString(dataProveedores.Rows[r].Cells[3].Value);
                string correo = Convert.ToString(dataProveedores.Rows[r].Cells[4].Value);
                string telefono = Convert.ToString(dataProveedores.Rows[r].Cells[5].Value);
                string id = Convert.ToString(dataProveedores.Rows[r].Cells[6].Value);


                string update = $"update proveedores set nombre='{nombre}',descipcion='{descripcion}',domicilio='{domicilio}',rfc='{rfc}',correo='{correo}',telefono='{telefono}' where id_proveedor={id}";
                globales.consulta(update);

            }
            catch
            {

            }


        }


        public void IniciaBodega()
        {
            string query = $"select * from bodegas where id_empresa={this.id_empresaPrincipal}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO SE ENCUENTRAN BODEGA RESGITRADAS", "AVISO", globales.menuPrincipal);
                return;
            }

            dataBodegas.Rows.Clear();

            foreach (var item in resultado)
            {
                string nombre_bodega = Convert.ToString(item["nombre_bodega"]);
                string ubicacion_bodega = Convert.ToString(item["ubicacion_bodega"]);
                string responsable = Convert.ToString(item["responsable_bodega"]);
                string sucursal = Convert.ToString(item["id_sucursal"]);

                string id_bodega = Convert.ToString(item["id_bodega"]);


                dataBodegas.Rows.Add(nombre_bodega, ubicacion_bodega, responsable, id_bodega);

            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.r + 1;
                var y = dataInventario.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }


        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            if (r == -1) return;
            DataGridViewRow row = dataInventario.Rows[r];
            c = e.ColumnIndex;

        }

        private void dataBodegas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id_bodega = Convert.ToString(dataBodegas.Rows[r].Cells[3].Value);
            if (string.IsNullOrWhiteSpace(id_bodega)) return;
            string quer = $"select * from bodegas where id_bodega ={id_bodega}";
            List<Dictionary<string, object>> res = globales.consulta(quer);
            txtNombre_bodega.Text = Convert.ToString(res[0]["nombre_bodega"]);
            txtDomicilio_bodega.Text = Convert.ToString(res[0]["ubicacion_bodega"]);
            TxtResponsbable.Text = Convert.ToString(res[0]["responsable_bodega"]);
            string id_sucursal = Convert.ToString(res[0]["id_sucursal"]);
            string querysusc = $"select nombre from sucursales where id_empresa={this.id_empresaPrincipal} ";
            List<Dictionary<string, object>> resultado = globales.consulta(querysusc);
            ComboSucursal.Items.Add(Convert.ToString(resultado[0]["nombre"]));
            ComboSucursal.SelectedIndex = 0;
            btnOk_bodega.Text = "ACTUALIZAR";

        }

        private void dataBodegas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string id_bodega = Convert.ToString(dataBodegas.Rows[r].Cells[3].Value);
                if (string.IsNullOrWhiteSpace(id_bodega)) return;

                DialogResult dialogoo = globales.MessageBoxQuestion("¿DESEA ELIMINAR EL REGISTRO?", "AVISO", globales.menuPrincipal);
                if (dialogoo == DialogResult.Yes)
                {
                    string query = $"select * from bodegas where id_bodega={id_bodega}";
                    globales.consulta(query);
                    IniciaBodega();
                }

            }


        }

      
        private void ComboSucursakl_Enter(object sender, EventArgs e)
        {
          
        }

        private void dataBodegas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnOk_bodega_Click(object sender, EventArgs e)
        {
          
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboSucursalMovimientos.Text) && string.IsNullOrWhiteSpace(comboBodegaMovimientos.Text))
            {
                DialogResult dialogo = globales.MessageBoxExclamation("CONFIGURE LA SUCURSAL Y BODEG ANTES DE INICIAR", "AVISO", globales.menuPrincipal);
                return;
            }
            if (rbEntrada.Checked)
            {
                dataMovimientos.Enabled = true;
                dataMovimientos.Focus();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCorte.Checked)
            {
                dataMovimientos.Enabled = false;
                if (string.IsNullOrWhiteSpace(comboSucursalMovimientos.Text)) return;

                DialogResult dialogo = globales.MessageBoxQuestion("Este proceso resetea los valores de los productos inventariados", "Generar Corte", globales.menuPrincipal);
                if (dialogo == DialogResult.No) return;
                string query = $"SELECT	distinct(a2.id_inventario)FROM	inventario a1 left JOIN control_movimientos a2 ON a1.id_inventario = a2.id_inventario WHERE	a2.id_empresa ={this.id_empresaPrincipal} AND a2.id_sucursal ={this.id_sucursal}";
                this.listaId = globales.consulta(query);
                foreach (var item in this.listaId)
                {
                    string inserta = $"insert into control_movimientos (cantidad,tipo_mov,id_inventario,ubicacion,id_sucursal,id_usuario,observaciones,fecha,hora,id_empresa) values (0,'C',{Convert.ToString(item["id_inventario"])},{this.id_bodegaMov},{this.id_sucursal},100,'CORTE',current_date,current_time,{this.id_empresaPrincipal});";
                    try

                    {
                        globales.consulta(inserta);
                    }
                    catch
                    {

                    }
                }

            }
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            try
            {
                comboSucursalMovimientos.Items.Clear();
                string query = $"select * from sucursales where id_empresa={this.id_empresaPrincipal}";
                this.resulta = globales.consulta(query);

                foreach (var ite in this.resulta)

                {
                    string nombre = Convert.ToString(ite["nombre"]);
                    comboSucursalMovimientos.Items.Add(nombre);
                }
            }
            catch
            {

            }

        }

        private void comboBodegaMovimientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in this.resulta)
            {
                if (Convert.ToString(item["nombre"]) == comboSucursalMovimientos.Text)
                {
                    this.id_sucursal = Convert.ToString(item["id"]);
                }
            }
        }

        private void comboBox1_Enter_1(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM bodegas where id_empresa={this.id_empresaPrincipal} and id_sucursal={this.id_sucursal}; ";
            if (string.IsNullOrWhiteSpace(this.id_sucursal)) return;
            this.listaBodega = globales.consulta(query);
            comboBodegaMovimientos.Items.Clear();
            foreach (var item in this.listaBodega)
            {
                string nombre = Convert.ToString(item["nombre_bodega"]);
                comboBodegaMovimientos.Items.Add(nombre);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBodegaMovimientos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            foreach (var item in this.listaBodega)
            {
                string nombre = Convert.ToString(item["nombre_bodega"]);
                if (nombre == comboBodegaMovimientos.Text)
                {
                    this.id_bodegaMov = Convert.ToString(item["id_bodega"]);

                }
            }
        }

        private void dataMovimientos_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (e.KeyCode == Keys.Insert)
                {
                    frmCatProductos catalogo = new frmCatProductos(this.id_bodegaMov, this.id_empresaPrincipal, this.id_sucursal);
                    catalogo.enviar = llenacampos;
                catalogo.Show();
                }

                if (e.KeyCode==Keys.Down)
            {
                dataMovimientos.Rows.Insert(0);
            }
        }

        private void dataMovimientos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            if (r == -1) return;
            c = e.ColumnIndex;
        }

        private void llenacampos(Dictionary<string, object> datos)
        {

            string nombre = Convert.ToString(datos["descripcion"]);
                dataMovimientos.Rows[r].Cells[0].Value = nombre;
            string unidad_medida = Convert.ToString(datos["unidad_medida"]);
            dataMovimientos.Rows[r].Cells[1].Value = unidad_medida;
            string id_inventraio = Convert.ToString(datos["id_inventario"]);
            dataMovimientos.Rows[r].Cells[3].Value = id_inventraio;




        }

        private void comboBodega_Enter(object sender, EventArgs e)
        {
            string traeBodega = $"select * from bodegas where id_empresa={this.id_empresaPrincipal}";
           this.BodegasLista = globales.consulta(traeBodega);
            if (BodegasLista.Count <= 0) return;
            comboBodega.Items.Clear();
            foreach(var item in BodegasLista)
            {
                string nombre = Convert.ToString(item["nombre_bodega"]);
                comboBodega.Items.Add(nombre);

            }
        }

        private void comboMedida_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void comboProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void comboClasificación_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void comboBodega_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void comboBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
          foreach(var item in this.BodegasLista)
            {
                string nombre_bodega = Convert.ToString(item["nombre_bodega"]);
                if (nombre_bodega==comboBodega.Text)
                {
                    this.ObtenIdBodega = Convert.ToInt32(item["id_bodega"]);
                }
            }
        }

        private void rbSalida_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSalida.Checked)
            {


                if (string.IsNullOrWhiteSpace(comboSucursalMovimientos.Text) && string.IsNullOrWhiteSpace(comboBodegaMovimientos.Text))
                {
                    DialogResult dialogo = globales.MessageBoxExclamation("CONFIGURE LA SUCURSAL Y BODEG ANTES DE INICIAR", "AVISO", globales.menuPrincipal);
                    return;
                }
                dataMovimientos.Rows.Clear();
                dataMovimientos.Enabled = true;
                dataMovimientos.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string movimiento = string.Empty;
            string observaciones = string.Empty;
            if (rbEntrada.Checked)
            {
                movimiento = "E";
                observaciones = "ENTRADA";
            }
            if (rbSalida.Checked)
            {
                observaciones = "SALIDA";
                movimiento = "S";

            }
            foreach (DataGridViewRow row in dataMovimientos.Rows)
            {
                try
                {
                    string cantidad = row.Cells[2].Value.ToString();
                    string id_inventario = row.Cells[3].Value.ToString();


                    string inserta = $"insert into control_movimientos (cantidad,tipo_mov,id_inventario,ubicacion,id_sucursal,observaciones,fecha,hora,id_empresa) values ({cantidad},'{movimiento}',{id_inventario},{this.id_bodegaMov},{this.id_sucursal},'{observaciones}',current_date, current_time,{this.id_empresaPrincipal});";
                    globales.consulta(inserta);
                }
                catch
                {
                    return;
                }
              

            }

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            nuevoInv();
            btnOk.Text = "INSERTAR";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = $"select id_bodega from bodegas where nombre_bodega='{comboBodega.Text}' and id_empresa='{this.id_empresaPrincipal}' ";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string id_bodega = Convert.ToString(resultado[0]["id_bodega"]);

            string quey1 = $"select id_proveedor from proveedores where nombre='{comboProveedor.Text}' and id_empresa={this.id_empresaPrincipal}";
            List<Dictionary<string, object>> res = globales.consulta(quey1);
            string id_proveedor = Convert.ToString(res[0]["id_proveedor"]);

            string id_inventario = Convert.ToString(dataInventario.Rows[r].Cells[1].Value);


            if (btnOk.Text == "ACTUALIZAR")
            {

                string actualiza = $"update inventario set codigo='{txtCodigo.Text}', descripcion= '{txtNombre.Text}', unidad_medida='{comboMedida.Text}', ubicacion={ObtenIdBodega}, categoria='{comboClasificación.Text}',minimo={txtMinimo.Text},costo_unitario={txtCosto.Text} where id_inventario ={id_inventario}";
                globales.consulta(query, true);
                rellenaGridInv();
                nuevoInv();
                btnOk.Text = "-";
            }

            if (btnOk.Text == "INSERTAR")
            {
                string inserta = $"insert into inventario (unidad_medida,descripcion,ubicacion,categoria,minimo,id_empresa,codigo,id_proveedor,costo_unitario) values ('{comboMedida.Text}','{txtNombre.Text}',{ObtenIdBodega},'{comboClasificación.Text}',{txtMinimo.Text},{this.id_empresaPrincipal},'{txtCodigo.Text}',{id_proveedor},{txtCosto.Text})  ";
                globales.consulta(inserta, true);
                rellenaGridInv();
                nuevoInv();
                btnOk.Text = "-";

            }
        }

        private void btnNuevo_bodega_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre_bodega.Text) || string.IsNullOrWhiteSpace(txtDomicilio_bodega.Text) || string.IsNullOrWhiteSpace(comboBodega.Text))
                return;
            string obtiene = $"select id from sucursales where nombre='{comboBodega.Text}' ";
            List<Dictionary<string, object>> re = globales.consulta(obtiene);
            string id_sucursal = Convert.ToString(re[0]["id"]);


            if (btnOk_bodega.Text == "INSERTAR")
            {
                string inserta = $"insert into bodegas (nombre_bodega,ubicacion_bodega,responsable_bodega,id_empresa,id_sucursal) values ('{txtNombre_bodega.Text}','{txtDomicilio_bodega.Text}','{TxtResponsbable.Text}',{this.id_empresaPrincipal},{id_sucursal})";
                globales.consulta(inserta);
            }
            if (btnOk_bodega.Text == "ACTUALIZAR")
            {
                string id_bodegaa = Convert.ToString(dataBodegas.Rows[r].Cells[3].Value);

                string actualiza = $"update bodegas setm nombre_bodega='{txtNombre_bodega.Text}' , ubicacion_bodega ='{txtDomicilio_bodega.Text}', id_sucursal={id_sucursal}, responsable_bodega='{TxtResponsbable.Text}' where id_bodega={id_bodegaa}";
                globales.consulta(actualiza);
            }

            txtNombre_bodega.Text = "";
            txtDomicilio_bodega.Text = "";
            TxtResponsbable.Text = "";
            comboBodega.Items.Clear();
            txtNombre_bodega.Focus();
            IniciaBodega();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            txtNombre_bodega.Text = "";
            txtDomicilio_bodega.Text = "";
            TxtResponsbable.Text = "";
            comboBodega.Items.Clear();
            btnOk_bodega.Text = "INSERTAR";
            txtNombre_bodega.Focus();
        }
   

        private void IniciaOrden ()
        {
             
            string query = $"select max (folio) as folio from ordenes_compra where id_empresa={this.id_empresaPrincipal}";
            List<Dictionary<string, object>> lista = globales.consulta(query);
          
                string folio = Convert.ToString(lista[0]["folio"]);
            if (string.IsNullOrWhiteSpace(folio))

            {
                this.folio_orden = 1;
            }
            txtOrden_Compra.Text = Convert.ToString(this.folio_orden);
            
        }

        private void ComboSucursal_DropDown(object sender, EventArgs e)
        {
            string busca = $"SELECT nombre FROM sucursales where id_empresa={this.id_empresaPrincipal} ;";
            List<Dictionary<string, object>> bodeg = globales.consulta(busca);

            comboBodega.Items.Clear();
            foreach (var item in bodeg)
            {
                string nombre_sucursal = Convert.ToString(item["nombre"]);
                comboBodega.Items.Add(nombre_sucursal);
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM bodegas where id_empresa={this.id_empresaPrincipal} and id_sucursal={this.sucursalOrden}; ";
            if (string.IsNullOrWhiteSpace(this.sucursalOrden)) return;
            this.listaBodega = globales.consulta(query);
            comboBodegaMovimientos.Items.Clear();
            txtBodega_orden.Items.Clear();
            foreach (var item in this.listaBodega)
            {
                string nombre = Convert.ToString(item["nombre_bodega"]);
                txtBodega_orden.Items.Add(nombre);
            }
        }

        private void comboOrdenesBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = $"select * from bodegas where nombre_bodega ='{txtBodega_orden.Text}' and id_empresa={this.id_empresaPrincipal}";
            List<Dictionary<string, object>> tempo = globales.consulta(query);
            if (tempo.Count >= 0) return;
            foreach (var a in tempo)
            {
                this.id_bodega_orden = Convert.ToString(a["id"]);
            }

        }

        private void dataOrdenes_Compra_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            if (r == -1) return;
            DataGridViewRow row = dataOrdenes_Compra.Rows[r];
            c = e.ColumnIndex;
        }

        private void dataOrdenes_Compra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataOrdenes_Compra_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);

        }

        private void comboSucursalBodega_DropDown(object sender, EventArgs e)
        {
            try
            {
                comboSucursalBodega.Items.Clear();
                string query = $"select * from sucursales where id_empresa={this.id_empresaPrincipal}";
                this.resulta = globales.consulta(query);
                comboSucursalBodega.Items.Clear();
                foreach (var ite in this.resulta)

                {
                    string nombre = Convert.ToString(ite["nombre"]);
                    comboSucursalBodega.Items.Add(nombre);
                }
            }
            catch
            {

            }
        }

        private void dataOrdenes_Compra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                frmCatProductos catalogo = new frmCatProductos(this.id_bodegaMov, this.id_empresaPrincipal, this.id_sucursal);
                catalogo.enviar = llenaOrden;
                catalogo.Show();
            }

            if (e.KeyCode == Keys.Down)
            {
                dataMovimientos.Rows.Insert(0);
            }
        }

        private void llenaOrden(Dictionary<string, object> datos)
        {

            string nombre = Convert.ToString(datos["descripcion"]);
            dataOrdenes_Compra.Rows[r].Cells[0].Value = nombre;
            string unidad_medida = Convert.ToString(datos["unidad_medida"]);
            dataOrdenes_Compra.Rows[r].Cells[1].Value = unidad_medida;
            string id_inventraio = Convert.ToString(datos["id_inventario"]);
            dataOrdenes_Compra.Rows[r].Cells[3].Value = id_inventraio;
            dataOrdenes_Compra.SelectedCells[c] = true; 




        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtOrden_Compra.Text)) return;

            string inser = $"insert into ordenes_compra (folio,fecha,elaboro,ubicacion,id_empresa) values ({txtOrden_Compra.Text},current_date,{txtElaboroOrden.Text},{this.id_bodega_orden},{this.id_empresaPrincipal});";
            globales.consulta(inser);

            foreach (DataGridViewRow row in dataMovimientos.Rows)
            {
                string id = Convert.ToString(dataOrdenes_Compra.Rows[r].Cells[3].Value);
                string unidad_medida = Convert.ToString(dataOrdenes_Compra.Rows[r].Cells[1].Value);
                string cantidad  = Convert.ToString(dataOrdenes_Compra.Rows[r].Cells[2].Value);

                string inserta= $"insert into control_movimientos (cantidad ,tipo_mov ,id_inventario , id_sucursal , id_usuario ,observaciones ,fecha , hora,id_empresa) values ({cantidad},'E',{id},{this.sucursalOrden},{globales.id_usuario},'ORDEN DE COMPRA',current_date,current_time,{this.id_empresaPrincipal});";
                globales.consulta(inserta);
                string detalle =$"insert into orden_compra_detalle (folio , id_inventario , cantidad ,unidad_medida , id_empresa) values ({txtOrden_Compra.Text},{id},{cantidad},'{unidad_medida}',{this.id_empresaPrincipal} );";
                globales.consulta(detalle);
                
            }
            DialogResult dialogo = globales.MessageBoxSuccess("SE TERMINO CORRECTAMENTE LA TAREA", "AVISO", globales.menuPrincipal);
            LimpiaOrdenCompra();
        }


        private void LimpiaOrdenCompra()
        {
            txtElaboroOrden.Text = "";
            comboSucursalBodega.Text = "";
            txtBodega_orden.Text = "";
            txtOrden_Compra.Text = "";
            dataOrdenes_Compra.Rows.Clear();
        }

        private void comboSucursalBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sucursal = $"select * from sucursales where nombre ='{comboSucursalBodega.Text}' and id_empresa={this.id_empresaPrincipal} ";
            List<Dictionary<string, object>> rem = globales.consulta(sucursal);
            if (String.IsNullOrWhiteSpace(comboSucursalBodega.Text)) return;
            this.sucursalOrden = Convert.ToString(rem[0]["id"]);
        }

        private void btnBuscarRfc_Click(object sender, EventArgs e)
        {
            txtOrden_Compra.ReadOnly = false;
            txtOrden_Compra.Focus();
        }

        private void txtOrden_Compra_Leave(object sender, EventArgs e)
        {
            string query = $"select * from orden_compra where folio ={txtOrden_Compra.Text}";
            List<Dictionary<string, object>> rem = globales.consulta(query);
            rem.ForEach(o =>
            {
                fecha_orden.Text = Convert.ToString(o["fecha"]);
                txtElaboroOrden.Text = Convert.ToString(o["elaboro"]);
                string bodega = Convert.ToString(o["ubicacion"]);
                string busca = $"select * from bodegas where id_bodega={bodega}";
                List<Dictionary<string, object>> estaesLista = globales.consulta(busca);
                txtBodega_orden.Text = Convert.ToString(estaesLista[0]["nombre_bodega"]);
                comboSucursalBodega.Text = "SUCURSAL ACTUAL";
            });

            string detalle_orden = $"SELECT a1.folio,a2.descripcion,a1.cantidad,a1.unidad_medida FROM orden_compra_detalle a1  join inventario a2 on a1.id_inventario = a2.id_inventario where a1.folio={txtOrden_Compra.Text}";
            List<Dictionary<string, object>> dtalle = globales.consulta(detalle_orden);
            dtalle.ForEach(i =>
            {
                string descripcion = Convert.ToString(i["descripcion"]);
                string unidad_medida = Convert.ToString(i["unidad_medida"]);
                string cantidad = Convert.ToString(i["cantidad"]);
                dataOrdenes_Compra.Rows.Add(descripcion, unidad_medida, cantidad, "0");
            });
        }
    }
}
