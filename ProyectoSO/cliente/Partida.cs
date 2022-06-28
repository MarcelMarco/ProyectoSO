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
    public partial class Partida : Form
    {
        int nForm;
        Socket server;
        
        public Partida(int nForm, Socket server) //iniciamos partida
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            
        }

        private void Form2_Load(object sender, EventArgs e) //cargamos form de partida
        {
            nFormulario.Text = nForm.ToString();
            Form1 f1 = new Form1();
            //MessageBox.Show(f1.Usuario.Text);
        }
        public void TomaRespuesta(string mensaje) //recoge string de cartas para jugadores 
        {
            string[] trozos = mensaje.Split('/');
            Confirmacion.Text = mensaje;
            int i = 2;
            int j = 0;
            PictureBox[] cartas = new PictureBox[100];
            cartas[0] = Carta3;
            cartas[1] = Carta4;
            cartas[2] = Carta5;
            cartas[3] = Carta6;
            cartas[4] = Carta7;
            cartas[5] = Carta1;
            cartas[6] = Carta2;

            Label[] ValorCartas = new Label[100];
            ValorCartas[0] = Carta3lbl;
            ValorCartas[1] = Carta4lbl;
            ValorCartas[2] = Carta5lbl;
            ValorCartas[3] = Carta6lbl;
            ValorCartas[4] = Carta7lbl;
            ValorCartas[5] = Carta1lbl;
            ValorCartas[6] = Carta2lbl;

            Label[] Palo = new Label[100];
            Palo[0] = Palo3;
            Palo[1] = Palo4;
            Palo[2] = Palo5;
            Palo[3] = Palo6;
            Palo[4] = Palo7;
            Palo[5] = Palo1;
            Palo[6] = Palo2;


            while (i <= 14) //recorremos string para identificar cartas
            {
                if (j > 7)
                {
                    j = 0;
                }
                int palo = Convert.ToInt32(trozos[i]);
                if (palo == 0) //Es Corazones
                {
                    
                    int valor = Convert.ToInt32(trozos[i+1]);
                    cartas[j].Image = Corazones.Images[valor];
                    cartas[j].Refresh();
                    ValorCartas[j].Text = valor.ToString();
                    Palo[j].Text = palo.ToString();

                    
                }
                if (palo == 1) //es picas
                {

                    int valor = Convert.ToInt32(trozos[i + 1]);
                    cartas[j].Image = Picas.Images[valor];
                    cartas[j].Refresh();
                    ValorCartas[j].Text = valor.ToString();
                    Palo[j].Text = palo.ToString();

                }
                if (palo == 2) //es diamantes
                {

                    int valor = Convert.ToInt32(trozos[i + 1]);
                    cartas[j].Image = Diamantes.Images[valor];
                    cartas[j].Refresh();
                    ValorCartas[j].Text = valor.ToString();
                    Palo[j].Text = palo.ToString();

                }
                if (palo ==3)//Es treboles palo == 3
                {
                   
                    int valor = Convert.ToInt32(trozos[i + 1]);
                    cartas[j].Image = Treboles.Images[valor];
                    cartas[j].Refresh();
                    ValorCartas[j].Text = valor.ToString();
                    Palo[j].Text = palo.ToString();

                }
                i = i + 2;
                j++;
            }
            
           
        }
        public void TomaRespuesta2(string mensaje) // comunicación entre jugadores
        {
            string[] trozos2 = mensaje.Split('/');
            Confirmacion4.Text = mensaje;
            Confirmacion1.Text = trozos2[0];
            Confirmacion2.Text = trozos2[1];
            Confirmacion3.Text = trozos2[2];
            MessageBox.Show(trozos2[2] + " te ha invitado a una partida");
        }
        public void TomaRespuesta3(string mensaje)
        {
            //    string[] trozos = mensaje.Split('/');
            //    if (trozos[2] == "SI")
            //    {
            //        //Tablero.Visible = true;
            //        MessageBox.Show("El rival a aceptado la partida, ahora te daremos las cartas");
            //    }
            //    else
            //        MessageBox.Show("El rival a rechazado la partida");
            //}
        }
        private void DameCartas_Btn_Click(object sender, EventArgs e) //asignar cartas
        {
            string mensaje = "7/" + nForm;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void InvitarPartida_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void AceptarBtn_Click(object sender, EventArgs e) //aceptamos partida
        {
            string jugador = jugadorLbl.Text;
            string mensaje = "9/" + nForm + "/SI/"+ jugador;
            //MessageBox.Show("Jugador: " + jugador);
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            
        }

        private void RechazarBtn_Click(object sender, EventArgs e) //rechazamos partida
        {
            string mensaje = "9/" + nForm + "/NO";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            
        }
        string jugadorPrincipal;
        public void TomaRespuesta6(string jugador) //recogemos nombre jugador
        {
            
            jugadorPrincipal = jugador;
            jugadorLbl.Text = jugador;
        }
        private void button1_Click(object sender, EventArgs e) //invitacion a partida
        {
            

            string nombre = InvitarPartida.Text;
            string jugadorPrincipal = jugadorLbl.Text;
            string mensaje = "8/"+ nForm +"/"+ nombre + "/" + jugadorPrincipal;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            MessageBox.Show("Solicitud de partida enviada a " + nombre);
        }
        string[] cartasMulti = new string[100];
        PictureBox[] cartas = new PictureBox[100];
        Label[] ValorCartas = new Label[100];
        Label[] Palo = new Label[100];
        public void TomaRespuesta4(string mensaje) //Asignación de cartas jugador principal
        {
            //MessageBox.Show("Recibido");
            string[] trozos1 = mensaje.Split('/');

            MessageBox.Show("El rival a aceptado la partida, ahora te daremos las cartas");
            //Ojo nuestras cartas dependerán de NumdeJugadores (tomarespuesta5)
            Confirmacion.Text = mensaje;
            int i = 2;
            int j = 0;

            
            cartas[0] = Carta3;
            cartas[1] = Carta4;
            cartas[2] = Carta5;
            cartas[3] = Carta6;
            cartas[4] = Carta7;
            cartas[5] = Carta1;
            cartas[6] = Carta2;

            
            ValorCartas[0] = Carta3lbl;
            ValorCartas[1] = Carta4lbl;
            ValorCartas[2] = Carta5lbl;
            ValorCartas[3] = Carta6lbl;
            ValorCartas[4] = Carta7lbl;
            ValorCartas[5] = Carta1lbl;
            ValorCartas[6] = Carta2lbl;

            
            Palo[0] = Palo3;
            Palo[1] = Palo4;
            Palo[2] = Palo5;
            Palo[3] = Palo6;
            Palo[4] = Palo7;
            Palo[5] = Palo1;
            Palo[6] = Palo2;


            while (i <= 12) //CARTAS DE MESA
            {
                if (j > 7)
                {
                    j = 0;
                }
                
                int palo = Convert.ToInt32(trozos1[i]);
                if (palo == 0) //Es Corazones
                {

                    int valor = Convert.ToInt32(trozos1[i + 1]);
                    if (j < 7)
                    {
                        cartas[j].Image = Corazones.Images[valor];
                        cartas[j].Refresh();
                        ValorCartas[j].Text = valor.ToString();
                        Palo[j].Text = palo.ToString();
                    }
                }
                if (palo == 1) //es picas
                {

                    int valor = Convert.ToInt32(trozos1[i + 1]);
                    if (j < 7)
                    {
                        cartas[j].Image = Picas.Images[valor];
                        cartas[j].Refresh();
                        ValorCartas[j].Text = valor.ToString();
                        Palo[j].Text = palo.ToString();
                    }
                    

                }
                if (palo == 2) //es diamantes
                {
                    int valor = Convert.ToInt32(trozos1[i + 1]);
                    if (j < 7)
                    {
                        cartas[j].Image = Diamantes.Images[valor];
                        cartas[j].Refresh();
                        ValorCartas[j].Text = valor.ToString();
                        Palo[j].Text = palo.ToString();
                    }
                }
                if (palo == 3)//Es treboles palo == 3
                {

                    int valor = Convert.ToInt32(trozos1[i + 1]);
                    if (j < 7)
                    {
                        cartas[j].Image = Treboles.Images[valor];
                        cartas[j].Refresh();
                        ValorCartas[j].Text = valor.ToString();
                        Palo[j].Text = palo.ToString();
                    }
                }
                i = i + 2;
                j++;
            }
            j = 6;

            //MessageBox.Show("Hay dos jugadores");
            while ( i <= 17)
            {
                if (j > 7)
                {
                    j = 6;
                }
                int palo = Convert.ToInt32(trozos1[i]);
                if (palo == 0) //Es Corazones
                {

                    int valor = Convert.ToInt32(trozos1[i + 1]);
                    if (j < 7)
                    {
                        cartas[j].Image = Corazones.Images[valor];
                        cartas[j].Refresh();
                        ValorCartas[j].Text = valor.ToString();
                        Palo[j].Text = palo.ToString();
                    }
                }
                if (palo == 1) //es picas
                {

                    int valor = Convert.ToInt32(trozos1[i + 1]);
                    if (j < 7)
                    {
                        cartas[j].Image = Picas.Images[valor];
                        cartas[j].Refresh();
                        ValorCartas[j].Text = valor.ToString();
                        Palo[j].Text = palo.ToString();
                    }


                }
                if (palo == 2) //es diamantes
                {
                    int valor = Convert.ToInt32(trozos1[i + 1]);
                    if (j < 7)
                    {
                        cartas[j].Image = Diamantes.Images[valor];
                        cartas[j].Refresh();
                        ValorCartas[j].Text = valor.ToString();
                        Palo[j].Text = palo.ToString();
                    }
                }
                if (palo == 3)//Es treboles palo == 3
                {

                    int valor = Convert.ToInt32(trozos1[i + 1]);
                    if (j < 7)
                    {
                        cartas[j].Image = Treboles.Images[valor];
                        cartas[j].Refresh();
                        ValorCartas[j].Text = valor.ToString();
                        Palo[j].Text = palo.ToString();
                    }
                }
                i = i + 2;
                j++;

            }
        }

        int NumeroDeJugador;
        public void TomaRespuesta5(string mensaje) //Assignacion de cartas segundo jugador
        {
            Confirmacion.Text = mensaje;
            //MessageBox.Show(mensaje);
            string[] trozos1 = mensaje.Split('/');
            LabelJugadores.Text = mensaje;
            Trozos1.Text = Convert.ToString(trozos1[0]);
            Trozos2.Text = Convert.ToString(trozos1[1]);
            Trozos3.Text = Convert.ToString(trozos1[2]);
            //NumeroDeJugador = Convert.ToInt32(trozos1[2]);
            NumeroJugador.Text = trozos1[2];
            //MessageBox.Show(trozos1[2]);
            //MessageBox.Show(trozos1[30]);

            cartas[2] = Carta3;
            cartas[3] = Carta4;
            cartas[4] = Carta5;
            cartas[5] = Carta6;
            cartas[6] = Carta7;
            cartas[7] = Carta1;
            cartas[8] = Carta2;


            ValorCartas[2] = Carta3lbl;
            ValorCartas[3] = Carta4lbl;
            ValorCartas[4] = Carta5lbl;
            ValorCartas[5] = Carta6lbl;
            ValorCartas[6] = Carta7lbl;
            ValorCartas[7] = Carta1lbl;
            ValorCartas[8] = Carta2lbl;


            Palo[2] = Palo3;
            Palo[3] = Palo4;
            Palo[4] = Palo5;
            Palo[5] = Palo6;
            Palo[6] = Palo7;
            Palo[7] = Palo1;
            Palo[8] = Palo2;

            int j = 2;
            int i = 4;
            if (trozos1[2] != "1")
            {
                //MessageBox.Show(trozos1[30]);
                while (i <= 14) //CARTAS DE MESA
                {
                    if (j > 9)
                    {
                        j = 2;
                    }
                    
                    int palo = Convert.ToInt32(trozos1[i]);

                    if (palo == 0) //Es Corazones
                    {

                        int valor = Convert.ToInt32(trozos1[i + 1]);
                        if (j < 9)
                        {
                            cartas[j].Image = Corazones.Images[valor];
                            cartas[j].Refresh();
                            ValorCartas[j].Text = valor.ToString();
                            Palo[j].Text = palo.ToString();
                        }
                    }
                    if (palo == 1) //es picas
                    {

                        int valor = Convert.ToInt32(trozos1[i + 1]);
                        if (j < 9)
                        {
                            cartas[j].Image = Picas.Images[valor];
                            cartas[j].Refresh();
                            ValorCartas[j].Text = valor.ToString();
                            Palo[j].Text = palo.ToString();
                        }


                    }
                    if (palo == 2) //es diamantes
                    {
                        int valor = Convert.ToInt32(trozos1[i + 1]);
                        if (j < 9)
                        {
                            cartas[j].Image = Diamantes.Images[valor];
                            cartas[j].Refresh();
                            ValorCartas[j].Text = valor.ToString();
                            Palo[j].Text = palo.ToString();
                        }
                    }
                    if (palo == 3)//Es treboles palo == 3
                    {

                        int valor = Convert.ToInt32(trozos1[i + 1]);
                        if (j < 9)
                        {
                            cartas[j].Image = Treboles.Images[valor];
                            cartas[j].Refresh();
                            ValorCartas[j].Text = valor.ToString();
                            Palo[j].Text = palo.ToString();
                        }
                    }
                    i = i + 2;
                    j++;
                }

                while (i <= 21)
                {
                    if (j > 9)
                    {
                        j = 8;
                    }
                    int palo = Convert.ToInt32(trozos1[i]);
                    if (palo == 0) //Es Corazones
                    {

                        int valor = Convert.ToInt32(trozos1[i + 1]);
                        if (j < 9)
                        {
                            cartas[j].Image = Corazones.Images[valor];
                            cartas[j].Refresh();
                            ValorCartas[j].Text = valor.ToString();
                            Palo[j].Text = palo.ToString();
                        }
                    }
                    if (palo == 1) //es picas
                    {

                        int valor = Convert.ToInt32(trozos1[i + 1]);
                        if (j < 9)
                        {
                            cartas[j].Image = Picas.Images[valor];
                            cartas[j].Refresh();
                            ValorCartas[j].Text = valor.ToString();
                            Palo[j].Text = palo.ToString();
                        }


                    }
                    if (palo == 2) //es diamantes
                    {
                        int valor = Convert.ToInt32(trozos1[i + 1]);
                        if (j < 9)
                        {
                            cartas[j].Image = Diamantes.Images[valor];
                            cartas[j].Refresh();
                            ValorCartas[j].Text = valor.ToString();
                            Palo[j].Text = palo.ToString();
                        }
                    }
                    if (palo == 3)//Es treboles palo == 3
                    {

                        int valor = Convert.ToInt32(trozos1[i + 1]);
                        if (j < 9)
                        {
                            cartas[j].Image = Treboles.Images[valor];
                            cartas[j].Refresh();
                            ValorCartas[j].Text = valor.ToString();
                            Palo[j].Text = palo.ToString();
                        }
                    }
                    i = i + 2;
                    j++;

                }
            }
        }

        public void jugadorLbl_Click(object sender, EventArgs e)
        {

        }

        private void Confirmacion_Click(object sender, EventArgs e)
        {

        }

        private void Trozos1_Click(object sender, EventArgs e)
        {

        }

        private void LabelJugadores_Click(object sender, EventArgs e)
        {

        }
    }
}
