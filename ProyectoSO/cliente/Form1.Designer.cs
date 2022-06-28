namespace cliente
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Registro = new System.Windows.Forms.RadioButton();
            this.Acceso = new System.Windows.Forms.RadioButton();
            this.Contra = new System.Windows.Forms.TextBox();
            this.Usuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Consultas = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.DameDeBaja = new System.Windows.Forms.RadioButton();
            this.Conectados = new System.Windows.Forms.RadioButton();
            this.Ganador = new System.Windows.Forms.RadioButton();
            this.Fecha = new System.Windows.Forms.RadioButton();
            this.Puntos = new System.Windows.Forms.RadioButton();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Partida_Btn = new System.Windows.Forms.Button();
            this.prueba = new System.Windows.Forms.Label();
            this.Notificaciones = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.Rechazar = new System.Windows.Forms.Button();
            this.Aceptar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desarroladoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eETACToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reglasDelJuegoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reglasDePokerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fondosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fondo1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fondo2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fondo3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entidadesAliadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pokerStarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bet365ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviciosExtraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reiniciarFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.EnviarChat = new System.Windows.Forms.Button();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.Consultas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Notificaciones.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DodgerBlue;
            this.groupBox1.Controls.Add(this.Registro);
            this.groupBox1.Controls.Add(this.Acceso);
            this.groupBox1.Controls.Add(this.Contra);
            this.groupBox1.Controls.Add(this.Usuario);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(60, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 214);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zona de Datos";
            // 
            // Registro
            // 
            this.Registro.Appearance = System.Windows.Forms.Appearance.Button;
            this.Registro.AutoSize = true;
            this.Registro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Registro.Location = new System.Drawing.Point(170, 120);
            this.Registro.Name = "Registro";
            this.Registro.Size = new System.Drawing.Size(79, 30);
            this.Registro.TabIndex = 3;
            this.Registro.Text = "Registro";
            this.Registro.UseVisualStyleBackColor = true;
            this.Registro.Click += new System.EventHandler(this.Registro_Click);
            // 
            // Acceso
            // 
            this.Acceso.Appearance = System.Windows.Forms.Appearance.Button;
            this.Acceso.AutoSize = true;
            this.Acceso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Acceso.Location = new System.Drawing.Point(58, 120);
            this.Acceso.Name = "Acceso";
            this.Acceso.Size = new System.Drawing.Size(72, 30);
            this.Acceso.TabIndex = 2;
            this.Acceso.Text = "Acceso";
            this.Acceso.UseVisualStyleBackColor = true;
            this.Acceso.Click += new System.EventHandler(this.Acceso_Click);
            // 
            // Contra
            // 
            this.Contra.Location = new System.Drawing.Point(126, 62);
            this.Contra.Name = "Contra";
            this.Contra.Size = new System.Drawing.Size(150, 26);
            this.Contra.TabIndex = 1;
            // 
            // Usuario
            // 
            this.Usuario.Location = new System.Drawing.Point(126, 32);
            this.Usuario.Name = "Usuario";
            this.Usuario.Size = new System.Drawing.Size(150, 26);
            this.Usuario.TabIndex = 1;
            this.Usuario.TextChanged += new System.EventHandler(this.Usuario_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario";
            // 
            // Consultas
            // 
            this.Consultas.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Consultas.Controls.Add(this.label5);
            this.Consultas.Controls.Add(this.label4);
            this.Consultas.Controls.Add(this.textBox6);
            this.Consultas.Controls.Add(this.textBox5);
            this.Consultas.Controls.Add(this.DameDeBaja);
            this.Consultas.Controls.Add(this.Conectados);
            this.Consultas.Controls.Add(this.Ganador);
            this.Consultas.Controls.Add(this.Fecha);
            this.Consultas.Controls.Add(this.Puntos);
            this.Consultas.Controls.Add(this.button4);
            this.Consultas.Controls.Add(this.textBox3);
            this.Consultas.Controls.Add(this.textBox2);
            this.Consultas.Controls.Add(this.textBox1);
            this.Consultas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Consultas.Location = new System.Drawing.Point(588, 57);
            this.Consultas.Name = "Consultas";
            this.Consultas.Size = new System.Drawing.Size(364, 375);
            this.Consultas.TabIndex = 1;
            this.Consultas.TabStop = false;
            this.Consultas.Text = "Consultas";
            this.Consultas.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Contraseña:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Usuario:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(164, 237);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 26);
            this.textBox6.TabIndex = 10;
            this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(24, 237);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 26);
            this.textBox5.TabIndex = 9;
            this.textBox5.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // DameDeBaja
            // 
            this.DameDeBaja.AutoSize = true;
            this.DameDeBaja.Location = new System.Drawing.Point(24, 172);
            this.DameDeBaja.Name = "DameDeBaja";
            this.DameDeBaja.Size = new System.Drawing.Size(130, 24);
            this.DameDeBaja.TabIndex = 8;
            this.DameDeBaja.TabStop = true;
            this.DameDeBaja.Text = "DameDeBaja";
            this.DameDeBaja.UseVisualStyleBackColor = true;
            // 
            // Conectados
            // 
            this.Conectados.AutoSize = true;
            this.Conectados.Location = new System.Drawing.Point(24, 282);
            this.Conectados.Name = "Conectados";
            this.Conectados.Size = new System.Drawing.Size(199, 24);
            this.Conectados.TabIndex = 7;
            this.Conectados.TabStop = true;
            this.Conectados.Text = "Jugadores Conectados";
            this.Conectados.UseVisualStyleBackColor = true;
            this.Conectados.Visible = false;
            // 
            // Ganador
            // 
            this.Ganador.AutoSize = true;
            this.Ganador.Location = new System.Drawing.Point(24, 132);
            this.Ganador.Name = "Ganador";
            this.Ganador.Size = new System.Drawing.Size(203, 24);
            this.Ganador.TabIndex = 5;
            this.Ganador.TabStop = true;
            this.Ganador.Text = "Ganador de una partida";
            this.Ganador.UseVisualStyleBackColor = true;
            // 
            // Fecha
            // 
            this.Fecha.AutoSize = true;
            this.Fecha.Location = new System.Drawing.Point(24, 88);
            this.Fecha.Name = "Fecha";
            this.Fecha.Size = new System.Drawing.Size(186, 24);
            this.Fecha.TabIndex = 5;
            this.Fecha.TabStop = true;
            this.Fecha.Text = "Fecha de una Partida";
            this.Fecha.UseVisualStyleBackColor = true;
            // 
            // Puntos
            // 
            this.Puntos.AutoSize = true;
            this.Puntos.Location = new System.Drawing.Point(24, 40);
            this.Puntos.Name = "Puntos";
            this.Puntos.Size = new System.Drawing.Size(190, 24);
            this.Puntos.TabIndex = 5;
            this.Puntos.TabStop = true;
            this.Puntos.Text = "Puntos de un Jugador";
            this.Puntos.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Location = new System.Drawing.Point(24, 322);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(310, 35);
            this.button4.TabIndex = 6;
            this.button4.Text = "Realizar Consulta";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.CursorChanged += new System.EventHandler(this.button4_Click);
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(236, 132);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(98, 26);
            this.textBox3.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(236, 85);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(98, 26);
            this.textBox2.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(236, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(98, 26);
            this.textBox1.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataGridView1.ColumnHeadersHeight = 34;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.GridColor = System.Drawing.Color.Ivory;
            this.dataGridView1.Location = new System.Drawing.Point(588, 557);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(196, 318);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // Partida_Btn
            // 
            this.Partida_Btn.Location = new System.Drawing.Point(78, 462);
            this.Partida_Btn.Name = "Partida_Btn";
            this.Partida_Btn.Size = new System.Drawing.Size(126, 52);
            this.Partida_Btn.TabIndex = 6;
            this.Partida_Btn.Text = "Iniciar";
            this.Partida_Btn.UseVisualStyleBackColor = true;
            this.Partida_Btn.Visible = false;
            this.Partida_Btn.Click += new System.EventHandler(this.Partida_Btn_Click);
            // 
            // prueba
            // 
            this.prueba.Location = new System.Drawing.Point(315, 294);
            this.prueba.Name = "prueba";
            this.prueba.Size = new System.Drawing.Size(250, 83);
            this.prueba.TabIndex = 7;
            this.prueba.Visible = false;
            this.prueba.Click += new System.EventHandler(this.prueba_Click);
            // 
            // Notificaciones
            // 
            this.Notificaciones.Controls.Add(this.button2);
            this.Notificaciones.Controls.Add(this.textBox4);
            this.Notificaciones.Controls.Add(this.Rechazar);
            this.Notificaciones.Controls.Add(this.Aceptar);
            this.Notificaciones.Controls.Add(this.label3);
            this.Notificaciones.Location = new System.Drawing.Point(60, 283);
            this.Notificaciones.Name = "Notificaciones";
            this.Notificaciones.Size = new System.Drawing.Size(228, 155);
            this.Notificaciones.TabIndex = 8;
            this.Notificaciones.TabStop = false;
            this.Notificaciones.Text = "Centro de Notificaciones";
            this.Notificaciones.Visible = false;
            this.Notificaciones.Enter += new System.EventHandler(this.Notificaciones_Enter);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(132, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 35);
            this.button2.TabIndex = 8;
            this.button2.Text = "Enviar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.CursorChanged += new System.EventHandler(this.button2_Click);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(6, 31);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 26);
            this.textBox4.TabIndex = 8;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // Rechazar
            // 
            this.Rechazar.Location = new System.Drawing.Point(110, 111);
            this.Rechazar.Name = "Rechazar";
            this.Rechazar.Size = new System.Drawing.Size(98, 38);
            this.Rechazar.TabIndex = 7;
            this.Rechazar.Text = "Rechazar";
            this.Rechazar.UseVisualStyleBackColor = true;
            this.Rechazar.Click += new System.EventHandler(this.Rechazar_Click);
            // 
            // Aceptar
            // 
            this.Aceptar.Location = new System.Drawing.Point(14, 111);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(92, 38);
            this.Aceptar.TabIndex = 7;
            this.Aceptar.Text = "Aceptar";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = " ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem,
            this.reglasDelJuegoToolStripMenuItem,
            this.fondosToolStripMenuItem,
            this.entidadesAliadasToolStripMenuItem,
            this.serviciosExtraToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(975, 33);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programaToolStripMenuItem,
            this.desarroladoresToolStripMenuItem,
            this.eETACToolStripMenuItem});
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(107, 29);
            this.acercaDeToolStripMenuItem.Text = "Acerca De";
            // 
            // programaToolStripMenuItem
            // 
            this.programaToolStripMenuItem.Name = "programaToolStripMenuItem";
            this.programaToolStripMenuItem.Size = new System.Drawing.Size(234, 34);
            this.programaToolStripMenuItem.Text = "Programa";
            this.programaToolStripMenuItem.Click += new System.EventHandler(this.programaToolStripMenuItem_Click);
            // 
            // desarroladoresToolStripMenuItem
            // 
            this.desarroladoresToolStripMenuItem.Name = "desarroladoresToolStripMenuItem";
            this.desarroladoresToolStripMenuItem.Size = new System.Drawing.Size(234, 34);
            this.desarroladoresToolStripMenuItem.Text = "Desarroladores";
            this.desarroladoresToolStripMenuItem.Click += new System.EventHandler(this.desarroladoresToolStripMenuItem_Click);
            // 
            // eETACToolStripMenuItem
            // 
            this.eETACToolStripMenuItem.Name = "eETACToolStripMenuItem";
            this.eETACToolStripMenuItem.Size = new System.Drawing.Size(234, 34);
            this.eETACToolStripMenuItem.Text = "EETAC";
            this.eETACToolStripMenuItem.Click += new System.EventHandler(this.eETACToolStripMenuItem_Click);
            // 
            // reglasDelJuegoToolStripMenuItem
            // 
            this.reglasDelJuegoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reglasDePokerToolStripMenuItem});
            this.reglasDelJuegoToolStripMenuItem.Name = "reglasDelJuegoToolStripMenuItem";
            this.reglasDelJuegoToolStripMenuItem.Size = new System.Drawing.Size(160, 29);
            this.reglasDelJuegoToolStripMenuItem.Text = "Reglas del Juego";
            // 
            // reglasDePokerToolStripMenuItem
            // 
            this.reglasDePokerToolStripMenuItem.Name = "reglasDePokerToolStripMenuItem";
            this.reglasDePokerToolStripMenuItem.Size = new System.Drawing.Size(239, 34);
            this.reglasDePokerToolStripMenuItem.Text = "Reglas de Poker";
            this.reglasDePokerToolStripMenuItem.Click += new System.EventHandler(this.reglasDePokerToolStripMenuItem_Click);
            // 
            // fondosToolStripMenuItem
            // 
            this.fondosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fondo1ToolStripMenuItem,
            this.fondo2ToolStripMenuItem,
            this.fondo3ToolStripMenuItem});
            this.fondosToolStripMenuItem.Name = "fondosToolStripMenuItem";
            this.fondosToolStripMenuItem.Size = new System.Drawing.Size(88, 29);
            this.fondosToolStripMenuItem.Text = "Fondos";
            // 
            // fondo1ToolStripMenuItem
            // 
            this.fondo1ToolStripMenuItem.Name = "fondo1ToolStripMenuItem";
            this.fondo1ToolStripMenuItem.Size = new System.Drawing.Size(176, 34);
            this.fondo1ToolStripMenuItem.Text = "Fondo1";
            this.fondo1ToolStripMenuItem.Click += new System.EventHandler(this.fondo1ToolStripMenuItem_Click);
            // 
            // fondo2ToolStripMenuItem
            // 
            this.fondo2ToolStripMenuItem.Name = "fondo2ToolStripMenuItem";
            this.fondo2ToolStripMenuItem.Size = new System.Drawing.Size(176, 34);
            this.fondo2ToolStripMenuItem.Text = "Fondo2";
            this.fondo2ToolStripMenuItem.Click += new System.EventHandler(this.fondo2ToolStripMenuItem_Click);
            // 
            // fondo3ToolStripMenuItem
            // 
            this.fondo3ToolStripMenuItem.Name = "fondo3ToolStripMenuItem";
            this.fondo3ToolStripMenuItem.Size = new System.Drawing.Size(176, 34);
            this.fondo3ToolStripMenuItem.Text = "Fondo3";
            this.fondo3ToolStripMenuItem.Click += new System.EventHandler(this.fondo3ToolStripMenuItem_Click);
            // 
            // entidadesAliadasToolStripMenuItem
            // 
            this.entidadesAliadasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pOToolStripMenuItem,
            this.pokerStarsToolStripMenuItem,
            this.bet365ToolStripMenuItem});
            this.entidadesAliadasToolStripMenuItem.Name = "entidadesAliadasToolStripMenuItem";
            this.entidadesAliadasToolStripMenuItem.Size = new System.Drawing.Size(167, 29);
            this.entidadesAliadasToolStripMenuItem.Text = "Entidades Aliadas";
            // 
            // pOToolStripMenuItem
            // 
            this.pOToolStripMenuItem.Name = "pOToolStripMenuItem";
            this.pOToolStripMenuItem.Size = new System.Drawing.Size(196, 34);
            this.pOToolStripMenuItem.Text = "888 Poker";
            this.pOToolStripMenuItem.Click += new System.EventHandler(this.pOToolStripMenuItem_Click);
            // 
            // pokerStarsToolStripMenuItem
            // 
            this.pokerStarsToolStripMenuItem.Name = "pokerStarsToolStripMenuItem";
            this.pokerStarsToolStripMenuItem.Size = new System.Drawing.Size(196, 34);
            this.pokerStarsToolStripMenuItem.Text = "PokerStars";
            this.pokerStarsToolStripMenuItem.Click += new System.EventHandler(this.pokerStarsToolStripMenuItem_Click);
            // 
            // bet365ToolStripMenuItem
            // 
            this.bet365ToolStripMenuItem.Name = "bet365ToolStripMenuItem";
            this.bet365ToolStripMenuItem.Size = new System.Drawing.Size(196, 34);
            this.bet365ToolStripMenuItem.Text = "Bet 365";
            this.bet365ToolStripMenuItem.Click += new System.EventHandler(this.bet365ToolStripMenuItem_Click);
            // 
            // serviciosExtraToolStripMenuItem
            // 
            this.serviciosExtraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reiniciarFormToolStripMenuItem});
            this.serviciosExtraToolStripMenuItem.Name = "serviciosExtraToolStripMenuItem";
            this.serviciosExtraToolStripMenuItem.Size = new System.Drawing.Size(140, 29);
            this.serviciosExtraToolStripMenuItem.Text = "Servicios Extra";
            // 
            // reiniciarFormToolStripMenuItem
            // 
            this.reiniciarFormToolStripMenuItem.Name = "reiniciarFormToolStripMenuItem";
            this.reiniciarFormToolStripMenuItem.Size = new System.Drawing.Size(225, 34);
            this.reiniciarFormToolStripMenuItem.Text = "Reiniciar Form";
            this.reiniciarFormToolStripMenuItem.Click += new System.EventHandler(this.reiniciarFormToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(40, 557);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(408, 264);
            this.listBox1.TabIndex = 10;
            this.listBox1.Visible = false;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // EnviarChat
            // 
            this.EnviarChat.Location = new System.Drawing.Point(304, 755);
            this.EnviarChat.Name = "EnviarChat";
            this.EnviarChat.Size = new System.Drawing.Size(98, 35);
            this.EnviarChat.TabIndex = 11;
            this.EnviarChat.Text = "Enviar";
            this.EnviarChat.UseVisualStyleBackColor = true;
            this.EnviarChat.Click += new System.EventHandler(this.EnviarChat_Click);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(60, 762);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(238, 26);
            this.textBox7.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::cliente.Properties.Resources.poker;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(975, 888);
            this.Controls.Add(this.EnviarChat);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Notificaciones);
            this.Controls.Add(this.prueba);
            this.Controls.Add(this.Partida_Btn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Consultas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Consultas.ResumeLayout(false);
            this.Consultas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Notificaciones.ResumeLayout(false);
            this.Notificaciones.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Registro;
        private System.Windows.Forms.RadioButton Acceso;
        private System.Windows.Forms.TextBox Contra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox Consultas;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RadioButton Ganador;
        private System.Windows.Forms.RadioButton Fecha;
        private System.Windows.Forms.RadioButton Puntos;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton Conectados;
        private System.Windows.Forms.Button Partida_Btn;
        private System.Windows.Forms.Label prueba;
        private System.Windows.Forms.GroupBox Notificaciones;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button Rechazar;
        private System.Windows.Forms.Button Aceptar;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox Usuario;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desarroladoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reglasDelJuegoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reglasDePokerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fondosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fondo1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fondo2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fondo3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eETACToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entidadesAliadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pokerStarsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bet365ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.RadioButton DameDeBaja;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button EnviarChat;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.ToolStripMenuItem serviciosExtraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reiniciarFormToolStripMenuItem;
    }
}

