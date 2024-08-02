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
        }
        private void isCheckMate()
        {

        }
        //private methods of the class
        private void DoesMoveEndGame()
        {

        }
        private void switchPlayer()
        {
            currentPlayer = !currentPlayer;
        }
        private bool checkTurnsPassed(bool gameEnded) //returns true if the game is a draw due to 50 move rule
        {
            if(TurnsPassedSinceLastCapture > 50) 
            {
                return true;
            }
            return false;
        }

        //public methods of the class
        public int ReturnBoardPiece(int r, int c)
        {
            return board.returnPiece(r, c);
        }

        public void MakeMove(Move move)
        {
            List<Move> moveList = logic.returnLegalMoves(board, currentPlayer);

            //check for gameOver
            DoesMoveEndGame();


            if (moveList.Contains(move)) //if move is legal
            {
                board.movePieceOnBoard(move);
                DisplayMove?.Invoke();
                switchPlayer();
            }
            else
            {
                Console.WriteLine("Move Was Not legal");
            }
        }
    }
}
