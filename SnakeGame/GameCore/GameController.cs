using SnakeGame.Drawing;
using SnakeGame.GameCore.Objects;
using SnakeGame.GameCore.Objects.Base;
using SnakeGame.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.GameCore
{
    public class GameController
    {
        private readonly Canvas _canvas;
        private SnakePlayer _player { get; set; }
        private LinkedList<Rectangle> _playerRectangles { get; set; }
        private LinkedList<EMovementDirection> _directions { get; set; }
        private List<Rectangle> _fruitRectangles { get; set; }
        private List<Rectangle> _wallRectangles { get; set; }
        private Rectangle _overlayRectangle { get; set; }
        private GameMap _gameMap { get; set; }
        public EGameState GameState { get; set; }

        public GameController(Canvas canvas)
        {
            _canvas = canvas;
            _gameMap = new GameMap(GameSettings.HorizontalTileCount,GameSettings.VerticalTileCount);
            _directions = new LinkedList<EMovementDirection>();
        }

        public void Initialize()
        {
            CreatePlayer();
            CreateGrid();
            CreateMap();
            SpawnNewFruit();
            CreateOverlay();

            GameState = EGameState.Continue;
        }

        private void CreateOverlay()
        {
            _overlayRectangle = CreateRect(0,
                0,
                new SolidColorBrush(Color.FromArgb(180, 2, 2, 2)),
                GameSettings.GameWidth,
                GameSettings.GameHeight,
                GameSettings.OverlayZIndex);
            _canvas.Children.Add(_overlayRectangle);
        }

        public void ShowOverlay()
        {
            _canvas.Children.Add(_overlayRectangle);
        }
        public void HideOverlay()
        {
            _canvas.Children.Remove(_overlayRectangle);
        }

        private void CreateMap()
        {
            _fruitRectangles = new List<Rectangle>();
            _wallRectangles = new List<Rectangle>();
            for (int x = 0; x < _gameMap.Width; x++)
            {
                for (int y = 0; y < _gameMap.Height; y++)
                {
                    if (_gameMap.Map[x][y].ObjectType == EGameObjectType.Fruit)
                    {
                        _fruitRectangles.Add(CreateRect(x, y, new SolidColorBrush(Colors.Green)));
                    } else if (_gameMap.Map[x][y].ObjectType == EGameObjectType.Wall)
                    {
                        _wallRectangles.Add(CreateRect(x, y, new SolidColorBrush(Colors.Gray)));
                    }
                }
            }
            foreach (var item in _fruitRectangles)
            {
                _canvas.Children.Add(item);
            }
            foreach (var item in _wallRectangles)
            {
                _canvas.Children.Add(item);
            }
        }

        private void CreatePlayer()
        {
            var _p = new LinkedList<GameObject>();
            _p.AddFirst(new SnakeObject(3, 3));
            _p.AddFirst(new SnakeObject(4, 3));
            _p.AddFirst(new SnakeObject(5, 3));
            _p.AddFirst(new SnakeObject(6, 3));

            _player = new SnakePlayer(_p, EMovementDirection.Right);

            _playerRectangles = new LinkedList<Rectangle>();

            _playerRectangles.AddFirst(CreateRect(3, 3, new SolidColorBrush(Colors.Red)));
            _playerRectangles.AddFirst(CreateRect(4, 3, new SolidColorBrush(Colors.Red)));
            _playerRectangles.AddFirst(CreateRect(5, 3, new SolidColorBrush(Colors.Red)));
            _playerRectangles.AddFirst(CreateRect(6, 3, new SolidColorBrush(Colors.Red)));

            foreach (var item in _playerRectangles)
            {
                _canvas.Children.Add(item);
            }
        }

        private void CreateGrid()
        {
            var grid = GameGridProvider.Get();

            foreach (var line in grid)
            {
                _canvas.Children.Add(line);
            }
        }

        private Rectangle CreateRect(int x, int y, SolidColorBrush brush, int width = 32, int height = 32, int zIndex = GameSettings.GameObjectZIndex)
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

        public EGameState GameTick()
        {
            //make an object for these values and separate logic into seperate methods at least
            GameState = EGameState.Continue;
            var first = _player.Body.First;
            var last = _player.Body.Last;
            var oldX = last.Value.X;
            var oldY = last.Value.Y;

            last.Value.X = first.Value.X;
            last.Value.Y = first.Value.Y;

            if(_directions.Count > 0)
            {
                var newDirection = _directions.First;
                _directions.RemoveFirst();

                _player.Direction = newDirection.Value;
            }

            switch (_player.Direction)
            {
                case EMovementDirection.Left:
                    last.Value.X -= 1;
                    break;
                case EMovementDirection.Right:
                    last.Value.X += 1;
                    break;
                case EMovementDirection.Up:
                    last.Value.Y -= 1;
                    break;
                case EMovementDirection.Down:
                    last.Value.Y += 1;
                    break;
            }

            if(last.Value.X < 0)
            {
                last.Value.X = _gameMap.Width - 1;
            } else if (last.Value.X >= _gameMap.Width)
            {
                last.Value.X = 0;
            }

            if (last.Value.Y < 0)
            {
                last.Value.Y = _gameMap.Height - 1;
            }
            else if (last.Value.Y >= _gameMap.Height)
            {
                last.Value.Y = 0;
            }

            _player.Body.RemoveLast();
            _player.Body.AddFirst(last);

            // eat fruit
            if (_gameMap.Map[last.Value.X][last.Value.Y].ObjectType == EGameObjectType.Fruit)
            {
                _gameMap.Map[last.Value.X][last.Value.Y] = new EmptyObject(last.Value.X, last.Value.Y);
                var fruitToDelete = _fruitRectangles
                    .FirstOrDefault(rec => Canvas.GetLeft(rec) / 32 == last.Value.X && Canvas.GetTop(rec) / 32 == last.Value.Y);
                if(fruitToDelete != null)
                {
                    _canvas.Children.Remove(fruitToDelete);
                    _player.Body.AddLast(new SnakeObject(oldX, oldY));
                    var newRect = CreateRect(oldX, oldY, new SolidColorBrush(Colors.Red));
                    _playerRectangles.AddLast(newRect);

                    Canvas.SetLeft(newRect, oldX * 32);
                    Canvas.SetTop(newRect, oldY * 32);
                    _canvas.Children.Add(newRect);

                    //SpawnNewFruit
                    SpawnNewFruit();
                    GameState = EGameState.FruitCollected;
                }
            } else if (_gameMap.Map[last.Value.X][last.Value.Y].ObjectType == EGameObjectType.Wall)
            {
                GameState = EGameState.PlayerLost;
                ShowOverlay();
                return GameState;
            }

            var lastRect = _playerRectangles.Last;
            _playerRectangles.RemoveLast();
            Canvas.SetLeft(lastRect.Value, last.Value.X * 32);
            Canvas.SetTop(lastRect.Value, last.Value.Y * 32);
            _playerRectangles.AddFirst(lastRect);

            //check if player has bitten itself
            var head = _player.Body.First.Value;

            foreach(var snakeObject in _player.Body)
            {
                if(snakeObject == head)
                {
                    continue;
                }
                if(snakeObject.X == head.X && snakeObject.Y == head.Y)
                {
                    ShowOverlay();
                    GameState = EGameState.PlayerLost;
                }
            }

            return GameState;
        }

        private void SpawnNewFruit()
        {
            List<Point> forbiddenLocations = new List<Point>();
            foreach(var column in _gameMap.Map)
            {
                foreach (var cell in column)
                {
                    if(cell.ObjectType == EGameObjectType.Wall)
                    {
                        forbiddenLocations.Add(new Point(cell.X, cell.Y));
                    }
                }
            }

            foreach(var snakeCell in _player.Body)
            {
                forbiddenLocations.Add(new Point(snakeCell.X, snakeCell.Y));
            }

            bool generatedFruit = false;
            var rnd = new Random();

            while(!generatedFruit)
            {
                var newX = rnd.Next(0, _gameMap.Width - 1);
                var newY = rnd.Next(0, _gameMap.Height - 1);

                if (forbiddenLocations.Count(cell => cell.X == newX || cell.Y == newY) > 0)
                {
                    continue;
                }

                _gameMap.Map[newX][newY] = new FruitObject(newX, newY);

                var fruitRect = CreateRect(newX, newY, new SolidColorBrush(Colors.Green));
                _fruitRectangles.Add(fruitRect);
                _canvas.Children.Add(fruitRect);

                generatedFruit = true;
            }

        }

        public void NotifyDirectionChange(ConsoleKey key)
        {
            EMovementDirection newDirection = EMovementDirection.Up;

            switch (key)
            {
                case ConsoleKey.W:
                    newDirection = EMovementDirection.Up;
                    break;
                case ConsoleKey.A:
                    newDirection = EMovementDirection.Left;
                    break;
                case ConsoleKey.S:
                    newDirection = EMovementDirection.Down;
                    break;
                case ConsoleKey.D:
                    newDirection = EMovementDirection.Right;
                    break;
            }

            if (_directions.Count > 0 ? !DirectionChangePossible(_directions.Last.Value, newDirection) : false)
            {
                return;
            } else if (!DirectionChangePossible(_player.Direction,newDirection) && _directions.Count == 0)
            {
                return;
            }

            if (_directions.Count > 0)
            {
                if(_directions.Last.Value != newDirection)
                {
                    _directions.AddLast(newDirection);
                }
            } else
            {
                _directions.AddLast(newDirection);
            }
        }

        private bool DirectionChangePossible(EMovementDirection dir1, EMovementDirection dir2)
        {
            if(dir1 == EMovementDirection.Left && dir2 == EMovementDirection.Right ||
                dir1 == EMovementDirection.Right && dir2 == EMovementDirection.Left ||
                dir1 == EMovementDirection.Up && dir2 == EMovementDirection.Down ||
                dir1 == EMovementDirection.Down && dir2 == EMovementDirection.Up)
            {
                return false;
            }
            return true;
        }
    }
}
