using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayEleven
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day11/Data.txt");

            var grid = new int[data.First().Length, data.Length];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var val = int.Parse(data[i][j].ToString());
                    grid[i, j] = val;
                }
            }

            long counter = 0;

            for (int i = 0; i < 100; i++)
            {
                //Debugging output
                //Console.WriteLine($"\nDebugging - Step {i}\n");
                //for (int j = 0; j < grid.GetLength(0); j++)
                //{
                //    for (int k = 0; k < grid.GetLength(1); k++)
                //    {
                //        Console.Write(grid[j, k]);
                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine();

                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    for (int k = 0; k < grid.GetLength(1); k++)
                    {
                        grid[j, k]++;
                    }
                }

                //This checks the ones around a point and adds to it, but this doesn't work
                //It seems to need to add to the ones around it if > 9 instead
                //for (int j = 0; j < grid.GetLength(0); j++)
                //{
                //    for (int k = 0; k < grid.GetLength(1); k++)
                //    {
                //        if (j > 0 && k > 0)
                //        {
                //            if (grid[j - 1, k - 1] > 9)
                //            {
                //                grid[j, k]++;
                //            }
                //        }
                //        if (k > 0)
                //        {
                //            if (grid[j, k - 1] > 9)
                //            {
                //                grid[j, k]++;
                //            }
                //        }
                //        if (j < grid.GetLength(0) - 1 && k > 0)
                //        {
                //            if (grid[j + 1, k - 1] > 9)
                //            {
                //                grid[j, k]++;
                //            }
                //        }
                //        if (j > 0)
                //        {
                //            if (grid[j - 1, k] > 9)
                //            {
                //                grid[j, k]++;
                //            }
                //        }
                //        if (j < grid.GetLength(0) - 1)
                //        {
                //            if (grid[j + 1, k] > 9)
                //            {
                //                grid[j, k]++;
                //            }
                //        }
                //        if (j > 0 && k < grid.GetLength(1) - 1)
                //        {
                //            if (grid[j - 1, k + 1] > 9)
                //            {
                //                grid[j, k]++;
                //            }
                //        }
                //        if (k > grid.GetLength(1) - 1)
                //        {
                //            if (grid[j, k + 1] > 9)
                //            {
                //                grid[j, k]++;
                //            }
                //        }
                //        if (j < grid.GetLength(0) - 1 && k < grid.GetLength(1) - 1)
                //        {
                //            if (grid[j + 1, k + 1] > 9)
                //            {
                //                grid[j, k]++;
                //            }
                //        }
                //    }
                //}

                List<Tuple<int, int>> flashedOctos = new List<Tuple<int, int>>();

                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    for (int k = 0; k < grid.GetLength(1); k++)
                    {
                        FlashOctopus(j, k, grid, flashedOctos);
                    }
                }

                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    for (int k = 0; k < grid.GetLength(1); k++)
                    {
                        if (grid[j, k] > 9)
                        {
                            grid[j, k] = 0;
                            counter++;
                        }
                    }
                }
            }

            return counter.ToString();
        }

        public void FlashOctopus(int x, int y, int[,] grid, List<Tuple<int, int>> flashedOctos)
        {
            if (grid[x, y] > 9 && !flashedOctos.Any(f => f.Item1 == x && f.Item2 == y))
            {
                flashedOctos.Add(new Tuple<int, int>(x, y));
                if (x > 0 && y > 0)
                {
                    var checkX = x - 1;
                    var checkY = y - 1;

                    grid[checkX, checkY]++;
                    FlashOctopus(checkX, checkY, grid, flashedOctos);
                }
                if (y > 0)
                {
                    var checkX = x;
                    var checkY = y - 1;

                    grid[checkX, checkY]++;
                    FlashOctopus(checkX, checkY, grid, flashedOctos);
                }
                if (x < grid.GetLength(0) - 1 && y > 0)
                {
                    var checkX = x + 1;
                    var checkY = y - 1;

                    grid[checkX, checkY]++;
                    FlashOctopus(checkX, checkY, grid, flashedOctos);
                }
                if (x > 0)
                {
                    var checkX = x - 1;
                    var checkY = y;

                    grid[checkX, checkY]++;
                    FlashOctopus(checkX, checkY, grid, flashedOctos);
                }
                if (x < grid.GetLength(0) - 1)
                {
                    var checkX = x + 1;
                    var checkY = y;

                    grid[checkX, checkY]++;
                    FlashOctopus(checkX, checkY, grid, flashedOctos);
                }
                if (x > 0 && y < grid.GetLength(1) - 1)
                {
                    var checkX = x - 1;
                    var checkY = y + 1;

                    grid[checkX, checkY]++;
                    FlashOctopus(checkX, checkY, grid, flashedOctos);
                }
                if (y < grid.GetLength(1) - 1)
                {
                    var checkX = x;
                    var checkY = y + 1;

                    grid[checkX, checkY]++;
                    FlashOctopus(checkX, checkY, grid, flashedOctos);
                }
                if (x < grid.GetLength(0) - 1 && y < grid.GetLength(1) - 1)
                {
                    var checkX = x + 1;
                    var checkY = y + 1;

                    grid[checkX, checkY]++;
                    FlashOctopus(checkX, checkY, grid, flashedOctos);
                }
            }
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day11/Data.txt");

            var grid = new int[data.First().Length, data.Length];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var val = int.Parse(data[i][j].ToString());
                    grid[i, j] = val;
                }
            }

            var allFlashed = false;
            var counter = 0;

            while (allFlashed != true)
            {

                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    for (int k = 0; k < grid.GetLength(1); k++)
                    {
                        grid[j, k]++;
                    }
                }

                List<Tuple<int, int>> flashedOctos = new List<Tuple<int, int>>();

                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    for (int k = 0; k < grid.GetLength(1); k++)
                    {
                        FlashOctopus(j, k, grid, flashedOctos);
                    }
                }

                var stepFlashed = 0;

                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    for (int k = 0; k < grid.GetLength(1); k++)
                    {
                        if (grid[j, k] > 9)
                        {
                            grid[j, k] = 0;
                            stepFlashed++;
                        }
                    }
                }

                allFlashed = stepFlashed == grid.Length;
                counter++;
            }

            return counter.ToString();
        }
    }
}
