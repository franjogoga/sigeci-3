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
using Modelo;
using Controlador;

namespace Vista
{
    public partial class ServiciosFormulario : Office2007Form
    {
        private ControladorServicio controladorServicio = ControladorServicio.Instancia();
        private ServiciosForm padre;
        private int modo;
        private Servicio servicio;

        public ServiciosFormulario(ServiciosForm serviciosForm, int modo, Servicio servicio)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.servicio = servicio;
            this.padre = serviciosForm;
            this.modo = modo;
            if (modo == 2) //modificar
            {
                llenaFormularioServicio(servicio);
            }
            if (modo == 1) //ver
            {
                llenaFormularioServicio(servicio);
                bloqueaFormularioServicio();
                btnAceptar.Enabled = false;
            }
            else
                btnAceptar.Enabled = true;
        }

        private void bloqueaFormularioServicio()
        {
            txtNombreServicio.ReadOnly = true;
            txtIntervaloHora.ReadOnly = true;
            txtCosto.ReadOnly = true;
            txtMaximoPacientes.ReadOnly = true;
        }

        private void llenaFormularioServicio(Servicio servicio)
        {
            txtNombreServicio.Text = servicio.nombreServicio;
            txtIntervaloHora.Text = "" + servicio.intervaloHora;
            txtCosto.Text = "" + servicio.costo;
            txtMaximoPacientes.Text = "" + servicio.maximoPacientes;
        }

        private bool validarCampoNoVacio(TextBox txtBox)
        {
            errorProvider.Clear();
            bool error = false;
            if (txtBox.TextLength == 0)
            {
                error = true;
                errorProvider.SetError(txtBox, "Llenar Dato");
            }
            return error;
        }

        private bool validarDatos()
        {
            return validarCampoNoVacio(txtNombreServicio) || validarCampoNoVacio(txtIntervaloHora) || validarCampoNoVacio(txtCosto) || validarCampoNoVacio(txtMaximoPacientes);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool error = validarDatos();
            if (!error)
            {
                if (modo == 0)
                {
                    Servicio servicio = new Servicio();
                    servicio.nombreServicio = txtNombreServicio.Text;
                    servicio.intervaloHora = int.Parse(txtIntervaloHora.Text);
                    servicio.costo = float.Parse(txtCosto.Text);
                    servicio.maximoPacientes = int.Parse(txtMaximoPacientes.Text);
                    servicio.estado = "activo";

                    if (controladorServicio.agregarServicio(servicio))
                    {
                        MessageBox.Show("Servicio Agregado");
                        this.Dispose();
                        padre.llenarServicios("");
                    }
                    else
                        MessageBox.Show("Ha ocurrido un error");
                }
                else
                {
                    servicio.nombreServicio = txtNombreServicio.Text;
                    servicio.intervaloHora = int.Parse(txtIntervaloHora.Text);
                    servicio.costo = float.Parse(txtCosto.Text);
                    servicio.maximoPacientes = int.Parse(txtMaximoPacientes.Text);

                    if (controladorServicio.modificarServicio(servicio))
                    {
                        MessageBox.Show("Servicio Modificado");
                        this.Dispose();
                        padre.llenarServicios("");
                    }
                    else
                        MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtIntervaloHora_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtMaximoPacientes_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNombreServicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar.Equals('-') || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
