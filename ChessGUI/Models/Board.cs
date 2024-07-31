using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGUI.Models
{
    public class Board
    {

        public int[,] Squares = new int[8, 8];
        private string startingFEN = "8/8/8/8/2K5/8/8/8";

        private Dictionary<char, int> FenToPiece = new()
        {
            {'p', Pieces.black | Pieces.pawn},
            {'n', Pieces.black | Pieces.knight},
            {'b', Pieces.black | Pieces.bishop},
            {'r', Pieces.black |Pieces.rook},
            {'q', Pieces.black | Pieces.queen},
            {'k', Pieces.black | Pieces.king},

            {'P', Pieces.white | Pieces.pawn},
            {'N', Pieces.white | Pieces.knight},
            {'B', Pieces.white | Pieces.bishop},
            {'R', Pieces.white | Pieces.rook},
            {'Q', Pieces.white | Pieces.queen},
            {'K', Pieces.white | Pieces.king}
        };


        public void ReadFen(string FEN)
        {
            Console.WriteLine("Reading FEN");
            int row = 0, col = 0;
            foreach (var character in FEN)
            {
                if (character == '/')
                {
                    col = 0;
                    row++;
                }
                else
                {
                    if (char.IsDigit(character))
                    {
                        col += (int)char.GetNumericValue(character);
                    }
                    else
                    {
                        Squares[row, col] = FenToPiece[character];
                        col++;
                    }
                }
            }
        }
        public void defaultStart()
        {
            ReadFen(startingFEN);
        }
        public void resetBoard()
        {

        }
        public void movePieceOnBoard(Move move)
        {
            
        }
        public int returnPiece((int, int) targetSquare)
        {
            return Squares[targetSquare.Item1, targetSquare.Item2];
        }
        public int returnPiece(int r, int c)
        {
            return Squares[r, c];
        }
    }
}
