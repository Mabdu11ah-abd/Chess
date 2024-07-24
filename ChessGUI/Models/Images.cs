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
             {Pieces.white | Pieces.pawn , loadFromSource("Assets\\white-pawn.png") },
             {Pieces.white | Pieces.knight, loadFromSource("Assets/white-knight.png") },
             {Pieces.white | Pieces.bishop, loadFromSource("Assets/white-bishop.png") },
             {Pieces.white | Pieces.rook, loadFromSource("Assets/white-rook.png") },
             {Pieces.white | Pieces.queen, loadFromSource("Assets/white-queen.png") },
             {Pieces.white | Pieces.king, loadFromSource("Assets/white-king.png") },
             //black pieces
             {Pieces.black| Pieces.pawn, loadFromSource("Assets/black-pawn.png") },
             {Pieces.black| Pieces.knight, loadFromSource("Assets/black-knight.png") },
        };

        public static ImageSource loadFromSource(string source)
        {
            return new BitmapImage(new Uri(source, UriKind.Relative));
        }
        
    }
}
