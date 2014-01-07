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
    public partial class PacientesForm : Office2007Form
    {
        private ControladorPaciente controladorPaciente = ControladorPaciente.Instancia();
        private List<Paciente> pacientes;
        public PacientesForm()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            PacientesFormulario pacientesFormulario = new PacientesFormulario(this, 0, null);
            pacientesFormulario.ShowDialog();
        }

        private void PacientesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        public void llenarPacientes(string strHistoriaClinica, string strDNI, string nombres, string apellidoPaterno, string apellidoMaterno)
        {
            string[] fila;
            pacientes = controladorPaciente.getListaPacientes(strHistoriaClinica, strDNI, nombres, apellidoPaterno, apellidoMaterno);
            dgvPacientes.Rows.Clear();
            foreach (Paciente paciente in pacientes)
            {
                fila = new string[] { "" + paciente.numeroHistoria, paciente.persona.nombres, paciente.persona.apellidoPaterno + " " + paciente.persona.apellidoMaterno, "" + paciente.persona.dni, paciente.telefonoCasa };
                dgvPacientes.Rows.Add(fila);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarPacientes(txtHistoriaClinica.Text, txtDNI.Text, txtNombres.Text, txtApellidoPaterno.Text, txtApellidoMaterno.Text);
        }

        private Paciente buscarPaciente(int numerohistoria)
        {
            Paciente paciente = null;
            foreach (Paciente p in pacientes)
            {
                if (numerohistoria == p.numeroHistoria)
                {
                    paciente = p;
                    break;
                }
            }
            return paciente;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Paciente paciente = buscarPaciente(int.Parse(dgvPacientes.CurrentRow.Cells[0].Value.ToString()));
                PacientesFormulario pacientesFormulario = new PacientesFormulario(this, 2, paciente);
                pacientesFormulario.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado un paciente");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar este paciente?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    Paciente paciente = buscarPaciente(int.Parse(dgvPacientes.CurrentRow.Cells[0].Value.ToString()));
                    if (controladorPaciente.eliminarPaciente(paciente))
                    {
                        MessageBox.Show("Paciente eliminado");
                        llenarPacientes("", "", "", "", "");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("No ha seleccionado un paciente");
                }
            }
        }

        private void dgvPacientes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                Paciente paciente = buscarPaciente(int.Parse(dgvPacientes.CurrentRow.Cells[0].Value.ToString()));
                PacientesFormulario pacientesFormulario = new PacientesFormulario(this, 1, paciente);
                pacientesFormulario.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado un paciente");
            }
        }
    }
}
