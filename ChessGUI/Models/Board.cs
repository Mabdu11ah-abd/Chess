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
        private string startingFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        private Move prevMove { get; set; }
        private int prevTarget { get; set; }
        private int preStart { get; set; }
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
            (int, int) Start = move.StartPosition;
            (int, int) Target = move.TargetPosition;
            prevMove = move;
            prevTarget = returnPiece(Target);
            preStart = returnPiece(Start);
            Squares[Target.Item1, Target.Item2] = returnPiece(Start);
            Squares[Start.Item1, Start.Item2] = 0;
        }

        public (int, int) returnKingSquare(bool isWhite)
        {
            int color = isWhite ? Pieces.white : Pieces.black;
            for (int i = 0; i < 8; i++)
            {
                for (global::System.Int32 j = 0; j < 8; j++)
                {
                    if (Squares[i, j] == (color | Pieces.king))
                        return (i, j);
                }
            }
            return (-1, -1);
        }
        public void redoMove()
        {
            (int, int) Start = prevMove.StartPosition;
            (int, int) Target = prevMove.TargetPosition;

            Squares[Target.Item1, Target.Item2] = prevTarget;
            Squares[Start.Item1, Start.Item2] = preStart;
        }

        public int returnPiece((int, int) targetSquare)
        {
            return Squares[targetSquare.Item1, targetSquare.Item2];
        }
        public void setPieceZero((int, int) target)
        {
            Squares[target.Item1, target.Item2] = 0;
        }
        public int returnPiece(int r, int c)
        {
            return Squares[r, c];
        }
        public string returnFEN()
        {

            string fen = "";
            int row = 0, col = 0, count = 0;
            while (true)
            {
                if (col > 7)
                {
                    row++;
                    if (row > 7)
                    {
                        break;
                    }
                    fen += '/';
                    col = 0;
                }

                if (Squares[row, col] == 0)
                {
                    col++;
                    count++;
                }
                else
                {
                    if (count != 0)
                        fen += Convert.ToChar(count);
                    count = 0;
                    char piece = FenToPiece.FirstOrDefault(x => x.Value == Squares[row, col]).Key;
                    fen += piece;
                    col++;
                }
            }
            return fen;

        }
    }
}
