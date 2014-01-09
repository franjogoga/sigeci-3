﻿using System;
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
        private List<Semana> semanas = new List<Semana>();
        private Paciente paciente;
        private DateTime fechaLunes, fechaMartes, fechaMiercoles, fechaJueves, fechaViernes, fechaSabado;
        private DateTime horaInicio;
        private DateTime horaFin;

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
            comboTerapeuta.DisplayMember = "nombreCompleto";
            comboTerapeuta.ValueMember = "idTerapeuta";

            foreach (Terapeuta t in terapeutas)
            {
                comboTerapeuta.Items.Add(new TerapeutaCombo{  nombreCompleto = t.persona.nombres + " " + t.persona.apellidoPaterno + " " + t.persona.apellidoMaterno, idTerapeuta = t.persona.idPersona });                
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
                comboModalidad.Items.Add(new Modalidad{ nombreModalidad = m.nombreModalidad, idModalidad = m.idModalidad });
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
                semanas.Clear();
                if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Monday"))
                {
                    poneFechas(0, 1, 2, 3, 4, 5);                   
                }
                else if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Tuesday"))
                {
                    poneFechas(-1, 0, 1, 2, 3, 4);                
                }
                else if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Wednesday"))
                {
                    poneFechas(-2, -1, 0, 1, 2, 3);                    
                }
                else if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Thursday"))
                {
                    poneFechas(-3, -2, -1, 0, 1, 2);                    
                }
                else if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Friday"))
                {
                    poneFechas(-4, -3, -2, -1, 0, 1);                    
                }
                else if (dateFechaCita.Value.DayOfWeek.ToString().Equals("Saturday"))
                {
                    poneFechas(-5, -4, -3, -2, -1, 0);                    
                }

                citasLunes = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaLunes);
                citasMartes = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaMartes);
                citasMiercoles = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaMiercoles);
                citasJueves = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaJueves);
                citasViernes = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaViernes);
                citasSabado = controladorCita.getListaCitas("", "", "", (comboServicios.SelectedItem as Servicio).idServicio.ToString(), fechaSabado);

                dgvCitas.Columns[1].HeaderText = "Lunes "+fechaLunes.ToShortDateString();
                dgvCitas.Columns[2].HeaderText = "Martes "+fechaMartes.ToShortDateString();
                dgvCitas.Columns[3].HeaderText = "Miércoles "+fechaMiercoles.ToShortDateString();
                dgvCitas.Columns[4].HeaderText = "Jueves "+fechaJueves.ToShortDateString();
                dgvCitas.Columns[5].HeaderText = "Viernes "+fechaViernes.ToShortDateString();
                dgvCitas.Columns[6].HeaderText = "Sábado "+fechaSabado.ToShortDateString();
                
                Terapeuta terapeutaSeleccionado = controladorTerapeuta.getTerapeuta((comboTerapeuta.SelectedItem as TerapeutaCombo).idTerapeuta);                

                if ((comboServicios.SelectedItem as Servicio).intervaloHora == 30)
                {
                    llenarSemana(30, "08:00", "14:00", "20:00", terapeutaSeleccionado);
                }
                else if ((comboServicios.SelectedItem as Servicio).intervaloHora == 40)
                {
                    llenarSemana(40, "08:00", "14:00", "20:00", terapeutaSeleccionado);                    
                }
                else if ((comboServicios.SelectedItem as Servicio).intervaloHora == 60)
                {
                    llenarSemana(60, "08:00", "14:00", "20:00", terapeutaSeleccionado);
                }
                else if ((comboServicios.SelectedItem as Servicio).intervaloHora == 80)
                {
                    llenarSemana(80, "08:00", "14:40", "20:00", terapeutaSeleccionado);                    
                }
                llenarCitas();                
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

        private Cita ponerCita(HorarioTerapeuta horarioTerapeutaDia, List<Cita> citasDia, DateTime horaDia)
        {
            Cita c = new Cita();
            if (TimeSpan.Compare(horarioTerapeutaDia.horaInicio.TimeOfDay, horaDia.TimeOfDay) <= 0 && TimeSpan.Compare(horarioTerapeutaDia.horaFin.TimeOfDay, horaDia.TimeOfDay) >= 0)
            {
                c = encuentraHoraEnListaCitas(citasDia, horaDia);
                if (c == null)
                {
                    c = new Cita();
                    c.estado = "Libre";
                }                                
            }
            else
            {
                c.estado = "No Disponible";
            }
            return c;
        }

        private Cita encuentraHoraEnListaCitas(List<Cita> citasDia, DateTime horaDia)
        {
            Cita c = null;
            foreach (Cita ci in citasDia)
            {
                if (TimeSpan.Compare(ci.horaCita.TimeOfDay, horaDia.TimeOfDay) == 0)
                {
                    c = ci;
                    break;
                }
            }
            return c;
        }

        private void llenarCitas()
        {                    
            string[] fila;            
            dgvCitas.Rows.Clear();
            foreach (Semana s in semanas)
            {
                fila = new string[] { ""+s.hora.ToShortTimeString(), s.citaLunes.estado, s.citaMartes.estado, s.citaMiercoles.estado, s.citaJueves.estado, s.citaViernes.estado, s.citaSabado.estado};
                dgvCitas.Rows.Add(fila);
            }        
        }

        private void llenarSemana(int intervaloHora, string strhora1, string strhora2, string strhora3, Terapeuta terapeutaSeleccionado)
        {
            DateTime horaSemana;
            if ((comboTurno.SelectedItem as Turno).idTurno == 0)
            {
                horaSemana = Convert.ToDateTime(strhora1);
                horaInicio = Convert.ToDateTime(strhora1);
                horaFin = Convert.ToDateTime(strhora2);
            }
            else
            {
                horaSemana = Convert.ToDateTime(strhora2);
                horaInicio = Convert.ToDateTime(strhora2);
                horaFin = Convert.ToDateTime(strhora3);
            }

            int i = 0;
            while (TimeSpan.Compare(horaSemana.TimeOfDay, horaFin.TimeOfDay) < 0)
            {
                Semana s = new Semana();
                s.hora = horaSemana;
                horaSemana = horaSemana.AddMinutes(intervaloHora);
                Cita citaLun = ponerCita(terapeutaSeleccionado.horarioTerapeuta[0], citasLunes, s.hora);
                Cita citaMar = ponerCita(terapeutaSeleccionado.horarioTerapeuta[1], citasMartes, s.hora);
                Cita citaMie = ponerCita(terapeutaSeleccionado.horarioTerapeuta[2], citasMiercoles, s.hora);
                Cita citaJue = ponerCita(terapeutaSeleccionado.horarioTerapeuta[3], citasJueves, s.hora);
                Cita citaVie = ponerCita(terapeutaSeleccionado.horarioTerapeuta[4], citasViernes, s.hora);
                Cita citaSab = ponerCita(terapeutaSeleccionado.horarioTerapeuta[5], citasSabado, s.hora);
                s.citaLunes = citaLun;
                s.citaMartes = citaMar;
                s.citaMiercoles = citaMie;
                s.citaJueves = citaJue;
                s.citaViernes = citaVie;
                s.citaSabado = citaSab;
                semanas.Add(s);
                i++;
            }
        }

        private void poneFechas(int diasLunes, int diasMartes, int diasMiercoles, int diasJueves, int diasViernes, int diasSabado)
        {
            fechaLunes = dateFechaCita.Value.AddDays(diasLunes);
            fechaMartes = dateFechaCita.Value.AddDays(diasMartes);
            fechaMiercoles = dateFechaCita.Value.AddDays(diasMiercoles);
            fechaJueves = dateFechaCita.Value.AddDays(diasJueves);
            fechaViernes = dateFechaCita.Value.AddDays(diasViernes);
            fechaSabado = dateFechaCita.Value.AddDays(diasSabado);
        }



    }

}
