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

            //IPlayer p1 = new DefensiveAggressivePlayer("P1-D", Color.Red, Style.Defensive);
            IPlayer p1 = new RandomPlayer("P1", Color.Red);

            IPlayer p2 = new DefensiveAggressivePlayer("P2-A", Color.Blue, Style.Aggressive);
            
            
            MultiGame multiGame = new MultiGame(p1, p2, 100000);
            multiGame.PlayerAllGames();
            
        }

    }
}
