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
    public partial class ModalidadesFormulario : Office2007Form
    {
        private ControladorModalidad controladorModalidad = ControladorModalidad.Instancia();
        private ModalidadesForm padre;
        private int modo;
        private Modalidad modalidad;
        private int idServicio;

        public ModalidadesFormulario(ModalidadesForm modalidadesForm, int modo, Modalidad modalidad, int idServicio)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.modalidad = modalidad;
            this.padre = modalidadesForm;
            this.modo = modo;
            this.idServicio = idServicio;
            if (modo == 2) //modificar
            {
                llenaFormularioModalidad(modalidad);
            }
            if (modo == 1) //ver
            {
                llenaFormularioModalidad(modalidad);
                bloqueaFormularioModalidad();
                btnAceptar.Enabled = false;
            }
            else
                btnAceptar.Enabled = true;
        }

        private void bloqueaFormularioModalidad()
        {
            txtNombreModalidad.ReadOnly = true;
        }

        private void llenaFormularioModalidad(Modalidad modalidad)
        {
            txtNombreModalidad.Text = modalidad.nombreModalidad;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (modo == 0)
            {
                Modalidad modalidad = new Modalidad();
                modalidad.nombreModalidad = txtNombreModalidad.Text;
                modalidad.estado = "activo";
                modalidad.idServicio = idServicio;

                if (controladorModalidad.agregarModalidad(modalidad))
                {
                    MessageBox.Show("Modalidad Agregada");
                    this.Dispose();
                    padre.llenarModalidades("", idServicio);
                }
                else
                    MessageBox.Show("Ha ocurrido un error");
            }
            else
            {
                modalidad.nombreModalidad = txtNombreModalidad.Text;

                if (controladorModalidad.modificarModalidad(modalidad))
                {
                    MessageBox.Show("Modalidad Modificada");
                    this.Dispose();
                    padre.llenarModalidades("", idServicio);
                }
                else
                    MessageBox.Show("Ha ocurrido un error");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
