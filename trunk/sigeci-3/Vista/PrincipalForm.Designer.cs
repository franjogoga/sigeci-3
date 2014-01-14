namespace Vista
{
    partial class PrincipalForm
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
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.btnReportes = new DevComponents.DotNetBar.ButtonX();
            this.btnUsuarios = new DevComponents.DotNetBar.ButtonX();
            this.btnTerapeutas = new DevComponents.DotNetBar.ButtonX();
            this.btnServicios = new DevComponents.DotNetBar.ButtonX();
            this.btnPacientes = new DevComponents.DotNetBar.ButtonX();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCitas = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            // 
            // btnReportes
            // 
            this.btnReportes.AccessibleRole = System.Windows.Forms.AccessibleRole.RadioButton;
            this.btnReportes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReportes.Image = global::Vista.Properties.Resources.reporte;
            this.btnReportes.ImageFixedSize = new System.Drawing.Size(50, 50);
            this.btnReportes.Location = new System.Drawing.Point(26, 325);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(116, 56);
            this.btnReportes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReportes.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem2,
            this.buttonItem3});
            this.btnReportes.TabIndex = 7;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.Tooltip = "Elija un tipo de reporte";
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUsuarios.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUsuarios.Image = global::Vista.Properties.Resources.login;
            this.btnUsuarios.ImageFixedSize = new System.Drawing.Size(52, 52);
            this.btnUsuarios.Location = new System.Drawing.Point(26, 263);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(116, 56);
            this.btnUsuarios.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUsuarios.TabIndex = 5;
            this.btnUsuarios.Text = "Usuarios";
            this.btnUsuarios.Tooltip = "Aquí puede gestionar los usuarios del sistema";
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnTerapeutas
            // 
            this.btnTerapeutas.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTerapeutas.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTerapeutas.Image = global::Vista.Properties.Resources.terapeuta;
            this.btnTerapeutas.ImageFixedSize = new System.Drawing.Size(50, 50);
            this.btnTerapeutas.Location = new System.Drawing.Point(26, 201);
            this.btnTerapeutas.Name = "btnTerapeutas";
            this.btnTerapeutas.Size = new System.Drawing.Size(116, 56);
            this.btnTerapeutas.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTerapeutas.TabIndex = 4;
            this.btnTerapeutas.Text = "Terapeutas";
            this.btnTerapeutas.Tooltip = "Aqupi puede gestionar los profesionales encargados de cada servicio";
            this.btnTerapeutas.Click += new System.EventHandler(this.btnTerapeutas_Click);
            // 
            // btnServicios
            // 
            this.btnServicios.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnServicios.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnServicios.Image = global::Vista.Properties.Resources.maletin;
            this.btnServicios.ImageFixedSize = new System.Drawing.Size(53, 53);
            this.btnServicios.Location = new System.Drawing.Point(26, 139);
            this.btnServicios.Name = "btnServicios";
            this.btnServicios.Size = new System.Drawing.Size(116, 56);
            this.btnServicios.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnServicios.TabIndex = 3;
            this.btnServicios.Text = "Servicios";
            this.btnServicios.Tooltip = "Aquí puede gestionar los servicos que se brinda";
            this.btnServicios.Click += new System.EventHandler(this.btnServicios_Click);
            // 
            // btnPacientes
            // 
            this.btnPacientes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPacientes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPacientes.Image = global::Vista.Properties.Resources.paciente2;
            this.btnPacientes.ImageFixedSize = new System.Drawing.Size(52, 52);
            this.btnPacientes.Location = new System.Drawing.Point(26, 77);
            this.btnPacientes.Name = "btnPacientes";
            this.btnPacientes.Size = new System.Drawing.Size(116, 56);
            this.btnPacientes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPacientes.TabIndex = 2;
            this.btnPacientes.Text = "Pacientes";
            this.btnPacientes.Tooltip = "Aquí se puede gestionar los pacientes de los servicios";
            this.btnPacientes.Click += new System.EventHandler(this.btnPacientes_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::Vista.Properties.Resources.kids;
            this.pictureBox1.Location = new System.Drawing.Point(178, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(502, 316);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnCitas
            // 
            this.btnCitas.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCitas.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCitas.Image = global::Vista.Properties.Resources.cita2;
            this.btnCitas.ImageFixedSize = new System.Drawing.Size(50, 50);
            this.btnCitas.ImageTextSpacing = 5;
            this.btnCitas.Location = new System.Drawing.Point(26, 12);
            this.btnCitas.Name = "btnCitas";
            this.btnCitas.Size = new System.Drawing.Size(116, 56);
            this.btnCitas.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCitas.TabIndex = 0;
            this.btnCitas.Text = "Citas";
            this.btnCitas.Tooltip = "Aquí puede gestionar las citas, hacer reservas, confirmaciones y cancelar las mis" +
    "mas";
            this.btnCitas.Click += new System.EventHandler(this.btnCitas_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.GlobalItem = false;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "Citas por Paciente";
            // 
            // buttonItem2
            // 
            this.buttonItem2.GlobalItem = false;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "Citas por Terapeuta";
            // 
            // buttonItem3
            // 
            this.buttonItem3.GlobalItem = false;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "Pagos";
            // 
            // PrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(714, 401);
            this.Controls.Add(this.btnReportes);
            this.Controls.Add(this.btnUsuarios);
            this.Controls.Add(this.btnTerapeutas);
            this.Controls.Add(this.btnServicios);
            this.Controls.Add(this.btnPacientes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCitas);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "PrincipalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Gestión de Citas - Angelitos de Jesus";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrincipalForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonX btnCitas;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.ButtonX btnPacientes;
        private DevComponents.DotNetBar.ButtonX btnServicios;
        private DevComponents.DotNetBar.ButtonX btnTerapeutas;
        private DevComponents.DotNetBar.ButtonX btnReportes;
        private DevComponents.DotNetBar.ButtonX btnUsuarios;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
    }
}