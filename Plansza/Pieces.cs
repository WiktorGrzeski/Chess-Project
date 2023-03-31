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
    public class Pieces
    {
        public int id = 0;
        public string name;
        public char color;
        public Image image;
        public int X;
        public int Y;
        public int value;
        public bool moved = false;
        public Pieces(string name, char color, int X, int Y)
        {
            id = id++;
            this.name = name;
            this.color = color;
            this.image = Image.FromFile("szachy/" + name + color + ".png");
            this.X = X;
            this.Y = Y;
        }
        public Pieces(Pieces piece) // Konstruktor kopiujący (na razie niepotrzebny)
        {
            this.id = piece.id;
            this.name = piece.name;
            this.color = piece.color;
            this.image = piece.image;
            this.X = piece.X;
            this.Y = piece.Y;
            this.value = piece.value;
            this.moved = piece.moved;
        }
        public virtual List<int[]> LegalMoves()
        { return null; }
    }
    class Pawn : Pieces
    {
        public Pawn(char color, int X, int Y) : base("Pawn", color, X, Y)
        {
            value = 1;
        }
        public override List<int[]> LegalMoves()
        {
            List<int[]> moves = new List<int[]>();

            if (color == 'B')
            {
                if ((string)Board.tiles[X, Y - 1].Tag == "empty")
                {
                    int[] move = new int[2];
                    move[0] = X;
                    move[1] = Y - 1;
                    moves.Add(move);
                }
                if (Y == 6)
                {
                    if ((string)Board.tiles[X, Y - 2].Tag == "empty" && (string)Board.tiles[X, Y - 1].Tag == "empty")
                    {
                        int[] move = new int[2];
                        move[0] = X;
                        move[1] = Y - 2;
                        moves.Add(move);
                    }
                }
                if (X > 0 && (string)Board.tiles[X - 1, Y - 1].Tag == "occupied")
                {
                    if (Board.pieces[X - 1, Y - 1].color != color)
                    {
                        int[] move = new int[2];
                        move[0] = X - 1;
                        move[1] = Y - 1;
                        moves.Add(move);
                    }
                }
                if (X < 7 && (string)Board.tiles[X + 1, Y - 1].Tag == "occupied")
                {
                    if (Board.pieces[X + 1, Y - 1].color != color)
                    {
                        int[] move = new int[2];
                        move[0] = X + 1;
                        move[1] = Y - 1;
                        moves.Add(move);
                    }
                }
            }
            else if (color == 'W')
            {
                if (Y < 7)
                {
                    if ((string)Board.tiles[X, Y + 1].Tag == "empty")
                    {
                        int[] move = new int[2];
                        move[0] = X;
                        move[1] = Y + 1;
                        moves.Add(move);
                    }
                    if (Y == 1)
                    {
                        if ((string)Board.tiles[X, Y + 2].Tag == "empty" && (string)Board.tiles[X, Y + 2].Tag == "empty")
                        {
                            int[] move = new int[2];
                            move[0] = X;
                            move[1] = Y + 2;
                            moves.Add(move);
                        }
                    }
                    if (X > 0 && (string)Board.tiles[X - 1, Y + 1].Tag == "occupied")
                    {
                        if (Board.pieces[X - 1, Y + 1].color != color)
                        {
                            int[] move = new int[2];
                            move[0] = X - 1;
                            move[1] = Y + 1;
                            moves.Add(move);
                        }
                    }
                    if (X < 7 && (string)Board.tiles[X + 1, Y + 1].Tag == "occupied")
                    {
                        if (Board.pieces[X + 1, Y + 1].color != color)
                        {
                            int[] move = new int[2];
                            move[0] = X + 1;
                            move[1] = Y + 1;
                            moves.Add(move);
                        }
                    }
                }
            }
            //En Passant
            if (Board.LastMovedPiece != null)
            {
                int[] move = new int[2];
                move = EnPassant();
                if (move[0] != 0 && move[1] != 0)
                    moves.Add(move);
            }
            //Odrzucanie nielegalnych
            if (Board.SkipCheck == false)
                moves = Checks.SimulateCheckChecks(Board.pieces[X, Y], moves);
            return moves;
        }
        private int[] EnPassant()
        {
            int[] move = new int[2];
            if (Board.LastMovedPiece.name == "Pawn" && Board.LastMovedPiece.color == 'B' && Board.LastY[0] == 6 && Board.LastY[1] == 4 &&
                Y == 4 && (X == Board.LastX[0] + 1 || X == Board.LastX[0] - 1))
            {
                move[0] = Board.LastX[0];
                move[1] = 5;
            }
            else if (Board.LastMovedPiece.name == "Pawn" && Board.LastMovedPiece.color == 'W' && Board.LastY[0] == 1 && Board.LastY[1] == 3 &&
                Y == 3 && (X == Board.LastX[0] + 1 || X == Board.LastX[0] - 1))
            {
                move[0] = Board.LastX[0];
                move[1] = 2;
            }
            return move;
        }
    }
    class Rook : Pieces
    {
        public Rook(char color, int X, int Y) : base("Rook", color, X, Y)
        {
            value = 5;
        }
        public override List<int[]> LegalMoves()
        {
            List<int[]> moves = new List<int[]>();

            int i = X + 1;
            while (i < 8)
            {
                int[] move = new int[2];
                if ((string)Board.tiles[i, Y].Tag == "occupied" && Board.pieces[i, Y].color == color)
                    break;
                move[0] = i; //legalne X
                move[1] = Y; //legalne Y
                moves.Add(move);
                if ((string)Board.tiles[i, Y].Tag == "occupied")
                    break;
                i++;
            }
            i = X - 1;
            while (i >= 0)
            {
                int[] move = new int[2];
                if ((string)Board.tiles[i, Y].Tag == "occupied" && Board.pieces[i, Y].color == color)
                    break;
                move[0] = i;
                move[1] = Y;
                moves.Add(move);
                if ((string)Board.tiles[i, Y].Tag == "occupied")
                    break;
                i--;
            }
            i = Y + 1;
            while (i < 8)
            {
                int[] move = new int[2];
                if ((string)Board.tiles[X, i].Tag == "occupied" && Board.pieces[X, i].color == color)
                    break;
                move[0] = X;
                move[1] = i;
                moves.Add(move);
                if ((string)Board.tiles[X, i].Tag == "occupied")
                    break;
                i++;
            }
            i = Y - 1;
            while (i >= 0)
            {
                int[] move = new int[2];
                if ((string)Board.tiles[X, i].Tag == "occupied" && Board.pieces[X, i].color == color)
                    break;
                move[0] = X;
                move[1] = i;
                moves.Add(move);
                if ((string)Board.tiles[X, i].Tag == "occupied")
                    break;
                i--;
            }
            //Odrzucanie nielegalnych
            if (Board.SkipCheck == false)
                moves = Checks.SimulateCheckChecks(Board.pieces[X, Y], moves);
            return moves;
        }
    }
    class Knight : Pieces
    {
        public Knight(char color, int X, int Y) : base("Knight", color, X, Y)
        {
            value = 3;
        }
        public override List<int[]> LegalMoves()
        {
            List<int[]> moves = new List<int[]>();

            if (Y + 2 <= 7 && X + 1 <= 7)
            {
                if ((string)Board.tiles[X + 1, Y + 2].Tag == "empty" || Board.pieces[X + 1, Y + 2].color != color)
                {
                    int[] move = new int[2];
                    move[0] = X + 1;
                    move[1] = Y + 2;
                    moves.Add(move);
                }
            }
            if (Y + 1 <= 7 && X + 2 <= 7)
            {
                if ((string)Board.tiles[X + 2, Y + 1].Tag == "empty" || Board.pieces[X + 2, Y + 1].color != color)
                {
                    int[] move = new int[2];
                    move[0] = X + 2;
                    move[1] = Y + 1;
                    moves.Add(move);
                }
            }
            if (Y - 1 >= 0 && X + 2 <= 7)
            {
                if ((string)Board.tiles[X + 2, Y - 1].Tag == "empty" || Board.pieces[X + 2, Y - 1].color != color)
                {
                    int[] move = new int[2];
                    move[0] = X + 2;
                    move[1] = Y - 1;
                    moves.Add(move);
                }
            }
            if (Y - 2 >= 0 && X + 1 <= 7)
            {
                if ((string)Board.tiles[X + 1, Y - 2].Tag == "empty" || Board.pieces[X + 1, Y - 2].color != color)
                {
                    int[] move = new int[2];
                    move[0] = X + 1;
                    move[1] = Y - 2;
                    moves.Add(move);
                }
            }
            if (Y - 2 >= 0 && X - 1 >= 0)
            {
                if ((string)Board.tiles[X - 1, Y - 2].Tag == "empty" || Board.pieces[X - 1, Y - 2].color != color)
                {
                    int[] move = new int[2];
                    move[0] = X - 1;
                    move[1] = Y - 2;
                    moves.Add(move);
                }
            }
            if (Y - 1 >= 0 && X - 2 >= 0)
            {
                if ((string)Board.tiles[X - 2, Y - 1].Tag == "empty" || Board.pieces[X - 2, Y - 1].color != color)
                {
                    int[] move = new int[2];
                    move[0] = X - 2;
                    move[1] = Y - 1;
                    moves.Add(move);
                }
            }
            if (Y + 1 <= 7 && X - 2 >= 0)
            {
                if ((string)Board.tiles[X - 2, Y + 1].Tag == "empty" || Board.pieces[X - 2, Y + 1].color != color)
                {
                    int[] move = new int[2];
                    move[0] = X - 2;
                    move[1] = Y + 1;
                    moves.Add(move);
                }
            }
            if (Y + 2 <= 7 && X - 1 >= 0)
            {
                if ((string)Board.tiles[X - 1, Y + 2].Tag == "empty" || Board.pieces[X - 1, Y + 2].color != color)
                {
                    int[] move = new int[2];
                    move[0] = X - 1;
                    move[1] = Y + 2;
                    moves.Add(move);
                }
            }

            //Odrzucanie nielegalnych
            if (Board.SkipCheck == false)
                moves = Checks.SimulateCheckChecks(Board.pieces[X, Y], moves);
            return moves;
        }
    }
    class Bishop : Pieces
    {
        public Bishop(char color, int X, int Y) : base("Bishop", color, X, Y)
        {
            value = 3;
        }
        public override List<int[]> LegalMoves()
        {
            List<int[]> moves = new List<int[]>();

            int i = X + 1;
            int j = Y + 1;
            while (i < 8 && j < 8)
            {
                if ((string)Board.tiles[i, j].Tag == "occupied" && Board.pieces[i, j].color == color)
                    break;
                int[] move = new int[2];
                move[0] = i;
                move[1] = j;
                moves.Add(move);
                if ((string)Board.tiles[i, j].Tag == "occupied")
                    break;
                i++; j++;
            }
            i = X + 1;
            j = Y - 1;
            while (i < 8 && j >= 0)
            {
                if ((string)Board.tiles[i, j].Tag == "occupied" && Board.pieces[i, j].color == color)
                    break;
                int[] move = new int[2];
                move[0] = i;
                move[1] = j;
                moves.Add(move);
                if ((string)Board.tiles[i, j].Tag == "occupied")
                    break;
                i++; j--;
            }
            i = X - 1;
            j = Y - 1;
            while (i >= 0 && j >= 0)
            {
                if ((string)Board.tiles[i, j].Tag == "occupied" && Board.pieces[i, j].color == color)
                    break;
                int[] move = new int[2];
                move[0] = i;
                move[1] = j;
                moves.Add(move);
                if ((string)Board.tiles[i, j].Tag == "occupied")
                    break;
                i--; j--;
            }
            i = X - 1;
            j = Y + 1;
            while (i >= 0 && j < 8)
            {
                if ((string)Board.tiles[i, j].Tag == "occupied" && Board.pieces[i, j].color == color)
                    break;
                int[] move = new int[2];
                move[0] = i;
                move[1] = j;
                moves.Add(move);
                if ((string)Board.tiles[i, j].Tag == "occupied")
                    break;
                i--; j++;
            }
            //Odrzucanie nielegalnych
            if (Board.SkipCheck == false)
                moves = Checks.SimulateCheckChecks(Board.pieces[X, Y], moves);
            return moves;
        }
    }
    class King : Pieces
    {
        public King(char color, int X, int Y) : base("King", color, X, Y)
        {
            value = 999;
        }
        public override List<int[]> LegalMoves()
        {
            List<int[]> moves = new List<int[]>();

            int i = 0;
            int j = 0;
            if (Y + 1 > 7) j = Y;
            else j = Y + 1;
            while (j >= 0 && j >= Y - 1)
            {
                if (X - 1 < 0) i = X;
                else i = X - 1;
                while (i < 8 && i <= X + 1)
                {
                    if ((string)Board.tiles[i, j].Tag == "occupied")
                        if (Board.pieces[i, j].color == color)
                        { i++; continue; }
                    int[] move = new int[2];
                    move[0] = i;
                    move[1] = j;
                    moves.Add(move);
                    i++;
                }
                j--;
            }

            //Odrzucanie nielegalnych
            if (Board.SkipCheck == false)
                moves = Checks.SimulateCheckChecks(Board.pieces[X, Y], moves);
            //castling
            if (Board.SkipCheck == false && name == "King" && moved == false)
            {
                int[] move = new int[2];
                move = Castling();
                if (move[0] != 8 && move[1] != 8)
                    moves.Add(move);
            }
            return moves;
        }
        private int[] Castling()
        {
            int[] move = new int[2];
            move[0] = 8;
            move[1] = 8;
            if (color == 'W')
            {
                if ((string)Board.tiles[0, 0].Tag == "occupied")
                {
                    if (Board.pieces[0, 0].moved == false && (string)Board.tiles[1, 0].Tag == "empty"
                        && (string)Board.tiles[2, 0].Tag == "empty" && (string)Board.tiles[3, 0].Tag == "empty"
                        && Checks.CheckChecks(0, 0, 'B') == false && Checks.CheckChecks(1, 0, 'B') == false
                        && Checks.CheckChecks(2, 0, 'B') == false && Checks.CheckChecks(3, 0, 'B') == false
                        && Checks.CheckChecks(4, 0, 'B') == false)
                    {
                        move[0] = 2;
                        move[1] = 0;
                    }
                }
                if ((string)Board.tiles[7, 0].Tag == "occupied")
                {
                    if (Board.pieces[7, 0].moved == false && (string)Board.tiles[6, 0].Tag == "empty"
                        && (string)Board.tiles[5, 0].Tag == "empty" && Checks.CheckChecks(7, 0, 'B') == false
                        && Checks.CheckChecks(6, 0, 'B') == false && Checks.CheckChecks(5, 0, 'B') == false
                        && Checks.CheckChecks(4, 0, 'B') == false)
                    {
                        move[0] = 6;
                        move[1] = 0;
                    }
                }
            }
            else
            {
                if ((string)Board.tiles[0, 7].Tag == "occupied")
                {
                    if (Board.pieces[0, 7].moved == false && (string)Board.tiles[1, 7].Tag == "empty"
                        && (string)Board.tiles[2, 7].Tag == "empty" && (string)Board.tiles[3, 7].Tag == "empty"
                        && Checks.CheckChecks(0, 7, 'W') == false && Checks.CheckChecks(1, 7, 'W') == false
                        && Checks.CheckChecks(2, 7, 'W') == false && Checks.CheckChecks(3, 7, 'W') == false
                        && Checks.CheckChecks(4, 7, 'W') == false)
                    {
                        move[0] = 2;
                        move[1] = 7;
                    }
                }
                if ((string)Board.tiles[7, 7].Tag == "occupied")
                {
                    if (Board.pieces[7, 7].moved == false && (string)Board.tiles[6, 7].Tag == "empty"
                        && (string)Board.tiles[5, 7].Tag == "empty" && Checks.CheckChecks(7, 7, 'W') == false
                        && Checks.CheckChecks(6, 7, 'W') == false && Checks.CheckChecks(5, 7, 'W') == false
                        && Checks.CheckChecks(4, 7, 'W') == false)
                    {
                        move[0] = 6;
                        move[1] = 7;
                    }
                }
            }
            return move;
        }
    }
    class Queen : Pieces
    {
        public Queen(char color, int X, int Y) : base("Queen", color, X, Y)
        {
            value = 9;
        }
        public override List<int[]> LegalMoves()
        {
            List<int[]> moves = new List<int[]>();

            //Rook moves
            int i = X + 1;
            while (i < 8)
            {
                int[] move = new int[2];
                if ((string)Board.tiles[i, Y].Tag == "occupied" && Board.pieces[i, Y].color == color)
                    break;
                move[0] = i; //legalne X
                move[1] = Y; //legalne Y
                moves.Add(move);
                if ((string)Board.tiles[i, Y].Tag == "occupied")
                    break;
                i++;
            }
            i = X - 1;
            while (i >= 0)
            {
                int[] move = new int[2];
                if ((string)Board.tiles[i, Y].Tag == "occupied" && Board.pieces[i, Y].color == color)
                    break;
                move[0] = i;
                move[1] = Y;
                moves.Add(move);
                if ((string)Board.tiles[i, Y].Tag == "occupied")
                    break;
                i--;
            }
            i = Y + 1;
            while (i < 8)
            {
                int[] move = new int[2];
                if ((string)Board.tiles[X, i].Tag == "occupied" && Board.pieces[X, i].color == color)
                    break;
                move[0] = X;
                move[1] = i;
                moves.Add(move);
                if ((string)Board.tiles[X, i].Tag == "occupied")
                    break;
                i++;
            }
            i = Y - 1;
            while (i >= 0)
            {
                int[] move = new int[2];
                if ((string)Board.tiles[X, i].Tag == "occupied" && Board.pieces[X, i].color == color)
                    break;
                move[0] = X;
                move[1] = i;
                moves.Add(move);
                if ((string)Board.tiles[X, i].Tag == "occupied")
                    break;
                i--;
            }

            //Bishop moves
            i = X + 1;
            int j = Y + 1;
            while (i < 8 && j < 8)
            {
                if ((string)Board.tiles[i, j].Tag == "occupied" && Board.pieces[i, j].color == color)
                    break;
                int[] move = new int[2];
                move[0] = i;
                move[1] = j;
                moves.Add(move);
                if ((string)Board.tiles[i, j].Tag == "occupied")
                    break;
                i++; j++;
            }
            i = X + 1;
            j = Y - 1;
            while (i < 8 && j >= 0)
            {
                if ((string)Board.tiles[i, j].Tag == "occupied" && Board.pieces[i, j].color == color)
                    break;
                int[] move = new int[2];
                move[0] = i;
                move[1] = j;
                moves.Add(move);
                if ((string)Board.tiles[i, j].Tag == "occupied")
                    break;
                i++; j--;
            }
            i = X - 1;
            j = Y - 1;
            while (i >= 0 && j >= 0)
            {
                if ((string)Board.tiles[i, j].Tag == "occupied" && Board.pieces[i, j].color == color)
                    break;
                int[] move = new int[2];
                move[0] = i;
                move[1] = j;
                moves.Add(move);
                if ((string)Board.tiles[i, j].Tag == "occupied")
                    break;
                i--; j--;
            }
            i = X - 1;
            j = Y + 1;
            while (i >= 0 && j < 8)
            {
                if ((string)Board.tiles[i, j].Tag == "occupied" && Board.pieces[i, j].color == color)
                    break;
                int[] move = new int[2];
                move[0] = i;
                move[1] = j;
                moves.Add(move);
                if ((string)Board.tiles[i, j].Tag == "occupied")
                    break;
                i--; j++;
            }

            //Odrzucanie nielegalnych
            if (Board.SkipCheck == false)
                moves = Checks.SimulateCheckChecks(Board.pieces[X, Y], moves);
            return moves;
        }
    }
}