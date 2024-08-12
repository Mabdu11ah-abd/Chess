using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChessGUI.Models
{
    public class ImageControl
    {
        public ImageSource Source { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }

        public ImageControl(ImageSource source, double left, double right)
        {
            Source = source;
            Left = left;
            this.Top = right;
        }
    }
}
