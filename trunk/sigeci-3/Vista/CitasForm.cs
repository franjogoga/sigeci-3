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
    public partial class CitasForm : Office2007Form
    {
        private ControladorCita controladorCita = ControladorCita.Instancia();
        private ControladorServicio controladorServicio = ControladorServicio.Instancia();
        private List<Cita> citas;
        public List<Servicio> servicios;

        public CitasForm()
        {            
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            llenarServicios();
        }

        private void CitasForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CitasFornulario citasFormulario = new CitasFornulario();
            citasFormulario.ShowDialog();
        }

        public void llenarServicios()
        {
            servicios = controladorServicio.getListaServicios("");            
            comboServicios.DataSource = servicios;
            comboServicios.DisplayMember = "nombreServicio";
            comboServicios.ValueMember = "idServicio";            
        }

        public void llenarCitas(string strNumeroCita, string nombres, string apellidoPaterno, string strIdServicio, DateTime strFecha)
        {
            string[] fila;
            citas = controladorCita.getListaCitas(strNumeroCita, nombres, apellidoPaterno, strIdServicio, strFecha);
            dgvCitas.Rows.Clear();
            foreach (Cita cita in citas)
            {
                float suma = 0;
                foreach (Pago p in cita.pagos)
                {
                    suma += p.monto;
                }
                fila = new string[] { "" + cita.idCita, "" + cita.fechaCita.ToShortDateString(), "" + cita.horaCita.ToShortTimeString(), cita.servicio.nombreServicio, cita.paciente.persona.nombres + " " + cita.paciente.persona.apellidoPaterno + " " + cita.paciente.persona.apellidoMaterno, "" + cita.costo, "" + suma, cita.estado };
                dgvCitas.Rows.Add(fila);
            }
        }

        private Cita buscarCita(int id)
        {
            Cita cita = null;
            foreach (Cita c in citas)
            {
                if (id == c.idCita)
                {
                    cita = c;
                    break;
                }
            }
            return cita;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                llenarCitas(txtNumeroCita.Text, txtNombres.Text, txtApellidoPaterno.Text, comboServicios.SelectedValue.ToString(), dateFechaCita.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBoxEx.Show("Seleccione un las opciones correspondientes");
            }
        }



    }
}
