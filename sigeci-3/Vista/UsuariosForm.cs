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
    public partial class UsuariosForm : Office2007Form
    {
        private ControladorUsuario controladorUsuario = ControladorUsuario.Instancia();
        private List<Usuario> usuarios;
        public UsuariosForm()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void UsuariosForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            UsuariosFormulario usuariosFormulario = new UsuariosFormulario(this, 0, null);
            usuariosFormulario.ShowDialog();
        }

        public void llenarUsuarios(string username, string nombres, string apellidoPaterno, string apellidoMaterno)
        {
            string[] fila;
            usuarios = controladorUsuario.getListaUsuarios(username, nombres, apellidoPaterno, apellidoMaterno);
            dgvUsuarios.Rows.Clear();
            foreach (Usuario usuario in usuarios)
            {
                fila = new string[] { "" + usuario.persona.idPersona, usuario.username, usuario.persona.nombres, usuario.persona.apellidoPaterno + " " + usuario.persona.apellidoMaterno, "" + usuario.persona.dni };
                dgvUsuarios.Rows.Add(fila);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarUsuarios(txtUsername.Text, txtNombre.Text, txtApellidoPaterno.Text, txtApellidoMaterno.Text);
        }

        private Usuario buscarUsuario(int id)
        {
            Usuario usuario = null;
            foreach (Usuario u in usuarios)
            {
                if (id == u.persona.idPersona)
                {
                    usuario = u;
                    break;
                }
            }
            return usuario;
        }

        private void dgvUsuarios_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                Usuario usuario = buscarUsuario(int.Parse(dgvUsuarios.CurrentRow.Cells[0].Value.ToString()));
                UsuariosFormulario usuariosFormulario = new UsuariosFormulario(this, 1, usuario);
                usuariosFormulario.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado un usuario");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = buscarUsuario(int.Parse(dgvUsuarios.CurrentRow.Cells[0].Value.ToString()));
                UsuariosFormulario usuariosFormulario = new UsuariosFormulario(this, 2, usuario);
                usuariosFormulario.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("No ha seleccionado un usuario");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar este usuario?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    Usuario usuario = buscarUsuario(int.Parse(dgvUsuarios.CurrentRow.Cells[0].Value.ToString()));
                    if (controladorUsuario.eliminarUsuario(usuario))
                    {
                        MessageBox.Show("Usuario eliminado");
                        llenarUsuarios("", "", "", "");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("No ha seleccionado un usuario");
                }
            }
        }
    }
}
