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
        public string startingFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        
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
            int i = 0;
            foreach (var character in FEN)
            {
                if (i > 8)
                    break;
                for (global::System.Int32 j = 0; j < 8; j++)
                {
                    if (character == '/')
                        break;

                    Squares[i, j] = FenToPiece[character];
                }
                i++;
            }
            for (int j = 0; j < 8; j++)
            {
                for (global::System.Int32 k = 0; k < 8; k++)
                {
                    Console.Write(Squares[j, k] + " ");
                }
                Console.WriteLine();
            }
        }   
        public void defaultStart()
        {
            ReadFen(startingFEN);
        }
        public void resetBoard()
        {

        }
        public void movePieceOnBoard(int r, int c)
        {

        }
        
    }
}
