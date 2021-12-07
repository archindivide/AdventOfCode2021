using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayFive
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day5/Data.txt");
            var lines = new List<LineSegment>();

            foreach (var datum in data)
            {
                var points = datum.Split("->");
                var point1 = points[0].Split(',');
                var x1 = int.Parse(point1[0]);
                var y1 = int.Parse(point1[1]);
                var point2 = points[1].Split(',');
                var x2 = int.Parse(point2[0]);
                var y2 = int.Parse(point2[1]);

                lines.Add(new LineSegment(x1, y1, x2, y2));
            }

            var gridMaxX = lines.Select(l => Math.Max(l.X1, l.X2)).Max();
            var gridMaxY = lines.Select(l => Math.Max(l.Y1, l.Y2)).Max();

            var grid = new int[gridMaxX + 1, gridMaxY + 1];

            foreach (var line in lines)
            {
                if (line.Y1 == line.Y2)
                {
                    var minX = Math.Min(line.X1, line.X2);
                    var maxX = Math.Max(line.X1, line.X2);

                    for (int i = minX; i <= maxX; i++)
                    {
                        grid[i, line.Y1]++;
                    }
                }
                else if (line.X1 == line.X2)
                {
                    var minY = Math.Min(line.Y1, line.Y2);
                    var maxY = Math.Max(line.Y1, line.Y2);

                    for (int i = minY; i <= maxY; i++)
                    {
                        grid[line.X1, i]++;
                    }
                }
            }

            var counter = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] > 1)
                    {
                        counter++;
                    }
                }
            }

            return counter.ToString();
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day5/Data.txt");
            var lines = new List<LineSegment>();

            foreach (var datum in data)
            {
                var points = datum.Split("->");
                var point1 = points[0].Split(',');
                var x1 = int.Parse(point1[0]);
                var y1 = int.Parse(point1[1]);
                var point2 = points[1].Split(',');
                var x2 = int.Parse(point2[0]);
                var y2 = int.Parse(point2[1]);

                lines.Add(new LineSegment(x1, y1, x2, y2));
            }

            var gridMaxX = lines.Select(l => Math.Max(l.X1, l.X2)).Max();
            var gridMaxY = lines.Select(l => Math.Max(l.Y1, l.Y2)).Max();

            var grid = new int[gridMaxX + 1, gridMaxY + 1];

            foreach (var line in lines)
            {
                if (line.Y1 == line.Y2)
                {
                    var minX = Math.Min(line.X1, line.X2);
                    var maxX = Math.Max(line.X1, line.X2);

                    for (int i = minX; i <= maxX; i++)
                    {
                        grid[i, line.Y1]++;
                    }
                }
                else if (line.X1 == line.X2)
                {
                    var minY = Math.Min(line.Y1, line.Y2);
                    var maxY = Math.Max(line.Y1, line.Y2);

                    for (int i = minY; i <= maxY; i++)
                    {
                        grid[line.X1, i]++;
                    }
                }
                else
                {
                    if(line.Y2 > line.Y1 && line.X2 > line.X1)
                    {
                        var startX = line.X1;
                        var startY = line.Y1;
                        var endX = line.X2;
                        var endY = line.Y2;
                        while(startX != endX && startY != endY)
                        {
                            grid[startX++, startY++]++;
                        }
                        grid[endX, endY]++;
                    }
                    else if(line.Y1 > line.Y2 && line.X1 > line.X2)
                    {
                        var startX = line.X1;
                        var startY = line.Y1;
                        var endX = line.X2;
                        var endY = line.Y2;
                        while (startX != endX && startY != endY)
                        {
                            grid[startX--, startY--]++;
                        }
                        grid[endX, endY]++;
                    }
                    else if (line.Y2 > line.Y1 && line.X1 > line.X2)
                    {
                        var startX = line.X1;
                        var startY = line.Y1;
                        var endX = line.X2;
                        var endY = line.Y2;
                        while (startX != endX && startY != endY)
                        {
                            grid[startX--, startY++]++;
                        }
                        grid[endX, endY]++;
                    }
                    else if (line.Y1 > line.Y2 && line.X2 > line.X1)
                    {
                        var startX = line.X1;
                        var startY = line.Y1;
                        var endX = line.X2;
                        var endY = line.Y2;
                        while (startX != endX && startY != endY)
                        {
                            grid[startX++, startY--]++;
                        }
                        grid[endX, endY]++;
                    }
                }
            }

            var counter = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] > 1)
                    {
                        counter++;
                    }
                }
            }

            return counter.ToString();
        }
    }

    public class LineSegment
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public LineSegment(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
    }
}
