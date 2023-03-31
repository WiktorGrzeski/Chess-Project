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
    internal class Movement : Board
    {
        public static void PieceViewer(Pieces piece) //wyświetla figurę na planszy
        {
            tiles[piece.X, piece.Y].Image = piece.image;
            tiles[piece.X, piece.Y].Tag = "occupied";
            pieces[piece.X, piece.Y] = piece;
        }
        public static void PieceMover(Pieces piece, int x, int y) //Usuwa figurę z pierwotnej pozycji i ustawia na nowej
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
            bool taken = false;
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
            //Czy nastąpiła roszada
            if (piece.name == "King" && piece.moved == false &&
                ((x == 2 && y == 0) || (x == 6 && y == 0) || (x == 2 && y == 7) || (x == 6 && y == 7)))
                castle = true;

            //przesunięcie figury
            piece.X = x; piece.Y = y;

            //Śledzi położenie królów
            if (piece.name == "King") 
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
            
            //Sprawdza czy figura została zbita
            if ((string)tiles[x, y].Tag == "occupied")
                taken = true;
            
            Movement.PieceViewer(piece);

            //PROMOTION SYSTEM - DO POPRAWY
            if ((piece.name == "Pawn" && piece.Y == 7 && piece.color == 'W')
                || (piece.name == "Pawn" && piece.Y == 0 && piece.color == 'B'))
            {  
                var promotion = new Promotion(piece.color, piece.X, piece.Y);
                promotion.ShowDialog();
            }

            //Roszada
            if (castle == true)
            {
                if (x == 2 && y == 0)
                {
                    tiles[0, 0].Image = null;
                    tiles[0, 0].Tag = "empty";
                    pieces[0, 0].X = 3;
                    Movement.PieceViewer(pieces[0, 0]);
                    pieces[0, 0] = null;
                }
                else if (x == 6 && y == 0)
                {
                    tiles[7, 0].Image = null;
                    tiles[7, 0].Tag = "empty";
                    pieces[7, 0].X = 5;
                    Movement.PieceViewer(pieces[7, 0]);
                    pieces[7, 0] = null;
                }
                else if (x == 2 && y == 7)
                {
                    tiles[0, 7].Image = null;
                    tiles[0, 7].Tag = "empty";
                    pieces[0, 7].X = 3;
                    Movement.PieceViewer(pieces[0, 7]);
                    pieces[0, 7] = null;
                }
                else if (x == 6 && y == 7)
                {
                    tiles[7, 7].Image = null;
                    tiles[7, 7].Tag = "empty";
                    pieces[7, 7].X = 5;
                    Movement.PieceViewer(pieces[7, 7]);
                    pieces[7, 7] = null;
                }
            }
            piece.moved = true;
            if (Turn == 'W')
                Check = Checks.CheckChecks(BKingPosition[0], BKingPosition[1], Turn);
            else
                Check = Checks.CheckChecks(WKingPosition[0], WKingPosition[1], Turn);

            if (Check == true)
            {
                if (Turn == 'W') Turn = 'B'; //Tymczasowy (prawdopodobnie) fix
                else if (Turn == 'B') Turn = 'W'; //Tymczasowy (prawdopodobnie) fix
                checkmate = Checks.IsItCheckmate();
                if (Turn == 'W') Turn = 'B'; //Tymczasowy (prawdopodobnie) fix
                else if (Turn == 'B') Turn = 'W'; //Tymczasowy (prawdopodobnie) fix
            }

            // GRANIE DŹWIĘKÓW
            if (checkmate == true)
            {
                sp5.Play();
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
    }
}
