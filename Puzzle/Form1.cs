using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class From1 : Form
    {
        Button[] b = new Button[16];
        Random rand = new Random();
        private int clickCount = 0;
        private Timer timer;
        private int elapsedSeconds;
        public From1()
        {
            InitializeComponent();
            b[0] = b1;
            b[1] = b2;
            b[2] = b3;
            b[3] = b4;
            b[4] = b5;
            b[5] = b6;
            b[6] = b7;
            b[7] = b8;
            b[8] = b9;
            b[9] = b10;
            b[10] = b11;
            b[11] = b12;
            b[12] = b13;
            b[13] = b14;
            b[14] = b15;
            b[15] = b16;
            this.IniciaBoton();
            timer = new Timer();
            timer.Interval = 1000; // Intervalo de 1 segundo (1000 milisegundos)
            elapsedSeconds = 0;
            UpdateElapsedTime();

            // Iniciar el cronómetro
            timer.Start();
            timer.Tick += timer1_Tick;

            // Agregar eventos de clic a los botones
            for (int i = 0; i < b.Length; i++)
            {
                b[i].Click += Button_Click;
                
            }
        }

        public void IniciaBoton()
        {

            List<int> numeros = Enumerable.Range(1, 15).ToList();
            Random rand = new Random();

            int emptyIndex = -1; // Índice del botón vacío

            // Buscar si ya existe un espacio vacío
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i].Text == "")
                {
                    emptyIndex = i;
                    break;
                }
            }

            // Si no existe un espacio vacío, generarlo aleatoriamente
            if (emptyIndex == -1)
            {
                emptyIndex = rand.Next(0, b.Length);
                b[emptyIndex].Text = "";
            }


            // Asignar números aleatorios a los demás botones
            foreach (Button button in b)
            {
                if (button.Text != "")
                {
                    int randomIndex = rand.Next(0, numeros.Count);
                    int number = numeros[randomIndex];

                    button.Text = number.ToString();
                    numeros.RemoveAt(randomIndex);
                }
            }



        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            int clickedIndex = Array.IndexOf(b, clickedButton);

            int emptyIndex = GetEmptyButtonIndex();

            if (IsValidMove(clickedIndex, emptyIndex))
            {
                SwapButtons(clickedIndex, emptyIndex);
                CheckOrder();
                clickCount++; // Incrementar el contador de clicks
                lblM.Text= "Numero de Movidas: "+clickCount.ToString();
            }
        }

        private int GetEmptyButtonIndex()
        {
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i].Text == "")
                {
                    return i;
                }
            }
            return -1; // Si no se encuentra el botón vacío, devuelve -1
        }

        private bool IsValidMove(int clickedIndex, int emptyIndex)
        {
            // Verificar si los botones están adyacentes
            return (Math.Abs(clickedIndex - emptyIndex) == 1 && Math.Max(clickedIndex, emptyIndex) % 4 != 0) ||
                   Math.Abs(clickedIndex - emptyIndex) == 4;
        }

        private void SwapButtons(int clickedIndex, int emptyIndex)
        {
            string tempText = b[clickedIndex].Text;
            b[clickedIndex].Text = b[emptyIndex].Text;
            b[emptyIndex].Text = tempText;
        }

        private void CheckOrder()
        {
            bool isCorrectOrder = true;
            for (int i = 0; i < b.Length - 1; i++)
            {
                if (b[i].Text != (i + 1).ToString())
                {
                    isCorrectOrder = false;
                    break;
                }
            }

            if (isCorrectOrder)
            {
                MessageBox.Show("¡Felicitaciones! Has completado el puzzle.");
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            this.IniciaBoton();
            clickCount = 0;
            lblM.Text = "Numero de Movidas: " + clickCount.ToString();
            elapsedSeconds = 0;
            UpdateElapsedTime();

            // Iniciar el cronómetro
            timer.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Actualizar el tiempo transcurrido
            elapsedSeconds++;
            UpdateElapsedTime();
        }
        private void UpdateElapsedTime()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedSeconds);
            lblTiempo.Text = "Tiempo de Juego: "+timeSpan.ToString(@"hh\:mm\:ss");
        }
    }
}
