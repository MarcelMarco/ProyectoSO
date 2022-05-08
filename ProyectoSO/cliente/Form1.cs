using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace cliente
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn c1 = new DataGridViewTextBoxColumn();
            c1.HeaderText = "Jugadores Conectados";
            c1.Width = 100;
            c1.ReadOnly = true;
            dataGridView1.Columns.Add(c1);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            Consultas.Visible = false;
            dataGridView1.Visible = false;
            MessageBox.Show("Desconectado");
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }


        
        int conectados=0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (conectados == 0)
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse("192.168.56.102");
                IPEndPoint ipep = new IPEndPoint(direc, 9068);

                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket
                    MessageBox.Show("Conectado");

                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
                conectados=1;

                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
            }
            if (Acceso.Checked)
            {
                string mensaje = "1/" + Usuario.Text + "/" + Contra.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                    Consultas.Visible = true;
                    dataGridView1.Visible = true;
            }
            else if (Registro.Checked)
            {
                string mensaje = "2/" + Usuario.Text + "/" + Contra.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Puntos.Checked)
            {
                string mensaje = "3/" + textBox1.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (Fecha.Checked)
            {
                string mensaje = "4/" + textBox2.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (Ganador.Checked)
            {
                string mensaje = "5/" + textBox3.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (Conectados.Checked)
            {
                dataGridView1.Rows.Clear();
                string mensaje = "6/Conectados";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }
        private void AtenderServidor()
        {
            while (true)
            {
                //recibimos mensaje del serviodor
                byte[] msg = new byte[80];
                server.Receive(msg);
                string[] trozos = Encoding.ASCII.GetString(msg).Split('/');
                string mensaje2 = Encoding.ASCII.GetString(msg).Split('\0')[0];
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = trozos[1].Split('\0')[0];

                switch (codigo)
                {
                    case 1: // Acceso al servidor

                        if (mensaje == "Correcto")
                        {
                            MessageBox.Show("Inicio de sesion correcto");
                            //Consultas.Visible = true;
                            //dataGridView1.Visible = true;
                        }
                        else {
                            MessageBox.Show("Ha habido algun error, vuelva a revisar los datos introducidos");
                        }
                        
                        break;
                    case 2: //Registro
                        
                        if (mensaje == "Correcto")
                            MessageBox.Show("Registro correcto");
                        else
                            MessageBox.Show("Ha habido algun error, vuelva a revisar los datos introducidos");
                        break;
                    case 3: //dame el jugador que tiene x puntos
                        
                        MessageBox.Show("El jugador tiene " + mensaje + " puntos");
                        break;

                    case 4: //Fecha partida 
                        MessageBox.Show("La fecha de la partida és: " + mensaje);
                        break;
                    case 5: //Ganador de x partida
                        MessageBox.Show("El ganador de la partida ha sido: " + mensaje);
                        break;
                    case 6: //lista conectados
                        string[] Conectados = mensaje2.Split('/');
                        foreach (var conectado in Conectados)
                        {
                            if (conectado != "6")
                            {
                                dataGridView1.Rows.Add(conectado);
                            }                            
                        }
                        break;
                }
            }
        }
    }
}
