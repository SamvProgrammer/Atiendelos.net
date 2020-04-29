using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AtiendelosDestktop.herramientas
{
    class herramientas
    {
        internal static void reportes(string nombreReporte, string tablaDataSet, object[] objeto, string mensaje, bool imprimir = false, object[] parametros = null, bool esPdf = false, string nombreArchivo = "")
        {
            frmReportes reporte = new frmReportes(nombreReporte, tablaDataSet);


            
            reporte.cargarDatos(tablaDataSet, objeto, mensaje, imprimir, parametros);
            reporte.ShowDialog();
        }

        internal static ReportViewer reportesParaPanel(string nombreReporte, string tablaDataSet, object[] objeto, string mensaje, bool imprimir = false, object[] parametros = null, bool esPdf = false, string nombreArchivo = "")
        {
            frmReportes reporte = new frmReportes(nombreReporte, tablaDataSet);


            
            reporte.cargarDatos(tablaDataSet, objeto, mensaje, imprimir, parametros);
            return reporte.reportViewer1;
        }
    }
}
