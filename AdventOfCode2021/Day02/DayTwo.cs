using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayTwo
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day02/Data.txt");

            var posX = 0;
            var posY = 0;

            foreach(string d in data)
            {
                var direction = d.Split(' ')[0];
                var unit = int.Parse(d.Split(' ')[1]);

                switch (direction)
                {
                    case "forward":
                        posX += unit;
                        break;
                    case "up":
                        posY -= unit;
                        break;
                    case "down":
                        posY += unit;
                        break;
                    default:
                        Console.WriteLine("unhandled direction: " + direction);
                        break;
                }
            }

            return (posX*posY).ToString();
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day02/Data.txt");

            var posX = 0;
            var posY = 0;
            var aim = 0;

            foreach (string d in data)
            {
                var direction = d.Split(' ')[0];
                var unit = int.Parse(d.Split(' ')[1]);

                switch (direction)
                {
                    case "forward":
                        posX += unit;
                        posY += aim * unit;
                        break;
                    case "up":
                        aim -= unit;
                        break;
                    case "down":
                        aim += unit;
                        break;
                    default:
                        Console.WriteLine("unhandled direction: " + direction);
                        break;
                }
            }

            return (posX * posY).ToString();
        }
    }
}
