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
        private readonly GameState gamestate;
        private readonly Move move;
        private readonly Board board;
        private double pieceSize { get; set; }
        public ManageMouse manageMouse=  new();
        //constructor
        public ChessBoardViewModel()
        {
            CellEvenColor = Colors.PeachPuff;
            CellOddColor = Colors.Red;
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
        public (int, int) getRowAndCol(Point position)
        {
            for (int i = 0; i < 8; i++)
            {
                for (global::System.Int32 j = 0; j < 8; j++)
                {
                    double X = j * squareSize;
                    double Y = i * squareSize;
                    int squareNum = i * 8 + j;

                    if (position.X < X && position.Y < Y)
                    {
                        return (i, j);
                    }
                }
            }
            return (0,0);
        }
        public Point SnapToCenter(int r, int c)
        {
            double Center = squareSize / 2;
            
            return new Point(Center * r, Center * c);
        }
    }
}
