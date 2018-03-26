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
            // SalaBlackJack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSalirSala);
            this.Name = "SalaBlackJack";
            this.Text = "SalaBlackJack";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSalirSala;
    }
}