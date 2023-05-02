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
            tbBrilho.Maximum = 100;
            tbBrilho.Minimum = 0;
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

        private void btExer7_Click(object sender, EventArgs e)
        {
            var form = new Exer7();
            form.Show();
        }

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
                    int gray = (r + g + b) / 3;
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
                imgFinal.Image = imagemResultadoSoma;

                int brightnessValue = tbBrilho.Value - 50;

                Image image3 = imgFinal.Image;

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

                Bitmap imagemResultado = new Bitmap(image3.Width, image3.Height);
                Graphics graphics = Graphics.FromImage(imagemResultado);
                graphics.DrawImage(image3, new Rectangle(0, 0, image3.Width, image3.Height), 0, 0, image3.Width, image3.Height, GraphicsUnit.Pixel, atributosImagem);
                image3 = imagemResultado;

                imgFinal.Image = imagemResultado;
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
            image1 = imagemResultado;

            imgFinal.Image = imagemResultado;

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

            }
        }

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
                int[] histogramaFinal = new int[256];
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
            }
        }
    }
}
