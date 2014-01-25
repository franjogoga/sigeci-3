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



            reportViewer1.RefreshReport();
        }
    }
}
