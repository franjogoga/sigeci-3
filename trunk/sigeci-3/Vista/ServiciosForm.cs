using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using DevComponents.DotNetBar;
using Controlador;
using Modelo;

namespace Vista
{
    public partial class ServiciosForm : Office2007Form
    {
        private ControladorServicio controladorServicio = ControladorServicio.Instancia();
        private List<Servicio> servicios;
        public ServiciosForm()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        public void llenarServicios(string nombreServicio)
        {
            string[] fila;
            servicios = controladorServicio.getListaServicios(nombreServicio);
            dgvServicios.Rows.Clear();
            foreach (Servicio servicio in servicios)
            {
                fila = new string[] { "" + servicio.idServicio, servicio.nombreServicio, "" + servicio.costo, "" + servicio.maximoPacientes };
                dgvServicios.Rows.Add(fila);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarServicios(txtNombreServicio.Text);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ServiciosFormulario serviciosFormulario = new ServiciosFormulario(this, 0, null);
            serviciosFormulario.ShowDialog();
        }

        private Servicio buscarServicio(int id)
        {
            Servicio servicio = null;
            foreach (Servicio s in servicios)
            {
                if (id == s.idServicio)
                {
                    servicio = s;
                    break;
                }
            }
            return servicio;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Servicio servicio = buscarServicio(int.Parse(dgvServicios.CurrentRow.Cells[0].Value.ToString()));
                ServiciosFormulario serviciosFormulario = new ServiciosFormulario(this, 2, servicio);
                serviciosFormulario.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado un servicio");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar este servicio?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    Servicio servicio = buscarServicio(int.Parse(dgvServicios.CurrentRow.Cells[0].Value.ToString()));
                    if (controladorServicio.eliminarServicio(servicio))
                    {
                        MessageBox.Show("Servicio eliminado");
                        llenarServicios("");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("No ha seleccionado un servicio");
                }
            }
        }

        private void dgvServicios_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                Servicio servicio = buscarServicio(int.Parse(dgvServicios.CurrentRow.Cells[0].Value.ToString()));
                ServiciosFormulario serviciosFormulario = new ServiciosFormulario(this, 1, servicio);
                serviciosFormulario.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado un servicio");
            }
        }

        private void btnModalidades_Click(object sender, EventArgs e)
        {
            try
            {
                Servicio servicio = buscarServicio(int.Parse(dgvServicios.CurrentRow.Cells[0].Value.ToString()));
                ModalidadesForm modalidadesForm = new ModalidadesForm(this, servicio);
                modalidadesForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado un servicio");
            }
        }
    }
}
