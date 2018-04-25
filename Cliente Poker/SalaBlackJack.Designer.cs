namespace Cliente_Poker
{
    partial class SalaBlackJack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalaBlackJack));
            this.btnSalirSala = new System.Windows.Forms.Button();
            this.btnFichaUno = new System.Windows.Forms.Button();
            this.btnFichaDos = new System.Windows.Forms.Button();
            this.btnFichaTres = new System.Windows.Forms.Button();
            this.btnPedir = new System.Windows.Forms.Button();
            this.cartaJugador1 = new System.Windows.Forms.PictureBox();
            this.cartaCrupier1 = new System.Windows.Forms.PictureBox();
            this.btnPlantarse = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.btnRetirarse = new System.Windows.Forms.Button();
            this.btnDoblar = new System.Windows.Forms.Button();
            this.lblValorCrupier = new System.Windows.Forms.Label();
            this.lblValorJugador = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cartaJugador1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartaCrupier1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirSala
            // 
            this.btnSalirSala.Location = new System.Drawing.Point(713, 415);
            this.btnSalirSala.Name = "btnSalirSala";
            this.btnSalirSala.Size = new System.Drawing.Size(75, 23);
            this.btnSalirSala.TabIndex = 0;
            this.btnSalirSala.Text = "Salir de sala";
            this.btnSalirSala.UseVisualStyleBackColor = true;
            this.btnSalirSala.Click += new System.EventHandler(this.btnSalirSala_Click);
            // 
            // btnFichaUno
            // 
            this.btnFichaUno.AutoSize = true;
            this.btnFichaUno.Image = global::Cliente_Poker.Properties.Resources.chipGreenWhite_sideBorder;
            this.btnFichaUno.Location = new System.Drawing.Point(213, 366);
            this.btnFichaUno.Name = "btnFichaUno";
            this.btnFichaUno.Size = new System.Drawing.Size(75, 53);
            this.btnFichaUno.TabIndex = 1;
            this.btnFichaUno.Tag = "25";
            this.btnFichaUno.Text = "25";
            this.btnFichaUno.UseVisualStyleBackColor = true;
            this.btnFichaUno.Click += new System.EventHandler(this.ClickListener);
            // 
            // btnFichaDos
            // 
            this.btnFichaDos.AutoSize = true;
            this.btnFichaDos.Image = global::Cliente_Poker.Properties.Resources.chipBlueWhite_sideBorder;
            this.btnFichaDos.Location = new System.Drawing.Point(332, 368);
            this.btnFichaDos.Name = "btnFichaDos";
            this.btnFichaDos.Size = new System.Drawing.Size(75, 48);
            this.btnFichaDos.TabIndex = 2;
            this.btnFichaDos.Tag = "50";
            this.btnFichaDos.Text = "50";
            this.btnFichaDos.UseVisualStyleBackColor = true;
            this.btnFichaDos.Click += new System.EventHandler(this.ClickListener);
            // 
            // btnFichaTres
            // 
            this.btnFichaTres.AutoSize = true;
            this.btnFichaTres.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFichaTres.Image = global::Cliente_Poker.Properties.Resources.chipBlackWhite_sideBorder;
            this.btnFichaTres.Location = new System.Drawing.Point(450, 368);
            this.btnFichaTres.Name = "btnFichaTres";
            this.btnFichaTres.Size = new System.Drawing.Size(75, 48);
            this.btnFichaTres.TabIndex = 3;
            this.btnFichaTres.Tag = "100";
            this.btnFichaTres.Text = "100";
            this.btnFichaTres.UseVisualStyleBackColor = true;
            this.btnFichaTres.Click += new System.EventHandler(this.ClickListener);
            // 
            // btnPedir
            // 
            this.btnPedir.Location = new System.Drawing.Point(213, 344);
            this.btnPedir.Name = "btnPedir";
            this.btnPedir.Size = new System.Drawing.Size(75, 23);
            this.btnPedir.TabIndex = 4;
            this.btnPedir.Tag = "Pedir";
            this.btnPedir.Text = "Pedir";
            this.btnPedir.UseVisualStyleBackColor = true;
            this.btnPedir.Visible = false;
            this.btnPedir.Click += new System.EventHandler(this.ClickListener);
            // 
            // cartaJugador1
            // 
            this.cartaJugador1.Location = new System.Drawing.Point(238, 211);
            this.cartaJugador1.Name = "cartaJugador1";
            this.cartaJugador1.Size = new System.Drawing.Size(50, 70);
            this.cartaJugador1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cartaJugador1.TabIndex = 5;
            this.cartaJugador1.TabStop = false;
            // 
            // cartaCrupier1
            // 
            this.cartaCrupier1.Location = new System.Drawing.Point(238, 74);
            this.cartaCrupier1.Name = "cartaCrupier1";
            this.cartaCrupier1.Size = new System.Drawing.Size(50, 70);
            this.cartaCrupier1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cartaCrupier1.TabIndex = 7;
            this.cartaCrupier1.TabStop = false;
            // 
            // btnPlantarse
            // 
            this.btnPlantarse.Location = new System.Drawing.Point(332, 344);
            this.btnPlantarse.Name = "btnPlantarse";
            this.btnPlantarse.Size = new System.Drawing.Size(75, 23);
            this.btnPlantarse.TabIndex = 8;
            this.btnPlantarse.Tag = "Plantarse";
            this.btnPlantarse.Text = "Plantarse";
            this.btnPlantarse.UseVisualStyleBackColor = true;
            this.btnPlantarse.Visible = false;
            this.btnPlantarse.Click += new System.EventHandler(this.ClickListener);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(30, 177);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(55, 13);
            this.lbl.TabIndex = 9;
            this.lbl.Text = "Resultado";
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(33, 211);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(0, 13);
            this.lblResultado.TabIndex = 10;
            // 
            // btnRetirarse
            // 
            this.btnRetirarse.Location = new System.Drawing.Point(450, 344);
            this.btnRetirarse.Name = "btnRetirarse";
            this.btnRetirarse.Size = new System.Drawing.Size(75, 23);
            this.btnRetirarse.TabIndex = 11;
            this.btnRetirarse.Text = "Retirarse";
            this.btnRetirarse.UseVisualStyleBackColor = true;
            this.btnRetirarse.Visible = false;
            this.btnRetirarse.Click += new System.EventHandler(this.ClickListener);
            // 
            // btnDoblar
            // 
            this.btnDoblar.Location = new System.Drawing.Point(332, 306);
            this.btnDoblar.Name = "btnDoblar";
            this.btnDoblar.Size = new System.Drawing.Size(75, 23);
            this.btnDoblar.TabIndex = 12;
            this.btnDoblar.Text = "Doblar";
            this.btnDoblar.UseVisualStyleBackColor = true;
            this.btnDoblar.Visible = false;
            this.btnDoblar.Click += new System.EventHandler(this.ClickListener);
            // 
            // lblValorCrupier
            // 
            this.lblValorCrupier.AutoSize = true;
            this.lblValorCrupier.Location = new System.Drawing.Point(170, 74);
            this.lblValorCrupier.Name = "lblValorCrupier";
            this.lblValorCrupier.Size = new System.Drawing.Size(0, 13);
            this.lblValorCrupier.TabIndex = 13;
            // 
            // lblValorJugador
            // 
            this.lblValorJugador.AutoSize = true;
            this.lblValorJugador.Location = new System.Drawing.Point(173, 211);
            this.lblValorJugador.Name = "lblValorJugador";
            this.lblValorJugador.Size = new System.Drawing.Size(0, 13);
            this.lblValorJugador.TabIndex = 14;
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Location = new System.Drawing.Point(55, 388);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(0, 13);
            this.lblSaldo.TabIndex = 15;
            // 
            // SalaBlackJack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.lblValorJugador);
            this.Controls.Add(this.lblValorCrupier);
            this.Controls.Add(this.btnDoblar);
            this.Controls.Add(this.btnRetirarse);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.btnPlantarse);
            this.Controls.Add(this.cartaCrupier1);
            this.Controls.Add(this.cartaJugador1);
            this.Controls.Add(this.btnPedir);
            this.Controls.Add(this.btnFichaTres);
            this.Controls.Add(this.btnFichaDos);
            this.Controls.Add(this.btnFichaUno);
            this.Controls.Add(this.btnSalirSala);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SalaBlackJack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SalaBlackJack";
            ((System.ComponentModel.ISupportInitialize)(this.cartaJugador1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartaCrupier1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirSala;
        private System.Windows.Forms.Button btnFichaUno;
        private System.Windows.Forms.Button btnFichaDos;
        private System.Windows.Forms.Button btnFichaTres;
        private System.Windows.Forms.Button btnPedir;
        private System.Windows.Forms.PictureBox cartaJugador1;
        private System.Windows.Forms.PictureBox cartaCrupier1;
        private System.Windows.Forms.Button btnPlantarse;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Button btnRetirarse;
        private System.Windows.Forms.Button btnDoblar;
        private System.Windows.Forms.Label lblValorCrupier;
        private System.Windows.Forms.Label lblValorJugador;
        private System.Windows.Forms.Label lblSaldo;
    }
}