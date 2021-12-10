using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayTen
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day10/Data.txt");

            List<string> illegal = new List<string>();

            foreach (string line in data)
            {
                Stack<string> stack = new Stack<string>();
                foreach (char c in line)
                {
                    if (c == '[' || c == '(' || c == '{' || c == '<')
                    {
                        stack.Push(c.ToString());
                    }
                    else
                    {
                        var eval = stack.Pop();
                        if (c == ']' && eval != "[")
                        {
                            illegal.Add(c.ToString());
                            break;
                        }
                        if (c == ')' && eval != "(")
                        {
                            illegal.Add(c.ToString());
                            break;
                        }
                        if (c == '}' && eval != "{")
                        {
                            illegal.Add(c.ToString());
                            break;
                        }
                        if (c == '>' && eval != "<")
                        {
                            illegal.Add(c.ToString());
                            break;
                        }
                    }
                }
            }

            return EvaluateScore(illegal).ToString();
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day10/Data.txt");

            List<string> illegal = new List<string>();
            List<Stack<string>> incompleteStacks = new List<Stack<string>>();

            foreach (string line in data)
            {
                Stack<string> stack = new Stack<string>();
                var addStack = true;
                foreach (char c in line)
                {
                    if (c == '[' || c == '(' || c == '{' || c == '<')
                    {
                        stack.Push(c.ToString());
                    }
                    else
                    {
                        var eval = stack.Pop();
                        if (c == ']' && eval != "[")
                        {
                            illegal.Add(c.ToString());
                            addStack = false;
                            break;
                        }
                        if (c == ')' && eval != "(")
                        {
                            illegal.Add(c.ToString());
                            addStack = false;
                            break;
                        }
                        if (c == '}' && eval != "{")
                        {
                            illegal.Add(c.ToString());
                            addStack = false;
                            break;
                        }
                        if (c == '>' && eval != "<")
                        {
                            illegal.Add(c.ToString());
                            addStack = false;
                            break;
                        }
                    }
                }
                if (addStack && stack.Count > 0)
                {
                    incompleteStacks.Add(stack);
                }
            }

            List<long> scores = new List<long>();

            foreach(var stack in incompleteStacks)
            {
                var completeStack = new List<string>();
                while(stack.Count > 0)
                {
                    string eval = stack.Pop();
                    if(eval == "[")
                    {
                        completeStack.Add("]");
                    }
                    else if (eval == "(")
                    {
                        completeStack.Add(")");
                    }
                    else if (eval == "{")
                    {
                        completeStack.Add("}");
                    }
                    else if (eval == "<")
                    {
                        completeStack.Add(">");
                    }
                }
                scores.Add(EvaluateScorePt2(completeStack));
            }

            scores.Sort();
            var finalScore = scores[(scores.Count-1) / 2];

            return finalScore.ToString();
        }

        public long EvaluateScore(List<string> illegalCharacters)
        {
            long sum = 0;
            foreach(var character in illegalCharacters)
            {
                switch (character)
                {
                    case ")":
                        sum += 3;
                        break;
                    case "]":
                        sum += 57;
                        break;
                    case "}":
                        sum += 1197;
                        break;
                    case ">":
                        sum += 25137;
                        break;
                    default:
                        throw new Exception("Unexpected illegal character");
                }
            }
            return sum;
        }

        public long EvaluateScorePt2(List<string> completetionCharacters)
        {
            long sum = 0;
            foreach (var character in completetionCharacters)
            {
                sum *= 5;
                switch (character)
                {
                    case ")":
                        sum += 1;
                        break;
                    case "]":
                        sum += 2;
                        break;
                    case "}":
                        sum += 3;
                        break;
                    case ">":
                        sum += 4;
                        break;
                    default:
                        throw new Exception("Unexpected illegal character");
                }
            }
            return sum;
        }
    }
}
