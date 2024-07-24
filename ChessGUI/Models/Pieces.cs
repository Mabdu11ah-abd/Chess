using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
    }
}
