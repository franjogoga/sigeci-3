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
    public partial class PacientesFormulario : Office2007Form
    {
        private ControladorPaciente controladorPaciente = ControladorPaciente.Instancia();
        private PacientesForm padre;
        private int modo;
        private Paciente paciente;

        public PacientesFormulario(PacientesForm pacientesForm, int modo, Paciente paciente)
        {
            this.paciente = paciente;
            InitializeComponent();
            txtEdad.ReadOnly = true;
            rbNo.Checked = true;
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.padre = pacientesForm;
            this.modo = modo;
            if (modo == 2) //modificar
            {
                llenaFormularioPaciente(paciente);
                txtNumeroHistoria.ReadOnly = true;
            }
            if (modo == 1) //ver
            {
                llenaFormularioPaciente(paciente);
                bloqueaFormularioPaciente();
                btnAceptar.Enabled = false;
            }
            else
                btnAceptar.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
                    if (txtApellidoMaterno.Text.Equals("")) txtApellidoMaterno.Text = "-";
                    if (txtDNI.Text.Equals("")) txtDNI.Text = "0";
                    if (txtLugarNacimiento.Text.Equals("")) txtLugarNacimiento.Text = "-";
                    if (txtDomicilio.Text.Equals("")) txtDomicilio.Text = "-";
                    if (txtDistrito.Text.Equals("")) txtDistrito.Text = "-";
                    if (txtTelefonoCasa.Text.Equals("")) txtTelefonoCasa.Text = "-";
                    if (txtCorreo.Text.Equals("")) txtCorreo.Text = "-";
                    if (txtComoEntero.Text.Equals("")) txtComoEntero.Text = "-";
                    if (txtNombrePadre.Text.Equals("")) txtNombrePadre.Text = "-";
                    if (txtNombreMadre.Text.Equals("")) txtNombreMadre.Text = "-";
                    if (txtCelularPadre.Text.Equals("")) txtCelularPadre.Text = "-";
                    if (txtCelularMadre.Text.Equals("")) txtCelularMadre.Text = "-";
                    if (txtNombreColegio.Text.Equals("")) txtNombreColegio.Text = "-";
                    if (txtUbicacionColegio.Text.Equals("")) txtUbicacionColegio.Text = "-";
                    if (txtCelular.Text.Equals("")) txtCelular.Text = "-";
                    if (txtGradoInstruccion.Text.Equals("")) txtGradoInstruccion.Text = "-";
                    if (txtOcupacion.Text.Equals("")) txtOcupacion.Text = "-";
                    if (txtLugarLaboral.Text.Equals("")) txtLugarLaboral.Text = "-";

                    Persona persona = new Persona();
                    persona.nombres = txtNombres.Text;
                    persona.apellidoPaterno = txtApellidoPaterno.Text;                    
                    persona.apellidoMaterno = txtApellidoMaterno.Text;                    
                    persona.dni = int.Parse(txtDNI.Text);
                    persona.estado = "activo";
                    Paciente paciente = new Paciente();
                    paciente.fechaNacimiento = Convert.ToDateTime(dateFechaNacimiento.Text);                    
                    paciente.lugarNacimiento = txtLugarNacimiento.Text;                    
                    paciente.domicilio = txtDomicilio.Text;                    
                    paciente.distrito = txtDistrito.Text;                    
                    paciente.telefonoCasa = txtTelefonoCasa.Text;                    
                    paciente.correo = txtCorreo.Text;                    
                    paciente.comoEntero = txtComoEntero.Text;
                    MenorEdad menorEdad = new MenorEdad();                    
                    menorEdad.nombrePadre = txtNombrePadre.Text;                    
                    menorEdad.nombreMadre = txtNombreMadre.Text;                    
                    menorEdad.celularPadre = txtCelularPadre.Text;                    
                    menorEdad.celularMadre = txtCelularMadre.Text;                    
                    menorEdad.nombreColegio = txtNombreColegio.Text;                    
                    menorEdad.ubicacionColegio = txtUbicacionColegio.Text;
                    if (rbSi.Checked) menorEdad.escolaridad = "si"; else menorEdad.escolaridad = "no";
                    MayorEdad mayorEdad = new MayorEdad();                    
                    mayorEdad.celular = txtCelular.Text;                    
                    mayorEdad.gradoInstruccion = txtGradoInstruccion.Text;                    
                    mayorEdad.ocupacion = txtOcupacion.Text;                    
                    mayorEdad.lugarLaboral = txtLugarLaboral.Text;
                    paciente.persona = persona;
                    paciente.menorEdad = menorEdad;
                    paciente.mayorEdad = mayorEdad;

                    if (controladorPaciente.agregarPaciente(paciente))
                    {
                        MessageBox.Show("Paciente Agregado");
                        this.Dispose();
                        padre.llenarPacientes("", "", "", "", "");
                    }
                    else
                        MessageBox.Show("Ha ocurrido un error");
                }
                else
                {
                    if (txtApellidoMaterno.Text.Equals("")) txtApellidoMaterno.Text = "-";
                    if (txtDNI.Text.Equals("")) txtDNI.Text = "0";
                    if (txtLugarNacimiento.Text.Equals("")) txtLugarNacimiento.Text = "-";
                    if (txtDomicilio.Text.Equals("")) txtDomicilio.Text = "-";
                    if (txtDistrito.Text.Equals("")) txtDistrito.Text = "-";
                    if (txtTelefonoCasa.Text.Equals("")) txtTelefonoCasa.Text = "-";
                    if (txtCorreo.Text.Equals("")) txtCorreo.Text = "-";
                    if (txtComoEntero.Text.Equals("")) txtComoEntero.Text = "-";
                    if (txtNombrePadre.Text.Equals("")) txtNombrePadre.Text = "-";
                    if (txtNombreMadre.Text.Equals("")) txtNombreMadre.Text = "-";
                    if (txtCelularPadre.Text.Equals("")) txtCelularPadre.Text = "-";
                    if (txtCelularMadre.Text.Equals("")) txtCelularMadre.Text = "-";
                    if (txtNombreColegio.Text.Equals("")) txtNombreColegio.Text = "-";
                    if (txtUbicacionColegio.Text.Equals("")) txtUbicacionColegio.Text = "-";
                    if (txtCelular.Text.Equals("")) txtCelular.Text = "-";
                    if (txtGradoInstruccion.Text.Equals("")) txtGradoInstruccion.Text = "-";
                    if (txtOcupacion.Text.Equals("")) txtOcupacion.Text = "-";
                    if (txtLugarLaboral.Text.Equals("")) txtLugarLaboral.Text = "-";

                    paciente.persona.nombres = txtNombres.Text;
                    paciente.persona.apellidoPaterno = txtApellidoPaterno.Text;
                    paciente.persona.apellidoMaterno = txtApellidoMaterno.Text;
                    paciente.persona.dni = int.Parse(txtDNI.Text);
                    paciente.lugarNacimiento = txtLugarNacimiento.Text;
                    paciente.fechaNacimiento = Convert.ToDateTime(dateFechaNacimiento.Text);
                    paciente.domicilio = txtDomicilio.Text;
                    paciente.distrito = txtDistrito.Text;
                    paciente.telefonoCasa = txtTelefonoCasa.Text;
                    paciente.correo = txtCorreo.Text;
                    paciente.comoEntero = txtComoEntero.Text;
                    paciente.menorEdad.nombrePadre = txtNombrePadre.Text;
                    paciente.menorEdad.nombreMadre = txtNombreMadre.Text;
                    paciente.menorEdad.celularPadre = txtCelularPadre.Text;
                    paciente.menorEdad.celularMadre = txtCelularMadre.Text;
                    if (rbSi.Checked)
                        paciente.menorEdad.escolaridad = "si";
                    else
                        paciente.menorEdad.escolaridad = "no";
                    paciente.menorEdad.nombreColegio = txtNombreColegio.Text;
                    paciente.menorEdad.ubicacionColegio = txtUbicacionColegio.Text;
                    paciente.mayorEdad.celular = txtCelular.Text;
                    paciente.mayorEdad.ocupacion = txtOcupacion.Text;
                    paciente.mayorEdad.gradoInstruccion = txtGradoInstruccion.Text;
                    paciente.mayorEdad.lugarLaboral = txtLugarLaboral.Text;

                    if (controladorPaciente.modificarPaciente(paciente))
                    {
                        MessageBox.Show("Paciente Modificado");
                        this.Dispose();
                        padre.llenarPacientes("", "", "", "", "");
                    }
                    else
                        MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void bloqueaFormularioPaciente()
        {
            txtNumeroHistoria.ReadOnly = true;
            txtNombres.ReadOnly = true;
            txtApellidoPaterno.ReadOnly = true;
            txtApellidoMaterno.ReadOnly = true;
            txtDNI.ReadOnly = true;
            txtLugarNacimiento.ReadOnly = true;
            dateFechaNacimiento.Enabled = false;
            txtEdad.ReadOnly = true;
            txtDomicilio.ReadOnly = true;
            txtDistrito.ReadOnly = true;
            txtTelefonoCasa.ReadOnly = true;
            txtCorreo.ReadOnly = true;
            txtComoEntero.ReadOnly = true;
            txtNombrePadre.ReadOnly = true;
            txtNombreMadre.ReadOnly = true;
            txtCelularPadre.ReadOnly = true;
            txtCelularMadre.ReadOnly = true;
            rbSi.Enabled = false;
            rbNo.Enabled = false;
            txtNombreColegio.ReadOnly = true;
            txtUbicacionColegio.ReadOnly = true;
            txtCelular.ReadOnly = true;
            txtOcupacion.ReadOnly = true;
            txtGradoInstruccion.ReadOnly = true;
            txtLugarLaboral.ReadOnly = true;
        }

        private void llenaFormularioPaciente(Paciente paciente)
        {
            txtNumeroHistoria.Text = "" + paciente.numeroHistoria;
            txtNombres.Text = paciente.persona.nombres;
            txtApellidoPaterno.Text = paciente.persona.apellidoPaterno;
            txtApellidoMaterno.Text = paciente.persona.apellidoMaterno;
            txtDNI.Text = "" + paciente.persona.dni;
            txtLugarNacimiento.Text = paciente.lugarNacimiento;
            dateFechaNacimiento.Text = "" + paciente.fechaNacimiento;
            txtDomicilio.Text = paciente.domicilio;
            txtDistrito.Text = paciente.distrito;
            txtTelefonoCasa.Text = paciente.telefonoCasa;
            txtCorreo.Text = paciente.correo;
            txtComoEntero.Text = paciente.comoEntero;
            txtNombrePadre.Text = paciente.menorEdad.nombrePadre;
            txtNombreMadre.Text = paciente.menorEdad.nombreMadre;
            txtCelularPadre.Text = paciente.menorEdad.celularPadre;
            txtCelularMadre.Text = paciente.menorEdad.celularMadre;
            if (paciente.menorEdad.escolaridad.ToLower().Equals("si"))
            {
                rbSi.Checked = true;
                rbNo.Checked = false;
            }
            else
            {
                rbSi.Checked = false;
                rbNo.Checked = true;
            }

            txtNombreColegio.Text = paciente.menorEdad.nombreColegio;
            txtUbicacionColegio.Text = paciente.menorEdad.ubicacionColegio;
            txtCelular.Text = paciente.mayorEdad.celular;
            txtOcupacion.Text = paciente.mayorEdad.ocupacion;
            txtGradoInstruccion.Text = paciente.mayorEdad.gradoInstruccion;
            txtLugarLaboral.Text = paciente.mayorEdad.lugarLaboral;

            int edad = calculaEdad(dateFechaNacimiento.Value);
            txtEdad.Text = "" + edad;
        }

        private void txtNumeroHistoria_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtLugarNacimiento_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDistrito_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Char.IsDigit(e.KeyChar) || Char.IsLetter(e.KeyChar) || e.KeyChar.Equals('-') || Char.IsWhiteSpace(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void txtTelefonoCasa_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtComoEntero_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || e.KeyChar.Equals('-') || e.KeyChar.Equals('@') || e.KeyChar.Equals('.') || e.KeyChar.Equals('_') || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtNombrePadre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar.Equals('-') || e.KeyChar.Equals('.') || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtNombreMadre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar.Equals('-') || e.KeyChar.Equals('.') || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCelularPadre_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCelularMadre_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNombreColegio_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtUbicacionColegio_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtOcupacion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtGradoInstruccion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtLugarLaboral_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
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

        public int calculaEdad(DateTime fechaNacimiento)
        {            
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            int difmes = DateTime.Now.Month - fechaNacimiento.Month;
            int difdias = DateTime.Now.Day - fechaNacimiento.Day;
            if (!((difmes == 0 && difdias > 0) || (difmes > 0))) edad--;            
            //DateTime nacimientoAhora = fechaNacimiento.AddYears(edad);
            ////Le resto un año si la fecha actual es anterior 
            ////al día de nacimiento.
            //if (DateTime.Now.CompareTo(nacimientoAhora) > 0)
            //{ 
            //    edad--; 
            //}
            return edad;
        }

        private void dateFechaNacimiento_ValueChanged(object sender, EventArgs e)
        {
            int edad = calculaEdad(dateFechaNacimiento.Value);
            txtEdad.Text = "" + edad;
        }

    }
}