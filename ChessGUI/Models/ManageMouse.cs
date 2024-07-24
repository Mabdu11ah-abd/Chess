using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ChessGUI.Models
{
    public class ManageMouse
    {

        private Image draggedImage;
        public Point mousePosition;

        public ManageMouse()
        {
        }

        public void onMouseUp()
        {
            if (draggedImage != null)
            {
                (int, int) coords = getRowAndCol(mousePosition);

                Debug.WriteLine(coords.ToString());
                
                Point point = SnapToCenter(coords.Item2 -1, coords.Item1 -1);

                Debug.WriteLine(point.ToString());

                Canvas.SetLeft(draggedImage, point.X);
                Canvas.SetTop(draggedImage, point.Y);
                Panel.SetZIndex(draggedImage, 1);
                draggedImage = null;
            }
        }
        public void OnMouseDown(Point position, Image image)
        {
            mousePosition = position;
            draggedImage = image;
        }
        public void OnMouseMove(Point position)
        {
            if (draggedImage != null)
            {
                var offset = position - mousePosition;
                mousePosition = position;
                Canvas.SetLeft(draggedImage, Canvas.GetLeft(draggedImage) + offset.X);
                Canvas.SetTop(draggedImage, Canvas.GetTop(draggedImage) + offset.Y);
            }
        }
        double squareSize = 62.5;
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
        public Point SnapToCenter(int r, int c)
        {
            

            return new Point(squareSize * r, squareSize * c);
        }
    }
}

