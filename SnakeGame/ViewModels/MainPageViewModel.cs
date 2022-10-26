using SnakeGame.Helpers;
using SnakeGame.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SnakeGame.ViewModels
{
    public class MainPageViewModel
    {
        private readonly NavigationService _navigationService;
        public MainPageViewModel()
        {
            _navigationService = NavigationServiceProvider.NavigationService;
        }

        public ICommand NavigateToGameCommand => new RelayCommand(NavigateToGame);
        public ICommand NavigateToLevelDesignerCommand => new RelayCommand(NavigateToGame);
        public ICommand NavigateToSelectLevelCommand => new RelayCommand(NavigateToGame);

        public void NavigateToGame()
        {
            _navigationService.Navigate(new GamePage());
        }
        public void NavigateToLevelDesigner()
        {
            _navigationService.Navigate(new GamePage());
        }
        public void NavigateToSelectLevel()
        {
            _navigationService.Navigate(new GamePage());
        }
    }
}
