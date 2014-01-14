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
using Modelo;
using Controlador;

namespace Vista
{
    public partial class TerapeutaForm : Office2007Form
    {
        private ControladorTerapeuta controladorTerapeuta = ControladorTerapeuta.Instancia();
        private ControladorServicio controladorServicio = ControladorServicio.Instancia();
        private List<Terapeuta> terapeutas;
        private List<Servicio> servicios;

        public TerapeutaForm()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            servicios = controladorServicio.getListaServicios("");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            TerapeutaFormulario terapeutaFormulario = new TerapeutaFormulario(this, 0, null, servicios);
            terapeutaFormulario.ShowDialog();
        }

        public void llenarTerapeutas(string nombres, string apellidoPaterno, string apellidoMaterno, string strDNI)
        {
            string[] fila;
            terapeutas = controladorTerapeuta.getListaTerapeutas(nombres, apellidoPaterno, apellidoMaterno, strDNI);
            dgvTerapeutas.Rows.Clear();
            foreach (Terapeuta terapeuta in terapeutas)
            {
                fila = new string[] { "" + terapeuta.persona.idPersona, terapeuta.persona.nombres, terapeuta.persona.apellidoPaterno + " " + terapeuta.persona.apellidoMaterno, "" + terapeuta.persona.dni, terapeuta.telefono };
                dgvTerapeutas.Rows.Add(fila);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarTerapeutas(txtNombres.Text, txtApellidoPaterno.Text, txtApellidoMaterno.Text, txtDNI.Text);
        }

        private Terapeuta buscarTerapeuta(int id)
        {
            Terapeuta terapeuta = null;
            foreach (Terapeuta t in terapeutas)
            {
                if (id == t.persona.idPersona)
                {
                    terapeuta = t;
                    break;
                }
            }
            return terapeuta;
        }

        private void dgvTerapeutas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                Terapeuta terapeuta = buscarTerapeuta(int.Parse(dgvTerapeutas.CurrentRow.Cells[0].Value.ToString()));
                TerapeutaFormulario terapeutaFormulario = new TerapeutaFormulario(this, 1, terapeuta, servicios);
                terapeutaFormulario.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado un terapeuta");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Terapeuta terapeuta = buscarTerapeuta(int.Parse(dgvTerapeutas.CurrentRow.Cells[0].Value.ToString()));
                TerapeutaFormulario terapeutaFormulario = new TerapeutaFormulario(this, 2, terapeuta, servicios);
                terapeutaFormulario.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado un terapeuta");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar este terapeuta?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    Terapeuta terapeuta = buscarTerapeuta(int.Parse(dgvTerapeutas.CurrentRow.Cells[0].Value.ToString()));
                    if (controladorTerapeuta.eliminarTerapeuta(terapeuta))
                    {
                        MessageBox.Show("Terapeuta eliminado");
                        llenarTerapeutas("", "", "", "");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("No ha seleccionado un terapeuta");
                }
            }
        }

        private void TerapeutaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
        }
    }
}
