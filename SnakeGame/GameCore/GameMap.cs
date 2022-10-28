using SnakeGame.GameCore.Objects;
using SnakeGame.GameCore.Objects.Base;
using System.Collections.Generic;

namespace SnakeGame.GameCore
{
    public class GameMap
    {
        public List<List<GameObject>> Map { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public GameMap(int horizontalCount, int verticalCount, List<List<GameObject>> map)
        {
            Width = horizontalCount;
            Height = verticalCount;
            Map = map;
        }
        public GameMap(int horizontalCount, int verticalCount)
        {
            Width = horizontalCount;
            Height = verticalCount;
            Map = new List<List<GameObject>>(horizontalCount);

            for(int x = 0; x < horizontalCount; x++)
            {
                Map.Add(new List<GameObject>());
                for (int y = 0; y < verticalCount; y++)
                {
                    Map[x].Add(new EmptyObject(x,y));
                }
            }

            Map[3][3] = new FruitObject(3, 3);
            Map[5][5] = new WallObject(3, 3);
        }
    }
}
