namespace Vista
{
    partial class ReportePagos
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
            this.dateHasta = new System.Windows.Forms.DateTimePicker();
            this.dateDesde = new System.Windows.Forms.DateTimePicker();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.btnBuscar = new DevComponents.DotNetBar.ButtonX();
            this.dgvPagos = new System.Windows.Forms.DataGridView();
            this.idPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeroCita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.dateHasta);
            this.groupBox1.Controls.Add(this.dateDesde);
            this.groupBox1.Controls.Add(this.labelX10);
            this.groupBox1.Controls.Add(this.labelX5);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Reporte";
            // 
            // dateHasta
            // 
            this.dateHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateHasta.Location = new System.Drawing.Point(432, 36);
            this.dateHasta.Name = "dateHasta";
            this.dateHasta.Size = new System.Drawing.Size(146, 20);
            this.dateHasta.TabIndex = 13;
            // 
            // dateDesde
            // 
            this.dateDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateDesde.Location = new System.Drawing.Point(122, 36);
            this.dateDesde.Name = "dateDesde";
            this.dateDesde.Size = new System.Drawing.Size(145, 20);
            this.dateDesde.TabIndex = 14;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(342, 33);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(84, 23);
            this.labelX10.TabIndex = 15;
            this.labelX10.Text = "Hasta :";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(32, 33);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(84, 23);
            this.labelX5.TabIndex = 16;
            this.labelX5.Text = "Desde :";
            // 
            // btnBuscar
            // 
            this.btnBuscar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBuscar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBuscar.Image = global::Vista.Properties.Resources.buscar;
            this.btnBuscar.ImageFixedSize = new System.Drawing.Size(25, 25);
            this.btnBuscar.Location = new System.Drawing.Point(122, 73);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(123, 32);
            this.btnBuscar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBuscar.TabIndex = 21;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvPagos
            // 
            this.dgvPagos.AllowUserToAddRows = false;
            this.dgvPagos.AllowUserToDeleteRows = false;
            this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idPago,
            this.numeroCita,
            this.monto,
            this.fecha,
            this.estado});
            this.dgvPagos.Location = new System.Drawing.Point(12, 153);
            this.dgvPagos.Name = "dgvPagos";
            this.dgvPagos.ReadOnly = true;
            this.dgvPagos.Size = new System.Drawing.Size(634, 185);
            this.dgvPagos.TabIndex = 1;
            // 
            // idPago
            // 
            this.idPago.HeaderText = "N° de Pago";
            this.idPago.Name = "idPago";
            this.idPago.ReadOnly = true;
            // 
            // numeroCita
            // 
            this.numeroCita.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.numeroCita.HeaderText = "N° de Cita";
            this.numeroCita.Name = "numeroCita";
            this.numeroCita.ReadOnly = true;
            // 
            // monto
            // 
            this.monto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.monto.HeaderText = "Monto";
            this.monto.Name = "monto";
            this.monto.ReadOnly = true;
            // 
            // fecha
            // 
            this.fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // estado
            // 
            this.estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.estado.HeaderText = "Estado";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // ReportePagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(658, 350);
            this.Controls.Add(this.dgvPagos);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MaximizeBox = false;
            this.Name = "ReportePagos";
            this.Text = "Reporte de Ingresos y Egresos";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateHasta;
        private System.Windows.Forms.DateTimePicker dateDesde;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnBuscar;
        private System.Windows.Forms.DataGridView dgvPagos;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroCita;
        private System.Windows.Forms.DataGridViewTextBoxColumn monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
    }
}