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
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulos;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listar();
            dgvArticulos.DataSource = listaArticulos;
            Helper.ocultarColumnas(dgvArticulos);
            Helper.cargarImagen(listaArticulos[0].UrlImagen, pbxUrlImagen);
            
            //cargo el comboBox para el filtroAvanzado
            cboFiltroAvanzado.Items.Add("Precio");
            cboFiltroAvanzado.Items.Add("Categoria");

            //oculto de primeras los controles dependiendo lo seleccionado
            lblDesde.Visible = false;
            txtDesde.Visible = false;
            lblHasta.Visible = false;
            txtHasta.Visible = false;
            btnFiltroAvanzado.Visible = false;

            lblCategoria.Visible = false;
            cboCategoria.Visible = false;
            btnBuscarCategoria.Visible = false;
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.DataSource != null && dgvArticulos.CurrentRow != null && dgvArticulos.CurrentRow.DataBoundItem != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                Helper.cargarImagen(seleccionado.UrlImagen, pbxUrlImagen);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            formAgregar agregar = new formAgregar();
            agregar.ShowDialog();
            Helper.actualizarGrilla(listaArticulos, dgvArticulos, pbxUrlImagen);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow == null)
            {
                MessageBox.Show("No hay nada seleccionado para modificar");
                return;
            }

            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            formAgregar agregar = new formAgregar(seleccionado);
            agregar.ShowDialog();
            Helper.actualizarGrilla(listaArticulos, dgvArticulos, pbxUrlImagen);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {               
                DialogResult resultado = MessageBox.Show("¿Estas seguro de querer eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    if (dgvArticulos.CurrentRow == null)
                    {
                        MessageBox.Show("No hay nada seleccionado en la grilla");
                        return;
                    }

                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    MessageBox.Show("Eliminado correctamente");
                    Helper.actualizarGrilla(listaArticulos, dgvArticulos, pbxUrlImagen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;

            listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltroRapido.Text.ToUpper()) || x.Marca.Descripcion.ToLower().Contains(txtFiltroRapido.Text.ToLower()));

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            Helper.ocultarColumnas(dgvArticulos);

        }

        private void btnDetalles_Click_1(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow == null)
            {
                MessageBox.Show("No hay ningún articulo seleccionado, vuelva a intentarlo");
                return;
            }

            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            formDetalles detalles = new formDetalles(seleccionado);
            detalles.ShowDialog();
        }

        private bool validarFiltroVacio()
        {
            if (txtDesde.Text == "")
            {
                MessageBox.Show("Ingrese un valor en el primer campo para poder filtar");
                return true;
            }
            else if (txtHasta.Text == "")
            {
                MessageBox.Show("Ingrese un valor en el segundo campo para poder filtar");
                return true;
            }

            return false;
        }
        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltroVacio() == true)
                    return;

                string campo = cboFiltroAvanzado.Text;
                string desde = txtDesde.Text;
                string hasta = txtHasta.Text;

                dgvArticulos.DataSource = negocio.filtrar(campo, desde, hasta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboFiltroAvanzado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ocultarBotones();
            CategoriaNegocio negocio = new CategoriaNegocio();
            string criterio = cboFiltroAvanzado.SelectedItem.ToString();

            if (criterio == "Precio")
            {
                lblDesde.Visible = true;
                txtDesde.Visible = true;
                lblHasta.Visible = true;
                txtHasta.Visible = true;
                btnFiltroAvanzado.Visible = true;
            }

            if (criterio == "Categoria")
            {
                //cboCategoria.Items.Clear();
                cboCategoria.DataSource = negocio.listar();
                lblCategoria.Visible = true;
                cboCategoria.Visible = true;
                btnBuscarCategoria.Visible = true;
            }

        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                string campo = cboFiltroAvanzado.Text;
                string categoria = cboCategoria.Text;

                dgvArticulos.DataSource = negocio.filtroCategoria(campo, categoria);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarBotones()
        {
            lblDesde.Visible = false;
            txtDesde.Visible = false;
            lblHasta.Visible = false;
            txtHasta.Visible = false;
            btnFiltroAvanzado.Visible = false;

            lblCategoria.Visible = false;
            cboCategoria.Visible = false;
            btnBuscarCategoria.Visible = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvArticulos.DataSource = listaArticulos;
            txtDesde.Text = "";
            txtHasta.Text = "";
            ocultarBotones();
        }

        private void txtDesde_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helper.soloNumeros(sender, e);
        }

        private void txtHasta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helper.soloNumeros(sender, e);
        }
    }
}
