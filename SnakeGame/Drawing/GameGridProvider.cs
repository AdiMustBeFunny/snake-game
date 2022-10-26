using SnakeGame.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.Drawing
{
    public class GameGridProvider
    {
        public static List<UIElement> Get()
        {
            var list = new List<UIElement>();
            var gameWidth = GameSettings.GameWidth;
            var gameHeight = GameSettings.GameHeight;
            var tileSize = GameSettings.TileSize;
            var gridZIndex = GameSettings.GridZIndex;


            for (int i = 0; i <= gameWidth; i++)
            {
                var line = new Line();
                line.Stroke = Brushes.Black;
                line.X1 = i * tileSize;
                line.X2 = i * tileSize;
                line.Y1 = 0;
                line.Y2 = gameHeight - 1;
                line.HorizontalAlignment = HorizontalAlignment.Left;
                line.VerticalAlignment = VerticalAlignment.Center;
                line.StrokeThickness = 1;
                Canvas.SetZIndex(line, gridZIndex);

                list.Add(line);
            }

            for (int i = 0; i <= gameHeight; i++)
            {
                var line = new Line();
                line.Stroke = Brushes.Black;
                line.X1 = 0;
                line.X2 = gameWidth - 1;
                line.Y1 = i * tileSize;
                line.Y2 = i * tileSize;
                line.HorizontalAlignment = HorizontalAlignment.Left;
                line.VerticalAlignment = VerticalAlignment.Center;
                line.StrokeThickness = 1;
                Canvas.SetZIndex(line, gridZIndex);

                list.Add(line);
            }

            return list;
        }
    }
}
