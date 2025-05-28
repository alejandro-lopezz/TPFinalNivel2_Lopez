using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace estatico
{
    public class Helper
    {
        public static void cargarImagen(string url, PictureBox pbx)
        {
            try
            {
                pbx.Load(url);
            }
            catch (Exception)
            {
                pbx.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSUwCJYSnbBLMEGWKfSnWRGC_34iCCKkxePpg&s");
                if (url == null)
                {
                    pbx.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSUwCJYSnbBLMEGWKfSnWRGC_34iCCKkxePpg&s");
                }
            }
        }

        public static void actualizarGrilla(List<Articulo> algo,  DataGridView dgv, PictureBox pbx)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            algo = negocio.listar();
            dgv.DataSource = algo;
            dgv.Columns["UrlImagen"].Visible = false;
            Helper.cargarImagen(algo[0].UrlImagen, pbx);
        }

        public static void ocultarColumnas(DataGridView dgv)
        {
            dgv.Columns["UrlImagen"].Visible = false;
            dgv.Columns["Id"].Visible = false;
        }

        public static void soloNumeros(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 59) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        public static bool validarString(TextBox nombre, string nombreCampo, bool terminaCona)
        {
            if (nombre.Text == "")
            {
                string articulo = terminaCona ? "una" : "un";
                MessageBox.Show("Por favor ingrese " + articulo + " " + nombreCampo);
                return true;
            }

            return false;
        }

    }
}
