using SnakeGame.GameCore.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameCore.Objects
{
    public class EmptyObject : GameObject
    {
        public EmptyObject(int x, int y) : base(x, y, EGameObjectType.Empty)
        {
        }
    }
}
