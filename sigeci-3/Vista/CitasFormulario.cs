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
        private CitasForm padre;

        public CitasFornulario(CitasForm citasForm)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            dgvCitas.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.padre = citasForm;
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
            try
            {
                Cita c = new Cita();
                int fila = dgvCitas.CurrentCell.RowIndex;
                int columna = dgvCitas.CurrentCell.ColumnIndex;
                if (columna != 0 && dgvCitas.CurrentCell.Value.ToString().Equals("Libre") || dgvCitas.CurrentCell.Value.ToString().Equals("Anulado") || dgvCitas.CurrentCell.Value.ToString().Equals("Permiso"))
                {
                    c.paciente = paciente;                    
                    switch (columna)
                    {
                        case 1: 
                            c.horaCita = semanas[fila].citaLunes.horaCita;
                            c.fechaCita = semanas[fila].citaLunes.fechaCita;
                            break;
                        case 2: 
                            c.horaCita = semanas[fila].citaMartes.horaCita;
                            c.fechaCita = semanas[fila].citaMartes.fechaCita;
                            break;
                        case 3: 
                            c.horaCita = semanas[fila].citaMiercoles.horaCita;
                            c.fechaCita = semanas[fila].citaMiercoles.fechaCita; 
                            break;
                        case 4: 
                            c.horaCita = semanas[fila].citaJueves.horaCita;
                            c.fechaCita = semanas[fila].citaJueves.fechaCita;
                            break;
                        case 5: 
                            c.horaCita = semanas[fila].citaViernes.horaCita;
                            c.fechaCita = semanas[fila].citaViernes.fechaCita;
                            break;
                        case 6: 
                            c.horaCita = semanas[fila].citaSabado.horaCita;
                            c.fechaCita = semanas[fila].citaSabado.fechaCita;
                            break;
                    }
                    Servicio s = buscarServicio((comboServicios.SelectedItem as Servicio).idServicio);
                    c.servicio = s;
                    Modalidad m = new Modalidad();
                    m.idModalidad = (comboModalidad.SelectedItem as Modalidad).idModalidad;
                    c.modalidad = m;
                    c.estado = "Reservado";
                    c.fechaRegistro = DateTime.Now;
                    c.costo = int.Parse(txtCostoFinal.Text);
                    c.descuento = int.Parse(txtDescuento.Text);
                    c.estadoEvaluacion = "-";
                    Terapeuta t = new Terapeuta();
                    Persona p = new Persona();
                    p.idPersona = (comboTerapeuta.SelectedItem as TerapeutaCombo).idTerapeuta;
                    t.persona = p;
                    c.terapeuta = t;

                    Pago pago = new Pago();
                    pago.fecha = DateTime.Now;
                    pago.monto = float.Parse(txtAdelanto.Text);
                    pago.estado = "Reservado";
                    if (int.Parse(txtCostoFinal.Text) == int.Parse(txtAdelanto.Text))
                    {
                        c.estado = "Confirmado";
                        pago.estado = "Confirmado";
                    }                    

                    int idCita = 0;
                    if (!controladorCita.tieneCrucesHorarioPaciente(paciente, c.horaCita, c.fechaCita, s.intervaloHora))
                    {
                        if (controladorCita.reservarCita(c, pago, out idCita))
                        {                            
                            c.idCita = idCita;
                            string nombreTerapeuta = (comboTerapeuta.SelectedItem as TerapeutaCombo).nombreCompleto;

                            CitaImpresion citaImpresion = new CitaImpresion(c, nombreTerapeuta);
                            citaImpresion.Show();

                            //MessageBox.Show("Cita N° " + idCita + "\n" + c.fechaCita.ToShortDateString() + "  " + c.horaCita.ToShortTimeString() + "\n" + "Servicio : " + c.servicio.nombreServicio + "\n" + "Terapeuta : " + (comboTerapeuta.SelectedItem as TerapeutaCombo).nombreCompleto + "\n" + "Paciente : " + c.paciente.persona.nombres + " " + c.paciente.persona.apellidoPaterno + " " + c.paciente.persona.apellidoMaterno);
                            padre.llenarCitas("", "", "", c.servicio.idServicio.ToString(), c.fechaCita);
                            this.Dispose();                            
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El paciente tiene cita en el mismo horario");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un horario libre");
                }                                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Faltan datos");
            }            
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            if (txtDescuento.Text.Equals(""))
            {
                txtDescuento.Text = "0";
            }         
            
            txtCostoFinal.Text = (float.Parse(txtCosto.Text) - float.Parse(txtDescuento.Text)).ToString();            
        }

        private void txtAdelanto_TextChanged(object sender, EventArgs e)
        {
            if (txtAdelanto.Text.Equals(""))
            {
                txtAdelanto.Text = "0";
            }
            
            txtSaldoRestante.Text = (float.Parse(txtCostoFinal.Text) - float.Parse(txtAdelanto.Text)).ToString();            
        }

        private void txtCostoFinal_TextChanged(object sender, EventArgs e)
        {
            txtSaldoRestante.Text = (float.Parse(txtCostoFinal.Text) - float.Parse(txtAdelanto.Text)).ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
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

                    Terapeuta terapeutaSeleccionado = controladorTerapeuta.getTerapeuta((comboTerapeuta.SelectedItem as TerapeutaCombo).idTerapeuta);

                    citasLunes = controladorCita.getListaCitasxServicioxTerapeuta(fechaLunes, (comboServicios.SelectedItem as Servicio).idServicio, terapeutaSeleccionado.persona.idPersona);
                    citasMartes = controladorCita.getListaCitasxServicioxTerapeuta(fechaMartes, (comboServicios.SelectedItem as Servicio).idServicio, terapeutaSeleccionado.persona.idPersona);
                    citasMiercoles = controladorCita.getListaCitasxServicioxTerapeuta(fechaMiercoles, (comboServicios.SelectedItem as Servicio).idServicio, terapeutaSeleccionado.persona.idPersona);
                    citasJueves = controladorCita.getListaCitasxServicioxTerapeuta(fechaJueves, (comboServicios.SelectedItem as Servicio).idServicio, terapeutaSeleccionado.persona.idPersona);
                    citasViernes = controladorCita.getListaCitasxServicioxTerapeuta(fechaViernes, (comboServicios.SelectedItem as Servicio).idServicio, terapeutaSeleccionado.persona.idPersona);
                    citasSabado = controladorCita.getListaCitasxServicioxTerapeuta(fechaSabado, (comboServicios.SelectedItem as Servicio).idServicio, terapeutaSeleccionado.persona.idPersona);                    

                    dgvCitas.Columns[1].HeaderText = "Lunes " + fechaLunes.ToShortDateString();
                    dgvCitas.Columns[2].HeaderText = "Martes " + fechaMartes.ToShortDateString();
                    dgvCitas.Columns[3].HeaderText = "Miércoles " + fechaMiercoles.ToShortDateString();
                    dgvCitas.Columns[4].HeaderText = "Jueves " + fechaJueves.ToShortDateString();
                    dgvCitas.Columns[5].HeaderText = "Viernes " + fechaViernes.ToShortDateString();
                    dgvCitas.Columns[6].HeaderText = "Sábado " + fechaSabado.ToShortDateString();                    

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

                    if (controladorCita.tieneEvolucion( int.Parse(txtNumeroHistoria.Text),(comboServicios.SelectedItem as Servicio).idServicio, terapeutaSeleccionado.persona.idPersona))
                    {
                        MessageBox.Show("Es necesario realizar un informe de evolución");
                    }

                    llenarCitas();
                }
                else
                {
                    MessageBox.Show("No hay citas Domingos");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Seleccione una terapeuta y paciente");
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

        private Cita ponerCita(HorarioTerapeuta horarioTerapeutaDia, List<Cita> citasDia, DateTime horaDia, DateTime fechaDia)
        {
            Cita c = new Cita();
            if (TimeSpan.Compare(horarioTerapeutaDia.horaInicio.TimeOfDay, horaDia.TimeOfDay) <= 0 && TimeSpan.Compare(horarioTerapeutaDia.horaFin.TimeOfDay, horaDia.TimeOfDay) >= 0)
            {
                c = encuentraHoraEnListaCitas(citasDia, horaDia);
                if (c == null)
                {
                    c = new Cita();
                    c.horaCita = horaDia;
                    c.fechaCita = fechaDia;
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
            string textoLunes, textoMartes, textoMiercoles, textoJueves, textoViernes, textoSabado;
            dgvCitas.Rows.Clear();
            foreach (Semana s in semanas)
            {
                textoLunes=s.citaLunes.estado;
                textoMartes=s.citaMartes.estado;
                textoMiercoles=s.citaMiercoles.estado;
                textoJueves=s.citaJueves.estado;
                textoViernes=s.citaViernes.estado;
                textoSabado=s.citaSabado.estado;

                if (s.citaLunes.estado.Equals("Reservado") || s.citaLunes.estado.Equals("Confirmado"))
                {
                    textoLunes = s.citaLunes.estado + "\n" + s.citaLunes.paciente.persona.nombres + " " + s.citaLunes.paciente.persona.apellidoPaterno + " " + s.citaLunes.paciente.persona.apellidoMaterno;
                }
                if (s.citaMartes.estado.Equals("Reservado") || s.citaMartes.estado.Equals("Confirmado"))
                {
                    textoMartes = s.citaMartes.estado + "\n" + s.citaMartes.paciente.persona.nombres + " " + s.citaMartes.paciente.persona.apellidoPaterno + " " + s.citaMartes.paciente.persona.apellidoMaterno;
                }
                if (s.citaMiercoles.estado.Equals("Reservado") || s.citaMiercoles.estado.Equals("Confirmado"))
                {
                    textoMiercoles = s.citaMiercoles.estado + "\n" + s.citaMiercoles.paciente.persona.nombres + " " + s.citaMiercoles.paciente.persona.apellidoPaterno + " " + s.citaMiercoles.paciente.persona.apellidoMaterno;
                }
                if (s.citaJueves.estado.Equals("Reservado") || s.citaJueves.estado.Equals("Confirmado"))
                {
                    textoJueves = s.citaJueves.estado + "\n" + s.citaJueves.paciente.persona.nombres + " " + s.citaJueves.paciente.persona.apellidoPaterno + " " + s.citaJueves.paciente.persona.apellidoMaterno;
                }
                if (s.citaViernes.estado.Equals("Reservado") || s.citaViernes.estado.Equals("Confirmado"))
                {
                    textoViernes = s.citaViernes.estado + "\n" + s.citaViernes.paciente.persona.nombres + " " + s.citaViernes.paciente.persona.apellidoPaterno + " " + s.citaViernes.paciente.persona.apellidoMaterno;
                }
                if (s.citaSabado.estado.Equals("Reservado") || s.citaSabado.estado.Equals("Confirmado"))
                {
                    textoSabado = s.citaSabado.estado + "\n" + s.citaSabado.paciente.persona.nombres + " " + s.citaSabado.paciente.persona.apellidoPaterno + " " + s.citaSabado.paciente.persona.apellidoMaterno;
                }

                fila = new string[] { ""+s.hora.ToShortTimeString(), textoLunes, textoMartes, textoMiercoles, textoJueves, textoViernes, textoSabado};
                dgvCitas.Rows.Add(fila);
            }

            foreach (DataGridViewRow row in dgvCitas.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {                    
                    if (cell.Value.ToString().Contains("No Disponible"))
                    {
                        cell.Style.BackColor = Color.Tomato;
                    }
                    if (cell.Value.ToString().Contains("Confirmado"))
                    {
                        cell.Style.BackColor = Color.MediumSpringGreen;
                    }
                    if (cell.Value.ToString().Contains("Reservado"))
                    {
                        cell.Style.BackColor = Color.Yellow;
                    }
                }                
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
                Cita citaLun = ponerCita(terapeutaSeleccionado.horarioTerapeuta[0], citasLunes, s.hora, fechaLunes);
                Cita citaMar = ponerCita(terapeutaSeleccionado.horarioTerapeuta[1], citasMartes, s.hora, fechaMartes);
                Cita citaMie = ponerCita(terapeutaSeleccionado.horarioTerapeuta[2], citasMiercoles, s.hora, fechaMiercoles);
                Cita citaJue = ponerCita(terapeutaSeleccionado.horarioTerapeuta[3], citasJueves, s.hora, fechaJueves);
                Cita citaVie = ponerCita(terapeutaSeleccionado.horarioTerapeuta[4], citasViernes, s.hora, fechaViernes);
                Cita citaSab = ponerCita(terapeutaSeleccionado.horarioTerapeuta[5], citasSabado, s.hora, fechaSabado);
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

        private void dgvCitas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int fila = dgvCitas.CurrentCell.RowIndex;
                int columna = dgvCitas.CurrentCell.ColumnIndex;
                if (columna != 0)
                {
                    if (dgvCitas.CurrentCell.Value.ToString().Equals("Reservado") || dgvCitas.CurrentCell.Value.ToString().Equals("Confirmado"))
                    {
                        Cita c = null;
                        switch (columna)
                        {                            
                            case 1: c = semanas[fila].citaLunes; break;
                            case 2: c = semanas[fila].citaMartes; break;
                            case 3: c = semanas[fila].citaMiercoles; break;
                            case 4: c = semanas[fila].citaJueves; break;
                            case 5: c = semanas[fila].citaViernes; break;
                            case 6: c = semanas[fila].citaSabado; break;
                        }
                        MessageBox.Show(c.fechaCita.ToShortDateString() + "   "+c.horaCita.ToShortTimeString()+ "\n"+ c.estado + "\n"+c.paciente.persona.nombres + " "+c.paciente.persona.apellidoPaterno + " "+c.paciente.persona.apellidoMaterno);
                    }
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void CitasFornulario_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAdelanto_KeyPress(object sender, KeyPressEventArgs e)
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

