using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessGUI.Models
{
    public static class Images
    {
        public static Dictionary<int, ImageSource> pieceSources = new()
        {   
            //white images 
             {Pieces.white | Pieces.pawn , loadFromSource("pack://application:,,,/Assets/white-pawn.png") },
             {Pieces.white | Pieces.knight, loadFromSource("pack://application:,,,/Assets/white-knight.png") },
             {Pieces.white | Pieces.bishop, loadFromSource("pack://application:,,,/Assets/white-bishop.png") },
             {Pieces.white | Pieces.rook, loadFromSource("pack://application:,,,/Assets/white-rook.png") },
             {Pieces.white | Pieces.queen, loadFromSource("pack://application:,,,/Assets/white-queen.png") },
             {Pieces.white | Pieces.king, loadFromSource("pack://application:,,,/Assets/white-king.png") },
             //black pieces
             {Pieces.black| Pieces.pawn, loadFromSource("pack://application:,,,/Assets/black-pawn.png") },
             {Pieces.black| Pieces.knight, loadFromSource("pack://application:,,,/Assets/black-knight.png") },
             {Pieces.black| Pieces.bishop, loadFromSource("pack://application:,,,/Assets/black-bishop.png") },
             {Pieces.black| Pieces.rook, loadFromSource("pack://application:,,,/Assets/black-rook.png") },
             {Pieces.black| Pieces.queen, loadFromSource("pack://application:,,,/Assets/black-queen.png") },
             {Pieces.black| Pieces.king, loadFromSource("pack://application:,,,/Assets/black-king.png") },

        };

        public static ImageSource loadFromSource(string source)
        {
            return new BitmapImage(new Uri(source, UriKind.Absolute));
        }

        public static ImageSource getImage(int key)
        {
            return pieceSources[key];
        }
    }
}
