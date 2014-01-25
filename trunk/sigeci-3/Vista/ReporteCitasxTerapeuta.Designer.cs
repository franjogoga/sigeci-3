namespace Vista
{
    partial class ReporteCitasxTerapeuta
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
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.comboTerapeuta = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.comboServicios = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.dgvCitas = new System.Windows.Forms.DataGridView();
            this.numeroCita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paciente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaCita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.horaCita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImprimir = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.dateHasta);
            this.groupBox1.Controls.Add(this.dateDesde);
            this.groupBox1.Controls.Add(this.labelX10);
            this.groupBox1.Controls.Add(this.labelX5);
            this.groupBox1.Controls.Add(this.comboTerapeuta);
            this.groupBox1.Controls.Add(this.labelX8);
            this.groupBox1.Controls.Add(this.comboServicios);
            this.groupBox1.Controls.Add(this.labelX6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Reporte";
            // 
            // btnBuscar
            // 
            this.btnBuscar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBuscar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBuscar.Image = global::Vista.Properties.Resources.buscar;
            this.btnBuscar.ImageFixedSize = new System.Drawing.Size(25, 25);
            this.btnBuscar.Location = new System.Drawing.Point(141, 109);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(123, 32);
            this.btnBuscar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBuscar.TabIndex = 25;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dateHasta
            // 
            this.dateHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateHasta.Location = new System.Drawing.Point(511, 70);
            this.dateHasta.Name = "dateHasta";
            this.dateHasta.Size = new System.Drawing.Size(182, 20);
            this.dateHasta.TabIndex = 21;
            // 
            // dateDesde
            // 
            this.dateDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateDesde.Location = new System.Drawing.Point(141, 70);
            this.dateDesde.Name = "dateDesde";
            this.dateDesde.Size = new System.Drawing.Size(182, 20);
            this.dateDesde.TabIndex = 22;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(398, 67);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(84, 23);
            this.labelX10.TabIndex = 23;
            this.labelX10.Text = "Hasta :";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(28, 67);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(84, 23);
            this.labelX5.TabIndex = 24;
            this.labelX5.Text = "Desde :";
            // 
            // comboTerapeuta
            // 
            this.comboTerapeuta.DisplayMember = "Text";
            this.comboTerapeuta.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboTerapeuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTerapeuta.FormattingEnabled = true;
            this.comboTerapeuta.ItemHeight = 14;
            this.comboTerapeuta.Location = new System.Drawing.Point(511, 31);
            this.comboTerapeuta.Name = "comboTerapeuta";
            this.comboTerapeuta.Size = new System.Drawing.Size(182, 20);
            this.comboTerapeuta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboTerapeuta.TabIndex = 20;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(398, 28);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(107, 23);
            this.labelX8.TabIndex = 19;
            this.labelX8.Text = "Terapeuta :";
            // 
            // comboServicios
            // 
            this.comboServicios.DisplayMember = "Text";
            this.comboServicios.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboServicios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboServicios.FormattingEnabled = true;
            this.comboServicios.ItemHeight = 14;
            this.comboServicios.Location = new System.Drawing.Point(141, 28);
            this.comboServicios.Name = "comboServicios";
            this.comboServicios.Size = new System.Drawing.Size(182, 20);
            this.comboServicios.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboServicios.TabIndex = 18;
            this.comboServicios.SelectedIndexChanged += new System.EventHandler(this.comboServicios_SelectedIndexChanged);
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(28, 28);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(107, 23);
            this.labelX6.TabIndex = 17;
            this.labelX6.Text = "Servicio :";
            // 
            // dgvCitas
            // 
            this.dgvCitas.AllowUserToAddRows = false;
            this.dgvCitas.AllowUserToDeleteRows = false;
            this.dgvCitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCitas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numeroCita,
            this.paciente,
            this.fechaCita,
            this.horaCita,
            this.estado});
            this.dgvCitas.Location = new System.Drawing.Point(12, 185);
            this.dgvCitas.Name = "dgvCitas";
            this.dgvCitas.ReadOnly = true;
            this.dgvCitas.Size = new System.Drawing.Size(740, 202);
            this.dgvCitas.TabIndex = 1;
            // 
            // numeroCita
            // 
            this.numeroCita.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.numeroCita.HeaderText = "N° de Cita";
            this.numeroCita.Name = "numeroCita";
            this.numeroCita.ReadOnly = true;
            // 
            // paciente
            // 
            this.paciente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.paciente.HeaderText = "Paciente";
            this.paciente.Name = "paciente";
            this.paciente.ReadOnly = true;
            this.paciente.Width = 200;
            // 
            // fechaCita
            // 
            this.fechaCita.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fechaCita.HeaderText = "Fecha";
            this.fechaCita.Name = "fechaCita";
            this.fechaCita.ReadOnly = true;
            // 
            // horaCita
            // 
            this.horaCita.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.horaCita.HeaderText = "Hora";
            this.horaCita.Name = "horaCita";
            this.horaCita.ReadOnly = true;
            // 
            // estado
            // 
            this.estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.estado.HeaderText = "Estado";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // btnImprimir
            // 
            this.btnImprimir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImprimir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImprimir.Image = global::Vista.Properties.Resources.imprimir;
            this.btnImprimir.ImageFixedSize = new System.Drawing.Size(25, 25);
            this.btnImprimir.Location = new System.Drawing.Point(326, 396);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(123, 32);
            this.btnImprimir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImprimir.TabIndex = 22;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // ReporteCitasxTerapeuta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 440);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.dgvCitas);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "ReporteCitasxTerapeuta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Citas por Terapeuta";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboServicios;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboTerapeuta;
        private DevComponents.DotNetBar.LabelX labelX8;
        private System.Windows.Forms.DateTimePicker dateHasta;
        private System.Windows.Forms.DateTimePicker dateDesde;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnBuscar;
        private System.Windows.Forms.DataGridView dgvCitas;
        private DevComponents.DotNetBar.ButtonX btnImprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroCita;
        private System.Windows.Forms.DataGridViewTextBoxColumn paciente;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaCita;
        private System.Windows.Forms.DataGridViewTextBoxColumn horaCita;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
    }
}