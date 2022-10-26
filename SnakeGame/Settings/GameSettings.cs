using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Settings
{
    public class GameSettings
    {
        public static int GameWidth => 1024;
        public static int GameHeight => 768;
        public static int TileSize => 32;

        public static int GridZIndex => 10;
        public static int PlayerZIndex => 9;
        public static int GameObjectZIndex => 8;
    }
}
