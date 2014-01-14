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
    public partial class UsuariosFormulario : Office2007Form
    {
        private ControladorUsuario controladorUsuario = ControladorUsuario.Instancia();
        private UsuariosForm padre;
        private int modo;
        private Usuario usuario;

        public UsuariosFormulario(UsuariosForm usuariosForm, int modo, Usuario usuario)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.usuario = usuario;
            InitializeComponent();
            this.padre = usuariosForm;
            this.modo = modo;
            if (modo == 2) //modificar
            {
                llenaFormularioUsuario(usuario);
            }
            if (modo == 1) //ver
            {
                llenaFormularioUsuario(usuario);
                bloqueaFormularioUsuario();
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
            return validarCampoNoVacio(txtUsername) || validarCampoNoVacio(txtPassword) || validarCampoNoVacio(txtNombres) || validarCampoNoVacio(txtApellidoPaterno);
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

                    Usuario usuario = new Usuario();
                    Persona persona = new Persona();
                    persona.nombres = txtNombres.Text;
                    persona.apellidoPaterno = txtApellidoPaterno.Text;
                    persona.apellidoMaterno = txtApellidoMaterno.Text;
                    persona.dni = int.Parse(txtDNI.Text);
                    persona.estado = "activo";
                    usuario.persona = persona;
                    usuario.username = txtUsername.Text;
                    usuario.password = txtPassword.Text;

                    if (controladorUsuario.agregarUsuario(usuario))
                    {
                        MessageBox.Show("Usuario Agregado");
                        this.Dispose();
                        padre.llenarUsuarios("", "", "", "");
                    }
                    else
                        MessageBox.Show("Ha ocurrido un error");
                }
                else
                {
                    if (txtApellidoMaterno.Text.Equals("")) txtApellidoMaterno.Text = "-";
                    if (txtDNI.Text.Equals("")) txtDNI.Text = "0";

                    usuario.username = txtUsername.Text;
                    usuario.password = txtPassword.Text;
                    usuario.persona.nombres = txtNombres.Text;
                    usuario.persona.apellidoPaterno = txtApellidoPaterno.Text;
                    usuario.persona.apellidoMaterno = txtApellidoMaterno.Text;
                    usuario.persona.dni = int.Parse(txtDNI.Text);

                    if (controladorUsuario.modificarUsuario(usuario))
                    {
                        MessageBox.Show("Usuario Modificado");
                        this.Dispose();
                        padre.llenarUsuarios("", "", "", "");
                    }
                    else
                        MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void bloqueaFormularioUsuario()
        {
            txtNombres.ReadOnly = true;
            txtPassword.ReadOnly = true;
            txtUsername.ReadOnly = true;
            txtApellidoPaterno.ReadOnly = true;
            txtApellidoMaterno.ReadOnly = true;
            txtDNI.ReadOnly = true;
        }

        private void llenaFormularioUsuario(Usuario usuario)
        {
            txtNombres.Text = usuario.persona.nombres;
            txtPassword.Text = usuario.password;
            txtApellidoPaterno.Text = usuario.persona.apellidoPaterno;
            txtApellidoMaterno.Text = usuario.persona.apellidoMaterno;
            txtUsername.Text = usuario.username;
            txtDNI.Text = "" + usuario.persona.dni;
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

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) ||  Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
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
    }
}
