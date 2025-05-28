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
using negocio;
using estatico;

namespace presentacion
{
    public partial class formAgregar : Form
    {
        private Articulo articulo = null;
        public formAgregar()
        {
            InitializeComponent();
        }

        public formAgregar(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar";
            pbxImagenAgregar.BorderStyle = BorderStyle.None;
        }

        //public bool validarString()
        //{
        //    if (txtCodArticulo.Text == "")
        //    {
        //        MessageBox.Show("Por favor ingrese un codigo de articulo");
        //        return true;
        //    }

        //    return false;
        //}
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Articulo articulo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (Helper.validarString(txtCodArticulo, "Codigo de Articulo", false) || Helper.validarString(txtNombre, "Nombre", false) || Helper.validarString(txtDescripcion, "Descripción", true)  || Helper.validarString(txturlImagen, "Url Imagen", true) || Helper.validarString(txtPrecio, "Precio", false))
                    return;

                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtCodArticulo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.UrlImagen = txturlImagen.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado correctamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }
 
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void formAgregar_Load(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            CategoriaNegocio negocio2 = new CategoriaNegocio();
            try
            {
                cboMarca.DataSource = negocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboCategoria.DataSource = negocio2.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodArticulo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txturlImagen.Text = articulo.UrlImagen;
                    txtPrecio.Text = articulo.Precio.ToString();
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txturlImagen_TextChanged(object sender, EventArgs e)
        {
            Helper.cargarImagen(txturlImagen.Text, pbxImagenAgregar);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helper.soloNumeros(sender, e);
        }
    }
}
