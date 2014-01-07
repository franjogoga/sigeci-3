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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (modo == 0)
            {
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
    }
}
