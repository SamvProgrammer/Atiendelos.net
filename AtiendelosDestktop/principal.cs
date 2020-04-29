using AtiendelosDestktop.Resources.codigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtiendelosDestktop
{
    static class principal
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //**********************************************************
            //**       ICONOS PARA EL SISTEMAS PÁGINA                 **
            //**   https://icons8.com/icon/new-icons/office           **
            //**********************************************************

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string host = AtiendelosDestktop.Properties.Resources.servidor;
            string usuario = AtiendelosDestktop.Properties.Resources.usuario;
            string password = AtiendelosDestktop.Properties.Resources.password;
            string database = AtiendelosDestktop.Properties.Resources.baseDatos;

            string queryConexion = string.Format("Host={0};Username={1};Password={2};Database={3};SSL Mode=Require;Trust Server Certificate=true", host, usuario, password, database);



            if (baseDatos.realizarConexion(queryConexion))
            {
                Application.Run(new login( ));///
            }
        }
    }
}