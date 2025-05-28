using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper
{
    public static class Helper
    {
        public static void cargarImagen(string url, PictureBox pbx)
        {
            try
            {
                pbx.Load(url);
            }
            catch (Exception)
            {
                pbx.Load("https://blocks.astratic.com/img/general-img-landscape.png");
            }
        }
    }
}
