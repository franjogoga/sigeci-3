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
    public partial class ReportePagos : Office2007Form
    {
        private ControladorPago controladorPago = ControladorPago.Instancia();
        private List<Pago> pagos = null;
        public ReportePagos()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void llenarPagos(DateTime fechaDesde, DateTime fechaHasta)
        {
            string[] fila;
            pagos = controladorPago.getListaPagos(fechaDesde,fechaHasta);
            dgvPagos.Rows.Clear();
            foreach (Pago pago in pagos)
            {
                fila = new string[] { ""+pago.idPago, "" + pago.idCita, "" + pago.monto, "" + pago.fecha.ToShortDateString(), pago.estado};
                dgvPagos.Rows.Add(fila);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarPagos(dateDesde.Value, dateHasta.Value);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (pagos != null)
            {
                ReportePagosRpt reportePagosRpt = new ReportePagosRpt(dateDesde.Value, dateHasta.Value, pagos);
                reportePagosRpt.Show();
            }
            else
            {
                MessageBox.Show("Primero realice una búsqueda antes de imprimir");
            }
        }
    }
}
