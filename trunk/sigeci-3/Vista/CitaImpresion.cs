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
using Microsoft.Reporting.WinForms;

namespace Vista
{
    public partial class CitaImpresion : Office2007Form
    {
        private Cita cita;
        private string nombreTerapeuta;

        public CitaImpresion(Cita cita, string nombreTerapeuta)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.cita = cita;
            this.nombreTerapeuta = nombreTerapeuta;
        }

        private void CitaImpresion_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
            
            ReportParameter[] parametros = new ReportParameter[8];
            parametros[0] = new ReportParameter("idCita", cita.idCita.ToString());
            parametros[1] = new ReportParameter("fechaRegistro", cita.fechaRegistro.ToShortDateString());
            parametros[2] = new ReportParameter("fechaCita", cita.fechaRegistro.ToShortDateString());
            parametros[3] = new ReportParameter("horaCita", cita.horaCita.ToShortTimeString());
            parametros[4] = new ReportParameter("servicio", cita.servicio.nombreServicio);
            parametros[5] = new ReportParameter("terapeuta", nombreTerapeuta);
            parametros[6] = new ReportParameter("paciente", cita.paciente.persona.nombres + " " + cita.paciente.persona.apellidoPaterno + " " + cita.paciente.persona.apellidoMaterno);
            parametros[7] = new ReportParameter("estado", cita.estado);

            reportViewer1.LocalReport.SetParameters(parametros);

            reportViewer1.RefreshReport();
        }
    }
}
