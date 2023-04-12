
namespace processamento_de_imagens
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgA = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btCarregaImgA = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btCarregaImgB = new System.Windows.Forms.Button();
            this.imgB = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btBlending = new System.Windows.Forms.Button();
            this.btMedia = new System.Windows.Forms.Button();
            this.btDivisao = new System.Windows.Forms.Button();
            this.btMultiplicacao = new System.Windows.Forms.Button();
            this.btSubtracao = new System.Windows.Forms.Button();
            this.btAdicao = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btSalvaImg = new System.Windows.Forms.Button();
            this.imgFinal = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btNot = new System.Windows.Forms.Button();
            this.btXor = new System.Windows.Forms.Button();
            this.btOr = new System.Windows.Forms.Button();
            this.btAnd = new System.Windows.Forms.Button();
            this.nupDivisao = new System.Windows.Forms.NumericUpDown();
            this.nupMultiplicacao = new System.Windows.Forms.NumericUpDown();
            this.nupBlending = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.imgA)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgB)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgFinal)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupDivisao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMultiplicacao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupBlending)).BeginInit();
            this.SuspendLayout();
            // 
            // imgA
            // 
            this.imgA.Location = new System.Drawing.Point(16, 14);
            this.imgA.Name = "imgA";
            this.imgA.Size = new System.Drawing.Size(345, 322);
            this.imgA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgA.TabIndex = 0;
            this.imgA.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btCarregaImgA);
            this.groupBox1.Controls.Add(this.imgA);
            this.groupBox1.Location = new System.Drawing.Point(35, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 482);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Imagem A";
            // 
            // btCarregaImgA
            // 
            this.btCarregaImgA.Location = new System.Drawing.Point(16, 393);
            this.btCarregaImgA.Name = "btCarregaImgA";
            this.btCarregaImgA.Size = new System.Drawing.Size(344, 46);
            this.btCarregaImgA.TabIndex = 1;
            this.btCarregaImgA.Text = "Carregar imagem A";
            this.btCarregaImgA.UseVisualStyleBackColor = true;
            this.btCarregaImgA.Click += new System.EventHandler(this.btCarregaImgA_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btCarregaImgB);
            this.groupBox2.Controls.Add(this.imgB);
            this.groupBox2.Location = new System.Drawing.Point(462, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 482);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Imagem A";
            // 
            // btCarregaImgB
            // 
            this.btCarregaImgB.Location = new System.Drawing.Point(22, 393);
            this.btCarregaImgB.Name = "btCarregaImgB";
            this.btCarregaImgB.Size = new System.Drawing.Size(345, 46);
            this.btCarregaImgB.TabIndex = 2;
            this.btCarregaImgB.Text = "Carregar imagem B";
            this.btCarregaImgB.UseVisualStyleBackColor = true;
            this.btCarregaImgB.Click += new System.EventHandler(this.btCarregaImgB_Click);
            // 
            // imgB
            // 
            this.imgB.Location = new System.Drawing.Point(22, 14);
            this.imgB.Name = "imgB";
            this.imgB.Size = new System.Drawing.Size(345, 322);
            this.imgB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgB.TabIndex = 0;
            this.imgB.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nupBlending);
            this.groupBox3.Controls.Add(this.nupMultiplicacao);
            this.groupBox3.Controls.Add(this.nupDivisao);
            this.groupBox3.Controls.Add(this.btBlending);
            this.groupBox3.Controls.Add(this.btMedia);
            this.groupBox3.Controls.Add(this.btDivisao);
            this.groupBox3.Controls.Add(this.btMultiplicacao);
            this.groupBox3.Controls.Add(this.btSubtracao);
            this.groupBox3.Controls.Add(this.btAdicao);
            this.groupBox3.Location = new System.Drawing.Point(872, 58);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 241);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Operações aritméticas";
            // 
            // btBlending
            // 
            this.btBlending.Location = new System.Drawing.Point(22, 179);
            this.btBlending.Name = "btBlending";
            this.btBlending.Size = new System.Drawing.Size(97, 26);
            this.btBlending.TabIndex = 5;
            this.btBlending.Text = "Blending";
            this.btBlending.UseVisualStyleBackColor = true;
            this.btBlending.Click += new System.EventHandler(this.btBlending_Click);
            // 
            // btMedia
            // 
            this.btMedia.Location = new System.Drawing.Point(22, 147);
            this.btMedia.Name = "btMedia";
            this.btMedia.Size = new System.Drawing.Size(97, 26);
            this.btMedia.TabIndex = 4;
            this.btMedia.Text = "Média";
            this.btMedia.UseVisualStyleBackColor = true;
            this.btMedia.Click += new System.EventHandler(this.btMedia_Click);
            // 
            // btDivisao
            // 
            this.btDivisao.Location = new System.Drawing.Point(22, 115);
            this.btDivisao.Name = "btDivisao";
            this.btDivisao.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btDivisao.Size = new System.Drawing.Size(97, 26);
            this.btDivisao.TabIndex = 3;
            this.btDivisao.Text = "Divisão";
            this.btDivisao.UseVisualStyleBackColor = true;
            this.btDivisao.Click += new System.EventHandler(this.btDivisao_Click);
            // 
            // btMultiplicacao
            // 
            this.btMultiplicacao.Location = new System.Drawing.Point(22, 83);
            this.btMultiplicacao.Name = "btMultiplicacao";
            this.btMultiplicacao.Size = new System.Drawing.Size(97, 26);
            this.btMultiplicacao.TabIndex = 2;
            this.btMultiplicacao.Text = "Multiplicação";
            this.btMultiplicacao.UseVisualStyleBackColor = true;
            this.btMultiplicacao.Click += new System.EventHandler(this.btMultiplicacao_Click);
            // 
            // btSubtracao
            // 
            this.btSubtracao.Location = new System.Drawing.Point(22, 51);
            this.btSubtracao.Name = "btSubtracao";
            this.btSubtracao.Size = new System.Drawing.Size(97, 26);
            this.btSubtracao.TabIndex = 1;
            this.btSubtracao.Text = "Subtração";
            this.btSubtracao.UseVisualStyleBackColor = true;
            this.btSubtracao.Click += new System.EventHandler(this.btSubtracao_Click);
            // 
            // btAdicao
            // 
            this.btAdicao.Location = new System.Drawing.Point(22, 19);
            this.btAdicao.Name = "btAdicao";
            this.btAdicao.Size = new System.Drawing.Size(97, 26);
            this.btAdicao.TabIndex = 0;
            this.btAdicao.Text = "Adição";
            this.btAdicao.UseVisualStyleBackColor = true;
            this.btAdicao.Click += new System.EventHandler(this.btAdicao_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btSalvaImg);
            this.groupBox4.Controls.Add(this.imgFinal);
            this.groupBox4.Location = new System.Drawing.Point(1133, 58);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(385, 482);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Resultado";
            // 
            // btSalvaImg
            // 
            this.btSalvaImg.Location = new System.Drawing.Point(22, 393);
            this.btSalvaImg.Name = "btSalvaImg";
            this.btSalvaImg.Size = new System.Drawing.Size(345, 46);
            this.btSalvaImg.TabIndex = 2;
            this.btSalvaImg.Text = "Salvar imagem";
            this.btSalvaImg.UseVisualStyleBackColor = true;
            this.btSalvaImg.Click += new System.EventHandler(this.btSalvaImg_Click);
            // 
            // imgFinal
            // 
            this.imgFinal.Location = new System.Drawing.Point(22, 14);
            this.imgFinal.Name = "imgFinal";
            this.imgFinal.Size = new System.Drawing.Size(345, 322);
            this.imgFinal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgFinal.TabIndex = 0;
            this.imgFinal.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btNot);
            this.groupBox5.Controls.Add(this.btXor);
            this.groupBox5.Controls.Add(this.btOr);
            this.groupBox5.Controls.Add(this.btAnd);
            this.groupBox5.Location = new System.Drawing.Point(872, 305);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(220, 235);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Operações lógicas";
            // 
            // btNot
            // 
            this.btNot.Location = new System.Drawing.Point(22, 163);
            this.btNot.Name = "btNot";
            this.btNot.Size = new System.Drawing.Size(179, 42);
            this.btNot.TabIndex = 11;
            this.btNot.Text = "NOT";
            this.btNot.UseVisualStyleBackColor = true;
            this.btNot.Click += new System.EventHandler(this.btNot_Click);
            // 
            // btXor
            // 
            this.btXor.Location = new System.Drawing.Point(22, 115);
            this.btXor.Name = "btXor";
            this.btXor.Size = new System.Drawing.Size(179, 42);
            this.btXor.TabIndex = 10;
            this.btXor.Text = "XOR";
            this.btXor.UseVisualStyleBackColor = true;
            this.btXor.Click += new System.EventHandler(this.btXor_Click);
            // 
            // btOr
            // 
            this.btOr.Location = new System.Drawing.Point(22, 67);
            this.btOr.Name = "btOr";
            this.btOr.Size = new System.Drawing.Size(179, 42);
            this.btOr.TabIndex = 9;
            this.btOr.Text = "OR";
            this.btOr.UseVisualStyleBackColor = true;
            this.btOr.Click += new System.EventHandler(this.btOr_Click);
            // 
            // btAnd
            // 
            this.btAnd.Location = new System.Drawing.Point(22, 19);
            this.btAnd.Name = "btAnd";
            this.btAnd.Size = new System.Drawing.Size(179, 42);
            this.btAnd.TabIndex = 8;
            this.btAnd.Text = "AND";
            this.btAnd.UseVisualStyleBackColor = true;
            this.btAnd.Click += new System.EventHandler(this.btAnd_Click);
            // 
            // nupDivisao
            // 
            this.nupDivisao.DecimalPlaces = 2;
            this.nupDivisao.Location = new System.Drawing.Point(125, 120);
            this.nupDivisao.Name = "nupDivisao";
            this.nupDivisao.Size = new System.Drawing.Size(75, 20);
            this.nupDivisao.TabIndex = 8;
            // 
            // nupMultiplicacao
            // 
            this.nupMultiplicacao.DecimalPlaces = 2;
            this.nupMultiplicacao.Location = new System.Drawing.Point(125, 88);
            this.nupMultiplicacao.Name = "nupMultiplicacao";
            this.nupMultiplicacao.Size = new System.Drawing.Size(75, 20);
            this.nupMultiplicacao.TabIndex = 9;
            // 
            // nupBlending
            // 
            this.nupBlending.DecimalPlaces = 2;
            this.nupBlending.Location = new System.Drawing.Point(125, 184);
            this.nupBlending.Name = "nupBlending";
            this.nupBlending.Size = new System.Drawing.Size(75, 20);
            this.nupBlending.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 644);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Processar imagens";
            ((System.ComponentModel.ISupportInitialize)(this.imgA)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgB)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgFinal)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nupDivisao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMultiplicacao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupBlending)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btCarregaImgA;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btCarregaImgB;
        private System.Windows.Forms.PictureBox imgB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btBlending;
        private System.Windows.Forms.Button btMedia;
        private System.Windows.Forms.Button btDivisao;
        private System.Windows.Forms.Button btMultiplicacao;
        private System.Windows.Forms.Button btSubtracao;
        private System.Windows.Forms.Button btAdicao;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btSalvaImg;
        private System.Windows.Forms.PictureBox imgFinal;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btNot;
        private System.Windows.Forms.Button btXor;
        private System.Windows.Forms.Button btOr;
        private System.Windows.Forms.Button btAnd;
        private System.Windows.Forms.NumericUpDown nupDivisao;
        private System.Windows.Forms.NumericUpDown nupBlending;
        private System.Windows.Forms.NumericUpDown nupMultiplicacao;
    }
}

