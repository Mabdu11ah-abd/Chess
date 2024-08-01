using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ChessGUI.Models
{
    public static class Pieces
    {
        public const int none = 0;
        public const int pawn = 1;
        public const int knight = 2;
        public const int bishop = 3;
        public const int rook = 4;
        public const int queen = 5;
        public const int king = 6;

        public const int white = 8;
        public const int black = 16;


        public static bool isFriendlyPiece(int currPiece, int encounteredPiece)
        {
            if (currPiece == none || encounteredPiece == none) return false;
            return (currPiece < black && encounteredPiece < black) || (currPiece >= black && encounteredPiece > black);

        }
        public static bool isEnemyPiece(int currPiece, int encounteredPiece)
        {
            if (currPiece == none || encounteredPiece == none) return false;
            return (currPiece < black && encounteredPiece >= black) || (currPiece >= black && encounteredPiece < black);
        }
        public static bool isRook(int piece)
        {
            if (piece == (rook | white) || piece == (rook | black))
            {
                return true;
            }
            return false;
        }
        public static bool isBishop(int piece)
        {
            if (piece == (bishop | white) || piece == (bishop | black))
            {
                return true;
            }
            return false;
        }
        public static bool isKnight(int piece)
        {
            if (piece == (knight | white) || piece == (knight | black))
            {
                return true;
            }
            return false;
        }
        public static bool isSliding(int piece)
        {
            int pieceType = piece & ~(white | black);
            if (pieceType == rook || pieceType == bishop || pieceType == queen || pieceType == king)
            {
                return true;
            }
            return false;
        }
        public static bool isPawn(int piece)
        {
            if (piece == (pawn | white) || piece == (pawn| black))
            {
                return true;
            }
            return false;
        }
        public static bool isKing(int piece)
        {
            if (piece == (king | white) || piece == (king | black))
            {
                return true;
            }
            return false;
        }
    }
}
