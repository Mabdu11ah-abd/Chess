using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChessGUI.Models
{
    public class GameState
    {
        //private attributes of the class
        private readonly MoveLogic logic = new MoveLogic();
        private readonly bool gameOver;
        private int TurnsPassedSinceLastCapture { get; set; }
        private bool currentPlayer { get; set; }// true for white, false for black
        private Board board = new Board();
        private bool WhiteKingInCheck;
        private bool BlackKingInCheck;

        //action delegates

        public event Action DisplayMove;
        public event Action<GameResult> GameEnded;
        public event Action GameStarted;

        //constructor
        public GameState()
        {
            board.defaultStart();
            TurnsPassedSinceLastCapture = 0;
            currentPlayer = true;
            gameOver = false;
            WhiteKingInCheck = false;
            BlackKingInCheck = false;
        }
        private void isCheckMate()
        {

        }
        //private methods of the class
        private void DoesMoveEndGame()
        {
            List<Move> moveList = logic.returnLegalMoves(board, !currentPlayer);
            if (!moveList.Any()) 
            {
                if(!currentPlayer  && WhiteKingInCheck) 
                {
                    Console.WriteLine("Black wins by checkMate");
                }
                else if(currentPlayer && BlackKingInCheck)
                {
                    Console.WriteLine("White wins by checkmate");
                }
                else
                {       
                    Console.WriteLine("Game Ended in a draw");
                }

            }
        }
        private void switchPlayer()
        {
            currentPlayer = !currentPlayer;
        }
        private bool checkTurnsPassed(bool gameEnded) //returns true if the game is a draw due to 50 move rule
        {
            if (TurnsPassedSinceLastCapture > 50)
            {
                return true;
            }
            return false;
        }
        private void isMoved(Move move)
        {

            if (move.StartPosition == (0, 0))
            {
                logic.BlackQueenRookMoved = true;
                //black queenside rook moved
            }
            if (move.StartPosition == (0, 7))
            {
                logic.BlackKingRookMoved = true;
                //black Kingside rook moved
            }
            if (move.StartPosition == (0, 4))
            {
                logic.BlackKingMoved = true;
                logic.BlackCastled = true;
                //black Kingside rook moved
            }
            if (move.StartPosition == (7, 0))
            {
                logic.WhiteQueenRookMoved = true;
                //white queenside rook moved
            }
            if (move.StartPosition == (7, 7))
            {
                logic.WhiteKingRookMoved = true;
                logic.WhiteCastled = true;
                //white Kingside rook moved
            }
            if (move.StartPosition == (7, 4))
            {
                logic.WhiteKingMoved = true;
                //White King moved
            }

        }
        //public methods of the class
        public int ReturnBoardPiece(int r, int c)
        {
            return board.returnPiece(r, c);
        }

        public void MakeMove(Move move)
        {
            List<Move> moveList = logic.returnLegalMoves(board, currentPlayer);
            bool inCheck = currentPlayer ? WhiteKingInCheck : BlackKingInCheck;
            //check for gameOver
            if (moveList.Contains(move)) //if move is legal
            {
                bool castled = false;
                var CastleTarget = board.returnKingSquare(currentPlayer);
                CastleTarget.Item2 = 6;
                //lets first define the long castle and short castle move
                if (move.StartPosition == board.returnKingSquare(currentPlayer) &&
                    move.TargetPosition == CastleTarget && !inCheck)
                {
                    //move before castling
                    castled = logic.castleShort(board, currentPlayer);
                    logic.markCastled(currentPlayer);
                }
                CastleTarget.Item2 = 2;

                if (move.StartPosition == board.returnKingSquare(currentPlayer) &&
                    move.TargetPosition == CastleTarget && !inCheck)
                {
                    castled = logic.castleLong(board, currentPlayer);
                    logic.markCastled(currentPlayer);

                }
                if (!castled)
                {
                    if (currentPlayer)
                        WhiteKingInCheck = false;
                    else
                        BlackKingInCheck = false;

                    board.movePieceOnBoard(move);
                    DisplayMove?.Invoke();
                }
                isMoved(move);
            }
            else
            {
                Console.WriteLine("Move Was Not legal");
            }

            moveList = logic.generateMoves(board, currentPlayer);
            if (moveList.Any(response => response.TargetPosition == board.returnKingSquare(!currentPlayer)))
            {

                if (currentPlayer)
                {
                    BlackKingInCheck = true;
                }
                else
                    WhiteKingInCheck = true;
            }
            DoesMoveEndGame();
            switchPlayer();
        }
    }
}
