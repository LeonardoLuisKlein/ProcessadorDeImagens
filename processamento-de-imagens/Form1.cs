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
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace processamento_de_imagens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Inicializando as variáveis do track bar com o máximo 100 e o mínimo 0
            tbBrilho.Maximum = 100;
            tbBrilho.Minimum = 0;
        }
        
        // Função para pegar o máximo da matriz
        private int GetMaximo(int[,] matriz)
        {
            int maximo = int.MinValue;

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] > maximo)
                    {
                        maximo = matriz[i, j];
                    }
                }
            }

            return maximo;
        }

        // Função para pegar o minimo da matriz
        private int GetMinimo(int[,] matriz)
        {
            int minimo = int.MaxValue;

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] < minimo)
                    {
                        minimo = matriz[i, j];
                    }
                }
            }

            return minimo;
        }

        // Função para pegar o valor médio da matriz
        private int GetMedia(int[,] matriz)
        {
            int soma = 0;

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    soma += matriz[i, j];
                }
            }

            int media = soma / (matriz.GetLength(0) * matriz.GetLength(1));
            return media;
        }

        // Função para pegar o valor mediano da matriz
        private int GetMediana(int[,] matriz)
        {
            int tamanho = matriz.GetLength(0) * matriz.GetLength(1);
            int[] elementos = new int[tamanho];

            int index = 0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    elementos[index] = matriz[i, j];
                    index++;
                }
            }

            Array.Sort(elementos);

            int mediana;
            if (tamanho % 2 == 0)
            {
                int meio = tamanho / 2;
                mediana = (elementos[meio - 1] + elementos[meio]) / 2;
            }
            else
            {
                int meio = tamanho / 2;
                mediana = elementos[meio];
            }

            return mediana;
        }

        private int GetSuave(int[,] matriz)
        {
            int tamanho = matriz.GetLength(0) * matriz.GetLength(1);
            int[] elementos = new int[tamanho];

            int index = 0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    elementos[index] = matriz[i, j];
                    index++;
                }
            }

            Array.Sort(elementos);
            int meio = elementos[4];
            if (rb3x3.Checked) 
            {
                meio = elementos[4];
            }
            else if(rb5x5.Checked)
            {
                meio = elementos[12];
            }
            else if (rb5x5.Checked)
            {
                meio = elementos[24];
            }

            int min = elementos.Min();
            int max = elementos.Max();
            if (meio > max)
            {
                meio = max;
            }
            else if (meio < min)
            {
                meio = min;
            }

            return meio;
        }

        private int GetOrdem(int[,] matriz)
        {
            int tamanho = matriz.GetLength(0) * matriz.GetLength(1);
            int[] elementos = new int[tamanho];

            int index = 0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    elementos[index] = matriz[i, j];
                    index++;
                }
            }

            Array.Sort(elementos);

            int ordem;
            ordem = elementos[(int)nupOrdem.Value];
            return ordem;
        }

        private double GetGaussian(int[,]  vizi, double sigma)
        {
            double soma = 0;

            int size = vizi.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int valorPixel = vizi[i, j];
                    double exponente = -(i * i + j * j) / (2 * sigma * sigma);
                    double peso = Math.Exp(exponente) / (2 * Math.PI * sigma * sigma);
                    soma += valorPixel * peso;
                }
            }

            return soma;
        }

        // Função para carregar uma imagem no campo A
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

        // Função para carregar uma imagem no campo B
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

        // Função para salvar a imagem processada
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

        // Função de adição
        private void btAdicao_Click(object sender, EventArgs e)
        {

            // Caso nenhuma opção de imagem está selecionada, mostra uma mensagem sem retorno
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            // Caso esteja selecionado ambas imagens para processar
            if (rbDuas.Checked) { 
            // Carrega as imagens do campo de imagem A e B
            Image image1 = imgA.Image;
            Image image2 = imgB.Image;

            // Trata para ver se não existe imagem em um dos campos
            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            // Verifica se o tamanho e o formato de ambas imagens conhecidem 
            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
           {
               MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
               return;
           }

            // Cria um novo bitmap, com a largura e a altura da primeira imagem
            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);


            // For para mapear todos os pixeis 
            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    //Color para pegar o valor dos pixeis R, G, B
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    Color color2 = ((Bitmap)image2).GetPixel(x, y);

                    // Soma cada "camada" da matriz
                    int r = color1.R + color2.R;
                    int g = color1.G + color2.G;
                    int b = color1.B + color2.B;

                    // Trunca para não passar de 255
                    r = Math.Min(r, 255);
                    g = Math.Min(g, 255);
                    b = Math.Min(b, 255);

                    // Seta os pixeis que estão sem valor com a soma dos valores r, g, b
                    imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            // Exibe a imagem no pictureBox imagem final
            imgFinal.Image = imagemResultado;
            }

            // Caso esteja selecionado a imagem A
            // Os passos são os mesmos para a soma de duas imagens, exeto a validação de tamanho e formato, e também, soma com ela mesma
            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo A");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);


                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);

                        int r = color1.R + color1.R;
                        int g = color1.G + color1.G;
                        int b = color1.B + color1.B;

                        r = Math.Min(r, 255);
                        g = Math.Min(g, 255);
                        b = Math.Min(b, 255);

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                imgFinal.Image = imagemResultado;
            }

            // Caso esteja selecionado a imagem B
            // Os passos são os mesmos para a soma de duas imagens, exeto a validação de tamanho e formato, e também, soma com ela mesma
            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo B");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);


                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);

                        int r = color1.R + color1.R;
                        int g = color1.G + color1.G;
                        int b = color1.B + color1.B;

                        r = Math.Min(r, 255);
                        g = Math.Min(g, 255);
                        b = Math.Min(b, 255);

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                imgFinal.Image = imagemResultado;
            }
        }

        // Função de subtração
        // Segue os mesmos passos da adição, até a parte da subtração
        private void btSubtracao_Click(object sender, EventArgs e)
        {
            if(!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked) { 
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
                    
                    // Math.Abs para os valores não serem negativos
                    int r = Math.Abs(color1.R - color2.R);
                    int g = Math.Abs(color1.G - color2.G);
                    int b = Math.Abs(color1.B - color2.B);

                    imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            imgFinal.Image = imagemResultado;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo A");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = Math.Abs(color1.R - color1.R);
                        int g = Math.Abs(color1.G - color1.G);
                        int b = Math.Abs(color1.B - color1.B);

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo B");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = Math.Abs(color1.R - color1.R);
                        int g = Math.Abs(color1.G - color1.G);
                        int b = Math.Abs(color1.B - color1.B);

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função de multiplicação 
        private void btMultiplicacao_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked) { 
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

            // Decimal para pegar o valor do campo numérico, para ver quantas vezes vai multiplicar
            // O valor do decimal não pode ser 0
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
                    // Trunca os valores entre 0 e 255 e também arredonda os valores
                    // Conforme o valor está no decimal, vai multiplicar
                    int r = (int)Math.Max(0, Math.Min(255, Math.Round(color1.R * multiplicaco + color2.R * multiplicaco)));
                    int g = (int)Math.Max(0, Math.Min(255, Math.Round(color1.G * multiplicaco + color2.G * multiplicaco)));
                    int b = (int)Math.Max(0, Math.Min(255, Math.Round(color1.B * multiplicaco + color2.B * multiplicaco)));

                    imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            imgFinal.Image = imagemResultado;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo A");
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
                        int r = (int)Math.Max(0, Math.Min(255, Math.Round(color1.R * multiplicaco + color1.R * multiplicaco)));
                        int g = (int)Math.Max(0, Math.Min(255, Math.Round(color1.G * multiplicaco + color1.G * multiplicaco)));
                        int b = (int)Math.Max(0, Math.Min(255, Math.Round(color1.B * multiplicaco + color1.B * multiplicaco)));

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo A");
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
                        int r = (int)Math.Max(0, Math.Min(255, Math.Round(color1.R * multiplicaco + color1.R * multiplicaco)));
                        int g = (int)Math.Max(0, Math.Min(255, Math.Round(color1.G * multiplicaco + color1.G * multiplicaco)));
                        int b = (int)Math.Max(0, Math.Min(255, Math.Round(color1.B * multiplicaco + color1.B * multiplicaco)));

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função de divisão
        // Segue os mesmos passos da multiplicação, somente mudando a operação
        private void btDivisao_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked) { 
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

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo A");
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
                        int r = (int)Math.Max(0, Math.Min(255, Math.Round(color1.R / divisao + color1.R / divisao)));
                        int g = (int)Math.Max(0, Math.Min(255, Math.Round(color1.G / divisao + color1.G / divisao)));
                        int b = (int)Math.Max(0, Math.Min(255, Math.Round(color1.B / divisao + color1.B / divisao)));

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo B");
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
                        int r = (int)Math.Max(0, Math.Min(255, Math.Round(color1.R / divisao + color1.R / divisao)));
                        int g = (int)Math.Max(0, Math.Min(255, Math.Round(color1.G / divisao + color1.G / divisao)));
                        int b = (int)Math.Max(0, Math.Min(255, Math.Round(color1.B / divisao + color1.B / divisao)));

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função média
        private void btMedia_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked) { 
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
                    
                    // Para a média, divide por dois
                    int r = (color1.R + color2.R) / 2;
                    int g = (color1.G + color2.G) / 2;
                    int b = (color1.B + color2.B) / 2;
                    
                    Color corResultado = Color.FromArgb(r, g, b);
                    
                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            imgFinal.Image = imagemResultado;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo A");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = (color1.R + color1.R) / 2;
                        int g = (color1.G + color1.G) / 2;
                        int b = (color1.B + color1.B) / 2;
                        Color corResultado = Color.FromArgb(r, g, b);
                        imagemResultado.SetPixel(x, y, corResultado);
                    }
                }

                imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo B");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = (color1.R + color1.R) / 2;
                        int g = (color1.G + color1.G) / 2;
                        int b = (color1.B + color1.B) / 2;
                        Color corResultado = Color.FromArgb(r, g, b);
                        imagemResultado.SetPixel(x, y, corResultado);
                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função Blending
        // Mesma coisa da multiplicação e divisão, mudando na hora da operação
        private void btBlending_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked) { 
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

                    // Para a operação de blending, diminuimos o valor do blending por 1 uma vez, depois, multiplica o valor do blending com a cor e após, soma as duas variáveis 
                    int r = Math.Min(255, Math.Max(0, (int)Math.Round((1 - blending) * color1.R + blending * color2.R)));
                    int g = Math.Min(255, Math.Max(0, (int)Math.Round((1 - blending) * color1.G + blending * color2.G)));
                    int b = Math.Min(255, Math.Max(0, (int)Math.Round((1 - blending) * color1.B + blending * color2.B)));

                    imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            imgFinal.Image = imagemResultado;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo A");
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
                        int r = Math.Min(255, Math.Max(0, (int)Math.Round((1 - blending) * color1.R + blending * color1.R)));
                        int g = Math.Min(255, Math.Max(0, (int)Math.Round((1 - blending) * color1.G + blending * color1.G)));
                        int b = Math.Min(255, Math.Max(0, (int)Math.Round((1 - blending) * color1.B + blending * color1.B)));

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo B");
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
                        int r = Math.Min(255, Math.Max(0, (int)Math.Round((1 - blending) * color1.R + blending * color1.R)));
                        int g = Math.Min(255, Math.Max(0, (int)Math.Round((1 - blending) * color1.G + blending * color1.G)));
                        int b = Math.Min(255, Math.Max(0, (int)Math.Round((1 - blending) * color1.B + blending * color1.B)));

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função AND
        private void btAnd_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked) { 
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

                    // Operação simples, usando apenas o sinal do AND
                    Color corResultado = Color.FromArgb(color1.R & color2.R, color1.G & color2.G, color1.B & color2.B);
                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            imgFinal.Image = imagemResultado;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo A");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        Color corResultado = Color.FromArgb(color1.R & color1.R, color1.G & color1.G, color1.B & color1.B);
                        imagemResultado.SetPixel(x, y, corResultado);
                    }
                }

                imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo B");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        Color corResultado = Color.FromArgb(color1.R & color1.R, color1.G & color1.G, color1.B & color1.B);
                        imagemResultado.SetPixel(x, y, corResultado);
                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função OR
        private void btOr_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked) { 
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

                    // Função simples, usando apenas o sinal do OR
                    Color corResultado = Color.FromArgb(color1.R | color2.R, color1.G | color2.G, color1.B | color2.B);
                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            imgFinal.Image = imagemResultado;

            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo A");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        Color corResultado = Color.FromArgb(color1.R | color1.R, color1.G | color1.G, color1.B | color1.B);
                        imagemResultado.SetPixel(x, y, corResultado);
                    }
                }

                imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo B");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        Color corResultado = Color.FromArgb(color1.R | color1.R, color1.G | color1.G, color1.B | color1.B);
                        imagemResultado.SetPixel(x, y, corResultado);
                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função XOR
        private void btXor_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked) { 
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

                        // Função simples, usando apenas o sinal do XOR
                        Color corResultado = Color.FromArgb(color1.R ^ color2.R, color1.G ^ color2.G, color1.B ^ color2.B);
                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            imgFinal.Image = imagemResultado;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo A");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        Color corResultado = Color.FromArgb(color1.R ^ color1.R, color1.G ^ color1.G, color1.B ^ color1.B);
                        imagemResultado.SetPixel(x, y, corResultado);
                    }
                }

                imgFinal.Image = imagemResultado;
            }
            
            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo B");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        Color corResultado = Color.FromArgb(color1.R ^ color1.R, color1.G ^ color1.G, color1.B ^ color1.B);
                        imagemResultado.SetPixel(x, y, corResultado);
                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função NOT
        private void btNot_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked) { 
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

                    // Soma as duas cores e diminui por 255, trunca os valores para não passar de 0 e 255
                    int r = 255 - Math.Min(255, Math.Max(0, color1.R + color2.R));
                    int g = 255 - Math.Min(255, Math.Max(0, color1.G + color2.G));
                    int b = 255 - Math.Min(255, Math.Max(0, color1.B + color2.B));

                    Color novaCor = Color.FromArgb(r, g, b);
                    imagemResultado.SetPixel(x, y, novaCor);
                }
            }

            imgFinal.Image = imagemResultado;
            }

            if (rbA.Checked || rbB.Checked)
            {
                MessageBox.Show("Para fazer o not de uma imagem, utilize a opção Negativo");
                return;
            }
        }

        // Exer 9
        private void btExer9_Click(object sender, EventArgs e)
        {

            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked) { 
            Image image1 = imgA.Image;

            if (image1 == null)
            {
                MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                return;
                
            }

            // Copia a largura * 2 e a altura normal
            Bitmap imagemResultado = new Bitmap(image1.Width * 2, image1.Height);

            // Usa a função Graphics na imagem resultado
            using (Graphics g = Graphics.FromImage(imagemResultado))
            {
                g.DrawImage(image1, 0, 0);
            }

            // Cria um novo bitmap para a imagem espelhada
            Bitmap mirroredImage = new Bitmap(image1);

            // Rotaciona a imagem
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

            // Usa a função graphics na imagem resultado e cria uma nova imagem, com a imagem espelhada e a imagem 1
            using (Graphics g = Graphics.FromImage(imagemResultado))
            {
                g.DrawImage(mirroredImage, image1.Width, 0);
            }

            imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem B");
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
        }

        // Exer 7 leva em outro form
        private void btExer7_Click(object sender, EventArgs e)
        {
            var form = new Exer7();
            form.Show();
        }

        // Função Cinza, a mais usada no projeto
        private void btRgbCinza_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;

                        // Soma todas as camadas da imagem e divide por 3
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemResultado.SetPixel(x, y, novaCor);

                    }
                }

                imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem B");
                    return;

                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemResultado.SetPixel(x, y, novaCor);

                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função Negativo
        private void btNegativo_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked) { 
            Image image1 = imgA.Image;

            if (image1 == null)
            {
                MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                return;

            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    Color color2 = ((Bitmap)image1).GetPixel(x, y);

                    // Funciona como um not, soma as duas camadas e diminui por 255, truncando os valores entre 0 e 255
                    int r = 255 - Math.Min(255, Math.Max(0, color1.R + color2.R));
                    int g = 255 - Math.Min(255, Math.Max(0, color1.G + color2.G));
                    int b = 255 - Math.Min(255, Math.Max(0, color1.B + color2.B));

                    Color novaCor = Color.FromArgb(r, g, b);

                    imagemResultado.SetPixel(x, y, novaCor);
                }
            }

            imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem B");
                    return;

                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        Color color2 = ((Bitmap)image1).GetPixel(x, y);
                        int r = 255 - Math.Min(255, Math.Max(0, color1.R + color2.R));
                        int g = 255 - Math.Min(255, Math.Max(0, color1.G + color2.G));
                        int b = 255 - Math.Min(255, Math.Max(0, color1.B + color2.B));

                        Color novaCor = Color.FromArgb(r, g, b);
                        imagemResultado.SetPixel(x, y, novaCor);
                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função Binária
        private void btRgbBin_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked) { 
            Image image1 = imgA.Image;

            if (image1 == null)
            {
                MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                return;

            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    int r = color1.R;
                    int g = color1.G;
                    int b = color1.B;
                    // Transforma a imagem para escala de cinza
                    int gray = (r + g + b) / 3;

                    // Operadores ternários, pois um if completo não funcionava, mas caso os valores forem maior que 128, o pixel fica 255, caso menores ou iguais, 0
                    int binario = gray > 128 ? 255 : 0;

                    Color novaImagem = Color.FromArgb(binario, binario, binario);

                    imagemResultado.SetPixel(x, y, novaImagem);
                }
            }

            imgFinal.Image = imagemResultado;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem B");
                    return;

                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;
                        int binario = gray > 128 ? 255 : 0;
                        Color novaImagem = Color.FromArgb(binario, binario, binario);
                        imagemResultado.SetPixel(x, y, novaImagem);
                    }
                }

                imgFinal.Image = imagemResultado;
            }
        }

        // Função para brilho
        // Função complexa, caso o usuário deseja aumentar o brilho de duas imagens, fica lento mas funcional, pois antes de aumentar o brilho, faz a soma das duas imagens
        private void tbBrilho_Scroll(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
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

                Bitmap imagemResultadoSoma = new Bitmap(image1.Width, image1.Height);

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

                        imagemResultadoSoma.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                // Para ajustar o valor do track bar no valor 0
                int brightnessValue = tbBrilho.Value - 50;

                Image image3 = imagemResultadoSoma;

                // Cria uma matriz aonde cada linha representa um componente da cor, R,G,B,A,B. 
                // A matriz tem on numero 1 nas diagonais para não alterar as cores da imagem
                // A última linha da matriz, corresponde ao brilho, que é dividido por 255/f, sendo valores entre 0 e 1, depois continua com 0 e 1 na diagonal
                float[][] colorMatrixElements = {
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {brightnessValue/255f, brightnessValue/255f, brightnessValue/255f, 0, 1}
                };

                // Cria um objeto ColorMatrix, para a criação do próximo objeto ImageAttributes
                ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

                // ImageAttributes serve para aplicar a transformação das cores na imagem original
                ImageAttributes atributosImagem = new ImageAttributes();
                atributosImagem.SetColorMatrix(colorMatrix);

                Bitmap imagemResultado = new Bitmap(image3.Width, image3.Height);

                // Usa-se o graphics para desenhar a nova imagem, com a altura e a largura da imagem3
                Graphics graphics = Graphics.FromImage(imagemResultado);
                graphics.DrawImage(image3, new Rectangle(0, 0, image3.Width, image3.Height), 0, 0, image3.Width, image3.Height, GraphicsUnit.Pixel, atributosImagem);

                imgFinal.Image = imagemResultado;
                lbBrilho.Text = Convert.ToString(tbBrilho.Value);
            }
            
            if (rbA.Checked) { 

            int brightnessValue = tbBrilho.Value - 50;

            Image image1 = imgA.Image;


                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                float[][] colorMatrixElements = {
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {brightnessValue/255f, brightnessValue/255f, brightnessValue/255f, 0, 1}
    };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            ImageAttributes atributosImagem = new ImageAttributes();
            atributosImagem.SetColorMatrix(colorMatrix);

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);
            Graphics graphics = Graphics.FromImage(imagemResultado);
            graphics.DrawImage(image1, new Rectangle(0, 0, image1.Width, image1.Height), 0, 0, image1.Width, image1.Height, GraphicsUnit.Pixel, atributosImagem);

            imgFinal.Image = imagemResultado;
            lbBrilho.Text = Convert.ToString(tbBrilho.Value);

            }

            if (rbB.Checked)
            {
                int brightnessValue = tbBrilho.Value - 50;

                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem B");
                    return;

                }
                float[][] colorMatrixElements = {
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {brightnessValue/255f, brightnessValue/255f, brightnessValue/255f, 0, 1}
    };
                ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

                ImageAttributes atributosImagem = new ImageAttributes();
                atributosImagem.SetColorMatrix(colorMatrix);

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);
                Graphics graphics = Graphics.FromImage(imagemResultado);
                graphics.DrawImage(image1, new Rectangle(0, 0, image1.Width, image1.Height), 0, 0, image1.Width, image1.Height, GraphicsUnit.Pixel, atributosImagem);
                image1 = imagemResultado;

                imgFinal.Image = imagemResultado;
                lbBrilho.Text = Convert.ToString(tbBrilho.Value);

            }
        }

        // Função Histograma
        private void btHistograma_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked) { 

            // Carrega a imagem em escala de cinza
            Image image1 = imgA.Image;

            if (image1 == null)
            {
                MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                return;

            }

            Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    int r = color1.R;
                    int g = color1.G;
                    int b = color1.B;
                    int gray = (r + g + b) / 3;

                    Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                    imagemCinza.SetPixel(x, y, novaCor);

                }
            }
            
            // Cria um array de 256 inteiros, correspondendo os valores da escala de cinza
            int[] histograma = new int[256];
            for (int i = 0; i < imagemCinza.Width; i++)
            {
                for (int j = 0; j < imagemCinza.Height; j++)
                {
                    // Calcula o peso da escala de cinza
                    Color c = imagemCinza.GetPixel(i, j);
                    int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                    histograma[gray]++;
                }
            }

            // Calcula a função de distribuição acumulada (CDF) do histograma
            int[] cdf = new int[256];
            int sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum += histograma[i];
                cdf[i] = sum;
            }

            // Equaliza o histograma
            int pixels = imagemCinza.Width * imagemCinza.Height;
            for (int i = 0; i < 256; i++)
            {
                cdf[i] = (int)(255 * ((float)cdf[i] / pixels));
            }

            // Cria uma nova imagem equalizada
            Bitmap imagemEqualizada = new Bitmap(imagemCinza.Width, imagemCinza.Height);
            for (int i = 0; i < imagemCinza.Width; i++)
            {
                for (int j = 0; j < imagemCinza.Height; j++)
                {
                    Color c = imagemCinza.GetPixel(i, j);
                    int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                    int eqGray = cdf[gray];
                    Color eqColor = Color.FromArgb(eqGray, eqGray, eqGray);
                    imagemEqualizada.SetPixel(i, j, eqColor);
                }
            }

                imgFinal.Image = imagemEqualizada;

                // Coloca o histograma final em um vetor de 256 valores, correspondendo os valores da escala de cinza
                int[] histogramaFinal = new int[256];
                for (int i = 0; i < imagemEqualizada.Width; i++)
                {
                    for (int j = 0; j < imagemEqualizada.Height; j++)
                    {
                        Color c = imagemEqualizada.GetPixel(i, j);
                        int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                        histogramaFinal[gray]++;
                    }
                }

                // Adiciona o gráfico do primeiro histograma
                chart1.Series.Clear();
                chart1.Series.Add("Imagem Inicial");
                chart1.Series["Imagem Inicial"].ChartType = SeriesChartType.Column;
                chart1.Series["Imagem Inicial"].Points.DataBindY(histograma);
                chart1.ChartAreas[0].AxisY.Maximum = histograma.Max() + 10;

                // Adiciona o gráfico do segundo histograma
                chart2.Series.Clear();
                chart2.Series.Add("Imagem Final");
                chart2.Series["Imagem Final"].ChartType = SeriesChartType.Column;
                chart2.Series["Imagem Final"].Points.DataBindY(histogramaFinal);
                chart2.ChartAreas[0].AxisY.Maximum = histogramaFinal.Max() + 10;

            }

            if (rbB.Checked)
            {
                // Carrega a imagem em escala de cinza
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem B");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int[] histograma = new int[256];
                for (int i = 0; i < imagemCinza.Width; i++)
                {
                    for (int j = 0; j < imagemCinza.Height; j++)
                    {
                        Color c = imagemCinza.GetPixel(i, j);
                        int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                        histograma[gray]++;
                    }
                }

                // Calcula a função de distribuição acumulada (CDF) do histograma
                int[] cdf = new int[256];
                int sum = 0;
                for (int i = 0; i < 256; i++)
                {
                    sum += histograma[i];
                    cdf[i] = sum;
                }

                // Equaliza o histograma
                int pixels = imagemCinza.Width * imagemCinza.Height;
                for (int i = 0; i < 256; i++)
                {
                    cdf[i] = (int)(255 * ((float)cdf[i] / pixels));
                }

                // Cria uma nova imagem equalizada
                Bitmap imagemEqualizada = new Bitmap(imagemCinza.Width, imagemCinza.Height);
                for (int i = 0; i < imagemCinza.Width; i++)
                {
                    for (int j = 0; j < imagemCinza.Height; j++)
                    {
                        Color c = imagemCinza.GetPixel(i, j);
                        int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                        int eqGray = cdf[gray];
                        Color eqColor = Color.FromArgb(eqGray, eqGray, eqGray);
                        imagemEqualizada.SetPixel(i, j, eqColor);
                    }
                }

                imgFinal.Image = imagemEqualizada;

                int[] histogramaFinal = new int[256];
                for (int i = 0; i < imagemEqualizada.Width; i++)
                {
                    for (int j = 0; j < imagemEqualizada.Height; j++)
                    {
                        Color c = imagemEqualizada.GetPixel(i, j);
                        int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                        histogramaFinal[gray]++;
                    }
                }

                chart1.Series.Clear();
                chart1.Series.Add("Imagem Inicial");
                chart1.Series["Imagem Inicial"].ChartType = SeriesChartType.Column;
                chart1.Series["Imagem Inicial"].Points.DataBindY(histograma);
                chart1.ChartAreas[0].AxisY.Maximum = histograma.Max() + 10;

                chart2.Series.Clear();
                chart2.Series.Add("Imagem Final");
                chart2.Series["Imagem Final"].ChartType = SeriesChartType.Column;
                chart2.Series["Imagem Final"].Points.DataBindY(histogramaFinal);
                chart2.ChartAreas[0].AxisY.Maximum = histogramaFinal.Max() + 10;
            }
        }

        // Função Vizinhança Max
        private void btMax_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                // Covnerte a imagem original em escala de cinza
                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                // Seleciona o tamanho da vizinhança
                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                }

                // Filtra a imagem
                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        // Cria um array dos pixeis percoridos e pega o tamanho da vizinhança
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        // Percorre esse array
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                // Trata os casos em que xIndex e yIndex estão fora dos limites da imagem
                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }
                        // Usa a função GetMaximo, declarada no inicio do programa para pegar os valores máximos da vizinhança
                        int maximo = GetMaximo(vizinhanca);

                        // Coloca esses valores conforme é cada vizinhança
                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(maximo, maximo, maximo));
                    }
                }

                // Exibe a imagem filtrada
                imgFinal.Image = imagemFiltrada;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem B");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                // Seleciona o tamanho da vizinhança
                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                }

                // Filtra a imagem
                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                // Trata os casos em que xIndex e yIndex estão fora dos limites da imagem
                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int maximo = GetMaximo(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(maximo, maximo, maximo));
                    }
                }

                // Exibe a imagem filtrada
                imgFinal.Image = imagemFiltrada;
            }
        }

        // Função Vizinhança Min
        // Mesmo esquema da função MAX, mudando apenas a última função declarada no início do código, onde pega o minimo da vizinhança
        private void btMin_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                }
                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int minimo = GetMinimo(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(minimo, minimo, minimo));
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }

                if (rbB.Checked)
                {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem B");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                }

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int minimo = GetMinimo(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(minimo, minimo, minimo));
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }
        }

        // Função Vizinhança Med
        // Mesmo esquema da função MAX, mudando apenas a última função declarada no início do código, onde pega a média da vizinhança
        private void btMed_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                }

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int media = GetMedia(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(media, media, media));
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                }

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int media = GetMedia(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(media, media, media));
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }
        }

        private void btMediana_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                }

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int media = GetMediana(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(media, media, media));
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                }

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int media = GetMediana(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(media, media, media));
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }
        }

        private void btSuave_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                }

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int suave = GetSuave(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(suave, suave, suave));
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                }

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int suave = GetSuave(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(suave, suave, suave));
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }
        }

        private void btOrdem_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                    nupOrdem.Maximum = 8;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                    nupOrdem.Maximum = 17;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                    nupOrdem.Maximum = 35;
                }

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int ordem = GetOrdem(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(ordem, ordem, ordem));
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 0;
                if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
                {
                    MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                    return;
                }
                if (rb3x3.Checked)
                {
                    tamanhoVizinhanca = 3;
                    nupOrdem.Maximum = 8;
                }
                if (rb5x5.Checked)
                {
                    tamanhoVizinhanca = 5;
                    nupOrdem.Maximum = 17;
                }
                if (rb7x7.Checked)
                {
                    tamanhoVizinhanca = 7;
                    nupOrdem.Maximum = 35;
                }

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        int ordem = GetOrdem(vizinhanca);

                        imagemFiltrada.SetPixel(x, y, Color.FromArgb(ordem, ordem, ordem));
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }
        }

        private void btGaussian_Click(object sender, EventArgs e)
        {
            if (!rbA.Checked && !rbB.Checked && !rbDuas.Checked)
            {
                MessageBox.Show("Por favor, selecione uma opção para processar uma imagem");
                return;
            }

            if (rbDuas.Checked)
            {
                MessageBox.Show("O processamento é feito somente com a imagem A ou B");
                return;
            }

            if (rbA.Checked)
            {
                Image image1 = imgA.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem A");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 5;
                double sigma = (double)nupGaussiana.Value;

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        double gaussian = GetGaussian(vizinhanca, sigma);

                        int pixelNovo = (int)Math.Round(gaussian);
                        if (pixelNovo < 0) pixelNovo = 0;
                        else if (pixelNovo > 255) pixelNovo = 255;
                        Color imagemNova = Color.FromArgb(pixelNovo, pixelNovo, pixelNovo);
                        imagemFiltrada.SetPixel(x, y, imagemNova);
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }

            if (rbB.Checked)
            {
                Image image1 = imgB.Image;

                if (image1 == null)
                {
                    MessageBox.Show("Por favor, selecione uma imagem no campo Imagem B");
                    return;

                }

                Bitmap imagemCinza = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = ((Bitmap)image1).GetPixel(x, y);
                        int r = color1.R;
                        int g = color1.G;
                        int b = color1.B;
                        int gray = (r + g + b) / 3;

                        Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                        imagemCinza.SetPixel(x, y, novaCor);

                    }
                }

                int tamanhoVizinhanca = 5;
                double sigma = (double)nupGaussiana.Value;

                Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

                for (int x = 0; x < imagemCinza.Width; x++)
                {
                    for (int y = 0; y < imagemCinza.Height; y++)
                    {
                        int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];
                        for (int i = 0; i < tamanhoVizinhanca; i++)
                        {
                            for (int j = 0; j < tamanhoVizinhanca; j++)
                            {
                                int xIndex = x + i - tamanhoVizinhanca / 2;
                                int yIndex = y + j - tamanhoVizinhanca / 2;

                                if (xIndex < 0)
                                {
                                    xIndex = 0;
                                }
                                if (xIndex >= imagemCinza.Width)
                                {
                                    xIndex = imagemCinza.Width - 1;
                                }
                                if (yIndex < 0)
                                {
                                    yIndex = 0;
                                }
                                if (yIndex >= imagemCinza.Height)
                                {
                                    yIndex = imagemCinza.Height - 1;
                                }

                                vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                            }
                        }

                        double gaussian = GetGaussian(vizinhanca, sigma);

                        int pixelNovo = (int)Math.Round(gaussian);
                        if (pixelNovo < 0) pixelNovo = 0;
                        else if (pixelNovo > 255) pixelNovo = 255;
                        Color imagemNova = Color.FromArgb(pixelNovo, pixelNovo, pixelNovo);
                        imagemFiltrada.SetPixel(x, y, imagemNova);
                    }
                }

                imgFinal.Image = imagemFiltrada;
            }
        }
    }

       
}

   

