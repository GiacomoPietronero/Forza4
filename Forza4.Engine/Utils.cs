using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forza4.Engine.Players;

namespace Forza4.Engine
{
    public enum CellStatus
    {
        Empty,
        Red,
        Blue
    }

    public enum Color
    {
        Red,
        Blue
    }

    public enum PlayerType
    {
        ConsolePlayer,
        RandomPlayer
    }

    public static class Utils
    {
        public static bool CanPlaceDiskAt(int r, int c, CellStatus[,] cells)
        {
            if (!IsValidCell(r, c, cells))
                return false;

            if (cells[r, c] == CellStatus.Empty)
            {
                if (r == 0 || (r > 0 && cells[r - 1, c] != CellStatus.Empty))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsValidCell(int r, int c, CellStatus[,] cells)
        {
            if (r < 0 || r >= cells.GetLength(0))
                return false;

            if (c < 0 || c >= cells.GetLength(1))
                return false;

            return true;
        }

        public static bool BoardIsEmpty(CellStatus[,] status)
        {
            for (int i = 0; i < status.GetLength(1); i++)
            {
                if (status[0, i] != CellStatus.Empty)
                    return false;
            }

            return true;
        }

        internal static bool IsPlayerFirstMove(CellStatus[,] cells, Color color)
        {
            CellStatus status = GetStatusFromPlayer(color);

            for (int row = 0; row < cells.GetLength(0); row++)
            {
                for (int col = 0; col < cells.GetLength(1); col++)
                {
                    if (cells[row, col] == status)
                        return false;
                }
            }

            return true;

        }

        public static Color GetPlayerFromStatus(CellStatus status)
        {
            switch (status)
            {
                case CellStatus.Red:
                    return Color.Red;
                case CellStatus.Blue:
                    return Color.Blue;
                default:
                    throw new SystemException();
            }
        }
        
        public static CellStatus GetStatusFromPlayer(Color player)
        {
            switch (player)
            {
                case Color.Red:
                    return CellStatus.Red;
                case Color.Blue:
                    return CellStatus.Blue;
                default:
                    throw new SystemException();
            }
        }


        public static IPlayer CreatePlayer(PlayerType type, string name, Color color)
        {
            switch (type)
            {
                case PlayerType.ConsolePlayer:
                    return new ConsolePlayer(name, color);
                case PlayerType.RandomPlayer:
                    return new RandomPlayer(name, color);
                default:
                    throw new SystemException();
                    break;
            }
        }


        public static string ToFriendlyString(CellStatus[,] cells)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = cells.GetLength(0) - 1; i >= 0; i--)
            {
                StringBuilder row = new StringBuilder();

                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    row.Append(cells[i, j]);
                    row.Append(",");
                }

                sb.AppendLine(row.ToString());
            }

            return sb.ToString();
        }
    }
}
