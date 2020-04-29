using AtiendelosDestktop.Resources.codigo;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtiendelosDestktop.herramientas
{
    class globales
    {

        internal static string id_usuario;
        internal static string nombre;
        internal static bool esReporte;
        internal static bool sinPagos;
        internal static bool leftButton = false;
        internal static string usuario;
        internal static string password;
        internal static int id_sucursal;
        internal static bool acceso;
        internal static Form menuPrincipal = null;
        public static Form aux;


        public static DialogResult showModal(Form ventana)
        {
            globales.aux = ventana;
            ventana.ShowInTaskbar = false;
            frmAvisoPregunta pregunta = new frmAvisoPregunta(globales.menuPrincipal, "", "", "modal", ventana);
            pregunta.Shown += new EventHandler(cargado);
            pregunta.Show();
            return pregunta.resultado;
        }

        public static Form showModalReturning(Form ventana)
        {
            globales.aux = ventana;
            ventana.ShowInTaskbar = false;
            frmAvisoPregunta pregunta = new frmAvisoPregunta(globales.menuPrincipal, "", "", "modal", ventana);
            pregunta.Shown += new EventHandler(cargado);
            pregunta.ShowDialog();
            return ventana;
        }


        public static dynamic consulta(string consulta, bool tipoSelect = false, bool eliminando = false)
        {
            return baseDatos.consulta(consulta, tipoSelect, eliminando);
        }



        public static void reportes(string nombreReporte, string tablaSetNombre, object[] objeto, string mensaje = "", bool imprimir = false, object[] parametros = null, bool espdf = false, string nombrePdf = "")
        {
            herramientas.reportes(nombreReporte, tablaSetNombre, objeto, mensaje, imprimir, parametros, espdf, nombrePdf);
        }

        public static ReportViewer reportesParaPanel(string nombreReporte, string tablaSetNombre, object[] objeto, string mensaje = "", bool imprimir = false, object[] parametros = null, bool espdf = false, string nombrePdf = "")
        {
            return herramientas.reportesParaPanel(nombreReporte, tablaSetNombre, objeto, mensaje, imprimir, parametros, espdf, nombrePdf);
        }




        private static void cargado(object sender, EventArgs e)
        {
            aux.Focus();
        }




        public static DialogResult MessageBoxQuestion(string mensaje1, Form ventana)
        {
            frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1);
            pregunta.ShowDialog();
            return pregunta.resultado;
        }

        public static DialogResult MessageBoxInformation(string mensaje1, string titulo, Form ventana)
        {
            frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1, titulo, "informacion");
            pregunta.ShowDialog();
            return pregunta.resultado;
        }
        public static DialogResult MessageBoxExclamation(string mensaje1, string titulo, Form ventana)
        {
            frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1, titulo, "exclamation");
            pregunta.BringToFront();
            pregunta.ShowDialog();
            return pregunta.resultado;
        }
        public static DialogResult MessageBoxError(string mensaje1, string titulo, Form ventana)
        {
            frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1, titulo, "error");
            pregunta.ShowDialog();
            return pregunta.resultado;
        }
        public static DialogResult MessageBoxSuccess(string mensaje1, string titulo, Form ventana)
        {
            frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1, titulo, "success");
            pregunta.ShowDialog();
            return pregunta.resultado;
        }

        internal static string parseDateTime(DateTime f_elab)
        {
            DateTime tiempo = new DateTime(1900, 01, 01);
            return f_elab <= tiempo ? "" : string.Format("{0:d}", f_elab);
        }

        public static DialogResult MessageBoxQuestion(string mensaje1, string titulo, Form ventana)
        {
            frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1, titulo, "question");
            pregunta.ShowDialog();
            return pregunta.resultado;
        }



     

    }
}
