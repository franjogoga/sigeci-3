namespace Vista
{
    partial class CitasxPaciente
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new DevComponents.DotNetBar.ButtonX();
            this.dateHasta = new System.Windows.Forms.DateTimePicker();
            this.dateDesde = new System.Windows.Forms.DateTimePicker();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtApellidoMaterno = new System.Windows.Forms.TextBox();
            this.txtApellidoPaterno = new System.Windows.Forms.TextBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.btnBuscarPaciente = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtNumeroHistoria = new System.Windows.Forms.TextBox();
            this.dgvCitas = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.dateHasta);
            this.groupBox1.Controls.Add(this.dateDesde);
            this.groupBox1.Controls.Add(this.labelX4);
            this.groupBox1.Controls.Add(this.labelX10);
            this.groupBox1.Controls.Add(this.labelX5);
            this.groupBox1.Controls.Add(this.labelX3);
            this.groupBox1.Controls.Add(this.txtApellidoMaterno);
            this.groupBox1.Controls.Add(this.txtApellidoPaterno);
            this.groupBox1.Controls.Add(this.labelX2);
            this.groupBox1.Controls.Add(this.txtNombres);
            this.groupBox1.Controls.Add(this.btnBuscarPaciente);
            this.groupBox1.Controls.Add(this.labelX1);
            this.groupBox1.Controls.Add(this.txtNumeroHistoria);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 189);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // btnBuscar
            // 
            this.btnBuscar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBuscar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBuscar.Image = global::Vista.Properties.Resources.buscar;
            this.btnBuscar.ImageFixedSize = new System.Drawing.Size(25, 25);
            this.btnBuscar.Location = new System.Drawing.Point(157, 145);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(123, 32);
            this.btnBuscar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBuscar.TabIndex = 20;
            this.btnBuscar.Text = "Buscar";
            // 
            // dateHasta
            // 
            this.dateHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateHasta.Location = new System.Drawing.Point(510, 105);
            this.dateHasta.Name = "dateHasta";
            this.dateHasta.Size = new System.Drawing.Size(189, 20);
            this.dateHasta.TabIndex = 2;
            // 
            // dateDesde
            // 
            this.dateDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateDesde.Location = new System.Drawing.Point(157, 108);
            this.dateDesde.Name = "dateDesde";
            this.dateDesde.Size = new System.Drawing.Size(182, 20);
            this.dateDesde.TabIndex = 2;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(397, 65);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(107, 23);
            this.labelX4.TabIndex = 12;
            this.labelX4.Text = "Apellido Materno :";
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(397, 104);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(107, 23);
            this.labelX10.TabIndex = 12;
            this.labelX10.Text = "Hasta :";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(44, 105);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(107, 23);
            this.labelX5.TabIndex = 12;
            this.labelX5.Text = "Desde :";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(44, 65);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(107, 23);
            this.labelX3.TabIndex = 12;
            this.labelX3.Text = "Apellido Paterno :";
            // 
            // txtApellidoMaterno
            // 
            this.txtApellidoMaterno.Location = new System.Drawing.Point(510, 68);
            this.txtApellidoMaterno.Name = "txtApellidoMaterno";
            this.txtApellidoMaterno.ReadOnly = true;
            this.txtApellidoMaterno.Size = new System.Drawing.Size(189, 20);
            this.txtApellidoMaterno.TabIndex = 11;
            // 
            // txtApellidoPaterno
            // 
            this.txtApellidoPaterno.Location = new System.Drawing.Point(157, 68);
            this.txtApellidoPaterno.Name = "txtApellidoPaterno";
            this.txtApellidoPaterno.ReadOnly = true;
            this.txtApellidoPaterno.Size = new System.Drawing.Size(182, 20);
            this.txtApellidoPaterno.TabIndex = 11;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(397, 29);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(107, 23);
            this.labelX2.TabIndex = 10;
            this.labelX2.Text = "Nombres :";
            // 
            // txtNombres
            // 
            this.txtNombres.Location = new System.Drawing.Point(510, 32);
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.ReadOnly = true;
            this.txtNombres.Size = new System.Drawing.Size(189, 20);
            this.txtNombres.TabIndex = 9;
            // 
            // btnBuscarPaciente
            // 
            this.btnBuscarPaciente.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBuscarPaciente.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBuscarPaciente.Image = global::Vista.Properties.Resources.buscar;
            this.btnBuscarPaciente.ImageFixedSize = new System.Drawing.Size(17, 17);
            this.btnBuscarPaciente.Location = new System.Drawing.Point(310, 29);
            this.btnBuscarPaciente.Name = "btnBuscarPaciente";
            this.btnBuscarPaciente.Size = new System.Drawing.Size(29, 20);
            this.btnBuscarPaciente.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBuscarPaciente.TabIndex = 8;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(44, 26);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(107, 23);
            this.labelX1.TabIndex = 7;
            this.labelX1.Text = "Historia Clinica :";
            // 
            // txtNumeroHistoria
            // 
            this.txtNumeroHistoria.Location = new System.Drawing.Point(157, 29);
            this.txtNumeroHistoria.Name = "txtNumeroHistoria";
            this.txtNumeroHistoria.ReadOnly = true;
            this.txtNumeroHistoria.Size = new System.Drawing.Size(147, 20);
            this.txtNumeroHistoria.TabIndex = 6;
            // 
            // dgvCitas
            // 
            this.dgvCitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCitas.Location = new System.Drawing.Point(12, 219);
            this.dgvCitas.Name = "dgvCitas";
            this.dgvCitas.Size = new System.Drawing.Size(750, 197);
            this.dgvCitas.TabIndex = 2;
            // 
            // CitasxPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(772, 428);
            this.Controls.Add(this.dgvCitas);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "CitasxPaciente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Citas por Paciente";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.TextBox txtApellidoMaterno;
        private System.Windows.Forms.TextBox txtApellidoPaterno;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.TextBox txtNombres;
        private DevComponents.DotNetBar.ButtonX btnBuscarPaciente;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.TextBox txtNumeroHistoria;
        private System.Windows.Forms.DateTimePicker dateHasta;
        private System.Windows.Forms.DateTimePicker dateDesde;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.DataGridView dgvCitas;
        private DevComponents.DotNetBar.ButtonX btnBuscar;

    }
}