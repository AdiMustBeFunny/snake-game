using SnakeGame.GameCore;
using SnakeGame.Helpers;
using SnakeGame.ViewModels.Base;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace SnakeGame.ViewModels
{
    public class GamePageViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;
        private readonly DispatcherTimer _timer;
        private readonly GameController _gameController;
        public Canvas _canvas { get; set; }

        public GamePageViewModel(Canvas canvas)
        {
            _navigationService = NavigationServiceProvider.NavigationService;
            _canvas = canvas;
            _gameController = new GameController(canvas);
            _timer = new DispatcherTimer();
        }

        public void Initialize()
        {
            _gameController.Initialize();
            _timer.Tick += gameTick;
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            //_timer.Start();
            _canvas.Focus();
        }

        private void gameTick(object sender, EventArgs e)
        {
            var gameState = _gameController.GameTick();

            if(gameState == GameCore.Objects.EGameState.PlayerLost)
            {
                _timer.Stop();
            }
        }

        private bool _toogleEnabled;
        public bool ToogleEnabled
        {
            get { return _toogleEnabled; }
            set 
            { 
                _toogleEnabled = value;
                Set(nameof(ToogleEnabled));
            }
        }

        private bool _showOverlay = true;
        public bool ShowOverlay
        {
            get { return _showOverlay; }
            set
            {
                _showOverlay = value;
                Set(nameof(ShowOverlay));
            }
        }

        private string _overlayText = "Press space to start";
        public string OverlayText
        {
            get { return _overlayText; }
            set
            {
                _overlayText = value;
                Set(nameof(OverlayText));
            }
        }


        public ICommand GoBackComand => new RelayCommand(GoBack);
        public ICommand TooglePauseComand => new RelayCommand(TooglePause);
        public ICommand Key_W_PressedCommand => new RelayCommand(Key_W_Pressed);
        public ICommand Key_A_PressedCommand => new RelayCommand(Key_A_Pressed);
        public ICommand Key_S_PressedCommand => new RelayCommand(Key_S_Pressed);
        public ICommand Key_D_PressedCommand => new RelayCommand(Key_D_Pressed);
        public ICommand Key_Space_PressedCommand => new RelayCommand(Key_Space_Pressed);

        public void Key_W_Pressed()
        {
            if (!_timer.IsEnabled)
            {
                return;
            }
            _gameController.NotifyDirectionChange(ConsoleKey.W);
        }
        public void Key_A_Pressed()
        {
            if (!_timer.IsEnabled)
            {
                return;
            }
            _gameController.NotifyDirectionChange(ConsoleKey.A);
        }
        public void Key_S_Pressed()
        {
            if (!_timer.IsEnabled)
            {
                return;
            }
            _gameController.NotifyDirectionChange(ConsoleKey.S);

        }
        public void Key_D_Pressed()
        {
            if (!_timer.IsEnabled)
            {
                return;
            }
            _gameController.NotifyDirectionChange(ConsoleKey.D);
        }
        public void Key_Space_Pressed()
        {
            if (!ToogleEnabled)
            {
                ToogleEnabled = true;
            }
            

            if (_gameController.GameState == GameCore.Objects.EGameState.PlayerLost)
            {
                return;
            }
            else if (_timer.IsEnabled)
            {
                OverlayText = "Press space to unpause";
                ShowOverlay = true;
                _gameController.ShowOverlay();
                _timer.Stop();
            }
            else
            {
                ShowOverlay = false;
                _gameController.HideOverlay();
                _timer.Start();
            }
        }
        public void GoBack()
        {
            //dispose of the game here
            _timer.Stop();
            _navigationService.GoBack();
        }
        public void TooglePause()
        {
            if(_timer.IsEnabled){
                OverlayText = "Press space to unpause";
                ShowOverlay = true;
                _gameController.ShowOverlay();
                _timer.Stop();
            }
            else if(_gameController.GameState != GameCore.Objects.EGameState.PlayerLost)
            {
                ShowOverlay = false;
                _gameController.HideOverlay();
                _timer.Start();
            }
            _canvas.Focus();
        }
    }
}
