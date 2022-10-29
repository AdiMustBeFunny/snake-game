using SnakeGame.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.Drawing
{
    public class TileBuilder
    {
        public static Rectangle Build(int x, int y, SolidColorBrush brush, int width = 32, int height = 32, int zIndex = GameSettings.GameObjectZIndex)
        {
            var rect = new Rectangle();
            rect.Fill = brush;
            rect.Width = width;
            rect.Height = height;
            Canvas.SetLeft(rect, x * width);
            Canvas.SetTop(rect, y * height);
            Canvas.SetZIndex(rect, GameSettings.GameObjectZIndex);
            return rect;
        }
    }
}
