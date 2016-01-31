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

    public abstract class BasePlayer : IPlayer
    {
        public BasePlayer(string name, Color color)
        {
            _color = color;
            _name = name;
        }

        private readonly Color _color;
        private readonly string _name;

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

        public abstract int Move(CellStatus[,] status);
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
