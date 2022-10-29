using SnakeGame.GameCore;
using SnakeGame.GameCore.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SnakeGame.Drawing
{
    public class MapWallsProvider
    {
        public static List<UIElement> Get(GameMap gameMap)
        {
            var list = new List<UIElement>();
            for (int x = 0; x < gameMap.Width; x++)
            {
                for (int y = 0; y < gameMap.Height; y++)
                {
                    if (gameMap.Map[x][y].ObjectType == EGameObjectType.Wall)
                    {
                        list.Add(TileBuilder.Build(x, y, new SolidColorBrush(Colors.Gray)));
                    }
                }
            }

            return list;
        }
    }
}
