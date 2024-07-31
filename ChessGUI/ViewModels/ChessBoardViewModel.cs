using ChessGUI.Core;
using ChessGUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChessGUI.ViewModels
{
    
    public class ChessBoardViewModel : BaseViewModel
    {   
        // objects of the class
        private readonly GameState gamestate = new GameState();
        private readonly MoveLogic logic = new MoveLogic();
        private readonly Board board = new();
        private double pieceSize { get; set; }

        List<Move> moves = new List<Move>();
        //constructor
        public ChessBoardViewModel()
        {
            Console.WriteLine("view model constructor called");
            CellEvenColor = Colors.LimeGreen;
            CellOddColor = Colors.WhiteSmoke;
            board.defaultStart();
        }
        public ChessBoardViewModel(int squareSize)
        {
            CellEvenColor = Colors.LightGreen;
            CellOddColor = Colors.WhiteSmoke;
        }
        //binding properties 
        private Color cellEvenColor;
        public Color CellEvenColor
        {
            get { return cellEvenColor; }
            set { cellEvenColor = value; OnPropertyChanged(); }
        }

        private Color cellOddColor;
        public Color CellOddColor
        {
            get { return cellOddColor; }
            set { cellOddColor = value; OnPropertyChanged(); }
        }

        private double squareSize;
        public double SquareSize
        {
            get { return squareSize; }
            set { squareSize = value; }
        }
        //makeMoveMethods
        private void isMoveValid()
        {

        }
        private void OnMoveMade()
        {

        }
        private (int,int) StartPos { get; set; }
        private (int,int) EndPos { get; set; }
        //mouse methods
        public void onMouseDown(Point position, Image image)
        {
            ManageMouse.OnMouseDown(position, image);
            StartPos = getRowAndCol(position);
            moves = logic.generateMoves(board, gamestate.currentPlayer);
        }
        public void onMouseUp(Point position)
        {   
            EndPos = getRowAndCol(position);
            
            //if move is legal 
            if(true)
            {
                Point point = SnapToCenter(EndPos.Item2 - 1, EndPos.Item1 - 1);
                ManageMouse.onMouseUp(point);

            }
            else
            {
                Point point = SnapToCenter(StartPos.Item2 - 1, StartPos.Item1 - 1);
                ManageMouse.onMouseUp(point);
            }
            //if move is illegal 

        }
        public void onMouseMove(Point position)
        {
            ManageMouse.OnMouseMove(position);
        }
        public Image setUpImage(int r, int c, out bool isPiece)
        {
            if (board.Squares[c, r] == 0)
            { 
                isPiece = false;
                return   null;
            }
            Point coords = SnapToCenter(r, c);
            Image image = new Image { Source = Images.getImage(board.Squares[c, r]) };

            image.Width = squareSize;
            image.Height = squareSize;
            image.Stretch = Stretch.Uniform;

            Canvas.SetLeft(image, coords.X);
            Canvas.SetTop(image, coords.Y);

            isPiece = true;
            return image;
        }
        //positioning methods
        public (int, int) getRowAndCol(Point position)
        {
            for (int i = 1; i <= 8; i++)
            {
                for (global::System.Int32 j = 1; j <= 8; j++)
                {
                    double X = j * squareSize;
                    double Y = i * squareSize;
                    int squareNum = i * 8 + j;

                    if (position.X < X && position.Y < Y)
                    {
                        Debug.WriteLine(i + " " + j);
                        return (i, j);
                    }
                }
            }
            return (0, 0);
        }
        private Point SnapToCenter(int r, int c)
        {
            return new Point(squareSize * r, squareSize * c);
        }
    }
}
