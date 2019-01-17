namespace SyncTAT
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnExaminarS = new System.Windows.Forms.Button();
            this.txtArchivoS = new System.Windows.Forms.TextBox();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionDeConexionBDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarConfiguracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionDeCorreoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.txtArchivoL = new System.Windows.Forms.TextBox();
            this.btnExaminarL = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dudSegundos = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtArchivo = new System.Windows.Forms.TextBox();
            this.btnExaminarP = new System.Windows.Forms.Button();
            this.chkCerrar = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dudSegundos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExaminarS
            // 
            this.btnExaminarS.Location = new System.Drawing.Point(561, 50);
            this.btnExaminarS.Name = "btnExaminarS";
            this.btnExaminarS.Size = new System.Drawing.Size(75, 23);
            this.btnExaminarS.TabIndex = 3;
            this.btnExaminarS.Text = "Examinar";
            this.btnExaminarS.UseVisualStyleBackColor = true;
            this.btnExaminarS.Click += new System.EventHandler(this.btnExaminarS_Click);
            // 
            // txtArchivoS
            // 
            this.txtArchivoS.Location = new System.Drawing.Point(149, 52);
            this.txtArchivoS.Name = "txtArchivoS";
            this.txtArchivoS.Size = new System.Drawing.Size(405, 20);
            this.txtArchivoS.TabIndex = 2;
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(561, 108);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(75, 23);
            this.btnCargar.TabIndex = 12;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Location = new System.Drawing.Point(642, 108);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 13;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Usuario";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Location = new System.Drawing.Point(68, 110);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(100, 20);
            this.txtUsuario.TabIndex = 8;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionDeConexionBDToolStripMenuItem,
            this.guardarConfiguracionToolStripMenuItem,
            this.configuracionDeCorreoToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(69, 20);
            this.toolStripMenuItem1.Text = "Opciones";
            // 
            // configuracionDeConexionBDToolStripMenuItem
            // 
            this.configuracionDeConexionBDToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.configuracionDeConexionBDToolStripMenuItem.Name = "configuracionDeConexionBDToolStripMenuItem";
            this.configuracionDeConexionBDToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.configuracionDeConexionBDToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.configuracionDeConexionBDToolStripMenuItem.Text = "Configuración de Conexión BD";
            this.configuracionDeConexionBDToolStripMenuItem.Click += new System.EventHandler(this.configuracionDeConexionBDToolStripMenuItem_Click);
            // 
            // guardarConfiguracionToolStripMenuItem
            // 
            this.guardarConfiguracionToolStripMenuItem.Name = "guardarConfiguracionToolStripMenuItem";
            this.guardarConfiguracionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.guardarConfiguracionToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.guardarConfiguracionToolStripMenuItem.Text = "Guardar Configuración";
            this.guardarConfiguracionToolStripMenuItem.Click += new System.EventHandler(this.guardarConfiguracionToolStripMenuItem_Click);
            // 
            // configuracionDeCorreoToolStripMenuItem
            // 
            this.configuracionDeCorreoToolStripMenuItem.Name = "configuracionDeCorreoToolStripMenuItem";
            this.configuracionDeCorreoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.configuracionDeCorreoToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.configuracionDeCorreoToolStripMenuItem.Text = "Configuración de Correo";
            this.configuracionDeCorreoToolStripMenuItem.Click += new System.EventHandler(this.configuracionDeCorreoToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Archivo Log";
            // 
            // txtArchivoL
            // 
            this.txtArchivoL.Location = new System.Drawing.Point(248, 110);
            this.txtArchivoL.Name = "txtArchivoL";
            this.txtArchivoL.Size = new System.Drawing.Size(217, 20);
            this.txtArchivoL.TabIndex = 10;
            // 
            // btnExaminarL
            // 
            this.btnExaminarL.Location = new System.Drawing.Point(470, 108);
            this.btnExaminarL.Name = "btnExaminarL";
            this.btnExaminarL.Size = new System.Drawing.Size(75, 23);
            this.btnExaminarL.TabIndex = 11;
            this.btnExaminarL.Text = "Examinar";
            this.btnExaminarL.UseVisualStyleBackColor = true;
            this.btnExaminarL.Click += new System.EventHandler(this.btnExaminarL_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dudSegundos
            // 
            this.dudSegundos.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.dudSegundos.Location = new System.Drawing.Point(766, 52);
            this.dudSegundos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dudSegundos.Name = "dudSegundos";
            this.dudSegundos.Size = new System.Drawing.Size(35, 20);
            this.dudSegundos.TabIndex = 17;
            this.dudSegundos.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(642, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Seg. para Auto Cerrado";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Ruta de archivos SAP";
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxLog.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textBoxLog.Location = new System.Drawing.Point(15, 148);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(873, 238);
            this.textBoxLog.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 26);
            this.label5.TabIndex = 21;
            this.label5.Text = "Ruta de archivos SAP\r\n(Procesados)";
            // 
            // txtArchivo
            // 
            this.txtArchivo.Location = new System.Drawing.Point(149, 80);
            this.txtArchivo.Name = "txtArchivo";
            this.txtArchivo.Size = new System.Drawing.Size(405, 20);
            this.txtArchivo.TabIndex = 22;
            // 
            // btnExaminarP
            // 
            this.btnExaminarP.Location = new System.Drawing.Point(561, 80);
            this.btnExaminarP.Name = "btnExaminarP";
            this.btnExaminarP.Size = new System.Drawing.Size(75, 23);
            this.btnExaminarP.TabIndex = 23;
            this.btnExaminarP.Text = "Examinar";
            this.btnExaminarP.UseVisualStyleBackColor = true;
            this.btnExaminarP.Click += new System.EventHandler(this.btnExaminarP_Click);
            // 
            // chkCerrar
            // 
            this.chkCerrar.AutoSize = true;
            this.chkCerrar.Location = new System.Drawing.Point(808, 55);
            this.chkCerrar.Name = "chkCerrar";
            this.chkCerrar.Size = new System.Drawing.Size(15, 14);
            this.chkCerrar.TabIndex = 24;
            this.chkCerrar.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 398);
            this.Controls.Add(this.chkCerrar);
            this.Controls.Add(this.btnExaminarP);
            this.Controls.Add(this.txtArchivo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dudSegundos);
            this.Controls.Add(this.btnExaminarL);
            this.Controls.Add(this.txtArchivoL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.txtArchivoS);
            this.Controls.Add(this.btnExaminarS);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Sincronización";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dudSegundos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExaminarS;
        private System.Windows.Forms.TextBox txtArchivoS;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configuracionDeConexionBDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarConfiguracionToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtArchivoL;
        private System.Windows.Forms.Button btnExaminarL;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown dudSegundos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem configuracionDeCorreoToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtArchivo;
        private System.Windows.Forms.Button btnExaminarP;
        private System.Windows.Forms.CheckBox chkCerrar;
    }
}

