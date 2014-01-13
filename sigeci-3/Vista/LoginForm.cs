using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Controlador;

namespace Vista
{
    public partial class LoginForm : Office2007Form
    {
        ControladorUsuario controladorUsuario = ControladorUsuario.Instancia();

        public LoginForm()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (controladorUsuario.logea(txtUsername.Text, txtPassword.Text))
            {
                PrincipalForm principalForm = new PrincipalForm();
                principalForm.Show();
                this.Hide();
            }
            else
            {
                lblError.Visible = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
