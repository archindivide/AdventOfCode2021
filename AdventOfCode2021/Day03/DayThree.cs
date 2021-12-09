using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayThree
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day03/Data.txt");

            var bits = new List<int>();
            
            for(int i = 0; i < data[0].Length; i++)
            {
                var ones = data.Select(d => d[i] == '1' ? 1 : 0).Sum();
                var zeros = data.Select(d => d[i] == '0' ? 1 : 0).Sum();
                if(ones >= zeros)
                {
                    bits.Add(1);
                }
                else
                {
                    bits.Add(0);
                }
            }

            var inverseBits = new List<int>();

            foreach(int i in bits)
            {
                if(i == 1)
                {
                    inverseBits.Add(0);
                }
                else
                {
                    inverseBits.Add(1);
                }
            }

            var gamma = BinaryToDecimal(bits);
            var epsilon = BinaryToDecimal(inverseBits);

            return (gamma * epsilon).ToString();
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day03/Data.txt");

            var bits = new List<int>();

            var oxygenSet = data.ToList();
            var carbonDioxideSet = data.ToList();
            var index = 0;

            while(oxygenSet.Count() > 1)
            {
                var ones = oxygenSet.Select(o => o[index] == '1' ? 1 : 0).Sum();
                var zeros = oxygenSet.Select(o => o[index] == '0' ? 1 : 0).Sum();

                if(ones >= zeros)
                {
                    oxygenSet = oxygenSet.Where(o => o[index] == '1').ToList();
                }
                else
                {
                    oxygenSet = oxygenSet.Where(o => o[index] == '0').ToList();
                }

                index++;
            }

            index = 0;

            while (carbonDioxideSet.Count() > 1)
            {
                var ones = carbonDioxideSet.Select(o => o[index] == '1' ? 1 : 0).Sum();
                var zeros = carbonDioxideSet.Select(o => o[index] == '0' ? 1 : 0).Sum();

                if (ones < zeros)
                {
                    carbonDioxideSet = carbonDioxideSet.Where(o => o[index] == '1').ToList();
                }
                else
                {
                    carbonDioxideSet = carbonDioxideSet.Where(o => o[index] == '0').ToList();
                }

                index++;
            }

            var oxRating = BinaryToDecimal(oxygenSet.Single());
            var coRating = BinaryToDecimal(carbonDioxideSet.Single());

            return (oxRating*coRating).ToString();
        }

        public int BinaryToDecimal(List<int> binaryList)
        {
            var result = 0;
            for(int i = 0; i < binaryList.Count(); i++)
            { 
                if(binaryList[i] == 1)
                {
                    result += (int)(binaryList[i] * Math.Pow(2, binaryList.Count()-1 - i));
                }
            }
            return result;
        }

        public int BinaryToDecimal(string binaryString)
        {
            var binList = binaryString.Select(b => int.Parse(b.ToString())).ToList();

            return BinaryToDecimal(binList);
        }
    }
}
