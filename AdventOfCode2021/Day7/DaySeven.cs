using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DaySeven
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day7/Data.txt");
            var crabSpots = data.First().Split(',').Select(d => int.Parse(d)).ToList();
            var mostCommon = new Dictionary<int, int>();

            //tried to be too smart here, arrived at the right answer through luck
            //doesn't consider every possible position the crabs could have moved to
            //discovered in part 2
            for (int i = 0; i < crabSpots.Count(); i++)
            {
                if (mostCommon.ContainsKey(crabSpots[i]))
                {
                    mostCommon[crabSpots[i]]++;
                }
                else
                {
                    mostCommon.Add(crabSpots[i], 1);
                }
            }

            mostCommon = mostCommon.OrderByDescending(m => m.Value).ToDictionary(m => m.Key, m => m.Value);
            var leastGas = -1;

            for (int i = 0; i < mostCommon.Count(); i++)
            {
                var check = CalculateCrabGas(mostCommon.Skip(i).Take(1).Single().Key, crabSpots);

                if(leastGas < 0 || leastGas > check)
                {
                    leastGas = check;
                }
            }
            
            return leastGas.ToString();
        }

        public int CalculateCrabGas(int positionToMove, List<int> crabPositions)
        {
            int gasUsed = 0;
            foreach (var crab in crabPositions)
            {
                gasUsed += Math.Abs(positionToMove - crab);
            }
            return gasUsed;
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day7/Data.txt");
            var crabSpots = data.First().Split(',').Select(d => int.Parse(d)).ToList();

            var minLocation = crabSpots.Min();
            var maxLocation = crabSpots.Max();
            //can reduce cycles by identifying the bottom of parabola - MtotheM

            long leastGas = -1;

            for (int i = minLocation; i <= maxLocation; i++)
            {
                var check = CalculateCrabGasMethod2(i, crabSpots);

                if (leastGas < 0 || leastGas > check)
                {
                    leastGas = check;
                }
            }

            return leastGas.ToString();
        }

        public long CalculateCrabGasMethod2(int positionToMove, List<int> crabPositions)
        {
            long gasUsed = 0;
            foreach (var crab in crabPositions)
            {
                gasUsed += CalculateTriangleNumber(Math.Abs(positionToMove - crab));
            }
            return gasUsed;
        }

        public long CalculateTriangleNumber(int num)
        {
            return ((num*num) + num)/2;
        }
    }
}
