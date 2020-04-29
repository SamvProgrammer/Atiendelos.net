using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtiendelosDestktop.Resources.codigo
{
    class baseDatos
    {
        private static string cadenaConexion = string.Empty;
        private static NpgsqlConnection conexion;

        //Cadena de conexión al servidor 192.168.100.102 
        private static string cadenaConexion2 = string.Empty;
        private static NpgsqlConnection conexion2;
        public static bool realizarConexion(string cadena)
        {
            cadenaConexion = cadena;
            bool conexionRealizada = false;
            try
            {

                conexion = new NpgsqlConnection(cadena);
                conexion.Open();
                conexionRealizada = true;
                conexion.Close();
            }
            catch (Exception e)
            {
                byte[] arreglo = System.Text.Encoding.Default.GetBytes(e.Message);
                string mensaje = System.Text.Encoding.ASCII.GetString(arreglo);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return conexionRealizada;
        }

        public static bool realizarConexion2(string cadena)
        {
            cadenaConexion2 = cadena;
            bool conexionRealizada = false;
            try
            {
                conexion2 = new NpgsqlConnection(cadenaConexion2);
                conexion2.Open();
                conexionRealizada = true;
                conexion2.Close();
            }
            catch (Exception e)
            {
                byte[] arreglo = System.Text.Encoding.Default.GetBytes(e.Message);
                string mensaje = System.Text.Encoding.ASCII.GetString(arreglo);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return conexionRealizada;
        }

        public static dynamic consulta(string query, bool tipoSelect = false, bool eliminando = false)
        {

            var consulta = new List<Dictionary<string, object>>();
            try
            {
                conexion.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexion);
                if (!tipoSelect)
                {
                    System.Data.Common.DbDataReader datos = cmd.ExecuteReader();

                    while (datos.Read())
                    {
                        Dictionary<string, object> objeto = new Dictionary<string, object>();
                        for (int x = 0; x < datos.FieldCount; x++)
                        {
                            objeto.Add(datos.GetName(x), datos.GetValue(x));
                        }
                        consulta.Add(objeto);
                    }
                    // consulta.Clear();
                    //  consulta = new List<Dictionary<string, object>>();
                }
                else
                {



                    try
                    {
                        cmd.ExecuteNonQuery();
                        conexion.Close();
                        cmd.Dispose();
                        return true;
                    }

                    catch (Exception e)
                    {
                        if (eliminando) return false;
                        MessageBox.Show(e.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        conexion.Close();
                        return false;
                    }
                }
                conexion.Close();

            }
            catch (Exception e)
            {
                if (eliminando) return false;
                MessageBox.Show(e.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                conexion.Close();
            }


            return consulta;
        }

        public static dynamic consulta2(string query, bool tipoSelect = false)
        {

            var consulta = new List<Dictionary<string, object>>();
            try
            {
                conexion2.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexion2);
                if (!tipoSelect)
                {
                    System.Data.Common.DbDataReader datos = cmd.ExecuteReader();

                    while (datos.Read())
                    {
                        Dictionary<string, object> objeto = new Dictionary<string, object>();
                        for (int x = 0; x < datos.FieldCount; x++)
                        {
                            objeto.Add(datos.GetName(x), datos.GetValue(x));
                        }
                        consulta.Add(objeto);
                    }
                }
                else
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conexion2.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        conexion2.Close();
                        return false;
                    }
                }
                conexion2.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                conexion2.Close();
            }
            return consulta;

        }
    }


    
}
