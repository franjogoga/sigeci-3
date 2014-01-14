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
    public partial class TerapeutaFormulario : Office2007Form
    {
        private ControladorTerapeuta controladorTerapeuta = ControladorTerapeuta.Instancia();
        private TerapeutaForm padre;
        private int modo;
        private Terapeuta terapeuta;
        private List<Servicio> servicios;

        public TerapeutaFormulario(TerapeutaForm terapeutaForm, int modo, Terapeuta terapeuta, List<Servicio> servicios)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.terapeuta = terapeuta;
            this.padre = terapeutaForm;
            this.servicios = servicios;
            this.modo = modo;
            if (modo == 0) //agregar
            {
                llenaServicios();
            }
            if (modo == 2) //modificar
            {
                llenaFormularioTerapeuta(terapeuta);
            }
            if (modo == 1) //ver
            {
                llenaFormularioTerapeuta(terapeuta);
                bloqueaFormularioTerapeuta();
                btnAceptar.Enabled = false;
            }
            else
                btnAceptar.Enabled = true;
        }

        private void llenaServicios()
        {
            foreach (Servicio s in servicios)
                checkListServicios.Items.Add(s.nombreServicio, false);
        }

        private bool estaServicio(string nombreServicio, List<Servicio> ss)
        {
            bool resultado = false;
            foreach (Servicio s in ss)
            {
                if (s.nombreServicio.Equals(nombreServicio))
                {
                    resultado = true;
                    break;
                }
            }
            return resultado;
        }

        private void llenaFormularioTerapeuta(Terapeuta terapeuta)
        {
            txtNombres.Text = terapeuta.persona.nombres;
            txtApellidoPaterno.Text = terapeuta.persona.apellidoPaterno;
            txtApellidoMaterno.Text = terapeuta.persona.apellidoMaterno;
            dateFechaNacimiento.Text = "" + terapeuta.fechaNacimiento;
            txtDNI.Text = "" + terapeuta.persona.dni;
            txtTelefono.Text = terapeuta.telefono;

            foreach (Servicio s in servicios)
            {
                if (estaServicio(s.nombreServicio, terapeuta.servicios))
                    checkListServicios.Items.Add(s.nombreServicio, true);
                else
                    checkListServicios.Items.Add(s.nombreServicio, false);
            }

            foreach (HorarioTerapeuta horarioTerapeuta in terapeuta.horarioTerapeuta)
            {
                if (horarioTerapeuta.dia.Equals("Lunes"))
                {
                    dateLunesInicio.Text = "" + horarioTerapeuta.horaInicio;
                    dateLunesFin.Text = "" + horarioTerapeuta.horaFin;
                }
                if (horarioTerapeuta.dia.Equals("Martes"))
                {
                    dateMartesInicio.Text = "" + horarioTerapeuta.horaInicio;
                    dateMartesFin.Text = "" + horarioTerapeuta.horaFin;
                }
                if (horarioTerapeuta.dia.Equals("Miércoles"))
                {
                    dateMiercolesInicio.Text = "" + horarioTerapeuta.horaInicio;
                    dateMiercolesFin.Text = "" + horarioTerapeuta.horaFin;
                }
                if (horarioTerapeuta.dia.Equals("Jueves"))
                {
                    dateJuevesInicio.Text = "" + horarioTerapeuta.horaInicio;
                    dateJuevesFin.Text = "" + horarioTerapeuta.horaFin;
                }
                if (horarioTerapeuta.dia.Equals("Viernes"))
                {
                    dateViernesInicio.Text = "" + horarioTerapeuta.horaInicio;
                    dateViernesFin.Text = "" + horarioTerapeuta.horaFin;
                }
                if (horarioTerapeuta.dia.Equals("Sábado"))
                {
                    dateSabadoInicio.Text = "" + horarioTerapeuta.horaInicio;
                    dateSabadoFin.Text = "" + horarioTerapeuta.horaFin;
                }
            }
        }

        private void bloqueaFormularioTerapeuta()
        {
            txtNombres.ReadOnly = true;
            txtApellidoPaterno.ReadOnly = true;
            txtApellidoMaterno.ReadOnly = true;
            dateFechaNacimiento.Enabled = false;
            txtDNI.ReadOnly = true;
            txtTelefono.ReadOnly = true;
            checkListServicios.SelectionMode = SelectionMode.None;
            dateLunesInicio.Enabled = false;
            dateLunesFin.Enabled = false;
            dateMartesInicio.Enabled = false;
            dateMartesFin.Enabled = false;
            dateMiercolesInicio.Enabled = false;
            dateMiercolesFin.Enabled = false;
            dateJuevesInicio.Enabled = false;
            dateJuevesFin.Enabled = false;
            dateViernesInicio.Enabled = false;
            dateViernesFin.Enabled = false;
            dateSabadoInicio.Enabled = false;
            dateSabadoFin.Enabled = false;
        }

        private bool validarCampoNoVacio(TextBox txtBox)
        {
            errorProvider.Clear();
            bool error = false;
            if (txtBox.TextLength == 0)
            {
                error = true;
                errorProvider.SetError(txtBox, "Llenar Dato");
            }
            return error;
        }

        private bool validarDatos()
        {
            return validarCampoNoVacio(txtNombres) || validarCampoNoVacio(txtApellidoPaterno);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool error = validarDatos();
            if (!error)
            {
                if (modo == 0)
                {
                    Terapeuta terapeuta = new Terapeuta();
                    Persona persona = new Persona();
                    persona.nombres = txtNombres.Text;
                    persona.apellidoPaterno = txtApellidoPaterno.Text;
                    persona.apellidoMaterno = txtApellidoMaterno.Text;
                    persona.dni = int.Parse(txtDNI.Text);
                    persona.estado = "activo";
                    terapeuta.persona = persona;
                    terapeuta.fechaNacimiento = Convert.ToDateTime(dateFechaNacimiento.Text);
                    terapeuta.telefono = txtTelefono.Text;
                    List<Servicio> serviciosxtera = new List<Servicio>();
                    List<HorarioTerapeuta> horarioTerapeuta = new List<HorarioTerapeuta>();

                    foreach (Servicio s in servicios)
                    {
                        foreach (object valor in checkListServicios.CheckedItems)
                        {
                            if (s.nombreServicio.Equals(valor.ToString()))
                            {
                                serviciosxtera.Add(s);
                            }
                        }
                    }
                    terapeuta.servicios = serviciosxtera;

                    HorarioTerapeuta horarioLunes = new HorarioTerapeuta();
                    HorarioTerapeuta horarioMartes = new HorarioTerapeuta();
                    HorarioTerapeuta horarioMiercoles = new HorarioTerapeuta();
                    HorarioTerapeuta horarioJueves = new HorarioTerapeuta();
                    HorarioTerapeuta horarioViernes = new HorarioTerapeuta();
                    HorarioTerapeuta horarioSabado = new HorarioTerapeuta();
                    horarioLunes.dia = "Lunes";
                    horarioMartes.dia = "Martes";
                    horarioMiercoles.dia = "Miércoles";
                    horarioJueves.dia = "Jueves";
                    horarioViernes.dia = "Viernes";
                    horarioSabado.dia = "Sábado";
                    horarioLunes.horaInicio = Convert.ToDateTime(dateLunesInicio.Text);
                    horarioMartes.horaInicio = Convert.ToDateTime(dateMartesInicio.Text);
                    horarioMiercoles.horaInicio = Convert.ToDateTime(dateMiercolesInicio.Text);
                    horarioJueves.horaInicio = Convert.ToDateTime(dateJuevesInicio.Text);
                    horarioViernes.horaInicio = Convert.ToDateTime(dateViernesInicio.Text);
                    horarioSabado.horaInicio = Convert.ToDateTime(dateSabadoInicio.Text);
                    horarioLunes.horaFin = Convert.ToDateTime(dateLunesFin.Text);
                    horarioMartes.horaFin = Convert.ToDateTime(dateMartesFin.Text);
                    horarioMiercoles.horaFin = Convert.ToDateTime(dateMiercolesFin.Text);
                    horarioJueves.horaFin = Convert.ToDateTime(dateJuevesFin.Text);
                    horarioViernes.horaFin = Convert.ToDateTime(dateViernesFin.Text);
                    horarioSabado.horaFin = Convert.ToDateTime(dateSabadoFin.Text);

                    horarioTerapeuta.Add(horarioLunes);
                    horarioTerapeuta.Add(horarioMartes);
                    horarioTerapeuta.Add(horarioMiercoles);
                    horarioTerapeuta.Add(horarioJueves);
                    horarioTerapeuta.Add(horarioViernes);
                    horarioTerapeuta.Add(horarioSabado);

                    terapeuta.horarioTerapeuta = horarioTerapeuta;

                    if (controladorTerapeuta.agregarTerapeuta(terapeuta))
                    {
                        MessageBox.Show("Terapeuta Agregado");
                        this.Dispose();
                        padre.llenarTerapeutas("", "", "", "");
                    }
                    else
                        MessageBox.Show("Ha ocurrido un error");
                }
                else
                {
                    terapeuta.persona.nombres = txtNombres.Text;
                    terapeuta.persona.apellidoPaterno = txtApellidoPaterno.Text;
                    terapeuta.persona.apellidoMaterno = txtApellidoMaterno.Text;
                    terapeuta.persona.dni = int.Parse(txtDNI.Text);
                    terapeuta.fechaNacimiento = Convert.ToDateTime(dateFechaNacimiento.Text);
                    terapeuta.telefono = txtTelefono.Text;
                    List<Servicio> serviciosxtera = new List<Servicio>();

                    foreach (Servicio s in servicios)
                    {
                        foreach (object valor in checkListServicios.CheckedItems)
                        {
                            if (s.nombreServicio.Equals(valor.ToString()))
                            {
                                serviciosxtera.Add(s);
                            }
                        }
                    }
                    terapeuta.servicios = serviciosxtera;

                    foreach (HorarioTerapeuta horarioTerapeuta in terapeuta.horarioTerapeuta)
                    {
                        if (horarioTerapeuta.dia.Equals("Lunes"))
                        {
                            horarioTerapeuta.horaInicio = Convert.ToDateTime(dateLunesInicio.Text);
                            horarioTerapeuta.horaFin = Convert.ToDateTime(dateLunesFin.Text);
                        }
                        if (horarioTerapeuta.dia.Equals("Martes"))
                        {
                            horarioTerapeuta.horaInicio = Convert.ToDateTime(dateMartesInicio.Text);
                            horarioTerapeuta.horaFin = Convert.ToDateTime(dateMartesFin.Text);
                        }
                        if (horarioTerapeuta.dia.Equals("Miércoles"))
                        {
                            horarioTerapeuta.horaInicio = Convert.ToDateTime(dateMiercolesInicio.Text);
                            horarioTerapeuta.horaFin = Convert.ToDateTime(dateMiercolesFin.Text);
                        }
                        if (horarioTerapeuta.dia.Equals("Jueves"))
                        {
                            horarioTerapeuta.horaInicio = Convert.ToDateTime(dateJuevesInicio.Text);
                            horarioTerapeuta.horaFin = Convert.ToDateTime(dateJuevesFin.Text);
                        }
                        if (horarioTerapeuta.dia.Equals("Viernes"))
                        {
                            horarioTerapeuta.horaInicio = Convert.ToDateTime(dateViernesInicio.Text);
                            horarioTerapeuta.horaFin = Convert.ToDateTime(dateViernesFin.Text);
                        }
                        if (horarioTerapeuta.dia.Equals("Sábado"))
                        {
                            horarioTerapeuta.horaInicio = Convert.ToDateTime(dateSabadoInicio.Text);
                            horarioTerapeuta.horaFin = Convert.ToDateTime(dateSabadoFin.Text);
                        }
                    }

                    if (controladorTerapeuta.modificarTerapeuta(terapeuta))
                    {
                        MessageBox.Show("Terapeuta Modificado");
                        this.Dispose();
                        padre.llenarTerapeutas("", "", "", "");
                    }
                    else
                        MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
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

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar.Equals('-') || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtApellidoPaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar.Equals('-') || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtApellidoMaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar.Equals('-') || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar.Equals('-') || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
