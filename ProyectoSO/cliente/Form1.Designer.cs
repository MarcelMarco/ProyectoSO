﻿namespace cliente
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Registro = new System.Windows.Forms.RadioButton();
            this.Acceso = new System.Windows.Forms.RadioButton();
            this.Contra = new System.Windows.Forms.TextBox();
            this.Usuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Consultas = new System.Windows.Forms.GroupBox();
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
            this.Tablero = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.Consultas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Notificaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tablero)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.Registro);
            this.groupBox1.Controls.Add(this.Acceso);
            this.groupBox1.Controls.Add(this.Contra);
            this.groupBox1.Controls.Add(this.Usuario);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(58, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 214);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zona de Datos";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "Enviar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Registro
            // 
            this.Registro.AutoSize = true;
            this.Registro.Location = new System.Drawing.Point(169, 120);
            this.Registro.Name = "Registro";
            this.Registro.Size = new System.Drawing.Size(94, 24);
            this.Registro.TabIndex = 3;
            this.Registro.TabStop = true;
            this.Registro.Text = "Registro";
            this.Registro.UseVisualStyleBackColor = true;
            // 
            // Acceso
            // 
            this.Acceso.AutoSize = true;
            this.Acceso.Location = new System.Drawing.Point(59, 120);
            this.Acceso.Name = "Acceso";
            this.Acceso.Size = new System.Drawing.Size(87, 24);
            this.Acceso.TabIndex = 2;
            this.Acceso.TabStop = true;
            this.Acceso.Text = "Acceso";
            this.Acceso.UseVisualStyleBackColor = true;
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
            this.Consultas.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Consultas.Controls.Add(this.Conectados);
            this.Consultas.Controls.Add(this.Ganador);
            this.Consultas.Controls.Add(this.Fecha);
            this.Consultas.Controls.Add(this.Puntos);
            this.Consultas.Controls.Add(this.button4);
            this.Consultas.Controls.Add(this.textBox3);
            this.Consultas.Controls.Add(this.textBox2);
            this.Consultas.Controls.Add(this.textBox1);
            this.Consultas.Location = new System.Drawing.Point(588, 34);
            this.Consultas.Name = "Consultas";
            this.Consultas.Size = new System.Drawing.Size(365, 243);
            this.Consultas.TabIndex = 1;
            this.Consultas.TabStop = false;
            this.Consultas.Text = "Consultas";
            this.Consultas.Visible = false;
            // 
            // Conectados
            // 
            this.Conectados.AutoSize = true;
            this.Conectados.Location = new System.Drawing.Point(24, 167);
            this.Conectados.Name = "Conectados";
            this.Conectados.Size = new System.Drawing.Size(199, 24);
            this.Conectados.TabIndex = 7;
            this.Conectados.TabStop = true;
            this.Conectados.Text = "Jugadores Conectados";
            this.Conectados.UseVisualStyleBackColor = true;
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
            this.Fecha.Location = new System.Drawing.Point(24, 87);
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
            this.button4.Location = new System.Drawing.Point(24, 201);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(311, 36);
            this.button4.TabIndex = 6;
            this.button4.Text = "Realizar Consulta";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(236, 132);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(99, 26);
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
            this.textBox1.Size = new System.Drawing.Size(99, 26);
            this.textBox1.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(588, 283);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(365, 319);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.Visible = false;
            // 
            // Partida_Btn
            // 
            this.Partida_Btn.Location = new System.Drawing.Point(78, 444);
            this.Partida_Btn.Name = "Partida_Btn";
            this.Partida_Btn.Size = new System.Drawing.Size(126, 53);
            this.Partida_Btn.TabIndex = 6;
            this.Partida_Btn.Text = "Iniciar";
            this.Partida_Btn.UseVisualStyleBackColor = true;
            this.Partida_Btn.Click += new System.EventHandler(this.Partida_Btn_Click);
            // 
            // prueba
            // 
            this.prueba.Location = new System.Drawing.Point(315, 294);
            this.prueba.Name = "prueba";
            this.prueba.Size = new System.Drawing.Size(250, 83);
            this.prueba.TabIndex = 7;
            this.prueba.Click += new System.EventHandler(this.prueba_Click);
            // 
            // Notificaciones
            // 
            this.Notificaciones.Controls.Add(this.button2);
            this.Notificaciones.Controls.Add(this.textBox4);
            this.Notificaciones.Controls.Add(this.Rechazar);
            this.Notificaciones.Controls.Add(this.Aceptar);
            this.Notificaciones.Controls.Add(this.label3);
            this.Notificaciones.Location = new System.Drawing.Point(60, 261);
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
            this.button2.Location = new System.Drawing.Point(132, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 36);
            this.button2.TabIndex = 8;
            this.button2.Text = "Enviar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(6, 30);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 26);
            this.textBox4.TabIndex = 8;
            // 
            // Rechazar
            // 
            this.Rechazar.Location = new System.Drawing.Point(110, 110);
            this.Rechazar.Name = "Rechazar";
            this.Rechazar.Size = new System.Drawing.Size(97, 39);
            this.Rechazar.TabIndex = 7;
            this.Rechazar.Text = "Rechazar";
            this.Rechazar.UseVisualStyleBackColor = true;
            this.Rechazar.Visible = false;
            this.Rechazar.Click += new System.EventHandler(this.Rechazar_Click);
            // 
            // Aceptar
            // 
            this.Aceptar.Location = new System.Drawing.Point(13, 110);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(91, 39);
            this.Aceptar.TabIndex = 7;
            this.Aceptar.Text = "Aceptar";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Visible = false;
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
            // Tablero
            // 
            this.Tablero.Image = global::cliente.Properties.Resources.poker_mesas_anonimas;
            this.Tablero.Location = new System.Drawing.Point(58, 515);
            this.Tablero.Name = "Tablero";
            this.Tablero.Size = new System.Drawing.Size(375, 229);
            this.Tablero.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Tablero.TabIndex = 9;
            this.Tablero.TabStop = false;
            this.Tablero.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::cliente.Properties.Resources.poker;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1114, 805);
            this.Controls.Add(this.Tablero);
            this.Controls.Add(this.Notificaciones);
            this.Controls.Add(this.prueba);
            this.Controls.Add(this.Partida_Btn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Consultas);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
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
            ((System.ComponentModel.ISupportInitialize)(this.Tablero)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton Registro;
        private System.Windows.Forms.RadioButton Acceso;
        private System.Windows.Forms.TextBox Contra;
        private System.Windows.Forms.TextBox Usuario;
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
        private System.Windows.Forms.PictureBox Tablero;
    }
}

