using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGUI.Models
{
    public class Board
    {

        public Player[,] gameBoard = new Player[8, 8];
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

        }
        public void resetBoard()
        {

        }
        public void movePieceOnBoard(int r, int c)
        {

        }
        
    }
}
