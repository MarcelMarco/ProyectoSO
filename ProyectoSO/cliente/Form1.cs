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
using System.IO;

namespace cliente
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        int nForm;
        delegate void DelegadoParaEscribir(string mensaje);
        


        List<Partida> formularios = new List<Partida>();
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
            try
            {
                if (conectado == 1)
                {
                    //Mensaje de desconexión
                    string mensaje = "0/" + nForm;

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    // Nos desconectamos
                    atender.Abort();
                    Consultas.Visible = false;
                    dataGridView1.Visible = false;

                    //MessageBox.Show("Desconectado");
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                }
                
            }
            catch (Exception ex)
            { 

            }
        }


        int conectado = 0;
        private void Acceso_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse("147.83.117.22");
                IPEndPoint ipep = new IPEndPoint(direc, 50010);

                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
               
                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket
                    MessageBox.Show("Conectado");
                    conectado = 1;
                    

                    string mensaje = "1/" + Usuario.Text + "/" + Contra.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    
                    

                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
                conectado = 1;


                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
            }
            
            Consultas.Visible = true;
            dataGridView1.Visible = true;
            Notificaciones.Visible = true;
            Partida_Btn.Visible = true;
            prueba.Visible = true;
            listBox1.Visible = true;
            textBox7.Visible = true;
            EnviarChat.Visible = true;
            this.PonerEnMarchaFormulario();

            
            
        }

        private void Registro_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse("147.83.117.22");
                IPEndPoint ipep = new IPEndPoint(direc, 50010);

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
                conectado = 1;

                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
                
            }
            string mensaje = "2/" + Usuario.Text + "/" + Contra.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse("147.83.117.22");
                IPEndPoint ipep = new IPEndPoint(direc, 50010);

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
                conectado = 1;

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

                
                if (mensaje == "Correcto")
                {
                    
                    Consultas.Visible = true;
                    dataGridView1.Visible = true;
                    Notificaciones.Visible = true;
                    Partida_Btn.Visible = true;
                    prueba.Visible = true;
                    listBox1.Visible = true;
                    textBox7.Visible = true;
                    EnviarChat.Visible = true;
                    this.PonerEnMarchaFormulario();
                   

                    
                }
                if (mensaje == "Incorrecto")
                {
                    conectado = 0;
                    atender.Abort();
                    Consultas.Visible = false;
                    dataGridView1.Visible = false;
                    //MessageBox.Show("Desconectado");
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();

                }
                
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
                string mensaje1 = "";
                mensaje1 = "3/"+ textBox1.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje1);
                server.Send(msg);

                
            }
            else if (Fecha.Checked)
            {
                string mensaje2 = "";
                mensaje2 = "4/" + textBox2.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                server.Send(msg);

                
            }
            else if (Ganador.Checked)
            {
                string mensaje3 = "";
                mensaje3 = "5/" +textBox3.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje3);
                server.Send(msg);                
            }
            else if (Conectados.Checked)
            {
                dataGridView1.Rows.Clear();
                string mensaje4 = "6/"+ "/Conectados";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje4);
                server.Send(msg);

                
            }
            else if (DameDeBaja.Checked)
            {
                string mensaje5 = "14/" + textBox5.Text + "/" + textBox6.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje5);
                server.Send(msg);
                Application.Restart();
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
                        
                        string mensaje3 = trozos2[0];
                        if (mensaje3 == "Correcto")
                        {
                            

                            MessageBox.Show("Inicio de sesion correcto");
                            
                        }
                        else {
                            MessageBox.Show("Ha habido algun error, vuelva a revisar los datos introducidos");

                            conectado = 0;


                            
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
               
                        mensaje3 = trozos2[1];
                        MessageBox.Show("El jugador tiene " + mensaje + " puntos");
                        break;

                    case 4: //Fecha partida 
                        trozos = mensaje.Split('/');
                        
                        MessageBox.Show("La fecha de la partida és: " + mensaje2);
                        break;
                    case 5: //Ganador de x partida
                        trozos = mensaje.Split('/');
                        
                        string resultado = trozos[0];
                        MessageBox.Show("El ganador de la partida ha sido: " + resultado);
                        break;
                    case 6: //lista conectados
                        dataGridView1.Rows.Clear();
                        trozos = mensaje.Split('/');
                        
                        
                        string[] Conectados = mensaje2.Split('/');
                        
                        foreach (var conectado in Conectados)
                        {
                            if (conectado != "6")
                            {
                                dataGridView1.Rows.Add(conectado);
                            }
                            
                        }
                        break;
                    case 7: //Dame cartas
                        int nForm = Convert.ToInt32(trozos[1]);
                        
                        prueba.Text = mensaje2;
                        formularios[nForm].TomaRespuesta(mensaje2);
                        break;
                    case 8: // Aceptar/rechazar partida
                        nForm = Convert.ToInt32(trozos[1]);                        
                        formularios[nForm].TomaRespuesta2(mensaje2);                     
                        
                        break;

                        
                    case 9: //iniciar partida
                        nForm = Convert.ToInt32(trozos[1]);
                        
                        break;
                    case 10: // cartas jugador1
                        nForm = Convert.ToInt32(trozos[1]);
                        formularios[nForm].TomaRespuesta4(mensaje2);
                        break;
                    case 11:// cartas jugador2
                        nForm = Convert.ToInt32(trozos[1]);
                        formularios[nForm].TomaRespuesta5(mensaje2);
                        break;
                    case 13:// chat
                        trozos2 = mensaje.Split('/');
                        
                        mensaje3 = trozos[1];
                        string texto = trozos[2];

                        DelegadoParaEscribir del = new DelegadoParaEscribir (AddItem);
                        this.Invoke(del, new Object[] { mensaje3 + ": " + texto });

                        break;
                    case 14: // Dame de baja
                        trozos = mensaje.Split('/');
                        //nForm = Convert.ToInt32(trozos[1]);
                        string usuarioeliminado = trozos[0];
                        MessageBox.Show("usuario: " + usuarioeliminado + "eliminado correctamente");



                        
                        //atender.Abort();
                        //Consultas.Visible = false;
                        //dataGridView1.Visible = false;

                        //MessageBox.Show("Desconectado");
                        //server.Shutdown(SocketShutdown.Both);
                        //server.Close();
                        break;

                }
            }
        }

        private void PonerEnMarchaFormulario ()
        {
            int cont = formularios.Count; 
            Partida f = new Partida(cont,server); //le pasamos el socket (server)
            f.jugadorLbl.Text = Usuario.Text;
            formularios.Add(f);
            f.ShowDialog();
            
        }
        private void Partida_Btn_Click(object sender, EventArgs e) // Empezar partida
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

        private void Aceptar_Click(object sender, EventArgs e) // Aceptar parida
        {
            string mensaje = "9/SI";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            MessageBox.Show("Has aceptado la partida");
            Aceptar.Visible = false;
            Rechazar.Visible = false;
        }

        private void Rechazar_Click(object sender, EventArgs e) // Rechazar partida
        {
            string mensaje = "9/NO";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            MessageBox.Show("Has rechazado la partida");
            Aceptar.Visible = false;
            Rechazar.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e) //Enviar solicitud partida
        {
            string nombre = textBox4.Text;
            string mensaje = "8/" + nombre;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            MessageBox.Show("Solicitud de partida enviada a " + nombre);
        }
        string jugador;
        public void Usuario_TextChanged(object sender, EventArgs e) //Nombre usuario
        {
            jugador = Usuario.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void desarroladoresToolStripMenuItem_Click(object sender, EventArgs e) //fotos desarrolladores
        {
            Desarroladores desarroladores = new Desarroladores();
            desarroladores.ShowDialog();
        }

        private void programaToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            MessageBox.Show("Videojuego/Simulador de Poker");
        }

        private void reglasDePokerToolStripMenuItem_Click(object sender, EventArgs e) //reglas juego
        {
            Reglas reglas = new Reglas();  
            reglas.ShowDialog();
        }

        private void fondo1ToolStripMenuItem_Click(object sender, EventArgs e) //cambiar fondo
        {
            Image i = Image.FromFile("C:\\share\\ProyectoSO_Ultimo\\ProyectoSO\\cliente\\Resources\\poker.jpg");;
            BackgroundImage = i;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void fondo2ToolStripMenuItem_Click(object sender, EventArgs e) //cambiar fondo 2
        {
            Image i = Image.FromFile("C:\\share\\ProyectoSO_Ultimo\\ProyectoSO\\cliente\\Resources\\poker2.jpg");
            BackgroundImage = i;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void fondo3ToolStripMenuItem_Click(object sender, EventArgs e) //cambiar fondo 3
        {
            Image i = Image.FromFile("C:\\share\\ProyectoSO_Ultimo\\ProyectoSO\\cliente\\Resources\\poker3.jpg");
            BackgroundImage = i;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void eETACToolStripMenuItem_Click(object sender, EventArgs e) //web escuela
        {
            System.Diagnostics.Process.Start("https://eetac.upc.edu/ca");            
        }

        private void pOToolStripMenuItem_Click(object sender, EventArgs e) // web poker
        {
            System.Diagnostics.Process.Start("https://www.888poker.es/");
        }

        private void pokerStarsToolStripMenuItem_Click(object sender, EventArgs e) //web poker2
        {
            System.Diagnostics.Process.Start("https://www.pokerstars.es/");
        }

        private void bet365ToolStripMenuItem_Click(object sender, EventArgs e) //web poker 3
        {
            System.Diagnostics.Process.Start("https://www.bet365.es/");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void AddItem(string mensaje) //añadir mensaje para chat
        {
            listBox1.Items.Add(mensaje);
        }

        private void EnviarChat_Click(object sender, EventArgs e) //boton enviar chat
        {
            string mensaje = "13/" + Usuario.Text + "/" + textBox7.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            
            

            textBox7.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) //list box chat
        {
            listBox1.Text = textBox7.Text;

        }

        private void reiniciarFormToolStripMenuItem_Click(object sender, EventArgs e) //reiniciar app
        {
            Application.Restart();
        }
    }
}
