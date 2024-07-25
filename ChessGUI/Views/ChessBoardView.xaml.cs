using ChessGUI.Models;
using ChessGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessGUI.Views
{
    /// <summary>
    /// Interaction logic for ChessBoardView.xaml
    /// </summary>
    public partial class ChessBoardView : UserControl
    {
        
        ChessBoardViewModel vm = new();
        public ChessBoardView()
        {
            InitializeComponent();
            DataContext = vm;
            double size = GameBoard.Width / 8;
            vm.SquareSize = size;
            DrawBoard();
            drawImage();
        }
        //Create CheckerBoardPattern
        private void DrawBoard()
        {
            var cellEven = this.Resources["color1"] as Brush;
            var cellOdd = this.Resources["color2"] as Brush;

            for (int i = 0; i < 8; i++)
            {
                for (global::System.Int32 j = 0; j < 8; j++)
                {
                    int boardIndex = i * 8 + j;

                    if (i % 2 == 0)
                        boardIndex++;

                    if (boardIndex % 2 == 0)
                    {
                        GameBoard.Children.Add(new Border { Background = cellEven });
                    }
                    else
                    {
                        GameBoard.Children.Add(new Border { Background = cellOdd });
                    }
                }
            }
        }

        //draw The Pieces at the start of the game
        private void drawImage()
        {
            for(int  r = 0; r < 8;r++) 
            {
                for (global::System.Int32 c = 0; c < 8; c++)
                {
                    Image image = vm.setUpImage(c,r, out bool isPiece);
                    if (isPiece)
                    {
                        Console.WriteLine("Adding image to the canvas");
                        PieceCanvas.Children.Add(image);
                    }
                }
            }
        }
        //manage left mouseEvents
        private void PieceCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(PieceCanvas);
            Image? image = e.Source as Image;

            if (image != null && PieceCanvas.CaptureMouse());
            vm.onMouseDown(pos, image);
        }

        private void PieceCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            vm.onMouseMove(e.GetPosition(PieceCanvas));
        }
        private void PieceCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            vm.onMouseUp(e.GetPosition(PieceCanvas));
            PieceCanvas.ReleaseMouseCapture();
        }

        
    }

}