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
        private readonly GameState gamestate;
        private readonly Move move;
        private readonly Board board = new();
        private double pieceSize { get; set; }

        //constructor
        public ChessBoardViewModel()
        {
            Console.WriteLine("view model constructor called");
            CellEvenColor = Colors.PeachPuff;
            CellOddColor = Colors.Red;
            board.defaultStart();
        }
        public ChessBoardViewModel(int squareSize)
        {
            CellEvenColor = Colors.PeachPuff;
            CellOddColor = Colors.Red;
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

        public void onMouseDown(Point position, Image image)
        {
            ManageMouse.OnMouseDown(position, image);
        }
        public void onMouseUp(Point position)
        {
            (int, int) Coords = getRowAndCol(position);
            Point point = SnapToCenter(Coords.Item2 - 1, Coords.Item1 - 1);

            ManageMouse.onMouseUp(point);
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
