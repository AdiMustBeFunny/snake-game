using SnakeGame.Drawing;
using SnakeGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeGame.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public GamePage()
        {
            InitializeComponent();

            var grid = GameGridProvider.Get();

            foreach(var line in grid)
            {
                gameCanvas.Children.Add(line);
            }

            var rec = new Rectangle();
            rec.Stroke = Brushes.LightSteelBlue;
            rec.Width = 32;
            rec.Height = 32;
            rec.StrokeThickness = 1;
            rec.Fill = Brushes.LightSteelBlue;

            Canvas.SetZIndex(rec, 11);
            Canvas.SetLeft(rec, 0);
            Canvas.SetTop(rec, 32);
            gameCanvas.Children.Add(rec);
            //CompositionTarget.Rendering += Loop;

            var vm = DataContext as GamePageViewModel;
            vm._canvas = gameCanvas;
            vm.player = rec;
            gameCanvas.Focus();

        }

        void Loop(object sender, EventArgs e)
        {
            Trace.WriteLine("hi");
        }
    }
}
