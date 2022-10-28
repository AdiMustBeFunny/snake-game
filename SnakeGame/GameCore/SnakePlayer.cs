using SnakeGame.GameCore.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.GameCore
{
    public class SnakePlayer
    {
        public EMovementDirection Direction { get; set; }
        public LinkedList<GameObject> Body { get; set; }

        public SnakePlayer(LinkedList<GameObject> body,EMovementDirection direction)
        {
            Body = body;
            Direction = direction;
        }
    }
}
