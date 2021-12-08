using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayEight
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day8/Data.txt");

            var countEasyNums = 0;

            foreach (var datum in data)
            {
                var signalPatterns = datum.Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                var outputValues = datum.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var ones = outputValues.Where(s => s.Length == 2);
                var fours = outputValues.Where(s => s.Length == 4);
                var sevens = outputValues.Where(s => s.Length == 3);
                var eights = outputValues.Where(s => s.Length == 7);

                countEasyNums += ones.Count() + fours.Count() + sevens.Count() + eights.Count();
            }

            return countEasyNums.ToString();
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day8/Data.txt");

            long returnVal = 0;

            foreach (var datum in data)
            {
                var signalPatterns = datum.Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                var outputValues = datum.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                signalPatterns.AddRange(outputValues);

                var determinedPatterns = DeterminePatterns(signalPatterns);

                string outputString = "";

                foreach(var output in outputValues)
                {
                    var patternMatched = PatternMatch(determinedPatterns, output);
                    if(patternMatched != null)
                    {
                        outputString += patternMatched;
                    }
                }

                returnVal += int.Parse(outputString);
            }

            return returnVal.ToString();
        }

        public Dictionary<string, string> DeterminePatterns(List<string> inputPatterns)
        {
            var validatedPatterns = new Dictionary<string, string>();

            var onePattern = inputPatterns.FirstOrDefault(s => s.Length == 2);
            if (onePattern != null)
            {
                validatedPatterns.Add(onePattern, "1");
            }
            var fourPattern = inputPatterns.FirstOrDefault(s => s.Length == 4);
            if (fourPattern != null)
            {
                validatedPatterns.Add(fourPattern, "4");
            }
            var sevenPattern = inputPatterns.FirstOrDefault(s => s.Length == 3);
            if (sevenPattern != null)
            {
                validatedPatterns.Add(sevenPattern, "7");
            }
            var eightPattern = "abcdefg"; //eight is always all signals
            validatedPatterns.Add(eightPattern, "8");

            while (PatternsLeftToDetermine(validatedPatterns, inputPatterns))
            {
                DetermineZeroPattern(validatedPatterns, inputPatterns);
                DetermineTwoPattern(validatedPatterns, inputPatterns);
                DetermineThreePattern(validatedPatterns, inputPatterns);
                DetermineFivePattern(validatedPatterns, inputPatterns);
                DetermineSixPattern(validatedPatterns, inputPatterns);
                DetermineNinePattern(validatedPatterns, inputPatterns);
                //DetermineFinalPattern(validatedPatterns, inputPatterns);
            }

            return validatedPatterns;
        }

        //private void DetermineFinalPattern(Dictionary<string, string> validatedPatterns, List<string> inputPatterns)
        //{
        //    var totalPatterns = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        //    if(validatedPatterns.Count == 9)
        //    {
        //        var lastNumber = totalPatterns.Except(validatedPatterns.Values).First();
        //        var lastPattern = inputPatterns.Where(i => PatternMatch(validatedPatterns, i) == null).First();
        //        validatedPatterns.Add(lastPattern, lastNumber);
        //    }
        //}

        private bool PatternsLeftToDetermine(Dictionary<string, string> validatedPatterns, List<string> inputPatterns)
        {
            var patternsLeft = false;
            foreach (var pattern in inputPatterns)
            {
                var output = PatternMatch(validatedPatterns, pattern);
                if(output == null)
                {
                    patternsLeft = true;
                }
            }
            return patternsLeft;
        }

        private string PatternMatch(Dictionary<string, string> validatedPatterns, string pattern)
        {
            var charArray = pattern.ToCharArray().ToList();
            charArray.Sort();

            foreach(var valPattern in validatedPatterns)
            {
                var checkArray = valPattern.Key.ToCharArray().ToList();
                checkArray.Sort();
                if (charArray.SequenceEqual(checkArray))
                {
                    return valPattern.Value;
                }
            }

            return null;
        }

        private bool CheckOverlap(string pattern, Dictionary<string, string> validatedPatterns, string[] checkPatterns)
        {
            var patternsToCheck = validatedPatterns.Where(v => checkPatterns.Contains(v.Value));

            foreach(var patternToCheck in patternsToCheck)
            {
                var charArray = pattern.ToCharArray();
                var checkArray = patternToCheck.Key.ToCharArray();
                if(checkArray.All(c => charArray.Contains(c)))
                {
                    return true;
                }
            }

            return false;
        }

        private bool OverlappedBy(string pattern, Dictionary<string, string> validatedPatterns, string[] checkPatterns)
        {
            var patternsToCheck = validatedPatterns.Where(v => checkPatterns.Contains(v.Value));

            foreach (var patternToCheck in patternsToCheck)
            {
                var charArray = pattern.ToCharArray();
                var checkArray = patternToCheck.Key.ToCharArray();
                if (charArray.All(c => checkArray.Contains(c)))
                {
                    return true;
                }
            }

            return false;
        }


        private void DetermineZeroPattern(Dictionary<string, string> validatedPatterns, List<string> inputPatterns)
        {
            if (validatedPatterns.Values.Contains("0"))
            {
                return;
            }
            
            var possibleZeros = inputPatterns.Where(i => i.Length == 6 && PatternMatch(validatedPatterns, i) == null).ToList();

            if (possibleZeros.Count == 1)
            {
                validatedPatterns.Add(possibleZeros.Single(), "0");
            }
            else if(validatedPatterns.Values.Contains("6") && validatedPatterns.Values.Contains("9"))
            {
                validatedPatterns.Add(possibleZeros.First(), "0");
            }
        }

        private void DetermineTwoPattern(Dictionary<string, string> validatedPatterns, List<string> inputPatterns)
        {
            if (validatedPatterns.Values.Contains("2"))
            {
                return;
            }

            var possibleTwos = inputPatterns.Where(i => i.Length == 5 && PatternMatch(validatedPatterns, i) == null).ToList();

            if (possibleTwos.Count == 1)
            {
                validatedPatterns.Add(possibleTwos.Single(), "2");
            }
            else if (validatedPatterns.Values.Contains("5") && validatedPatterns.Values.Contains("3"))
            {
                validatedPatterns.Add(possibleTwos.First(), "2");
            }
        }

        private void DetermineThreePattern(Dictionary<string, string> validatedPatterns, List<string> inputPatterns)
        {
            if (validatedPatterns.Values.Contains("3"))
            {
                return;
            }

            var possibleThrees = inputPatterns.Where(i => i.Length == 5 && PatternMatch(validatedPatterns, i) == null).ToList();

            if (possibleThrees.Count == 1)
            {
                validatedPatterns.Add(possibleThrees.Single(), "3");
            }
            else
            {
                //2 and 5 cannot overlap 1 or 7
                possibleThrees = possibleThrees.Where(t => CheckOverlap(t, validatedPatterns, new string[] { "1", "7", "9" })).ToList();
                //could be multiple in different orders, we only care about one
                if (possibleThrees.Count > 0)
                {
                    validatedPatterns.Add(possibleThrees.First(), "3");
                }
            }
        }

        private void DetermineFivePattern(Dictionary<string, string> validatedPatterns, List<string> inputPatterns)
        {
            if (validatedPatterns.Values.Contains("5"))
            {
                return;
            }

            var possibleFives = inputPatterns.Where(i => i.Length == 5 && PatternMatch(validatedPatterns, i) == null).ToList();

            if (possibleFives.Count == 1)
            {
                validatedPatterns.Add(possibleFives.Single(), "5");
            }
            else
            {
                //5 can overlap 6 and 9, 2 and 3 cannot
                possibleFives = possibleFives.Where(t => OverlappedBy(t, validatedPatterns, new string[] { "6", "9" })).ToList();
                //could be multiple in different orders, we only care about one
                if (possibleFives.Count > 0)
                {
                    validatedPatterns.Add(possibleFives.First(), "5");
                }
            }
        }

        private void DetermineSixPattern(Dictionary<string, string> validatedPatterns, List<string> inputPatterns)
        {
            if (validatedPatterns.Values.Contains("6"))
            {
                return;
            }

            var possibleSixes = inputPatterns.Where(i => i.Length == 6 && PatternMatch(validatedPatterns, i) == null).ToList();

            if (possibleSixes.Count == 1)
            {
                validatedPatterns.Add(possibleSixes.Single(), "6");
            }
            else
            {
                var notSixes = possibleSixes.Where(t => CheckOverlap(t, validatedPatterns, new string[] { "1", "7" })).ToList();
                possibleSixes = possibleSixes.Except(notSixes).ToList();

                if (possibleSixes.Count > 0)
                {
                    validatedPatterns.Add(possibleSixes.First(), "6");
                }
            }
        }

        private void DetermineNinePattern(Dictionary<string, string> validatedPatterns, List<string> inputPatterns)
        {
            if (validatedPatterns.Values.Contains("9"))
            {
                return;
            }

            var possibleNines = inputPatterns.Where(i => i.Length == 6 && PatternMatch(validatedPatterns, i) == null).ToList();

            if (possibleNines.Count == 1)
            {
                validatedPatterns.Add(possibleNines.Single(), "9");
            }
            else
            {
                //0 and 6 cannot overlap 3 and 4, where 9 can
                possibleNines = possibleNines.Where(t => CheckOverlap(t, validatedPatterns, new string[] { "3", "4", "5" })).ToList();

                if (possibleNines.Count > 0)
                {
                    validatedPatterns.Add(possibleNines.First(), "9");
                }
            }
        }
    }
}
