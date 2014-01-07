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
    public partial class BusquedaPacienteForm : Office2007Form
    {
        private ControladorPaciente controladorPaciente = ControladorPaciente.Instancia();
        private List<Paciente> pacientes;
        private CitasFornulario padre;

        public BusquedaPacienteForm(CitasFornulario citasFormulario)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.padre = citasFormulario;
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

        private void seleccionaPaciente()
        {
            try
            {
                Paciente paciente = buscarPaciente(int.Parse(dgvPacientes.CurrentRow.Cells[0].Value.ToString()));
                padre.llenarPaciente(paciente);
                this.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado un paciente");
            }
        }

        private void dgvPacientes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            seleccionaPaciente();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            seleccionaPaciente();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



    }
}
