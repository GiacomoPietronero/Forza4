using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forza4.Engine.Players
{
    public interface IPlayer
    {
        string Name { get; }

        Color Color { get; }

        int Move(CellStatus[,] status);
    }

    public class RandomPlayer : IPlayer
    {
        private Random _rand;
        private Color _color;

        public RandomPlayer(Color color)
        {
            _color = color;
            _rand = new Random(666);
        }

        public string Name
        {
            get
            {
                return "RandomPlayer";
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
        }

        public int Move(CellStatus[,] status)
        {
            return _rand.Next(0, 7);
        }
    }

    public class ConsolePlayer : IPlayer
    {
        private readonly string _name;
        private readonly Color _color;

        
        public ConsolePlayer(string name, Color color)
        {
            _name = name;
            _color = color;
        }

        public Color Color
        {
            get
            {
                return _color;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int Move(CellStatus[,] status)
        {
            
            int col;

            while (true)
            {
                Console.WriteLine("Insert column index");

                bool isValid = int.TryParse(Console.ReadLine(), out col);

                if (!isValid)
                {
                    Console.WriteLine("Insert valid column index!");
                    continue;
                }

                break;
            }

            return col;

        }
    }
}
