using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayOne
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day1/Data.txt");
            var typedData = data.Select(d => int.Parse(d)).ToList();
            var counter = 0;

            for (var i = 1; i < typedData.Count(); i++)
            {
                if (typedData[i] > typedData[i - 1])
                {
                    counter++;
                }
            }

            return counter.ToString();
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day1/Data.txt");
            var typedData = data.Select(d => int.Parse(d)).ToList();
            var counter = 0;

            var windows = new List<List<int>>();

            for (var i = 0; i < typedData.Count(); i++)
            {
                try
                {
                    var newWindow = new List<int>() { typedData[i], typedData[i + 1], typedData[i + 2] };
                    windows.Add(newWindow);
                }
                catch (Exception ex)
                {
                    //when it fails the completed windows are done
                    break;
                }
            }

            for (var i = 1; i < windows.Count; i++)
            {
                if(windows[i].Sum() > windows[i - 1].Sum())
                {
                    counter++;
                }
            }

            return counter.ToString();
        }
    }
}
