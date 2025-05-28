using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using estatico;

namespace presentacion
{
    public partial class formDetalles : Form
    {
        private Articulo articulo;
        public formDetalles()
        {
            InitializeComponent();
        }

        public formDetalles(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void formDetalles_Load(object sender, EventArgs e)
        {
            lblCodArticuloDetalle.Text = articulo.Codigo;
            lblNombreDetalle.Text = articulo.Nombre;
            lblDescDetalle.Text = articulo.Descripcion;
            lblPrecioDetalle.Text = articulo.Precio.ToString();
            lblMarcaDetalle.Text = articulo.Marca.Descripcion;
            lblCategoriaDetalle.Text = articulo.Categoria.Descripcion;

            Helper.cargarImagen(articulo.UrlImagen, pbxImagenDetalle);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
