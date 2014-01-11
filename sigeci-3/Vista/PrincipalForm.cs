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

namespace Vista
{
    public partial class PrincipalForm : Office2007Form
    {
        public PrincipalForm()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void PrincipalForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (resultado == DialogResult.Yes)
            //{
                Application.Exit();
            //}
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            CitasForm citasForm = new CitasForm();
            citasForm.ShowDialog();
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            PacientesForm pacientesForm = new PacientesForm();
            pacientesForm.ShowDialog();
        }

        private void btnServicios_Click(object sender, EventArgs e)
        {
            ServiciosForm serviciosForm = new ServiciosForm();
            serviciosForm.ShowDialog();
        }

        private void btnTerapeutas_Click(object sender, EventArgs e)
        {
            TerapeutaForm terapeutaForm = new TerapeutaForm();
            terapeutaForm.ShowDialog();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            UsuariosForm usuariosForm = new UsuariosForm();
            usuariosForm.ShowDialog();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesForm reportesForm = new ReportesForm();
            reportesForm.ShowDialog();            
        }

    }
}