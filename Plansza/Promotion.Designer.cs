namespace Chess
{
    partial class Promotion
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
            this.Queen = new System.Windows.Forms.PictureBox();
            this.Knight = new System.Windows.Forms.PictureBox();
            this.Rook = new System.Windows.Forms.PictureBox();
            this.Bishop = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Queen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Knight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bishop)).BeginInit();
            this.SuspendLayout();
            // 
            // Queen
            // 
            this.Queen.Location = new System.Drawing.Point(0, -1);
            this.Queen.Name = "Queen";
            this.Queen.Size = new System.Drawing.Size(135, 135);
            this.Queen.TabIndex = 0;
            this.Queen.TabStop = false;
            // 
            // Knight
            // 
            this.Knight.Location = new System.Drawing.Point(0, 140);
            this.Knight.Name = "Knight";
            this.Knight.Size = new System.Drawing.Size(135, 135);
            this.Knight.TabIndex = 1;
            this.Knight.TabStop = false;
            // 
            // Rook
            // 
            this.Rook.Location = new System.Drawing.Point(0, 281);
            this.Rook.Name = "Rook";
            this.Rook.Size = new System.Drawing.Size(135, 135);
            this.Rook.TabIndex = 2;
            this.Rook.TabStop = false;
            // 
            // Bishop
            // 
            this.Bishop.Location = new System.Drawing.Point(0, 422);
            this.Bishop.Name = "Bishop";
            this.Bishop.Size = new System.Drawing.Size(135, 135);
            this.Bishop.TabIndex = 3;
            this.Bishop.TabStop = false;
            // 
            // Promotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(135, 557);
            this.Controls.Add(this.Bishop);
            this.Controls.Add(this.Rook);
            this.Controls.Add(this.Knight);
            this.Controls.Add(this.Queen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Promotion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.Queen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Knight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bishop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Queen;
        private System.Windows.Forms.PictureBox Knight;
        private System.Windows.Forms.PictureBox Rook;
        private System.Windows.Forms.PictureBox Bishop;
    }
}