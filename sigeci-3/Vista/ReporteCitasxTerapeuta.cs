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
    public partial class ReporteCitasxTerapeuta : Office2007Form
    {
        private ControladorCita controladorCita = ControladorCita.Instancia();
        private ControladorServicio controladorServicio = ControladorServicio.Instancia();
        private ControladorTerapeuta controladorTerapeuta = ControladorTerapeuta.Instancia();
        private List<Cita> citas;
        private List<ReporteCita> reporteCitas=null;
        private List<Terapeuta> terapeutas;
        private List<Servicio> servicios;

        public ReporteCitasxTerapeuta()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            llenarServicios();
        }

        public void llenarServicios()
        {
            servicios = controladorServicio.getListaServicios("");
            comboServicios.DataSource = servicios;
            comboServicios.DisplayMember = "nombreServicio";
            comboServicios.ValueMember = "idServicio";
        }

        private void llenarTerapeutas(int idServicio)
        {
            terapeutas = controladorTerapeuta.getListaTerapeutasxServicio(idServicio);
            comboTerapeuta.Items.Clear();
            comboTerapeuta.DisplayMember = "nombreCompleto";
            comboTerapeuta.ValueMember = "idTerapeuta";

            foreach (Terapeuta t in terapeutas)
            {
                comboTerapeuta.Items.Add(new TerapeutaCombo { nombreCompleto = t.persona.nombres + " " + t.persona.apellidoPaterno + " " + t.persona.apellidoMaterno, idTerapeuta = t.persona.idPersona });
            }
        }

        private void comboServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            Servicio s = new Servicio();
            s = comboServicios.SelectedItem as Servicio;
            llenarTerapeutas(s.idServicio);                        
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                llenarCitas(dateDesde.Value, dateHasta.Value, (comboTerapeuta.SelectedItem as TerapeutaCombo).idTerapeuta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Seleccione un terapeuta");
            }
        }

        private void llenarCitas(DateTime fechaDesde, DateTime fechaHasta, int idTerapeuta)
        {            
            string[] fila;
            citas = controladorCita.getListaCitasxTerapeuta(fechaDesde, fechaHasta, idTerapeuta);
            reporteCitas = new List<ReporteCita>();
            dgvCitas.Rows.Clear();
            foreach (Cita cita in citas)
            {
                ReporteCita reporteCita = new ReporteCita();
                reporteCita.idCita = cita.idCita;
                reporteCita.paciente = cita.paciente.persona.nombres + " " + cita.paciente.persona.apellidoPaterno + " " + cita.paciente.persona.apellidoMaterno;
                reporteCita.fecha = cita.fechaCita;
                reporteCita.hora = cita.horaCita;
                reporteCita.estado = cita.estado;
                reporteCitas.Add(reporteCita);

                fila = new string[] { "" + cita.idCita, cita.paciente.persona.nombres+" "+cita.paciente.persona.apellidoPaterno+" "+cita.paciente.persona.apellidoMaterno,cita.fechaCita.ToShortDateString(), cita.horaCita.ToShortTimeString(), cita.estado};
                dgvCitas.Rows.Add(fila);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (reporteCitas != null)
            {
                ReporteCitasxTerapeutaRpt reporteCitasxTerapeutaRpt = new ReporteCitasxTerapeutaRpt(dateDesde.Value, dateHasta.Value, reporteCitas);
                reporteCitasxTerapeutaRpt.Show();
            }
            else
            {
                MessageBox.Show("Primero realice una búsqueda antes de imprimir");
            }
        }


    }
}
