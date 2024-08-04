using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChessGUI.Models
{

    public struct Move
    {
        public (int, int) StartPosition;
        public (int, int) TargetPosition;

        public Move((int, int) startPosition, (int, int) targetPosition)
        {
            StartPosition = startPosition;
            TargetPosition = targetPosition;
        }
        public override bool Equals(object obj)
        {
            if (obj is Move otherMove)
            {
                return StartPosition == otherMove.StartPosition && TargetPosition == otherMove.TargetPosition;
            }
            return false;
        }
        public override string ToString()
        {
            return StartPosition.ToString() + TargetPosition.ToString();
        }
    }

    public class MoveLogic
    {
        public bool BlackCastled = false;
        public bool WhiteCastled = false;
        private bool HasCastled(bool white)
        {
            if (white && !WhiteCastled)
            {
                return false;
            }
            if (!white && !BlackCastled)
            {
                return false;
            }
            return true;
        }
        public void markCastled(bool white)
        {
            if (white)
            {
                WhiteCastled = true;
            }
            else
                BlackCastled = true;
        }

        private readonly Direction direction = new();

        //private bools for castling pieces moved
        public bool WhiteQueenRookMoved = false;
        public bool WhiteKingRookMoved = false;
        public bool WhiteKingMoved = false;

        public bool BlackKingMoved = false;
        public bool BlackQueenRookMoved = false;
        public bool BlackKingRookMoved = false;
        //methods to check if castling is possible
        public (int, int) PrevPawnMovedFinalPosition { get; set; }
        private bool CheckLongCastlePossibility(bool white)
        {
            if (white)
            {
                if (!(WhiteQueenRookMoved && WhiteKingMoved))
                    return true;
            }
            else
            {
                if (!(BlackKingMoved && BlackQueenRookMoved))
                    return true;
            }
            return false;
        }
        private bool CheckShortCastlePossibility(bool white)
        {
            if (white)
            {
                if (!(WhiteKingRookMoved || WhiteKingMoved))
                    return true;
            }
            else
            {
                if (!(BlackKingMoved || BlackKingRookMoved))
                    return true;
            }
            return false;
        }
        //logic for generating moves 
        private List<Move> moves;
        public List<Move> EnpassantMoves;
        public List<Move> CheckThreefoldRepitition;
        public bool enPassantPossible = false;
        public MoveLogic()
        {
            WhiteCastled = false;
            BlackCastled = false;
            WhiteKingMoved = false;
            PrevPawnMovedFinalPosition = (-1, -1);
            EnpassantMoves = new();
        }
        public List<Move> generateMoves(Board board, bool ColorToMove)
        {
            moves = new List<Move>();
            for (int r = 0; r < 8; r++)
            {
                for (global::System.Int32 c = 0; c < 8; c++)
                {
                    int piece = board.returnPiece(r, c);
                    bool Color = FindPieceColor(piece);

                    if (Color == ColorToMove)
                    {
                        (int, int) startPosition = (r, c);
                        if (board.returnPiece(startPosition) == Pieces.none)
                            continue;
                        //generateKnightMoves
                        if (Pieces.isKnight(piece))
                        {
                            generateKnightMoves(piece, startPosition, board);
                        }
                        //generate sliding piece moves
                        if (Pieces.isSliding(piece))
                        {

                            generateSlidingMoves(piece, startPosition, board, ColorToMove);
                        }
                        //generate PawnMoves
                        if (Pieces.isPawn(piece))
                        {
                            generatePawnMoves(piece, startPosition, board);
                        }
                    }
                }
            }
            return moves;
        }
        private void generateSlidingMoves(int piece, (int, int) StartPosition, Board board, bool white)
        {
            int startSquare = StartPosition.Item1 * 8 + StartPosition.Item2;
            int limit = Pieces.isKing(piece) ? 1 : 8;

            for (int directionIndex = 0; directionIndex < 8; directionIndex++)
            {

                //conditions for rooks and bishops
                if (Pieces.isRook(piece) && directionIndex == 4) break;
                if (Pieces.isBishop(piece) && directionIndex == 0) directionIndex += 4;

                (int, int) targetSquare = StartPosition;
                for (int n = 0; n < limit; n++)
                {
                    //incrementing the value of target square by direction index
                    targetSquare.Item1 += direction.SlidingDirections[directionIndex].Item1;
                    targetSquare.Item2 += direction.SlidingDirections[directionIndex].Item2;
                    if (exceedsBoundaries(targetSquare))
                        break;

                    //if friendly piece encountered
                    if (Pieces.isFriendlyPiece(piece, board.returnPiece(targetSquare)))
                        break;

                    //if enemy piece encountered
                    if (Pieces.isEnemyPiece(piece, board.returnPiece(targetSquare)))
                    {
                        moves.Add(new Move(StartPosition, targetSquare));
                        break;
                    }
                    //else add the move
                    moves.Add(new Move(StartPosition, targetSquare));
                }
            }

            // Logic for Castling
            if (limit == 1 && !HasCastled(white))
            {
                (int, int) TargetSquare = StartPosition;
                //long Castle to the right
                bool NoObstructionRight = true;
                for (int i = 1; i <= 2; i++)
                {
                    TargetSquare = (StartPosition.Item1, StartPosition.Item2 + i);
                    if (!exceedsBoundaries(TargetSquare))
                        if (Pieces.isFriendlyPiece(piece, board.returnPiece(TargetSquare)) || Pieces.isEnemyPiece(piece, board.returnPiece(TargetSquare)))
                        {
                            NoObstructionRight = false;
                            break;
                        }
                }
                if (NoObstructionRight)
                    moves.Add(new Move(StartPosition, (StartPosition.Item1, StartPosition.Item2 + 2)));

                //long Castle to the left
                TargetSquare = StartPosition;
                bool NoObstructionLeft = true;
                for (int i = 1; i <= 2; i++)
                {
                    TargetSquare = (StartPosition.Item1, StartPosition.Item2 - i);
                    if (!exceedsBoundaries(TargetSquare))
                        if (Pieces.isFriendlyPiece(piece, board.returnPiece(TargetSquare)) || Pieces.isEnemyPiece(piece, board.returnPiece(TargetSquare)))
                        {
                            NoObstructionLeft = false;
                            break;
                        }
                }
                if (NoObstructionLeft)
                    moves.Add(new Move(StartPosition, (StartPosition.Item1, StartPosition.Item2 - 2)));
            }


        }
        private void generateKnightMoves(int piece, (int, int) StartPosition, Board board)
        {
            for (int directionIndex = 0; directionIndex < 8; directionIndex++)
            {
                (int, int) targetSquare = StartPosition;

                targetSquare.Item1 += direction.KnightDirections[directionIndex].Item1;
                targetSquare.Item2 += direction.KnightDirections[directionIndex].Item2;

                if (exceedsBoundaries(targetSquare))
                    continue;

                if (Pieces.isFriendlyPiece(piece, board.returnPiece(targetSquare)))
                    continue;

                moves.Add(new Move(StartPosition, targetSquare));
            }
        }
        private void generatePawnMoves(int piece, (int, int) StartPosition, Board board)
        {

            int directionIndex = 0;
            if (piece > 16)
                directionIndex += 3;


            (int, int) targetSquareofRegularMove = StartPosition;

            targetSquareofRegularMove.Item1 += direction.PawnDirections[directionIndex].Item1;
            if (!exceedsBoundaries(targetSquareofRegularMove))
                moves.Add(new Move(StartPosition, targetSquareofRegularMove));

            //enPassant enPassant possible is true and enemy piece located on left or right then capture 
            if (enPassantPossible)
            {
                bool EnPassantPawn = false;
                var newTarget = StartPosition;
                newTarget.Item2++;
                if (newTarget == PrevPawnMovedFinalPosition)
                    EnPassantPawn = true;
                newTarget.Item2 -= 2;
                if (newTarget == PrevPawnMovedFinalPosition)
                    EnPassantPawn = true;
                
                if (EnPassantPawn)
                {
                    newTarget= PrevPawnMovedFinalPosition;
                    newTarget.Item1 += (piece > 16) ? 1 : -1;
                    var tempMove = new Move(StartPosition, newTarget);
                    moves.Add(tempMove);
                    EnpassantMoves.Add(tempMove);
                }
            }
            //located on 2nd and 7th rank
            if (StartPosition.Item1 == 1 || StartPosition.Item1 == 6)
            {
                (int, int) targetSquare = StartPosition;
                targetSquare.Item1 += (piece > 16) ? 2 : -2;

                if (!exceedsBoundaries(targetSquare))
                    moves.Add(new Move(StartPosition, targetSquare));
            }

            //capture enemy piece 
            (int, int) EnemySquare = StartPosition;
            EnemySquare.Item1 += direction.PawnDirections[directionIndex + 1].Item1;
            EnemySquare.Item2 += direction.PawnDirections[directionIndex + 1].Item2;
            if (!exceedsBoundaries(EnemySquare))
                if (Pieces.isEnemyPiece(piece, board.returnPiece(EnemySquare)))
                {
                    moves.Add(new Move(StartPosition, EnemySquare));
                }
            EnemySquare.Item2 += direction.PawnDirections[directionIndex + 2].Item2 * 2;

            if (!exceedsBoundaries(EnemySquare))
                if (Pieces.isEnemyPiece(piece, board.returnPiece(EnemySquare)) && !exceedsBoundaries(EnemySquare))
                {
                    moves.Add(new Move(StartPosition, EnemySquare));
                }
        }
        public List<Move> returnLegalMoves(Board board, bool color)
        {
            var pseudoLegalMoves = generateMoves(board, color);
            var actualLegalMoves = new List<Move>();
            foreach (var item in pseudoLegalMoves)
            {
                board.movePieceOnBoard(item);
                var EnemyMoves = generateMoves(board, !color);
                if (EnemyMoves.Any(response => response.TargetPosition == board.returnKingSquare(color)))
                {

                }
                else
                {
                    actualLegalMoves.Add(item);
                }
                board.redoMove();
            }
            //logic to verify castling

            var kingStartPosition = board.returnKingSquare(color);
            var kingLeftMove = new Move(kingStartPosition, (kingStartPosition.Item1, kingStartPosition.Item2 - 1));
            var kingRightMove = new Move(kingStartPosition, (kingStartPosition.Item1, kingStartPosition.Item2 + 1));
            if (!actualLegalMoves.Any(m => m.Equals(kingLeftMove)))
            {
                var CastleTarget = (kingStartPosition.Item1, kingStartPosition.Item2 - 2);
                var castlingMove = new Move(kingStartPosition, CastleTarget);
                actualLegalMoves.Remove(castlingMove);
            }

            if (!actualLegalMoves.Any(m => m.Equals(kingLeftMove)))
            {

                var CastleTarget = kingRightMove.TargetPosition;
                CastleTarget.Item2 += 2;
                actualLegalMoves.Remove(new Move(kingStartPosition, CastleTarget));

            }
            return actualLegalMoves;
        }
        private bool exceedsBoundaries((int, int) targetSquare)
        {
            if ((targetSquare.Item1 > 7 || targetSquare.Item2 > 7) || (targetSquare.Item1 < 0 || targetSquare.Item2 < 0))
                return true;
            else
                return false;
        }
        private bool FindPieceColor(int piece)
        {
            return piece < 16 ? true : false;
        }

        public bool castleLong(Board board, bool white)
        {
            if (CheckLongCastlePossibility(white))
            {
                (int, int) RookSquare;
                RookSquare.Item1 = white ? 7 : 0;
                RookSquare.Item2 = 0;
                Console.WriteLine(RookSquare);
                (int, int) KingSquare = board.returnKingSquare(white);
                (int, int) TargetSquare = KingSquare;
                TargetSquare.Item2 -= 2;
                board.movePieceOnBoard(new Move(KingSquare, TargetSquare));
                KingSquare.Item2 += 1;
                TargetSquare.Item2 = 3;
                board.movePieceOnBoard(new Move(RookSquare, TargetSquare));
                return true;
            }
            return false;
        }
        public bool castleShort(Board board, bool white)
        {
            if (CheckShortCastlePossibility(white))
            {
                (int, int) RookSquare;
                RookSquare.Item1 = white ? 7 : 0;
                RookSquare.Item2 = 7;
                (int, int) KingSquare = board.returnKingSquare(white);

                (int, int) TargetSquare = KingSquare;
                TargetSquare.Item2 += 2;
                board.movePieceOnBoard(new Move(KingSquare, TargetSquare));
                TargetSquare.Item2 = 5;
                board.movePieceOnBoard(new Move(RookSquare, TargetSquare));
                return true;
            }
            return false;
        }
    }

}

