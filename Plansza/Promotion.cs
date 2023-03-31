using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Chess
{
    public partial class Promotion : Form
    {
        char color;
        int x;
        int y;
        public Promotion(char color, int x, int y)
        {
            InitializeComponent();
            Location = Cursor.Position;
            if (Top > Screen.PrimaryScreen.Bounds.Height - 600)
                Top -= 600;
            Queen.SizeMode = PictureBoxSizeMode.Zoom;
            Queen.Image = Image.FromFile("szachy/Queen" + color + ".png");
            Queen.Click += Queen_Click;
            Knight.SizeMode = PictureBoxSizeMode.Zoom;
            Knight.Image = Image.FromFile("szachy/Knight" + color + ".png");
            Knight.Click += Knight_Click;
            Rook.SizeMode = PictureBoxSizeMode.Zoom;
            Rook.Image = Image.FromFile("szachy/Rook" + color + ".png");
            Rook.Click += Rook_Click;
            Bishop.SizeMode = PictureBoxSizeMode.Zoom;
            Bishop.Image = Image.FromFile("szachy/Bishop" + color + ".png");
            Bishop.Click += Bishop_Click;
            this.color = color;
            this.x = x;
            this.y = y;
        }

        private void Bishop_Click(object sender, EventArgs e)
        {
            Pieces Promo = new Bishop(color, x, y);
            Movement.PieceViewer(Promo);
            this.Close();
        }

        private void Rook_Click(object sender, EventArgs e)
        {
            Pieces Promo = new Rook(color, x, y);
            Movement.PieceViewer(Promo);
            this.Close();
        }

        private void Knight_Click(object sender, EventArgs e)
        {
            Pieces Promo = new Knight(color, x, y);
            Movement.PieceViewer(Promo);
            this.Close();
        }

        private void Queen_Click(object sender, EventArgs e)
        {
            Pieces Promo = new Queen(color, x, y);
            Movement.PieceViewer(Promo);
            this.Close();
        }
    }
}
