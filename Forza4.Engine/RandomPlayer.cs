using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Forza4.Engine.Players
{
    public class RandomPlayer : BasePlayer
    {
        private Random _rand;

        public RandomPlayer(string name, Color color)
            :base(name,color)
        {
            _rand = new Random();
        }

        public override int Move(CellStatus[,] status)
        {
            return _rand.Next(0, status.GetLength(1));
        }
    }
}
