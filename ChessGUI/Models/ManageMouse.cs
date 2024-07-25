using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ChessGUI.Models
{
    public static class ManageMouse
    {

        private static Image? draggedImage;
        private static Point mousePosition;

        public static void onMouseUp(Point point)
        {
            if (draggedImage != null)
            {
                //set the position of the image on the canvas
                Canvas.SetLeft(draggedImage, point.X);
                Canvas.SetTop(draggedImage, point.Y);   

                //set the z index of the image 
                Panel.SetZIndex(draggedImage, 1);
                draggedImage = null;
            }
        }
        public static void OnMouseDown(Point position, Image image)
        {
            mousePosition = position;
            draggedImage = image;
        }
        public static void OnMouseMove(Point position)
        {
            if (draggedImage != null)
            {
                var offset = position - mousePosition;
                mousePosition = position;
                Canvas.SetLeft(draggedImage, Canvas.GetLeft(draggedImage) + offset.X);
                Canvas.SetTop(draggedImage, Canvas.GetTop(draggedImage) + offset.Y);
            }
        }       
    }
}

