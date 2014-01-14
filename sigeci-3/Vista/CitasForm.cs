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
            //cancelar las citas que se pasaron los dos dias antes de confirmar
        }

        private void CitasForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CitasFornulario citasFormulario = new CitasFornulario(this);
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
                fila = new string[] { "" + cita.idCita, "" + cita.fechaCita.ToShortDateString(), "" + cita.horaCita.ToShortTimeString(), cita.servicio.nombreServicio, cita.paciente.persona.nombres + " " + cita.paciente.persona.apellidoPaterno + " " + cita.paciente.persona.apellidoMaterno, "" + cita.costo, "" + suma, cita.estado, cita.terapeuta.persona.nombres + " "+cita.terapeuta.persona.apellidoPaterno + " "+cita.terapeuta.persona.apellidoMaterno };
                dgvCitas.Rows.Add(fila);
                foreach (DataGridViewRow row in dgvCitas.Rows)
                {
                    if (row.Cells[7].Value.ToString().Equals("Reservado"))
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    if (row.Cells[7].Value.ToString().Equals("Confirmado"))
                    {
                        row.DefaultCellStyle.BackColor = Color.MediumSpringGreen;
                    }
                    if (row.Cells[7].Value.ToString().Equals("Cancelado"))
                    {
                        row.DefaultCellStyle.BackColor = Color.Tomato;
                    }
                }
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

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea confirmar esta cita?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    if (dgvCitas.CurrentRow.Cells[7].Value.ToString().Equals("Reservado"))
                    {
                        Cita cita = buscarCita(int.Parse(dgvCitas.CurrentRow.Cells[0].Value.ToString()));
                        Pago pago = new Pago();
                        pago.idCita = cita.idCita;
                        pago.fecha = DateTime.Now;
                        pago.monto = int.Parse(dgvCitas.CurrentRow.Cells[5].Value.ToString()) - int.Parse(dgvCitas.CurrentRow.Cells[6].Value.ToString());
                        pago.estado = "Confirmado";
                        if (controladorCita.confirmarCita(cita, pago))
                        {
                            MessageBox.Show("Cita confirmada");
                            llenarCitas("", "", "", cita.servicio.idServicio.ToString(), Convert.ToDateTime(dgvCitas.CurrentRow.Cells[1].Value.ToString()));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecciona una cita reservada para confirmar");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("No ha seleccionado una cita");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar esta cita?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    if (!dgvCitas.CurrentRow.Cells[7].Value.ToString().Equals("Cancelado"))
                    {
                        Cita cita = buscarCita(int.Parse(dgvCitas.CurrentRow.Cells[0].Value.ToString()));
                        Pago pago = new Pago();
                        pago.idCita = cita.idCita;
                        pago.fecha = DateTime.Now;
                        pago.monto = (-1) * int.Parse(dgvCitas.CurrentRow.Cells[6].Value.ToString());
                        pago.estado = "Cancelado";
                        if (controladorCita.cancelarCita(cita, pago))
                        {
                            MessageBox.Show("Cita cancelada");
                            llenarCitas("", "", "", cita.servicio.idServicio.ToString(), Convert.ToDateTime(dgvCitas.CurrentRow.Cells[1].Value.ToString()));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una cita reservada o confirmada");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("No ha seleccionado una cita");
                }
            }
        }

        private void txtNumeroCita_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
        }



    }
}
