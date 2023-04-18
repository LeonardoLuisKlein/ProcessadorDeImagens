using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace processamento_de_imagens
{
    public partial class Exer7 : Form
    {
        public Exer7()
        {
            InitializeComponent();
        }

        private void btExer7_Click(object sender, EventArgs e)
        {
            int[,] matriz1;
            matriz1 = new int[3, 3];

            int[,] matriz2;
            matriz2 = new int[3, 3];

            int[,] matrizAux;
            matrizAux = new int[3, 3];

            int[,] saida1;
            saida1 = new int[3, 3];

            int[,] saida2;
            saida2 = new int[3, 3];

            Random randNum = new Random();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int num;

                    num = randNum.Next(0, 255);

                    matriz1[i, j] = num;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int pixel;

                    pixel = randNum.Next(0, 255);

                    matriz2[i, j] = pixel;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int num;

                    if (matriz1[i, j] + matriz2[i, j] > 255) num = 255;
                    else num = matriz1[i, j] + matriz2[i, j];

                    saida1[i, j] = num;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int num;

                    num = matriz1[i, j] + matriz2[i, j];

                    matrizAux[i, j] = num;
                }
            }


            int pxMax = 0;
            int pxMin = 255;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int num = matriz1[i, j] + matriz2[i, j];

                    if (num > pxMax) pxMax = num;
                    if (num < pxMin) pxMin = num;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int pixel_normalizado;

                    pixel_normalizado = (int)(((double)(matrizAux[i, j] - pxMin) / (pxMax - pxMin)) * 255);

                    saida2[i, j] = pixel_normalizado;
                }
            }

            ExibirMatriz(matriz1, label1);
            ExibirMatriz(matriz2, label2);
            ExibirMatriz(matrizAux, label3);
            ExibirMatriz(saida1, label4);
            ExibirMatriz(saida2, label5);

        }

        private void ExibirMatriz(int[,] matriz, Label label)
        {
            string texto = "";
            int largura = matriz.GetLength(0);
            int altura = matriz.GetLength(1);

            for (int i = 0; i < largura; i++)
            {
                for (int j = 0; j < altura; j++)
                {
                    texto += matriz[i, j] + " ";
                }
                texto += "\n";
            }

            label.Text = texto;
        }
    }
}
