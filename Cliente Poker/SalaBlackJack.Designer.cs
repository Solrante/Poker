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
            this.btnSalirSala = new System.Windows.Forms.Button();
            this.btnFicha25 = new System.Windows.Forms.Button();
            this.btnFicha50 = new System.Windows.Forms.Button();
            this.btnFicha100 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            this.btnFicha25.Text = "25";
            this.btnFicha25.UseVisualStyleBackColor = true;
            this.btnFicha25.Click += new System.EventHandler(this.btnFicha25_Click);
            // 
            // btnFicha50
            // 
            this.btnFicha50.AutoSize = true;
            this.btnFicha50.Image = global::Cliente_Poker.Properties.Resources.chipBlueWhite_sideBorder;
            this.btnFicha50.Location = new System.Drawing.Point(332, 368);
            this.btnFicha50.Name = "btnFicha50";
            this.btnFicha50.Size = new System.Drawing.Size(75, 48);
            this.btnFicha50.TabIndex = 2;
            this.btnFicha50.Text = "50";
            this.btnFicha50.UseVisualStyleBackColor = true;
            this.btnFicha50.Click += new System.EventHandler(this.btnFicha50_Click);
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
            this.btnFicha100.Text = "100";
            this.btnFicha100.UseVisualStyleBackColor = true;
            this.btnFicha100.Click += new System.EventHandler(this.btnFicha100_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(81, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SalaBlackJack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnFicha100);
            this.Controls.Add(this.btnFicha50);
            this.Controls.Add(this.btnFicha25);
            this.Controls.Add(this.btnSalirSala);
            this.Name = "SalaBlackJack";
            this.Text = "SalaBlackJack";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirSala;
        private System.Windows.Forms.Button btnFicha25;
        private System.Windows.Forms.Button btnFicha50;
        private System.Windows.Forms.Button btnFicha100;
        private System.Windows.Forms.Button button1;
    }
}