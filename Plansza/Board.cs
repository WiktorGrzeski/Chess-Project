using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Windows;

namespace Chess
{
    public partial class Board : Form
    {
        static public PictureBox[,] tiles = new PictureBox[8, 8];
        static public Pieces[,] pieces = new Pieces[8, 8];
        static public int[] WKingPosition = new int[2];
        static public int[] BKingPosition = new int[2];

        //Zmienne dotyczące ostatniego ruchu
        public static int[] LastX = new int[2], LastY = new int[2];
        public static Pieces LastMovedPiece = null;
        public static bool Check = false;
        public static bool SkipCheck = false;
        public Board()
        {
            CreateBoard();
            InitializeComponent();
        }

        private void CreateBoard() //Tworzenie planszy i sytuacji początkowej
        {
            int top = 0;
            int left = 0;
            bool co2 = false;
            //rząd
            for (int i = 0; i < 8; i++)
            {
                //kolumna
                for (int j = 0; j < 8; j++)
                {
                    PictureBox tile = new PictureBox();
                    tile.Height = 120;
                    tile.Width = 120;
                    tile.Click += Tile_Click;
                    tile.Top = top;
                    tile.Left = left;
                    tile.Tag = "empty";
                    tile.Name = ((char)(65+j)) + "" + (8-i);
                    tile.SizeMode = PictureBoxSizeMode.Zoom;
                    if (co2 == false)
                    {
                        tile.BackColor = Color.FromArgb(238, 238, 213);
                        co2 = true;
                    }
                    else
                    {
                        tile.BackColor = Color.FromArgb(125, 148, 93);
                        co2 = false;
                    }
                    this.Controls.Add(tile);
                    tiles[j,7-i] = tile;
                    left += 120;
                }
                top += 120;
                left = 0;
                if (co2 == true) co2 = false;
                else co2 = true;
            }
            
            //black
            Pieces RookA8 = new Rook('B', 0, 7);
            Pieces KnightB8 = new Knight('B', 1, 7);
            Pieces BishopC8 = new Bishop('B', 2, 7);
            Pieces QueenD8 = new Queen('B', 3, 7);
            Pieces KingE8 = new King('B', 4, 7);
            BKingPosition[0] = KingE8.X;
            BKingPosition[1] = KingE8.Y;
            Pieces BishopF8 = new Bishop('B', 5, 7);
            Pieces KnightG8 = new Knight('B', 6, 7);
            Pieces RookH8 = new Rook('B', 7, 7);
            Pieces PawnA7 = new Pawn('B', 0, 6);
            Pieces PawnB7 = new Pawn('B', 1, 6);
            Pieces PawnC7 = new Pawn('B', 2, 6);
            Pieces PawnD7 = new Pawn('B', 3, 6);
            Pieces PawnE7 = new Pawn('B', 4, 6);
            Pieces PawnF7 = new Pawn('B', 5, 6);
            Pieces PawnG7 = new Pawn('B', 6, 6);
            Pieces PawnH7 = new Pawn('B', 7, 6);
            Movement.PieceViewer(RookA8);
            Movement.PieceViewer(KnightB8);
            Movement.PieceViewer(BishopC8);
            Movement.PieceViewer(QueenD8);
            Movement.PieceViewer(KingE8);
            Movement.PieceViewer(BishopF8);
            Movement.PieceViewer(KnightG8);
            Movement.PieceViewer(RookH8);
            Movement.PieceViewer(PawnA7);
            Movement.PieceViewer(PawnB7);
            Movement.PieceViewer(PawnC7);
            Movement.PieceViewer(PawnD7);
            Movement.PieceViewer(PawnE7);
            Movement.PieceViewer(PawnF7);
            Movement.PieceViewer(PawnG7);
            Movement.PieceViewer(PawnH7);

            //white
            Pieces RookA1 = new Rook('W', 0, 0);
            Pieces KnightB1 = new Knight('W', 1, 0);
            Pieces BishopC1 = new Bishop('W', 2, 0);
            Pieces QueenD1 = new Queen('W', 3, 0);
            Pieces KingE1 = new King('W', 4, 0);
            WKingPosition[0] = KingE1.X;
            WKingPosition[1] = KingE1.Y;
            Pieces BishopF1 = new Bishop('W', 5, 0);
            Pieces KnightG1 = new Knight('W', 6, 0);
            Pieces RookH1 = new Rook('W', 7, 0);
            Pieces PawnA2 = new Pawn('W', 0, 1);
            Pieces PawnB2 = new Pawn('W', 1, 1);
            Pieces PawnC2 = new Pawn('W', 2, 1);
            Pieces PawnD2 = new Pawn('W', 3, 1);
            Pieces PawnE2 = new Pawn('W', 4, 1);
            Pieces PawnF2 = new Pawn('W', 5, 1);
            Pieces PawnG2 = new Pawn('W', 6, 1);
            Pieces PawnH2 = new Pawn('W', 7, 1);
            Movement.PieceViewer(RookA1);
            Movement.PieceViewer(KnightB1);
            Movement.PieceViewer(BishopC1);
            Movement.PieceViewer(QueenD1);
            Movement.PieceViewer(KingE1);
            Movement.PieceViewer(BishopF1);
            Movement.PieceViewer(KnightG1);
            Movement.PieceViewer(RookH1);
            Movement.PieceViewer(PawnA2);
            Movement.PieceViewer(PawnB2);
            Movement.PieceViewer(PawnC2);
            Movement.PieceViewer(PawnD2);
            Movement.PieceViewer(PawnE2);
            Movement.PieceViewer(PawnF2);
            Movement.PieceViewer(PawnG2);
            Movement.PieceViewer(PawnH2);
        }

        PictureBox firstTile = null;
        int firstTileX = 0;
        int firstTileY = 0;
        List<int[]> goodTiles = null;
        public static char Turn = 'W'; //Tura gracza białego Turn = 'W'; Tura gracza czarnego Turn = 'B';
        private void Tile_Click(object sender, EventArgs e) //Event kliknięcia na pole
        {
            SkipCheck = false;
            PictureBox clickedTile = sender as PictureBox;
            int x = clickedTile.Left / 120;
            int y = 7 - (clickedTile.Top / 120);
            bool good = false;
            int[] tab = {x, y};

            if (clickedTile != null)
            {
                if (firstTile == null && clickedTile.Image != null && pieces[x, y].color == Turn)
                {
                    firstTile = clickedTile;
                    firstTileX = x; firstTileY = y;
                    firstTile.BackColor= Color.Red;
                    goodTiles = pieces[x, y].LegalMoves();
                    Visuals.ShowMoves(goodTiles);
                    return;
                }

                if (firstTile == clickedTile)
                {
                    firstTile = null;
                    Visuals.HideMoves(goodTiles);
                    goodTiles = null;
                    if ((firstTileX % 2 == 0 && firstTileY % 2 != 0) || (firstTileX % 2 != 0 && firstTileY % 2 == 0))
                    {
                        tiles[firstTileX, firstTileY].BackColor = Color.FromArgb(238, 238, 213);
                    }
                    else
                        tiles[firstTileX, firstTileY].BackColor = Color.FromArgb(125, 148, 93);

                    firstTileX = 0; firstTileY = 0;
                    return;
                }

                if (firstTile == null)
                    return;

                foreach (var item in goodTiles)
                {
                    if (item[0] == tab[0] && item[1] == tab[1])
                    { good = true; break; }
                }

                if (good == false)
                    return;
                Movement.PieceMover(pieces[firstTileX, firstTileY], x, y);
                Visuals.HideMoves(goodTiles);
                firstTile = null;
                goodTiles = null;
                if ((firstTileX % 2 == 0 && firstTileY % 2 != 0) || (firstTileX % 2 != 0 && firstTileY % 2 == 0))
                    tiles[firstTileX, firstTileY].BackColor = Color.FromArgb(238, 238, 213);
                else
                    tiles[firstTileX, firstTileY].BackColor = Color.FromArgb(125, 148, 93);
                Visuals.LastMove(firstTileX, firstTileY, x, y);
                LastMovedPiece = pieces[x,y];
                firstTileX = 0; firstTileY = 0;
                if (Turn == 'W')
                    Turn = 'B';
                else if (Turn == 'B')
                    Turn = 'W';
                return;
            }
        }
    }
}