using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGUI.Models
{
        
    public struct Move
    {   
        public (int, int) StartPosition { get; set; }
        public (int, int) TargetPosition { get; set; }

        public Move((int, int) startPosition, (int, int) targetPosition)
        {
            StartPosition = startPosition;
            TargetPosition = targetPosition;
        }
        public override string ToString()
        {
            return StartPosition.ToString() + TargetPosition.ToString();
        }
    }   
        
    public class MoveLogic
    {   
        private bool LongCastlePossible { get; set; }
        private bool ShortCastlePossible { get; set; }
        private readonly Direction direction = new();
        private List<Move> moves;
        private bool enPassantPossible { get; set; }
        
        public MoveLogic ()
        {
            LongCastlePossible = false;
            ShortCastlePossible = false;
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
                            
                            generateSlidingMoves(piece, startPosition, board);
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
        private void generateSlidingMoves(int piece, (int, int) StartPosition, Board board)
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
            if (enPassantPossible && directionIndex == 0 || directionIndex == 3)
            {
                (int, int) EnPassantTarget = StartPosition;
                EnPassantTarget.Item1++; //move to east 

                //If east then add 
                if (Pieces.isEnemyPiece(piece, board.returnPiece(EnPassantTarget)) &&
                    Pieces.isPawn(board.returnPiece(EnPassantTarget)))
                {
                    (int, int) targetSquare = StartPosition;

                    targetSquare.Item2 += direction.PawnDirections[directionIndex + 1].Item2;
                    if (!exceedsBoundaries(targetSquare))
                        moves.Add(new Move(StartPosition, targetSquare));
                }
                //if west then add
                if (Pieces.isEnemyPiece(piece, board.returnPiece(EnPassantTarget)) &&
                    Pieces.isPawn(board.returnPiece(EnPassantTarget)))
                {
                    (int, int) targetSquare = StartPosition;
                    targetSquare.Item2 += direction.PawnDirections[directionIndex + 2].Item2;
                    if (!exceedsBoundaries(targetSquare))
                        moves.Add(new Move(StartPosition, targetSquare));
                }
                enPassantPossible = false;
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
                if (EnemyMoves.Any(response => response.TargetPosition == board.returnKingSquare(true)))
                {
                }
                else
                {
                    actualLegalMoves.Add(item);
                }
                board.redoMove();
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



        public void castleLong(Board board, bool white)
        {
            if(LongCastlePossible)
            {
                (int, int) RookSquare;
                RookSquare.Item1 = white ? 7 : 0;
                RookSquare.Item2 = 0;
                (int, int) KingSquare = board.returnKingSquare(white);
                (int, int) TargetSquare = KingSquare;
                TargetSquare.Item2 += 2;
                board.movePieceOnBoard(new Move(KingSquare, TargetSquare));
                KingSquare.Item2 -= 1;
                TargetSquare = KingSquare;
                board.movePieceOnBoard(new Move(KingSquare, TargetSquare));

            }
        }
        private void castleShort()
        {

        }
    }

}

