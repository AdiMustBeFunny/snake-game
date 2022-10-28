using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Settings
{
    public class GameSettings
    {
        public const int GameWidth = 1024;
        public const int GameHeight = 768;
        public const int TileSize = 32;
        public static int HorizontalTileCount => GameWidth / TileSize;
        public static int VerticalTileCount => GameHeight / TileSize;

        public const int OverlayZIndex = 10;
        public const int GridZIndex = 10;
        public const int PlayerZIndex = 9;
        public const int GameObjectZIndex = 8;
    }
}
