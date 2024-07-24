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
            Console.WriteLine(GameBoard.Width + "GameBoardWidth " + size);
            var image = new Image();
            image.Source = Images.getImage(9);
            image.Height = size;
            image.Width = size;
            image.Stretch = Stretch.Uniform;
            Canvas.SetLeft(image, 10);
            Canvas.SetTop(image, 10);
            PieceCanvas.Children.Add(image);
            DrawBoard();
            
        }
        //Create 
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
                        Debug.WriteLine(boardIndex);
                        GameBoard.Children.Add(new Border { Background = cellOdd });
                    }
                }
            }
        }
        private void drawImage()
        {

        }
        //manage left mouseEvents
        private void PieceCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(PieceCanvas);

            if (e.Source is Image image && PieceCanvas.CaptureMouse())
                vm.manageMouse.OnMouseDown(pos, image);
        }

        private void PieceCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            vm.manageMouse.OnMouseMove(e.GetPosition(PieceCanvas));
        }

        private void PieceCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (int, int) position = vm.getRowAndCol(e.GetPosition(PieceCanvas));
            PieceCanvas.ReleaseMouseCapture();
            vm.manageMouse.onMouseUp();
        }

        
    }

}