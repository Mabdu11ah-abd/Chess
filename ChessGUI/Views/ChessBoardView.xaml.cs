using ChessGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public ChessBoardView()
        {
            InitializeComponent();
         
            DataContext = new ChessBoardViewModel();
            drawBoard();
        }
        private void drawBoard()
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
        private void drawOnCanvas()
        {

        }
        private void startBoard()
        {

        }
    }
    
}
