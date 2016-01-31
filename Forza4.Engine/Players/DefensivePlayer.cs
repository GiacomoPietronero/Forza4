using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forza4.Engine.Players
{
    public enum Style
    {
        Aggressive,
        Defensive,
        Random
    }

    public class DefensiveAggressivePlayer : BasePlayer
    {
        private Color _opponent;
        private Style _style;

        private Random _rand;

        public DefensiveAggressivePlayer(string name, Color color, Style style)
           : base(name, color)
        {
            _style = style;
            _rand = new Random();
        }


        private CellStatus GetTargetColor()
        {
            Color targetColor = Color.Blue;

            switch (_style)
            {
                case Style.Aggressive:
                    targetColor = Color;
                    break;
                case Style.Defensive:
                    targetColor = _opponent;
                    break;
                case Style.Random:
                    targetColor = (Color)_rand.Next(0, 2);
                    break;
                default:
                    throw new SystemException();
            }
            
            return Utils.GetStatusFromPlayer(targetColor);
        }


        private bool PlayInTheMiddle(CellStatus[,] cells)
        {
            if (Utils.BoardIsEmpty(cells))
                return true;

            if (Utils.IsPlayerFirstMove(cells, Color))
                return true;

            return false;
        }

        public override int Move(CellStatus[,] cells)
        {
            CellStatus target = GetTargetColor();

            int nRows = cells.GetLength(0);
            int nCols = cells.GetLength(1);

            // if board is empty start in the middle
            if (PlayInTheMiddle(cells))
                return (int)Math.Ceiling(nCols / 2.0);

            Combination best = new Combination()
            {
                Lenght = 0
            };

            for (int r = 0; r < nRows; r++)
            {
                int counter = 0;
                for (int c = 0; c < nCols; c++)
                {
                    if (cells[r, c] != target)
                    {
                        if (counter > best.Lenght)
                        {
                            List<int> colToStop = new List<int>();

                            // check if the sequence can be stopped here!
                            if (Utils.CanPlaceDiskAt(r, c, cells))
                            {
                                colToStop.Add(c);
                            }

                            // check if the sequence can be stopped on the other end
                            if (Utils.CanPlaceDiskAt(r, c - counter - 1, cells))
                            {
                                colToStop.Add(c - counter - 1);
                            }

                            if (colToStop.Count > 0)
                            {
                                best = new Combination()
                                {
                                    Lenght = counter,
                                    CanBeStopped = colToStop.Count > 0,
                                    ColumnsToStop = colToStop,
                                };
                            }
                        }

                        counter = 0;
                    }
                    else
                    {
                        counter++;
                    }
                }
            }

            for (int c = 0; c < nCols; c++)
            {
                int counter = 0;
                for (int r = 0; r < nRows; r++)
                {
                    if (cells[r, c] != target)
                    {
                        if (counter > best.Lenght)
                        {
                            List<int> colToStop = new List<int>();

                            // check if the sequence can be stopped here!
                            if (Utils.CanPlaceDiskAt(r, c, cells))
                            {
                                colToStop.Add(c);
                            }

                            if (colToStop.Count > 0)
                            {
                                best = new Combination()
                                {
                                    Lenght = counter,
                                    CanBeStopped = colToStop.Count > 0,
                                    ColumnsToStop = colToStop,
                                };
                            }
                        }

                        counter = 0;
                    }
                    else
                    {
                        counter++;
                    }
                }
            }


            // check diagonal backward
            for (int c = 3; c < nCols + 2; c++)
            {
                int counter = 0;
                for (int r = 0; r < nRows; r++)
                {
                    int col = c - r;
                    int row = r;
                    if (col >= 0 && col < nCols)
                    {
                        if (cells[row, col] != target)
                        {
                            if (counter > best.Lenght)
                            {
                                List<int> colToStop = new List<int>();

                                // check if the sequence can be stopped here!
                                if (Utils.CanPlaceDiskAt(row, col, cells))
                                {
                                    colToStop.Add(col);
                                }

                                // check if the sequence can be stopped on the other side!
                                if (Utils.CanPlaceDiskAt(row - counter - 1, col + counter + 1, cells))
                                {
                                    colToStop.Add(col + counter + 1);
                                }

                                if (colToStop.Count > 0)
                                {
                                    best = new Combination()
                                    {
                                        Lenght = counter,
                                        CanBeStopped = colToStop.Count > 0,
                                        ColumnsToStop = colToStop,
                                    };
                                }
                            }

                            counter = 0;
                        }
                        else
                        {
                            counter++;
                        }

                    }
                }
            }


            // check diagonal forward
            for (int c = -2; c < nCols -3; c++)
            {
                int counter = 0;
                for (int r = 0; r < nRows; r++)
                {
                    int col = c + r;
                    int row = r;
                    if (col >= 0 && col < nCols)
                    {
                        if (cells[row, col] != target)
                        {
                            if (counter > best.Lenght)
                            {
                                List<int> colToStop = new List<int>();

                                // check if the sequence can be stopped here!
                                if (Utils.CanPlaceDiskAt(row, col, cells))
                                {
                                    colToStop.Add(col);
                                }

                                // check if the sequence can be stopped on the other side!
                                if (Utils.CanPlaceDiskAt(row - counter - 1, col - counter - 1, cells))
                                {
                                    colToStop.Add(col - counter - 1);
                                }

                                if (colToStop.Count > 0)
                                {
                                    best = new Combination()
                                    {
                                        Lenght = counter,
                                        CanBeStopped = colToStop.Count > 0,
                                        ColumnsToStop = colToStop,
                                    };
                                }
                            }

                            counter = 0;

                        }
                        else
                        {
                            counter++;
                        }
                    }
                }
            }




            if (best.ColumnsToStop != null)
            {

                if (best.ColumnsToStop.Count == 1)
                {
                    return best.ColumnsToStop[0];
                }
                else
                {
                    double center = nCols / 2.0;

                    int closest = -1;
                    double closestDistance = double.MaxValue;

                    foreach (var col in best.ColumnsToStop)
                    {
                        double distance = col - center;
                        if (distance < closestDistance)
                        {
                            closest = col;
                            closestDistance = distance;
                        }
                    }
                    
                    return closest;
                }
            }
            else
            {
                // why does it end up here?
                return new Random().Next(0, nCols);
            }
        }

        public class Combination
        {
            public int Lenght { get; set; }
            public bool CanBeStopped{ get; set; }
            public List<int> ColumnsToStop { get; set; }
        }
    }
}
