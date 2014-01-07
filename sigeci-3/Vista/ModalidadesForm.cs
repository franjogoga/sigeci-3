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
    public partial class ModalidadesForm : Office2007Form
    {
        private ControladorModalidad controladorModalidad = ControladorModalidad.Instancia();
        private List<Modalidad> modalidades;
        private int idServicio;

        public ModalidadesForm(ServiciosForm serviciosForm, Servicio servicio)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.idServicio = servicio.idServicio;
            llenarModalidades("", idServicio);
        }

        public void llenarModalidades(string nombreModalidad, int idServicio)
        {
            string[] fila;
            modalidades = controladorModalidad.getListaModalidadesxServicio(nombreModalidad, idServicio);
            dgvModalidades.Rows.Clear();
            foreach (Modalidad modalidad in modalidades)
            {
                fila = new string[] { "" + modalidad.idModalidad, modalidad.nombreModalidad };
                dgvModalidades.Rows.Add(fila);
            }
        }

        private Modalidad buscarModalidad(int id)
        {
            Modalidad modalidad = null;
            foreach (Modalidad m in modalidades)
            {
                if (id == m.idModalidad)
                {
                    modalidad = m;
                    break;
                }
            }
            return modalidad;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarModalidades(txtModalidad.Text, idServicio);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ModalidadesFormulario modalidadesFormulario = new ModalidadesFormulario(this, 0, null, idServicio);
            modalidadesFormulario.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Modalidad modalidad = buscarModalidad(int.Parse(dgvModalidades.CurrentRow.Cells[0].Value.ToString()));
                ModalidadesFormulario modalidadesFormulario = new ModalidadesFormulario(this, 2, modalidad, idServicio);
                modalidadesFormulario.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado una modalidad");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar esta modalidad?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    Modalidad modalidad = buscarModalidad(int.Parse(dgvModalidades.CurrentRow.Cells[0].Value.ToString()));
                    if (controladorModalidad.eliminarModalidad(modalidad))
                    {
                        MessageBox.Show("Modalidad eliminado");
                        llenarModalidades("", idServicio);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("No ha seleccionado una modalidad");
                }
            }
        }

        private void dgvModalidades_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                Modalidad modalidad = buscarModalidad(int.Parse(dgvModalidades.CurrentRow.Cells[0].Value.ToString()));
                ModalidadesFormulario modalidadesFormulario = new ModalidadesFormulario(this, 1, modalidad, idServicio);
                modalidadesFormulario.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado una modalidad");
            }
        }



    }
}
