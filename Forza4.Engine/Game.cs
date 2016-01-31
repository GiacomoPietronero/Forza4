using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forza4.Engine.Players;

namespace Forza4.Engine
{
    public class Game
    {
        private Board board;

        public Color nextToPlay;

        private Dictionary<Color, IPlayer> players;

        public Game(IPlayer player1, IPlayer player2, Color startingPlayer = Color.Blue)
        {
            board = new Board();

            nextToPlay = startingPlayer;

            players = new Dictionary<Color, IPlayer>();
            players.Add(player1.Color, player1);
            players.Add(player2.Color, player2);
        }


        public void StartFromConsole(bool verbose = false)
        {
            int moveIndex = 0;

            while (!HasWinner && !board.BoardIsFull())
            {
                if (verbose)
                {
                    Console.WriteLine(string.Format("Player {0} is next to play", nextToPlay));
                    Console.WriteLine("Insert column index");
                }
                int col = players[nextToPlay].Move(board.BoardStatus);

                bool isValidMove = Move(col);

                if (!isValidMove)
                {
                    if (verbose)
                    {
                        Console.WriteLine("Cannot play that!");
                    }
                    continue;
                }

                moveIndex++;
                if (verbose)
                {
                    Console.WriteLine(string.Format("Move number {0}", moveIndex));
                }
            }

            if (HasWinner)
            {
                if (verbose)
                    Console.WriteLine(string.Format("Player {0} has won the game!", Winner));
            }
            else if (board.BoardIsFull())
            {
                if (verbose)
                    Console.WriteLine("No more moves available!");
            }

            //Console.WriteLine("Press any key...");
            //Console.ReadLine();
        }

        public bool Move(int col)
        {
            bool isValidMove = board.DropDisk(col, nextToPlay);

            if (isValidMove)
                UpdateNextToPlay();

            return isValidMove;
        }


        public void UpdateNextToPlay()
        {
            switch (nextToPlay)
            {
                case Color.Red:
                    nextToPlay = Color.Blue;
                    break;
                case Color.Blue:
                    nextToPlay = Color.Red;
                    break;
                default:
                    throw new SystemException();
            }
        }

        public CellStatus[,] GetCurrentBoardStatus()
        {
            return board.BoardStatus;
        }

        public bool HasWinner
        {
            get
            {
                return board.HasWinner();
            }
        }

        public Color Winner
        {
            get
            {
                return board.winner;
            }
        }
    }
}
