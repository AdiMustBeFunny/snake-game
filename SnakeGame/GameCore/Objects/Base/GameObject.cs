using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameCore.Objects.Base
{
    public class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public EGameObjectType ObjectType { get; set; }

        public GameObject(int x, int y, EGameObjectType objectType)
        {
            X = x;
            Y = y;
            ObjectType = objectType;
        }
    }
}
