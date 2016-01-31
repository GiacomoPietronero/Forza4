using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public static class Utils
    {
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
    }
}
