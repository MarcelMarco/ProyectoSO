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
        int nForm;

        List<Form2> formularios = new List<Form2>();
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
            string mensaje = "0/" +nForm;

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


        
        int conectado=0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse("192.168.56.101");
                IPEndPoint ipep = new IPEndPoint(direc, 9050);

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
                conectado=1;

                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
            }
            if (Acceso.Checked)
            {
                string mensaje = "1/"+nForm + "/" + Usuario.Text + "/" + Contra.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                //byte[] msg2 = new byte[80];
                //server.Receive(msg2);
                //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //if (mensaje == "Correcto")
                //{
                //MessageBox.Show("Inicio de sesion correcto");
                Consultas.Visible = true;
                dataGridView1.Visible = true;
                Notificaciones.Visible = true;
                //}                    
                //else
                //MessageBox.Show("Ha habido algun error, vuelva a revisar los datos introducidos");
            }
            else if (Registro.Checked)
            {
                string mensaje = "2/"+nForm + "/" + Usuario.Text + "/" + Contra.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                //byte[] msg2 = new byte[80];
                //server.Receive(msg2);
                //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //if (mensaje == "Correcto")
                //    MessageBox.Show("Registro correcto");
                //else
                //    MessageBox.Show("Ha habido algun error, vuelva a revisar los datos introducidos");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Puntos.Checked)
            {
                string mensaje = "3/" +nForm + "/" + textBox1.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                //byte[] msg2 = new byte[80];
                //server.Receive(msg2);
                //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //MessageBox.Show("El jugador tiene " + mensaje + " puntos");
            }
            else if (Fecha.Checked)
            {
                string mensaje = "4/" +nForm + "/" + textBox2.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                //byte[] msg2 = new byte[80];
                //server.Receive(msg2);
                //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //MessageBox.Show("La fecha de la partida és: " + mensaje);
            }
            else if (Ganador.Checked)
            {
                string mensaje = "5/" +nForm +"/" +textBox3.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                //byte[] msg2 = new byte[80];
                //server.Receive(msg2);
                //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //MessageBox.Show("El ganador de la partida ha sido: " + mensaje);
            }
            else if (Conectados.Checked)
            {
                dataGridView1.Rows.Clear();
                string mensaje = "6/"+nForm + "/Conectados";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                //byte[] msg2 = new byte[80];
                //server.Receive(msg2);
                //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //MessageBox.Show("Los jugadores conectados són: " + mensaje);
                //string[] Conectados = mensaje.Split('/');
                
                //foreach (var conectado in Conectados)
                //{
                //    dataGridView1.Rows.Add(conectado);
                //}
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
                        string[] trozos2= mensaje.Split('/');
                        //int nForm = Convert.ToInt32(trozos2[0]);
                        string mensaje3 = trozos2[0];
                        if (mensaje3 == "Correcto")
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
                        trozos2 = mensaje2.Split('/');
                        //nForm = Convert.ToInt32(trozos2[0]);
                        mensaje3 = trozos2[1];
                        if (mensaje == "Correcto")
                            MessageBox.Show("Registro correcto");
                        else
                            MessageBox.Show("Ha habido algun error, vuelva a revisar los datos introducidos");
                        break;
                    case 3: //dame el jugador que tiene x puntos
                        trozos2 = mensaje2.Split('/');
                        //nForm = Convert.ToInt32(trozos2[0]);
                        mensaje3 = trozos2[1];
                        MessageBox.Show("El jugador tiene " + mensaje + " puntos");
                        break;

                    case 4: //Fecha partida 
                        trozos = mensaje.Split('/');
                        //nForm = Convert.ToInt32(trozos[1]);
                        //mensaje = trozos[2];
                        MessageBox.Show("La fecha de la partida és: " + mensaje2);
                        break;
                    case 5: //Ganador de x partida
                        trozos = mensaje.Split('/');
                        //nForm = Convert.ToInt32(trozos[1]);
                        string resultado = trozos[0];
                        MessageBox.Show("El ganador de la partida ha sido: " + resultado);
                        break;
                    case 6: //lista conectados
                        dataGridView1.Rows.Clear();
                        trozos = mensaje.Split('/');
                        //nForm = Convert.ToInt32(trozos[1]);
                        
                        string[] Conectados = mensaje2.Split('/');
                        
                        foreach (var conectado in Conectados)
                        {
                            if (conectado != "6")
                            {
                                dataGridView1.Rows.Add(conectado);
                            }
                            
                        }
                        break;
                    case 7:
                        int nForm = Convert.ToInt32(trozos[1]);
                        //mensaje3 = trozos2[0];
                        prueba.Text = mensaje2;
                        formularios[nForm].TomaRespuesta(mensaje2);
                        break;
                    case 8:

                        //Invitacion a una partida
                        //MessageBox.Show(mensaje + " te ha invitado a una partida");
                        Aceptar.Visible = true;
                        Rechazar.Visible = true;

                        break;
                    case 9:
                        if (mensaje == "SI")
                        {
                            Tablero.Visible = true;
                            //MessageBox.Show("El rival a aceptado la partida");
                        }
                        //else
                        //MessageBox.Show("El rival a rechazado la partida");
                        break;
                        


                }
            }
        }

        private void PonerEnMarchaFormulario ()
        {
            int cont = formularios.Count ; ; 
            Form2 f = new Form2(cont,server); //le pasamos el socket (server)
            formularios.Add(f);
            f.ShowDialog();
            
        }
        private void Partida_Btn_Click(object sender, EventArgs e)
        {
            ThreadStart ts = delegate { PonerEnMarchaFormulario(); };
            Thread T = new Thread(ts);
            T.Start();

        }

        private void prueba_Click(object sender, EventArgs e)
        {

        }

        private void Notificaciones_Enter(object sender, EventArgs e)
        {

        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            string mensaje = "9/SI";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            MessageBox.Show("Has aceptado la partida");
            Aceptar.Visible = false;
            Rechazar.Visible = false;
            Tablero.Visible = true;
        }

        private void Rechazar_Click(object sender, EventArgs e)
        {
            string mensaje = "9/NO";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            MessageBox.Show("Has rechazado la partida");
            Aceptar.Visible = false;
            Rechazar.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nombre = textBox4.Text;
            string mensaje = "8/" + nombre;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            MessageBox.Show("Solicitud de partida enviada a " + nombre);
        }
    }
}
