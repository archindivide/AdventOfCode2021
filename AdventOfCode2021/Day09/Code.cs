using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayNine
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day09/Data.txt");

            var grid = new int[data.First().Length, data.Length];
            List<Point> points = new List<Point>();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var val = int.Parse(data[j][i].ToString());
                    grid[i, j] = val;
                    points.Add(new Point() { X = i, Y = j });
                }
            }

            var lowPoints = new List<Point>();

            foreach (var point in points)
            {
                if (point.IsLowPoint(grid))
                {
                    lowPoints.Add(point);
                }
            }

            var sumLowPoints = 0;

            foreach (var point in lowPoints)
            {
                sumLowPoints += grid[point.X, point.Y] + 1;
            }

            return sumLowPoints.ToString();
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day09/Data.txt");

            var grid = new int[data.First().Length, data.Length];
            List<Point> points = new List<Point>();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var val = int.Parse(data[j][i].ToString());
                    grid[i, j] = val;
                    points.Add(new Point() { X = i, Y = j });
                }
            }

            var basins = new List<List<Point>>();

            foreach (var point in points)
            {
                if (basins.Any(b => b.Any(p => p.X == point.X && p.Y == point.Y)))
                {
                    continue;
                }
                else
                {
                    var basin = new List<Point>();
                    point.GetBasin(grid, basin);
                    if (basin.Count > 0)
                    {
                        basins.Add(basin);
                    }
                }
            }
            var top3 = basins.OrderByDescending(b => b.Count).Take(3).ToList();

            return (top3[0].Count*top3[1].Count*top3[2].Count).ToString();
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool IsLowPoint(int[,] grid)
        {
            var leftLowerEqual = false;
            if (X > 0)
            {
                if (grid[X - 1, Y] <= grid[X, Y])
                {
                    leftLowerEqual = true;
                }
            }
            var upLowerEqual = false;
            if (Y > 0)
            {
                if (grid[X, Y - 1] <= grid[X, Y])
                {
                    upLowerEqual = true;
                }
            }
            var rightLowerEqual = false;
            if (X < grid.GetLength(0) - 1)
            {
                if (grid[X + 1, Y] <= grid[X, Y])
                {
                    rightLowerEqual = true;
                }
            }
            var downLowerEqual = false;
            if (Y < grid.GetLength(1) - 1)
            {
                if (grid[X, Y + 1] <= grid[X, Y])
                {
                    downLowerEqual = true;
                }
            }

            return !leftLowerEqual && !upLowerEqual && !rightLowerEqual && !downLowerEqual;
        }

        public void GetBasin(int[,] grid, List<Point> basin)
        {
            if (grid[X, Y] == 9 || (basin != null && basin.Any(p => p.X == X && p.Y == Y)))
            {
                return;
            }

            basin.Add(this);
            if (X > 0)
            {
                Point checkLeft = new Point() { X = X - 1, Y = Y };
                checkLeft.GetBasin(grid, basin);
            }
            if (Y > 0)
            {
                Point checkUp = new Point() { X = X, Y = Y - 1 };
                checkUp.GetBasin(grid, basin);
            }
            if (X < grid.GetLength(0) - 1)
            {
                Point checkRight = new Point() { X = X + 1, Y = Y };
                checkRight.GetBasin(grid, basin);
            }
            if (Y < grid.GetLength(1) - 1)
            {
                Point checkDown = new Point() { X = X, Y = Y + 1 };
                checkDown.GetBasin(grid, basin);
            }
        }
    }
}
