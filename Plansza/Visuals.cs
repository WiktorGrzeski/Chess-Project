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
    internal class Visuals : Board
    {
        public static void ShowMoves(List<int[]> moves) //Wyświetla dostępne ruchy w postaci kropek i zmiany koloru na planszy
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
        public static void HideMoves(List<int[]> moves) //Ukrywa ruchy wyświetlone przez showMoves
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
        
        public static void LastMove(int firstX, int firstY, int x, int y) //Podświetla pola na których wydarzył się ostatni ruch
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
        public static void BoardFlip() //Odwraca plansze dla gracza czarnych 
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
    }
}
