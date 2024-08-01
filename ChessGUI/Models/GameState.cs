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
        private readonly MoveLogic move = new MoveLogic();
        private readonly Board board = new Board();
        private readonly bool gameOver;
        
        public  bool currentPlayer; // true for white, false for black
        private int TurnsPassedSinceLastCapture { get; set; }
        //action delegates

        public event Action<int, int> DisplayMove;
        public event Action<GameResult> GameEnded;
        public event Action GameRestarted;
        
        //constructor
        public GameState()
        {
            board.defaultStart();
            TurnsPassedSinceLastCapture = 0;
            currentPlayer = true;
            gameOver = false;
        }
        
        
        
        //private methods of the class
        private void doesMoveEndGame()
        {

        }

        private void switchPlayer()
        {

        }
        private bool checkTurnsPassed() //returns true if the game is a draw due to 50 move rule
        {



            return true;
        }
        public int ReturnBoardPiece(int r, int c)
        {
            return board.returnPiece(r, c);
        }
        //public methods of the class
        public void MakeMove((int, int)StartSquare, (int, int)TargetSquare)
        {
            //if move is legal
            
            //if move is not legal 
        }



    }
}
