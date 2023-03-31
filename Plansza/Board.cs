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

namespace Chess
{
    public partial class Board : Form
    {
        static public PictureBox[,] tiles = new PictureBox[8, 8];
        static public Pieces[,] pieces = new Pieces[8, 8];
        static public int[] WKingPosition = new int[2];
        static public int[] BKingPosition = new int[2];

        public Board()
        {
            InitializeComponent();
            CreateBoard();
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
            Pieces RookA8 = new Pieces("Rook", 'B', 0, 7);
            Pieces KnightB8 = new Pieces("Knight", 'B', 1, 7);
            Pieces BishopC8 = new Pieces("Bishop", 'B', 2, 7);
            Pieces QueenD8 = new Pieces("Queen", 'B', 3, 7);
            Pieces KingE8 = new Pieces("King", 'B', 4, 7);
            BKingPosition[0] = KingE8.X;
            BKingPosition[1] = KingE8.Y;
            Pieces BishopF8 = new Pieces("Bishop", 'B', 5, 7);
            Pieces KnightG8 = new Pieces("Knight", 'B', 6, 7);
            Pieces RookH8 = new Pieces("Rook", 'B', 7, 7);
            Pieces PawnA7 = new Pieces("Pawn", 'B', 0, 6);
            Pieces PawnB7 = new Pieces("Pawn", 'B', 1, 6);
            Pieces PawnC7 = new Pieces("Pawn", 'B', 2, 6);
            Pieces PawnD7 = new Pieces("Pawn", 'B', 3, 6);
            Pieces PawnE7 = new Pieces("Pawn", 'B', 4, 6);
            Pieces PawnF7 = new Pieces("Pawn", 'B', 5, 6);
            Pieces PawnG7 = new Pieces("Pawn", 'B', 6, 6);
            Pieces PawnH7 = new Pieces("Pawn", 'B', 7, 6);
            PieceViewer(RookA8);
            PieceViewer(KnightB8);
            PieceViewer(BishopC8);
            PieceViewer(QueenD8);
            PieceViewer(KingE8);
            PieceViewer(BishopF8);
            PieceViewer(KnightG8);
            PieceViewer(RookH8);
            PieceViewer(PawnA7);
            PieceViewer(PawnB7);
            PieceViewer(PawnC7);
            PieceViewer(PawnD7);
            PieceViewer(PawnE7);
            PieceViewer(PawnF7);
            PieceViewer(PawnG7);
            PieceViewer(PawnH7);

            //white
            Pieces RookA1 = new Pieces("Rook", 'W', 0, 0);
            Pieces KnightB1 = new Pieces("Knight", 'W', 1, 0);
            Pieces BishopC1 = new Pieces("Bishop", 'W', 2, 0);
            Pieces QueenD1 = new Pieces("Queen", 'W', 3, 0);
            Pieces KingE1 = new Pieces("King", 'W', 4, 0);
            WKingPosition[0] = KingE1.X;
            WKingPosition[1] = KingE1.Y;
            Pieces BishopF1 = new Pieces("Bishop", 'W', 5, 0);
            Pieces KnightG1 = new Pieces("Knight", 'W', 6, 0);
            Pieces RookH1 = new Pieces("Rook", 'W', 7, 0);
            Pieces PawnA2 = new Pieces("Pawn", 'W', 0, 1);
            Pieces PawnB2 = new Pieces("Pawn", 'W', 1, 1);
            Pieces PawnC2 = new Pieces("Pawn", 'W', 2, 1);
            Pieces PawnD2 = new Pieces("Pawn", 'W', 3, 1);
            Pieces PawnE2 = new Pieces("Pawn", 'W', 4, 1);
            Pieces PawnF2 = new Pieces("Pawn", 'W', 5, 1);
            Pieces PawnG2 = new Pieces("Pawn", 'W', 6, 1);
            Pieces PawnH2 = new Pieces("Pawn", 'W', 7, 1);
            PieceViewer(RookA1);
            PieceViewer(KnightB1);
            PieceViewer(BishopC1);
            PieceViewer(QueenD1);
            PieceViewer(KingE1);
            PieceViewer(BishopF1);
            PieceViewer(KnightG1);
            PieceViewer(RookH1);
            PieceViewer(PawnA2);
            PieceViewer(PawnB2);
            PieceViewer(PawnC2);
            PieceViewer(PawnD2);
            PieceViewer(PawnE2);
            PieceViewer(PawnF2);
            PieceViewer(PawnG2);
            PieceViewer(PawnH2);
        }
        private void BoardFlip() //Odwraca plansze dla gracza czarnych 
        {
            PictureBox temp = new PictureBox();
            int odwi = 0;
            int odwj = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    odwj = 7 - j; odwi = 7 - i; 
                    temp.Top = tiles[j, i].Top;
                    temp.Left = tiles[j, i].Left;
                    tiles[j, i].Top = tiles[odwj, odwi].Top;
                    tiles[j, i].Left = tiles[odwj, odwi].Left;
                    tiles[odwj, odwi].Top = temp.Top;
                    tiles[odwj, odwi].Left = temp.Left;
                }
            }
        }
        private void PieceViewer(Pieces piece) //wyświetla figurę na planszy
        {
            tiles[piece.X, piece.Y].Image = piece.image;
            tiles[piece.X, piece.Y].Tag = "occupied";
            pieces[piece.X, piece.Y] = piece;
        }
        private void PieceMover(Pieces piece, int x, int y) //Usuwa figurę z pierwotnej pozycji i ustawia na nowej
        {
            SoundPlayer sp = new SoundPlayer("sounds/move.wav");
            SoundPlayer sp2 = new SoundPlayer("sounds/capture.wav");
            SoundPlayer sp3 = new SoundPlayer("sounds/check.wav");
            SoundPlayer sp4 = new SoundPlayer("sounds/castle.wav");
            SoundPlayer sp5 = new SoundPlayer("sounds/checkmate.wav");

            tiles[piece.X, piece.Y].Image = null;
            tiles[piece.X, piece.Y].Tag = "empty";
            pieces[piece.X, piece.Y] = null;
            bool bravo = false;
            bool castle = false;
            Check = false;
            bool checkmate = false;

            //En Passant
            if (LastMovedPiece != null)
            {
                if (LastMovedPiece.name == "Pawn" && LastMovedPiece.color == 'B' && LastY[0] == 6 && LastY[1] == 4 &&
                    piece.Y == 4 && x == LastX[0] && y == 5)
                {
                    pieces[LastX[0], 4] = null;
                    tiles[LastX[0], 4].Image = null;
                    tiles[LastX[0], 4].Tag = "empty";
                    bravo = true;
                }
                else if (LastMovedPiece.name == "Pawn" && LastMovedPiece.color == 'W' && LastY[0] == 1 && LastY[1] == 3 &&
                    piece.Y == 3 && x == LastX[0] && y == 2)
                {
                    pieces[LastX[0], 3] = null;
                    tiles[LastX[0], 3].Image = null;
                    tiles[LastX[0], 3].Tag = "empty";
                    bravo = true;
                }
            }
            //Castling
            if(piece.name == "King" && piece.moved == false && 
                ((x == 2 && y == 0)||(x == 6 && y == 0)||(x == 2 && y == 7)||(x == 6 && y == 7)))
                    castle = true;

            piece.X = x; piece.Y = y;
            if ((piece.name=="Pawn" && piece.Y == 7 && piece.color == 'W')
                || (piece.name == "Pawn" && piece.Y == 0 && piece.color == 'B'))
            {  //PROMOTION SYSTEM - DO POPRAWY
                piece.name = "Queen";
                piece.image = Image.FromFile("szachy/" + piece.name + piece.color + ".png");
            }
            if(piece.name == "King")
            {
                if (piece.color == 'W')
                {
                    WKingPosition[0] = piece.X;
                    WKingPosition[1] = piece.Y;
                }
                else
                {
                    BKingPosition[0] = piece.X;
                    BKingPosition[1] = piece.Y;
                }
            }
            bool taken = false;
            if ((string)tiles[x, y].Tag == "occupied")
                taken = true;
            PieceViewer(piece);
            if (castle == true)
            {
                if (x == 2 && y == 0)
                {
                    tiles[0, 0].Image = null;
                    tiles[0, 0].Tag = "empty";
                    pieces[0, 0].X = 3;
                    PieceViewer(pieces[0, 0]);
                    pieces[0, 0] = null;
                }
                else if (x == 6 && y == 0)
                {
                    tiles[7, 0].Image = null;
                    tiles[7, 0].Tag = "empty";
                    pieces[7, 0].X = 5;
                    PieceViewer(pieces[7, 0]);
                    pieces[7, 0] = null;
                }
                else if (x == 2 && y == 7)
                {
                    tiles[0, 7].Image = null;
                    tiles[0, 7].Tag = "empty";
                    pieces[0, 7].X = 3;
                    PieceViewer(pieces[0, 7]);
                    pieces[0, 7] = null;
                }
                else if (x == 6 && y == 7)
                {
                    tiles[7, 7].Image = null;
                    tiles[7, 7].Tag = "empty";
                    pieces[7, 7].X = 5;
                    PieceViewer(pieces[7, 7]);
                    pieces[7, 7] = null;
                }
            }
            piece.moved = true;
            if (Turn == 'W')
                Check = CheckChecks(BKingPosition[0], BKingPosition[1], Turn);
            else
                Check = CheckChecks(WKingPosition[0], WKingPosition[1], Turn);

            if (Check == true)
            {
                if (Turn == 'W') Turn = 'B'; //Tymczasowy (prawdopodobnie) fix
                else if (Turn == 'B') Turn = 'W'; //Tymczasowy (prawdopodobnie) fix
                checkmate = IsItCheckmate();
                if (Turn == 'W') Turn = 'B'; //Tymczasowy (prawdopodobnie) fix
                else if (Turn == 'B') Turn = 'W'; //Tymczasowy (prawdopodobnie) fix
            }

            // GRANIE DŹWIĘKÓW + SZACHMAT
            if (checkmate == true)
            {
                sp5.Play();
                koniec.Visible = true;              
            }
            else if (castle == true)
                sp4.Play();
            else if (Check == true)
                sp3.Play();
            else if (taken == true || bravo == true)
                sp2.Play();
            else
                sp.Play();
            sp.Dispose();
            sp2.Dispose();
            sp3.Dispose();
            sp4.Dispose();
            sp5.Dispose();
        }
        private void ShowMoves(List<int[]> moves) //Wyświetla dostępne ruchy w postaci kropek i zmiany koloru na planszy
        {
            for (int i = 0; i < moves.Count; i++)
            {
                if ((string)tiles[moves[i][0], moves[i][1]].Tag == "empty")
                {
                    tiles[moves[i][0], moves[i][1]].Image = Image.FromFile("szachy/dot.png");
                }
                else
                {
                    tiles[moves[i][0], moves[i][1]].BackColor = Color.PaleVioletRed;
                    //tiles[moves[i][0], moves[i][1]].BorderStyle = BorderStyle.FixedSingle; 
                }
            }
        }
        private void HideMoves(List<int[]> moves) //Ukrywa ruchy wyświetlone przez showMoves
        {
            for (int i = 0; i < moves.Count; i++)
            {
                if ((string)tiles[moves[i][0], moves[i][1]].Tag == "empty")
                {
                    tiles[moves[i][0], moves[i][1]].Image = null;
                }
                else
                {
                    if ((moves[i][0] == LastX[0] && moves[i][1] == LastY[0]) || (moves[i][0] == LastX[1] && moves[i][1] == LastY[1]))
                    {
                        tiles[moves[i][0], moves[i][1]].BackColor = Color.FromArgb(187, 203, 43);
                    }
                    else if ((moves[i][0] % 2 == 0 && moves[i][1] % 2 != 0) || (moves[i][0] % 2 != 0 && moves[i][1] % 2 == 0))
                    {
                        tiles[moves[i][0], moves[i][1]].BackColor = Color.FromArgb(238, 238, 213);
                    }
                    else
                        tiles[moves[i][0], moves[i][1]].BackColor = Color.FromArgb(125, 148, 93);
                    //tiles[moves[i][0], moves[i][1]].BorderStyle = BorderStyle.None; Powoduje flicker
                }
            }
        }
        //Zmienne dotyczące ostatniego ruchu
        public static int[] LastX = new int[2], LastY = new int[2];
        public static Pieces LastMovedPiece = null;
        private void LastMove(int firstX, int firstY, int x, int y) //Podświetla pola na których wydarzył się ostatni ruch
        {
            for (int i = 0; i < 2; i++)
            {
                if ((LastX[i] % 2 == 0 && LastY[i] % 2 != 0) || (LastX[i] % 2 != 0 && LastY[i] % 2 == 0))
                    tiles[LastX[i], LastY[i]].BackColor = Color.FromArgb(238, 238, 213);
                else
                    tiles[LastX[i], LastY[i]].BackColor = Color.FromArgb(125, 148, 93);
                //tiles[LastX[i], LastY[i]].BorderStyle = BorderStyle.None; Powoduje flicker
            }
            LastX[0] = firstX; LastY[0] = firstY;
            LastX[1] = x; LastY[1] = y;
            tiles[firstX, firstY].BackColor = Color.FromArgb(187, 203, 43);
            tiles[x, y].BackColor = Color.FromArgb(187, 203, 43);
            //tiles[firstX, firstY].BorderStyle = BorderStyle.FixedSingle;
            //tiles[x, y].BorderStyle = BorderStyle.FixedSingle;
        }

        public static bool Check = false;
        public static bool SkipCheck = false;
        public static bool CheckChecks(int x, int y, char color) //Sprawdza czy pole jest szachowane
        {
            SkipCheck = true;
            bool littleCheck = false;
            List<int[]> moves = new List<int[]>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((string)tiles[i, j].Tag == "occupied")
                    {
                        if (pieces[i, j].color == color)
                        {
                            moves = pieces[i, j].LegalMoves();
                            foreach (var move in moves)
                            {
                                if (x == move[0] && y == move[1])
                                {
                                    littleCheck = true;
                                }
                            }
                        }
                    }
                }
            }
            return littleCheck;
        } 
        public static List<int[]> SimulateCheckChecks(Pieces piece, List<int[]> moves) //Symuluje ruch i sprawdza czy jest legalny
        {
            SkipCheck = true;

            Pieces destinationPieceCopy = new Pieces("Pawn", 'W', 0, 0);
            int baseX = 0; int baseY = 0;
            baseX = piece.X; baseY = piece.Y;
            List<int> indexy = new List<int>();

            int[] OWKingPosition = new int[2];
            OWKingPosition[0] = WKingPosition[0];
            OWKingPosition[1] = WKingPosition[1];

            int[] OBKingPosition = new int[2];
            OBKingPosition[0] = BKingPosition[0];
            OBKingPosition[1] = BKingPosition[1];

            foreach (var move in moves)
            {
                destinationPieceCopy = pieces[move[0], move[1]];
                List<int[]> deepMoves = new List<int[]>();
                bool simCheck = false;

                piece.X = move[0]; piece.Y = move[1];
                pieces[piece.X, piece.Y] = piece;
                pieces[baseX, baseY] = null;
                tiles[baseX, baseY].Tag = "empty";
                tiles[piece.X, piece.Y].Tag = "occupied";

                if(piece.name == "King")
                {
                    if(piece.color == 'W')
                    {
                        WKingPosition[0] = piece.X;
                        WKingPosition[1] = piece.Y;
                    }
                    else
                    {
                        BKingPosition[0] = piece.X;
                        BKingPosition[1] = piece.Y;
                    }
                }

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if ((string)tiles[i, j].Tag == "occupied")
                        {
                            if (pieces[i, j].color != Turn)
                            {
                                deepMoves = pieces[i, j].LegalMoves();
                                foreach (var deepMove in deepMoves)
                                {
                                    //if (piece.name == "King" && piece.X == deepMove[0] && piece.Y == deepMove[1])
                                   //     simCheck = true;
                                    if ((WKingPosition[0] == deepMove[0] && WKingPosition[1] == deepMove[1]) || (BKingPosition[0] == deepMove[0] && BKingPosition[1] == deepMove[1]))
                                        simCheck = true;
                                }

                            }
                        }
                    }
                }
                pieces[move[0], move[1]] = destinationPieceCopy;
                tiles[baseX, baseY].Tag = "occupied";
                piece.X = baseX;
                piece.Y = baseY;
                pieces[baseX, baseY] = piece;
                if (pieces[move[0], move[1]] == null)
                    tiles[move[0], move[1]].Tag = "empty";
                WKingPosition[0] = OWKingPosition[0];
                WKingPosition[1] = OWKingPosition[1];
                BKingPosition[0] = OBKingPosition[0];
                BKingPosition[1] = OBKingPosition[1];
                if (simCheck == true)
                    indexy.Add(moves.IndexOf(move));
            }
            if (indexy != null)
            {
                indexy.Reverse();
                foreach (var index in indexy)
                {
                    moves.RemoveAt(index);
                }
            }
            SkipCheck = false;
            return moves;
        }
        static bool IsItCheckmate() // is it checkmate?
        {
            bool isIt=true;
            List<int[]> moves = new List<int[]>();
            foreach (var piece in pieces)
            {
                if (piece != null)
                {
                    if (piece.color == Turn)
                    {
                        SkipCheck = false;
                        moves = piece.LegalMoves();
                        if (moves.Count != 0)
                        { isIt = false; break; }
                    }
                }
            }
            return isIt;
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
                    ShowMoves(goodTiles);
                    return;
                }

                if (firstTile == clickedTile)
                {
                    firstTile = null;
                    HideMoves(goodTiles);
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
                PieceMover(pieces[firstTileX, firstTileY], x, y);
                HideMoves(goodTiles);
                firstTile = null;
                goodTiles = null;
                if ((firstTileX % 2 == 0 && firstTileY % 2 != 0) || (firstTileX % 2 != 0 && firstTileY % 2 == 0))
                    tiles[firstTileX, firstTileY].BackColor = Color.FromArgb(238, 238, 213);
                else
                    tiles[firstTileX, firstTileY].BackColor = Color.FromArgb(125, 148, 93);
                LastMove(firstTileX, firstTileY, x, y);
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