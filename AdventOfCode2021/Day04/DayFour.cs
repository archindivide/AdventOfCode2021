using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayFour
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day04/Data.txt");
            var numbers = data.First().Split(',');

            var lines = new List<string>();
            var boards = new List<Board>();

            for(var i = 2; i < data.Length; i++)
            {
                if(lines.Count() == 5)
                {
                    boards.Add(new Board(lines));
                    lines = new List<string>();
                }
                else
                {
                    lines.Add(data[i]);
                }
            }
            boards.Add(new Board(lines));

            foreach(var number in numbers)
            {
                foreach(var board in boards)
                {
                    var won = board.CallNumberDidWin(int.Parse(number));
                    if (won)
                    {
                        return board.CalculateScore().ToString();
                    }
                }
            }

            return "";
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day04/Data.txt");
            var numbers = data.First().Split(',');

            var lines = new List<string>();
            var boards = new List<Board>();

            for (var i = 2; i < data.Length; i++)
            {
                if (lines.Count() == 5)
                {
                    boards.Add(new Board(lines));
                    lines = new List<string>();
                }
                else
                {
                    lines.Add(data[i]);
                }
            }
            boards.Add(new Board(lines));
            var winningBoards = new List<Board>();

            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    var won = board.CallNumberDidWin(int.Parse(number));
                    if (won && !winningBoards.Contains(board))
                    {
                        winningBoards.Add(board);
                    }
                }
            }

            return winningBoards.Last().CalculateScore().ToString();
        }
    }

    public class Board
    {
        public int[][] Values { get; set; }
        public List<int> Called { get; set; }
        public bool[][] Matches { get; set; }
        public bool Won { get; set; }

        public Board(List<string> lines)
        {
            Values = new int[lines.Count()][];
            Matches = new bool[lines.Count()][];
            var counter = 0;
            var counter2 = 0;
            foreach(var line in lines)
            {
                var nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Values[counter] = new int[nums.Length];
                Matches[counter] = new bool[nums.Length];
                foreach(var num in nums)
                {
                    Values[counter][counter2] = int.Parse(num);
                    Matches[counter][counter2] = false;
                    counter2++;
                }
                counter2 = 0;
                counter++;
            }
            Called = new List<int>();
        }

        public bool CallNumberDidWin(int justCalled)
        {
            if (!Won)
            {
                Called.Add(justCalled);
                for (int i = 0; i < Values.Length; i++)
                {
                    for (int j = 0; j < Values[i].Length; j++)
                    {
                        if (Values[i][j] == justCalled)
                        {
                            Matches[i][j] = true;
                            if (Matches[i].All(m => m))
                            {
                                Won = true;
                                return Won;
                            }
                            else if (Matches.All(m => m[j]))
                            {
                                Won = true;
                                return Won;
                            }
                        }
                    }
                }
                Won = false;
            }

            return Won;
        }

        public int CalculateScore()
        {
            var sumUnmarked = 0;
            for(int i = 0; i < Values.Length; i++)
            {
                for(int j = 0; j < Values[i].Length; j++)
                {
                    if (!Matches[i][j])
                    {
                        sumUnmarked += Values[i][j];
                    }
                }
            }
            return sumUnmarked * Called.Last();
        }
    }
}
