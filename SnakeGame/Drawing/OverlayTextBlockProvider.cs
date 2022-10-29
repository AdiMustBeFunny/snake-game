using SnakeGame.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SnakeGame.Drawing
{
    public class OverlayTextBlockProvider
    {
        public static TextBlock Get(string text)
        {
            var textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.FontSize = 50;
            textBlock.Foreground = new SolidColorBrush(Colors.AntiqueWhite);

            textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            textBlock.TextAlignment = TextAlignment.Center;
            var size = textBlock.DesiredSize;
            Canvas.SetLeft(textBlock, GameSettings.GameWidth / 2 - size.Width / 2);
            Canvas.SetTop(textBlock, GameSettings.GameHeight / 2 - size.Height / 2);
            Canvas.SetZIndex(textBlock, GameSettings.OverlayTextZIndex);

            return textBlock;
        }
    }
}
