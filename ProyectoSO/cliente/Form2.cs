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
    public partial class Form2 : Form
    {
        int nForm;
        Socket server;
        
        public Form2(int nForm, Socket server)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            nFormulario.Text = nForm.ToString();
        }
        public void TomaRespuesta(string mensaje)
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


            while (i <= 14)
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
        private void DameCartas_Btn_Click(object sender, EventArgs e)
        {
            string mensaje = "7/" + nForm;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }
}
