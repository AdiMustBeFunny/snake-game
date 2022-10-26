using SnakeGame.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeGame.ViewModels
{
    public class GamePageViewModel
    {
        private readonly NavigationService _navigationService;
        
        public GamePageViewModel()
        {
            _navigationService = NavigationServiceProvider.NavigationService;
        }

        public Canvas _canvas { get; set; }
        public Rectangle player { get; set; }
        public ICommand GoBackComand => new RelayCommand(GoBack);
        public void GoBack()
        {
            //dispose of the game here
            _navigationService.GoBack();
        }

        public ICommand Key_W_PressedCommand => new RelayCommand(Key_W_Pressed);
        public ICommand Key_A_PressedCommand => new RelayCommand(Key_A_Pressed);
        public ICommand Key_S_PressedCommand => new RelayCommand(Key_S_Pressed);
        public ICommand Key_D_PressedCommand => new RelayCommand(Key_D_Pressed);
        public ICommand Key_Space_PressedCommand => new RelayCommand(Key_Space_Pressed);

        public void Key_W_Pressed()
        {
            Trace.WriteLine("W");
        }
        public void Key_A_Pressed()
        {
            Trace.WriteLine("A");
        }
        public void Key_S_Pressed()
        {
            Trace.WriteLine("S");
        }
        public void Key_D_Pressed()
        {
            Trace.WriteLine("D");
            Canvas.SetLeft(player, Canvas.GetLeft(player) + 32);

        }
        public void Key_Space_Pressed()
        {
            Trace.WriteLine("Space");
        }
    }
}
