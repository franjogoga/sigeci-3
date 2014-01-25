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
    public partial class ReporteCitasxTerapeutaRpt : Office2007Form
    {
        private DateTime fechaDesde;
        private DateTime fechaHasta;
        private List<ReporteCita> reporteCitas;
        private string terapeuta;
        private string servicio;

        public ReporteCitasxTerapeutaRpt(DateTime fechaDesde, DateTime fechaHasta, List<ReporteCita> reporteCitas, string terapeuta, string servicio)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.fechaDesde = fechaDesde;
            this.fechaHasta = fechaHasta;
            this.reporteCitas = reporteCitas;
            this.terapeuta = terapeuta;
            this.servicio = servicio;
        }

        private void ReporteCitasxTerapeutaRpt_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();

            ReportParameter[] parametros = new ReportParameter[4];
            parametros[0] = new ReportParameter("pFechaDesde", fechaDesde.ToShortDateString());
            parametros[1] = new ReportParameter("pFechaHasta", fechaHasta.ToShortDateString());
            parametros[2] = new ReportParameter("servicio", servicio);
            parametros[3] = new ReportParameter("terapeuta", terapeuta);

            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetReporteCitas", reporteCitas));

            reportViewer1.LocalReport.SetParameters(parametros);

            reportViewer1.RefreshReport();
        }
    }
}
