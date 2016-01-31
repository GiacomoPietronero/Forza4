using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forza4.Engine
{
    public class Board
    {
        const int nrows = 6;
        const int ncols = 7;
        const int consecutiveToWin = 4;

        private CellStatus[,] cells;

        public Color winner;

        public Board()
        {
            cells = new CellStatus[nrows, ncols];
        }
        
        public bool DropDisk(int col, Color player)
        {
            if (col < 0 || col >= ncols)
                throw new SystemException();

            if (cells[nrows - 1, col] != CellStatus.Empty)
                return false; // no cells available in this col!


            for (int row = 0; row < nrows; row++)
            {
                if (cells[row, col] == CellStatus.Empty)
                {
                    cells[row, col] = Utils.GetStatusFromPlayer(player);
                    return true;
                }
            }
            
            throw new SystemException();
        }

        public bool HasWinner()
        {
            // check rows
            int counter = 0;
            CellStatus player = CellStatus.Empty;

            for (int r = 0; r < nrows; r++)
            {
                for (int c = 0; c < ncols; c++)
                {
                    if (cells[r, c] == CellStatus.Empty)
                    {
                        counter = 0;
                        player = CellStatus.Empty;
                    }
                    else if (cells[r, c] != player)
                    {
                        counter = 1;
                        player = cells[r, c];
                    }
                    else
                    {
                        counter++;

                        if (counter == consecutiveToWin)
                        {
                            winner = Utils.GetPlayerFromStatus(player);
                            return true;
                        }
                    }
                }
            }

            counter = 0;
            player = CellStatus.Empty;

            // check cols
            for (int c = 0; c < ncols; c++)
            {
                for (int r = 0; r < nrows; r++)
                {
                    if (cells[r, c] == CellStatus.Empty)
                    {
                        counter = 0;
                        player = CellStatus.Empty;
                    }
                    else if (cells[r, c] != player)
                    {
                        counter = 1;
                        player = cells[r, c];
                    }
                    else
                    {
                        counter++;

                        if (counter == consecutiveToWin)
                        {
                            winner = Utils.GetPlayerFromStatus(player);
                            return true;
                        }
                    }
                }
            }
            // check diagonal backwards
            counter = 0;
            player = CellStatus.Empty;

            for (int c = 3; c < ncols + 2; c++)
            {
                for (int r = 0; r < nrows; r++)
                {
                    int col = c - r;
                    int row = r;

                    if (col >= 0 && col < ncols)
                    {
                        if (cells[row, col] == CellStatus.Empty)
                        {
                            counter = 0;
                            player = CellStatus.Empty;
                        }
                        else if (cells[row, col] != player)
                        {
                            counter = 1;
                            player = cells[row, col];
                        }
                        else
                        {
                            counter++;

                            if (counter == consecutiveToWin)
                            {
                                winner = Utils.GetPlayerFromStatus(player);
                                return true;
                            }
                        }
                    }
                }
            }

            counter = 0;
            player = CellStatus.Empty;

            // check diagonal forward
            counter = 0;
            player = CellStatus.Empty;

            for (int c = 3; c < ncols + 2; c++)
            {
                for (int r = 0; r < nrows; r++)
                {
                    int col = c + r;
                    int row = r;

                    if (col >= 0 && col < ncols)
                    {
                        if (cells[row, col] == CellStatus.Empty)
                        {
                            counter = 0;
                            player = CellStatus.Empty;
                        }
                        else if (cells[row, col] != player)
                        {
                            counter = 1;
                            player = cells[row, col];
                        }
                        else
                        {
                            counter++;

                            if (counter == consecutiveToWin)
                            {
                                winner = Utils.GetPlayerFromStatus(player);
                                return true;
                            }
                        }
                    }
                }
            }


            return false;
        }


        public bool BoardIsFull()
        {
            for (int c = 0; c < ncols; c++)
            {
                if (cells[nrows - 1, c] == CellStatus.Empty)
                    return false;
            }

            return true;
        }

        public CellStatus[,] BoardStatus
        {
            get { return cells; }
        }

    }
}
