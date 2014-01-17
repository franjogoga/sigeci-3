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
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    e.Cancel = false;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }          
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
            //ReportesForm reportesForm = new ReportesForm();
            //reportesForm.ShowDialog();            
        }

        private void btnReporteCitas_Click(object sender, EventArgs e)
        {

        }

        private void btnReporteCitasxPaciente_Click(object sender, EventArgs e)
        {
            CitasxPaciente citasxPaciente = new CitasxPaciente();
            citasxPaciente.ShowDialog();
        }

        private void btnReportePagos_Click(object sender, EventArgs e)
        {
            ReportePagos reportePagos = new ReportePagos();            
            reportePagos.ShowDialog();            
        }

        private void btnReporteCitasxTerapeuta_Click(object sender, EventArgs e)
        {
            ReporteCitasxTerapeuta reporteCitasxTerapeuta = new ReporteCitasxTerapeuta();
            reporteCitasxTerapeuta.ShowDialog();
        }
       

    }
}