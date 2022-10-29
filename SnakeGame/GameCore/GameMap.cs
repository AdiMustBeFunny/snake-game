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
        }

        public GameMapExportObject ToExportData()
        {
            var mapToList = new List<int>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Map[x][y].ObjectType == EGameObjectType.Wall)
                    {
                        mapToList.Add(1);
                    }
                    else
                    {
                        mapToList.Add(0);
                    }
                }
            }
            return new GameMapExportObject()
            {
                MapData = mapToList,
                Width = Width,
                Height = Height
            };
        }

        public void Load(GameMapExportObject exportObject)
        {
            Width = exportObject.Width;
            Height = exportObject.Height;
            Map = new List<List<GameObject>>();
            var x = 0;
            var y = 0;

            for (int i = 0; i < exportObject.MapData.Count; i++)
            {
                if( i % exportObject.Height == 0)
                {
                    x = i / exportObject.Height;
                    Map.Add(new List<GameObject>());
                }
                y = i % exportObject.Height;

                if (exportObject.MapData[i] == 0)
                {
                    Map[x].Add(new EmptyObject(x, y));
                } 
                else
                {
                    Map[x].Add(new WallObject(x, y));
                }
            }
        }
    }
}
