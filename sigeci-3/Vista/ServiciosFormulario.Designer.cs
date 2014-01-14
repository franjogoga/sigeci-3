namespace Vista
{
    partial class ServiciosFormulario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbUsuario = new System.Windows.Forms.GroupBox();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtIntervaloHora = new System.Windows.Forms.TextBox();
            this.txtMaximoPacientes = new System.Windows.Forms.TextBox();
            this.txtCosto = new System.Windows.Forms.TextBox();
            this.txtNombreServicio = new System.Windows.Forms.TextBox();
            this.btnCancelar = new DevComponents.DotNetBar.ButtonX();
            this.btnAceptar = new DevComponents.DotNetBar.ButtonX();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.gbUsuario.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUsuario
            // 
            this.gbUsuario.BackColor = System.Drawing.Color.White;
            this.gbUsuario.Controls.Add(this.labelX3);
            this.gbUsuario.Controls.Add(this.labelX6);
            this.gbUsuario.Controls.Add(this.labelX2);
            this.gbUsuario.Controls.Add(this.labelX1);
            this.gbUsuario.Controls.Add(this.txtIntervaloHora);
            this.gbUsuario.Controls.Add(this.txtMaximoPacientes);
            this.gbUsuario.Controls.Add(this.txtCosto);
            this.gbUsuario.Controls.Add(this.txtNombreServicio);
            this.gbUsuario.Location = new System.Drawing.Point(12, 12);
            this.gbUsuario.Name = "gbUsuario";
            this.gbUsuario.Size = new System.Drawing.Size(434, 193);
            this.gbUsuario.TabIndex = 2;
            this.gbUsuario.TabStop = false;
            this.gbUsuario.Text = "Datos del Servicio";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(40, 147);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(151, 23);
            this.labelX3.TabIndex = 7;
            this.labelX3.Text = "Máximo de Pacientes :";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(40, 66);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(151, 23);
            this.labelX6.TabIndex = 6;
            this.labelX6.Text = "Intervalo de Hora :";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(40, 104);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(151, 23);
            this.labelX2.TabIndex = 6;
            this.labelX2.Text = "Costo :";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(40, 28);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(151, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "Nombre del Servicio :";
            // 
            // txtIntervaloHora
            // 
            this.txtIntervaloHora.Location = new System.Drawing.Point(197, 69);
            this.txtIntervaloHora.Name = "txtIntervaloHora";
            this.txtIntervaloHora.Size = new System.Drawing.Size(182, 20);
            this.txtIntervaloHora.TabIndex = 1;
            this.txtIntervaloHora.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIntervaloHora_KeyPress);
            // 
            // txtMaximoPacientes
            // 
            this.txtMaximoPacientes.Location = new System.Drawing.Point(197, 149);
            this.txtMaximoPacientes.Name = "txtMaximoPacientes";
            this.txtMaximoPacientes.Size = new System.Drawing.Size(182, 20);
            this.txtMaximoPacientes.TabIndex = 2;
            this.txtMaximoPacientes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaximoPacientes_KeyPress);
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new System.Drawing.Point(197, 107);
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.Size = new System.Drawing.Size(182, 20);
            this.txtCosto.TabIndex = 1;
            this.txtCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCosto_KeyPress);
            // 
            // txtNombreServicio
            // 
            this.txtNombreServicio.Location = new System.Drawing.Point(197, 31);
            this.txtNombreServicio.Name = "txtNombreServicio";
            this.txtNombreServicio.Size = new System.Drawing.Size(182, 20);
            this.txtNombreServicio.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancelar.Image = global::Vista.Properties.Resources.borrar;
            this.btnCancelar.ImageFixedSize = new System.Drawing.Size(25, 25);
            this.btnCancelar.Location = new System.Drawing.Point(245, 216);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(123, 32);
            this.btnCancelar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAceptar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAceptar.Image = global::Vista.Properties.Resources.aceptar;
            this.btnAceptar.ImageFixedSize = new System.Drawing.Size(25, 25);
            this.btnAceptar.Location = new System.Drawing.Point(94, 216);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(123, 32);
            this.btnAceptar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            // 
            // ServiciosFormulario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(459, 260);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.gbUsuario);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ServiciosFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servicios";
            this.gbUsuario.ResumeLayout(false);
            this.gbUsuario.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUsuario;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.TextBox txtIntervaloHora;
        private System.Windows.Forms.TextBox txtMaximoPacientes;
        private System.Windows.Forms.TextBox txtCosto;
        private System.Windows.Forms.TextBox txtNombreServicio;
        private DevComponents.DotNetBar.ButtonX btnCancelar;
        private DevComponents.DotNetBar.ButtonX btnAceptar;
        private DevComponents.DotNetBar.StyleManager styleManager1;
    }
}