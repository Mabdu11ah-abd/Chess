using ChessGUI.Core;
using ChessGUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private double pieceSize { get; set; }
        //constructor
        public ChessBoardViewModel()
        {
            colorCells = new ObservableCollection<Brush>();
            CheckerBoardPattern();

            gamestate.DisplayMove += OnMoveMade;
            gamestate.GameStarted += OnGameStart;

        }
        public ChessBoardViewModel(ObservableCollection<Brush> temp)
        {
            colorCells = temp;
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
        private ObservableCollection<Brush> colorCells;
        public ObservableCollection<Brush> ColorCells
        {
            get { return colorCells;}
            set { colorCells = value; OnPropertyChanged(); }
        }
        private double squareSize;
        public double SquareSize
        {
            get { return squareSize; }
            set { squareSize = value; }
        }
        public void CheckerBoardPattern()
        {
            ColorCells.Clear();
            for (int r = 0; r < 8; r++)
            {
                for (global::System.Int32 c = 0; c < 8; c++)
                {
                    ColorCells.Add(((r + c) % 2 == 0) ? Brushes.White : Brushes.LightGreen);
                }
            }
        }
        //makeMoveMethods
        private void OnGameStart()
        {

        }
        private (int,int) StartPos { get; set; }
        private (int,int) EndPos { get; set; }
        //mouse methods
        public void onMouseDown(Point position, Image image)
        {
            ManageMouse.OnMouseDown(position, image);
            StartPos = getRowAndCol(position);
        }
        private void OnMoveMade()
        {

        }
        public void onMouseUp(Point position)
        {   

            EndPos = getRowAndCol(position);
            Move move = new Move(StartPos, EndPos);
            gamestate.MakeMove(move);
            
            //if move is legal 
            Point point = SnapToCenter(EndPos.Item2 - 1, EndPos.Item1 - 1);
            ManageMouse.onMouseUp(point);
        }
        public void onMouseMove(Point position)
        {
            ManageMouse.OnMouseMove(position);
        }
        public Image setUpImage(int r, int c, out bool isPiece)
        {
            if (gamestate.ReturnBoardPiece(c,r) == 0)
            { 
                isPiece = false;
                return null;
            }
            Point coords = SnapToCenter(r, c);
            Image image = new Image { Source = Images.getImage(gamestate.ReturnBoardPiece(c, r))};

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
                        return (i-1, j-1);
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
