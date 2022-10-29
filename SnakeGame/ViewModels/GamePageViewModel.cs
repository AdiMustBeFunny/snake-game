using SnakeGame.Drawing;
using SnakeGame.GameCore;
using SnakeGame.Helpers;
using SnakeGame.Settings;
using SnakeGame.ViewModels.Base;
using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace SnakeGame.ViewModels
{
    public class GamePageViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;
        private readonly DispatcherTimer _timer;
        private TextBlock _overlayTextBlock;
        private readonly GameController _gameController;
        public Canvas _canvas { get; set; }

        public GamePageViewModel(Canvas canvas)
        {
            _navigationService = NavigationServiceProvider.NavigationService;
            _canvas = canvas;
            _gameController = new GameController(canvas);
            _timer = new DispatcherTimer();
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

        private int _score = 0;

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                Set(nameof(Score));
            }
        }


        public ICommand GoBackComand => new RelayCommand(GoBack);
        public ICommand TooglePauseComand => new RelayCommand(TooglePause);
        public ICommand Key_W_PressedCommand => new RelayCommand(Key_W_Pressed);
        public ICommand Key_A_PressedCommand => new RelayCommand(Key_A_Pressed);
        public ICommand Key_S_PressedCommand => new RelayCommand(Key_S_Pressed);
        public ICommand Key_D_PressedCommand => new RelayCommand(Key_D_Pressed);
        public ICommand Key_Space_PressedCommand => new RelayCommand(Key_Space_Pressed);
        public ICommand FocusCanvasCommand => new RelayCommand(FocusCanvas);

        private void FocusCanvas()
        {
            _canvas.Focus();
        }

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
            if (_gameController.GameState == GameCore.Objects.EGameState.PlayerLost)
            {
                RestartGame();
                return;
            }
            else if (_timer.IsEnabled)
            {
                _overlayTextBlock.Text = "Press space to unpause";
                _overlayTextBlock.Visibility = Visibility.Visible;
                _gameController.ShowOverlay();
                _timer.Stop();
            }
            else
            {
                _gameController.HideOverlay();
                _overlayTextBlock.Visibility = Visibility.Hidden;
                _timer.Start();
            }

            if (!ToogleEnabled)
            {
                ToogleEnabled = true;
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
                _overlayTextBlock.Text = "Press space to unpause";
                _overlayTextBlock.Visibility = Visibility.Visible;
                _gameController.ShowOverlay();
                _timer.Stop();
            }
            else if(_gameController.GameState != GameCore.Objects.EGameState.PlayerLost)
            {
                _overlayTextBlock.Visibility = Visibility.Hidden;
                _gameController.HideOverlay();
                _timer.Start();
            }
            _canvas.Focus();
        }

        public void Initialize()
        {
            _canvas.Children.Clear();
            Score = 0;

            _gameController.Initialize();
            _overlayTextBlock = OverlayTextBlockProvider.Get("Press space to start\nMove with - W, A, S, D");
            _canvas.Children.Add(_overlayTextBlock);

            _timer.Tick += gameTick;
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _canvas.Focus();
        }

        public void RestartGame()
        {
            _timer.Tick -= gameTick;
            Initialize();
            _gameController.HideOverlay();
            _overlayTextBlock.Visibility = Visibility.Hidden;
            ToogleEnabled = true;
            _timer.Start();
        }

        private void gameTick(object sender, EventArgs e)
        {
            var gameState = _gameController.GameTick();

            if (gameState == GameCore.Objects.EGameState.PlayerLost)
            {
                _canvas.Children.Remove(_overlayTextBlock);
                _overlayTextBlock = OverlayTextBlockProvider.Get($"You have lost.\nYour score: {Score}.\nPress space to restart the game");
                _overlayTextBlock.Visibility = Visibility.Visible;
                _canvas.Children.Add(_overlayTextBlock);
                ToogleEnabled = false;
                _timer.Stop();
            }
            else if (gameState == GameCore.Objects.EGameState.FruitCollected)
            {
                SystemSounds.Beep.Play();
                Score++;
            }
        }
    }
}
