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
    public partial class CitasFornulario : Office2007Form
    {
        private ControladorServicio controladorServicio = ControladorServicio.Instancia();
        private ControladorTerapeuta controladorTerapeuta = ControladorTerapeuta.Instancia();
        private ControladorModalidad controladorModalidad = ControladorModalidad.Instancia();
        private ControladorCita controladorCita = ControladorCita.Instancia();
        private List<Cita> citasLunes, citasMartes, citasMiercoles, citasJueves, citasViernes, citasSabado;
        private List<Terapeuta> terapeutas;
        private List<Servicio> servicios;
        private List<Turno> turnos;
        private List<Modalidad> modalidades;
        private Paciente paciente;
        private DateTime fechaLunes, fechaMartes, fechaMiercoles, fechaJueves, fechaViernes, fechaSabado;

        public CitasFornulario()
        {
            InitializeComponent();
            llenarServicios();
            llenarTurnos();
        }

        private void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            BusquedaPacienteForm busquedaPacientesForm = new BusquedaPacienteForm(this);
            busquedaPacientesForm.ShowDialog();
        }

        public void llenarServicios()
        {
            servicios = controladorServicio.getListaServicios("");
            comboServicios.DataSource = servicios;
            comboServicios.DisplayMember = "nombreServicio";
            comboServicios.ValueMember = "idServicio";
        }

        public void llenarPaciente(Paciente paciente)
        {
            this.paciente = paciente;
            txtNumeroHistoria.Text = "" + paciente.numeroHistoria;
            txtNombres.Text = paciente.persona.nombres;
            txtApellidoPaterno.Text = paciente.persona.apellidoPaterno;
            txtApellidoMaterno.Text = paciente.persona.apellidoMaterno;
            txtDNI.Text = "" + paciente.persona.dni;
        }

        public void llenarTurnos()
        {
            turnos = new List<Turno>();
            Turno turno0 = new Turno();
            turno0.idTurno = 0;
            turno0.nombreTurno = "Mañana";
            Turno turno1 = new Turno();
            turno1.idTurno = 1;
            turno1.nombreTurno = "Tarde";

            turnos.Add(turno0);
            turnos.Add(turno1);

            comboTurno.DataSource = turnos;
            comboTurno.DisplayMember = "nombreTurno";
            comboTurno.ValueMember = "idTurno";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void llenarTerapeutas(int idServicio)
        {
            terapeutas = controladorTerapeuta.getListaTerapeutasxServicio(idServicio);
            comboTerapeuta.Items.Clear();
            comboTerapeuta.DisplayMember = "nombres";
            comboTerapeuta.ValueMember = "idPersona";

            foreach (Terapeuta t in terapeutas)
            {
                comboTerapeuta.Items.Add(new { nombres = t.persona.nombres + " " + t.persona.apellidoPaterno + " " + t.persona.apellidoMaterno, idPersona = t.persona.idPersona });
            }
        }

        private void llenarModalidades(int idServicio)
        {
            modalidades = controladorModalidad.getListaModalidadesxServicio("", idServicio);
            comboModalidad.Items.Clear();
            comboModalidad.DisplayMember = "nombreModalidad";
            comboModalidad.ValueMember = "idModalidad";

            foreach (Modalidad m in modalidades)
            {
                comboModalidad.Items.Add(new { nombreModalidad = m.nombreModalidad, idModalidad = m.idModalidad });
            }
        }

        private void comboServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            Servicio s = new Servicio();
            s = comboServicios.SelectedItem as Servicio;
            llenarTerapeutas(s.idServicio);
            llenarModalidades(s.idServicio);
            txtCosto.Text = "" + s.costo;
            txtCostoFinal.Text = (float.Parse(txtCosto.Text) - float.Parse(txtDescuento.Text)).ToString();
            txtSaldoRestante.Text = (float.Parse(txtCostoFinal.Text) - float.Parse(txtAdelanto.Text)).ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            txtCostoFinal.Text = (float.Parse(txtCosto.Text) - float.Parse(txtDescuento.Text)).ToString();
        }

        private void txtAdelanto_TextChanged(object sender, EventArgs e)
        {
            txtSaldoRestante.Text = (float.Parse(txtCostoFinal.Text) - float.Parse(txtAdelanto.Text)).ToString();
        }

        private void txtCostoFinal_TextChanged(object sender, EventArgs e)
        {
            txtSaldoRestante.Text = (float.Parse(txtCostoFinal.Text) - float.Parse(txtAdelanto.Text)).ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!dateFechaCita.Value.DayOfWeek.ToString().Equals("Sunday"))
            {
                if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Monday"))
                {
                    fechaLunes = dateFechaCita.Value;
                    fechaMartes = dateFechaCita.Value.AddDays(1);
                    fechaMiercoles = dateFechaCita.Value.AddDays(2);
                    fechaJueves = dateFechaCita.Value.AddDays(3);
                    fechaViernes = dateFechaCita.Value.AddDays(4);
                    fechaSabado = dateFechaCita.Value.AddDays(5);
                }
                else if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Tuesday"))
                {
                    fechaLunes = dateFechaCita.Value.AddDays(-1);
                    fechaMartes = dateFechaCita.Value;
                    fechaMiercoles = dateFechaCita.Value.AddDays(1);
                    fechaJueves = dateFechaCita.Value.AddDays(2);
                    fechaViernes = dateFechaCita.Value.AddDays(3);
                    fechaSabado = dateFechaCita.Value.AddDays(4);
                }
                else if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Wednesday"))
                {
                    fechaLunes = dateFechaCita.Value.AddDays(-2);
                    fechaMartes = dateFechaCita.Value.AddDays(-1);
                    fechaMiercoles = dateFechaCita.Value;
                    fechaJueves = dateFechaCita.Value.AddDays(1);
                    fechaViernes = dateFechaCita.Value.AddDays(2);
                    fechaSabado = dateFechaCita.Value.AddDays(3);
                }
                else if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Thursday"))
                {
                    fechaLunes = dateFechaCita.Value.AddDays(-3);
                    fechaMartes = dateFechaCita.Value.AddDays(-2);
                    fechaMiercoles = dateFechaCita.Value.AddDays(-1);
                    fechaJueves = dateFechaCita.Value;
                    fechaViernes = dateFechaCita.Value.AddDays(1);
                    fechaSabado = dateFechaCita.Value.AddDays(2);
                }
                else if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Friday"))
                {
                    fechaLunes = dateFechaCita.Value.AddDays(-4);
                    fechaMartes = dateFechaCita.Value.AddDays(-3);
                    fechaMiercoles = dateFechaCita.Value.AddDays(-2);
                    fechaJueves = dateFechaCita.Value.AddDays(-1);
                    fechaViernes = dateFechaCita.Value;
                    fechaSabado = dateFechaCita.Value.AddDays(1);
                }
                else if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Saturday"))
                {
                    fechaLunes = dateFechaCita.Value.AddDays(-5);
                    fechaMartes = dateFechaCita.Value.AddDays(-4);
                    fechaMiercoles = dateFechaCita.Value.AddDays(-3);
                    fechaJueves = dateFechaCita.Value.AddDays(-2);
                    fechaViernes = dateFechaCita.Value.AddDays(-1);
                    fechaSabado = dateFechaCita.Value;
                }

                citasLunes = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaLunes);
                citasMartes = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaMartes);
                citasMiercoles = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaMiercoles);
                citasJueves = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaJueves);
                citasViernes = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaViernes);
                citasSabado = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaSabado);

                dgvCitas.Columns[1].HeaderText = "Lun "+fechaLunes.ToShortDateString();
                dgvCitas.Columns[2].HeaderText = "Mar "+fechaMartes.ToShortDateString();
                dgvCitas.Columns[3].HeaderText = "Mie "+fechaMiercoles.ToShortDateString();
                dgvCitas.Columns[4].HeaderText = "Jue "+fechaJueves.ToShortDateString();
                dgvCitas.Columns[5].HeaderText = "Vie "+fechaViernes.ToShortDateString();
                dgvCitas.Columns[6].HeaderText = "Sab "+fechaSabado.ToShortDateString();



            }
            else
            {
                MessageBox.Show("No hay citas Domingos");                
            }
        }

        private void navFecha_NavigateToday(object sender, EventArgs e)
        {
            dateFechaCita.Text = "" + DateTime.Now;
        }

        private void navFecha_NavigateNextPage(object sender, EventArgs e)
        {
            dateFechaCita.Text = "" + dateFechaCita.Value.AddDays(7);
        }

        private void navFecha_NavigatePreviousPage(object sender, EventArgs e)
        {
            dateFechaCita.Text = "" + dateFechaCita.Value.AddDays(-7);
        }


    }
}

