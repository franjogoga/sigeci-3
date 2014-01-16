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
    public partial class ReportePagosRpt : Office2007Form
    {
        private DateTime fechaDesde;
        private DateTime fechaHasta;
        private List<Pago> pagos;

        public ReportePagosRpt(DateTime fechaDesde, DateTime fechaHasta, List<Pago> pagos)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.fechaDesde = fechaDesde;
            this.fechaHasta = fechaHasta;
            this.pagos = pagos;
        }

        private void ReportePagosRpt_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();

            ReportParameter[] parametros = new ReportParameter[2];
            parametros[0] = new ReportParameter("pFechaDesde", fechaDesde.ToShortDateString());
            parametros[1] = new ReportParameter("pFechaHasta", fechaHasta.ToShortDateString());

            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("datasetPago", pagos));

            reportViewer1.LocalReport.SetParameters(parametros);

            reportViewer1.RefreshReport();
        }
    }
}
