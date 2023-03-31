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
    internal class Checks : Board
    {
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
        public static bool IsItCheckmate() // is it checkmate?
        {
            bool isIt = true;
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
    }
}
