using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace processamento_de_imagens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btCarregaImgA_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Arquivos de imagem (**.jpg, *.jpeg, *.png *.tif)|*.jpg;*.jpeg;*.png; *.tif";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                imgA.Load(openFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("Selecione uma imagem valida");
                return;
            }
        }

        private void btCarregaImgB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Arquivos de imagem (*.jpg, *.jpeg, *.png *.tif)|*.jpg;*.jpeg;*.png; *.tif";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                imgB.Load(openFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("Selecione uma imagem valida");
                return;
            }
        }

        private void btSalvaImg_Click(object sender, EventArgs e)
        {
            if (imgFinal.Image == null)
            {
                MessageBox.Show("A imagem resultante não pode ser salva porque ela não existe.");
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Imagem JPEG|*.jpg|Imagem PNG|*.png|Todos os arquivos|*.*";
            saveFileDialog1.Title = "Salvar Imagem Resultante";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                ImageFormat format;
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        format = ImageFormat.Jpeg;
                        break;
                    case 2:
                        format = ImageFormat.Png;
                        break;
                    case 3:
                        format = ImageFormat.Tiff;
                        break;
                    default:
                        format = ImageFormat.Jpeg;
                        break;
                }

                imgFinal.Image.Save(saveFileDialog1.FileName, format);
            }
        }

        private void btAdicao_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
           {
               MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
               return;
           }


            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);


            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    Color color2 = ((Bitmap)image2).GetPixel(x, y);

                    int r = color1.R + color2.R;
                    int g = color1.G + color2.G;
                    int b = color1.B + color2.B;

                    r = Math.Min(r, 255);
                    g = Math.Min(g, 255);
                    b = Math.Min(b, 255);

                    imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            imgFinal.Image = imagemResultado;
        }

        private void btSubtracao_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    Color color2 = ((Bitmap)image2).GetPixel(x, y);
                    int r = Math.Abs(color1.R - color2.R);
                    int g = Math.Abs(color1.G - color2.G);
                    int b = Math.Abs(color1.B - color2.B);

                    imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            imgFinal.Image = imagemResultado;
        }

        private void btMultiplicacao_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            decimal multiplicaco = nupMultiplicacao.Value;
            if (multiplicaco == 0 || multiplicaco < 0)
            {
                MessageBox.Show("Somente valores maiores que 0");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    Color color2 = ((Bitmap)image2).GetPixel(x, y);
                    int r = (int)Math.Max(0, Math.Min(255, Math.Round(color1.R * multiplicaco + color2.R * multiplicaco)));
                    int g = (int)Math.Max(0, Math.Min(255, Math.Round(color1.G * multiplicaco + color2.G * multiplicaco)));
                    int b = (int)Math.Max(0, Math.Min(255, Math.Round(color1.B * multiplicaco + color2.B * multiplicaco)));

                    imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            imgFinal.Image = imagemResultado;
        }

        private void btDivisao_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            decimal divisao = nupDivisao.Value;
            if (divisao == 0 || divisao < 0)
            {
                MessageBox.Show("Somente valores maiores que 0");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        Color color2 = ((Bitmap)image2).GetPixel(x, y);
                    int r = (int)Math.Max(0, Math.Min(255, Math.Round(color1.R / divisao + color2.R / divisao)));
                    int g = (int)Math.Max(0, Math.Min(255, Math.Round(color1.G / divisao + color2.G / divisao)));
                    int b = (int)Math.Max(0, Math.Min(255, Math.Round(color1.B / divisao + color2.B / divisao)));

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
                }
            
            imgFinal.Image = imagemResultado;
        }

        private void btMedia_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    Color color2 = ((Bitmap)image1).GetPixel(x, y);
                    int mediaR = (color1.R + color2.R) / 3;
                    int mediaG = (color1.G + color2.G) / 3;
                    int mediaB = (color1.B + color2.B) / 3;
                    Color corResultado = Color.FromArgb(mediaR, mediaG, mediaB);
                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            imgFinal.Image = imagemResultado;
        }

        private void btBlending_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            decimal blending = nupBlending.Value;
            if (blending == 0 || blending < 0)
            {
                MessageBox.Show("Somente valores maiores que 0");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    Color color2 = ((Bitmap)image2).GetPixel(x, y);
                    int r = (int)Math.Round((1 - blending) * color1.R + blending * color2.R);
                    int g = (int)Math.Round((1 - blending) * color1.G + blending * color2.G);
                    int b = (int)Math.Round((1 - blending) * color1.B + blending * color2.B);

                    imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            imgFinal.Image = imagemResultado;
        }

        private void btAnd_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color cor1 = ((Bitmap)image1).GetPixel(x, y);
                    Color cor2 = ((Bitmap)image2).GetPixel(x, y);
                    Color corResultado = Color.FromArgb(cor1.R & cor2.R, cor1.G & cor2.G, cor1.B & cor2.B);
                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            imgFinal.Image = imagemResultado;
        }

        private void btOr_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color cor1 = ((Bitmap)image1).GetPixel(x, y);
                    Color cor2 = ((Bitmap)image2).GetPixel(x, y);
                    Color corResultado = Color.FromArgb(cor1.R | cor2.R, cor1.G | cor2.G, cor1.B | cor2.B);
                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            imgFinal.Image = imagemResultado;
        }

        private void btXor_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color cor1 = ((Bitmap)image1).GetPixel(x, y);
                    Color cor2 = ((Bitmap)image2).GetPixel(x, y);
                    Color corResultado = Color.FromArgb(cor1.R ^ cor2.R, cor1.G ^ cor2.G, cor1.B ^ cor2.B);
                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            imgFinal.Image = imagemResultado;
        }

        private void btNot_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color cor1 = ((Bitmap)image1).GetPixel(x, y);
                    Color cor2 = ((Bitmap)image1).GetPixel(x, y);
                    int novoR = 255 - Math.Min(255, Math.Max(0, cor1.R + cor2.R));
                    int novoG = 255 - Math.Min(255, Math.Max(0, cor1.G + cor2.G));
                    int novoB = 255 - Math.Min(255, Math.Max(0, cor1.B + cor2.B));

                    Color novaCor = Color.FromArgb(novoR, novoG, novoB);
                    imagemResultado.SetPixel(x, y, novaCor);
                }
            }

            imgFinal.Image = imagemResultado;
        }

        private void btExer9_Click(object sender, EventArgs e)
        {
            Image image1 = imgA.Image;

            if (image1 == null)
            {
                MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                return;
                
            }

            Bitmap imagemResultado = new Bitmap(image1.Width * 2, image1.Height);

            using (Graphics g = Graphics.FromImage(imagemResultado))
            {
                g.DrawImage(image1, 0, 0);
            }

            Bitmap mirroredImage = new Bitmap(image1);
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

            using (Graphics g = Graphics.FromImage(imagemResultado))
            {
                g.DrawImage(mirroredImage, image1.Width, 0);
            }

            imgFinal.Image = imagemResultado;
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
