using ChessGUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChessGUI.ViewModels
{
    
    public class ChessBoardViewModel : BaseViewModel
    {
        private readonly GameState gamestate;
        private readonly Move move;
        private readonly Board board;
        

        //constructor
        public ChessBoardViewModel()
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


    }
}
