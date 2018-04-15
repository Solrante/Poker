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
            this.btnFicha25 = new System.Windows.Forms.Button();
            this.btnFicha50 = new System.Windows.Forms.Button();
            this.btnFicha100 = new System.Windows.Forms.Button();
            this.btnPedir = new System.Windows.Forms.Button();
            this.cartaJugador1 = new System.Windows.Forms.PictureBox();
            this.cartaCrupier1 = new System.Windows.Forms.PictureBox();
            this.btnPlantarse = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
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
            // btnFicha25
            // 
            this.btnFicha25.AutoSize = true;
            this.btnFicha25.Image = global::Cliente_Poker.Properties.Resources.chipGreenWhite_sideBorder;
            this.btnFicha25.Location = new System.Drawing.Point(213, 366);
            this.btnFicha25.Name = "btnFicha25";
            this.btnFicha25.Size = new System.Drawing.Size(75, 53);
            this.btnFicha25.TabIndex = 1;
            this.btnFicha25.Tag = "Ficha25";
            this.btnFicha25.Text = "25";
            this.btnFicha25.UseVisualStyleBackColor = true;
            this.btnFicha25.Click += new System.EventHandler(this.ClickListener);
            // 
            // btnFicha50
            // 
            this.btnFicha50.AutoSize = true;
            this.btnFicha50.Image = global::Cliente_Poker.Properties.Resources.chipBlueWhite_sideBorder;
            this.btnFicha50.Location = new System.Drawing.Point(332, 368);
            this.btnFicha50.Name = "btnFicha50";
            this.btnFicha50.Size = new System.Drawing.Size(75, 48);
            this.btnFicha50.TabIndex = 2;
            this.btnFicha50.Tag = "Ficha50";
            this.btnFicha50.Text = "50";
            this.btnFicha50.UseVisualStyleBackColor = true;
            this.btnFicha50.Click += new System.EventHandler(this.ClickListener);
            // 
            // btnFicha100
            // 
            this.btnFicha100.AutoSize = true;
            this.btnFicha100.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFicha100.Image = global::Cliente_Poker.Properties.Resources.chipBlackWhite_sideBorder;
            this.btnFicha100.Location = new System.Drawing.Point(450, 368);
            this.btnFicha100.Name = "btnFicha100";
            this.btnFicha100.Size = new System.Drawing.Size(75, 48);
            this.btnFicha100.TabIndex = 3;
            this.btnFicha100.Tag = "Ficha100";
            this.btnFicha100.Text = "100";
            this.btnFicha100.UseVisualStyleBackColor = true;
            this.btnFicha100.Click += new System.EventHandler(this.ClickListener);
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
            this.cartaJugador1.Location = new System.Drawing.Point(238, 263);
            this.cartaJugador1.Name = "cartaJugador1";
            this.cartaJugador1.Size = new System.Drawing.Size(50, 70);
            this.cartaJugador1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cartaJugador1.TabIndex = 5;
            this.cartaJugador1.TabStop = false;
            // 
            // cartaCrupier1
            // 
            this.cartaCrupier1.Location = new System.Drawing.Point(308, 98);
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
            // SalaBlackJack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.btnPlantarse);
            this.Controls.Add(this.cartaCrupier1);
            this.Controls.Add(this.cartaJugador1);
            this.Controls.Add(this.btnPedir);
            this.Controls.Add(this.btnFicha100);
            this.Controls.Add(this.btnFicha50);
            this.Controls.Add(this.btnFicha25);
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
        private System.Windows.Forms.Button btnFicha25;
        private System.Windows.Forms.Button btnFicha50;
        private System.Windows.Forms.Button btnFicha100;
        private System.Windows.Forms.Button btnPedir;
        private System.Windows.Forms.PictureBox cartaJugador1;
        private System.Windows.Forms.PictureBox cartaCrupier1;
        private System.Windows.Forms.Button btnPlantarse;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblResultado;
    }
}