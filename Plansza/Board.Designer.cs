namespace Chess
{
    partial class Board
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.koniec = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // koniec
            // 
            this.koniec.AutoSize = true;
            this.koniec.BackColor = System.Drawing.Color.Transparent;
            this.koniec.Font = new System.Drawing.Font("Microsoft Sans Serif", 80.25F);
            this.koniec.ForeColor = System.Drawing.Color.Red;
            this.koniec.Location = new System.Drawing.Point(143, 305);
            this.koniec.Name = "koniec";
            this.koniec.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.koniec.Size = new System.Drawing.Size(624, 120);
            this.koniec.TabIndex = 0;
            this.koniec.Text = "Checkmate!";
            this.koniec.Visible = false;
            // 
            // Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 959);
            this.Controls.Add(this.koniec);
            this.Name = "Board";
            this.Text = "Chess";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label koniec;
    }
}

