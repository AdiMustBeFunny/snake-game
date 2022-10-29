using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameCore
{
    public class GameMapExportObject
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<int> MapData { get; set; } = new List<int>();
    }
}
