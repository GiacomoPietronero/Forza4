using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forza4.Engine;
using Forza4.Engine.Players;
namespace Forza4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Game starts...");
            
            //Game game = new Game(new ConsolePlayer("Jack", Color.Red) , new ConsolePlayer("Fra", Color.Blue));

            Game game = new Game(new RandomPlayer(Color.Red), new RandomPlayer(Color.Blue));


            game.StartFromConsole();


        }

    }
}
