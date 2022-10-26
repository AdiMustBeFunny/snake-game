using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace SnakeGame.Helpers
{
    public class NavigationServiceProvider
    {
        public static NavigationService NavigationService => (Application.Current.MainWindow as MainWindow).mainFrame.NavigationService;
    }
}
