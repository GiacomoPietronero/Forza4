using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forza4.Engine.Players;
using System.Diagnostics;

namespace Forza4.Engine
{
    public class MultiGame
    {

        private int numberOfMatches;
        private int matchesCompleted;
        private Dictionary<Color, int> wins;
        private Dictionary<Color, IPlayer> players;

        public MultiGame(IPlayer player1, IPlayer player2, int nMatches)
        {
            numberOfMatches = nMatches;
            wins = new Dictionary<Color, int>();
            wins.Add(Color.Blue, 0);
            wins.Add(Color.Red, 0);

            players = new Dictionary<Color, IPlayer>();
            players.Add(player1.Color, player1);
            players.Add(player2.Color, player2);
        }

        public void PlayerAllGames()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            double timeStart = sw.ElapsedMilliseconds;

            while (numberOfMatches > matchesCompleted)
            {
                Color colorToStart = matchesCompleted % 2 == 0 ? Color.Red : Color.Blue;

                Game game = new Game(players[Color.Red], players[Color.Blue], colorToStart);
                
                game.StartFromConsole();

                if (game.HasWinner)
                {
                    wins[game.Winner]++;
                }

                matchesCompleted++;
            }

            double timeEnd = sw.ElapsedMilliseconds;

            double sec = (timeEnd - timeStart) / 1000.0;

            Console.WriteLine(string.Format("{0} games completed", matchesCompleted));

            Console.WriteLine(string.Format("Time to complete: {0} secs", sec.ToString("0.##")));


            Console.WriteLine(string.Format("Player {0}-{2} has won {1} games", players[Color.Red].Name, wins[Color.Red], players[Color.Red].GetType().Name));
            Console.WriteLine(string.Format("Player {0}-{2} has won {1} games", players[Color.Blue].Name, wins[Color.Blue], players[Color.Blue].GetType().Name));

            Console.ReadLine();
        }


    }
}
