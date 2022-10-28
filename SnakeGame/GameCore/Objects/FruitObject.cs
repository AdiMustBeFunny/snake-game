using SnakeGame.GameCore.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameCore.Objects
{
    public class FruitObject : GameObject
    {
        public FruitObject(int x, int y) : base(x, y, EGameObjectType.Fruit)
        {
        }
    }
}
