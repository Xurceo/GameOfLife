using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife
{
    public class Cell
    {
        private double _width;
        private double _height;
        private double _left;
        private double _top;
        private bool _alive;

        public bool Alive
        {
            get => _alive;
            set
            {
                _alive = value;
                Rectangle.Fill = _alive ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
            }
        }

        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                Canvas.SetLeft(Rectangle, value);
            }
        }
        public double Top
        {
            get => _top;
            set
            {
                _top = value;
                Canvas.SetTop(Rectangle, value);
            }
        }
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                Rectangle.Width = value;
            }
        }
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                Rectangle.Height = value;
            }
        }
        private Rectangle Rectangle { get; set; }

        public Cell(double left, double top, double width, double height, Canvas canvas)
        {
            Rectangle = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(Colors.Black),
            };

            Left = left;
            Top = top;
            Width = width;
            Height = height;

            Canvas.SetLeft(Rectangle, left);
            Canvas.SetTop(Rectangle, top);

            canvas.Children.Add(Rectangle);

            Rectangle.MouseLeftButtonDown += (s, e) => OnClick();
        }

        private void OnClick()
        {
            Alive = !Alive;
        } 
    }
}
