using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayEightAbandoned
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day08/Data.txt");
            //maybe used later, got a little ahead of myself and started writing the decoding method
            //var defaultValues = new Dictionary<string, int>();
            //defaultValues.Add("abcefg", 0);
            //defaultValues.Add("cf", 1);
            //defaultValues.Add("acdeg", 2);
            //defaultValues.Add("acdfg", 3);
            //defaultValues.Add("bcdf", 4);
            //defaultValues.Add("abdfg", 5);
            //defaultValues.Add("abdefg", 6);
            //defaultValues.Add("acf", 7);
            //defaultValues.Add("abcdefg", 8);
            //defaultValues.Add("abcdfg", 9);

            var countEasyNums = 0;

            foreach (var datum in data)
            {
                var signalPatterns = datum.Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                var outputValues = datum.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                //signalPatterns.AddRange(outputValues);

                //var key = DecodeReplacementKeys(signalPatterns);
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
            var data = File.ReadAllLines("./Day08/Data.txt");

            long returnVal = 0;

            foreach (var datum in data)
            {
                var signalPatterns = datum.Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                var outputValues = datum.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                signalPatterns.AddRange(outputValues);

                SignalDecoder decoder = new SignalDecoder(signalPatterns, outputValues);

                returnVal += decoder.Decode();
            }

            return returnVal.ToString();
        }
    }

    public class SignalDecoder
    {
        string _nonOverLappingFourSignals;
        string ZeroSignal
        {
            get
            {
                string zero = null;
                return zero;
            }
        }
        string OneSignal
        {
            get
            {
                var one = AllSignals.FirstOrDefault(s => s.Length == 2);
                if (ReplacementKey.Keys.Contains('c') && !ReplacementKey.Keys.Contains('f'))
                {
                    ReplacementKey.Add('f', one.Single(s => s != ReplacementKey['c']));
                }
                if (!ReplacementKey.Keys.Contains('c') && ReplacementKey.Keys.Contains('f'))
                {
                    ReplacementKey.Add('c', one.Single(s => s != ReplacementKey['f']));
                }
                return one;
            }
        }
        string TwoSignal
        {
            get
            {
                string two = null;
                return two;
            }
        }
        string ThreeSignal
        {
            get
            {
                string three = null;
                if (NineSignal != null)
                {
                    var possibleThrees = AllSignals.Where(s => s.Length == 5);
                    three = possibleThrees.Where(s => s.All(c => NineSignal.Contains(c))).FirstOrDefault();
                    if (three != null && !ReplacementKey.Keys.Contains('b'))
                    {
                        ReplacementKey.Add('b', NineSignal.Where(s => !three.Contains(s)).Single());
                    }
                }
                return three;
            }
        }
        string FourSignal
        {
            get
            {
                var four = AllSignals.FirstOrDefault(s => s.Length == 4);
                if (four != null)
                {
                    if (OneSignal != null)
                    {
                        _nonOverLappingFourSignals = new string(four.Where(s => !OneSignal.Contains(s)).ToArray());
                    }
                    else if (SevenSignal != null)
                    {
                        _nonOverLappingFourSignals = new string(four.Where(s => !SevenSignal.Contains(s)).ToArray());
                    }

                    if (!string.IsNullOrWhiteSpace(_nonOverLappingFourSignals) && ReplacementKey.Keys.Contains('b') && !ReplacementKey.Keys.Contains('d'))
                    {
                        ReplacementKey.Add('d', _nonOverLappingFourSignals.Where(c => c != ReplacementKey['b']).Single());
                    }

                    if (!string.IsNullOrWhiteSpace(_nonOverLappingFourSignals) && ReplacementKey.Keys.Contains('d') && !ReplacementKey.Keys.Contains('b'))
                    {
                        ReplacementKey.Add('b', _nonOverLappingFourSignals.Where(c => c != ReplacementKey['d']).Single());
                    }
                }
                return four;
            }
        }
        string FiveSignal
        {
            get
            {
                string five = null;
                var set = new char[] { 'a', 'b', 'd', 'g' };
                if (set.All(s => ReplacementKey.Keys.Contains(s)))
                {
                    var valueSet = ReplacementKey.Where(k => set.Contains(k.Key)).Select(k => k.Value);
                    five = AllSignals.Where(s => s.Length == 5 && valueSet.All(c => s.Contains(c))).FirstOrDefault();
                    if (five != null && !ReplacementKey.Keys.Contains('f'))
                    {
                        ReplacementKey.Add('f', five.Except(ReplacementKey.Where(k => set.Contains(k.Key)).Select(k => k.Value)).Single());
                    }
                }
                return five;
            }
        }
        string SixSignal
        {
            get
            {
                string six = null;
                return six;
            }
        }
        string SevenSignal
        {
            get
            {
                var seven = AllSignals.FirstOrDefault(s => s.Length == 3);
                if (OneSignal != null && seven != null && !ReplacementKey.Keys.Contains('a'))
                {
                    ReplacementKey.Add('a', seven.Where(s => !OneSignal.Contains(s)).Single());
                }
                return seven;
            }
        }
        string EightSignal
        {
            get
            {
                var set = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
                if (ReplacementKey.Count() == 6)
                {
                    var missing = set.Except(ReplacementKey.Keys).Single();
                    var missingValue = set.Except(ReplacementKey.Values).Single();
                    ReplacementKey.Add(missing, missingValue);
                }
                return AllSignals.FirstOrDefault(s => s.Length == 7);
            }
        }
        string NineSignal
        {
            get
            {
                string nine = null;
                if (!string.IsNullOrWhiteSpace(_nonOverLappingFourSignals) && SevenSignal != null)
                {
                    var combinedSignal = _nonOverLappingFourSignals + SevenSignal;
                    var possibleNineSignals = AllSignals.Where(s => s.Length == 6);

                    nine = possibleNineSignals.Where(s => combinedSignal.All(c => s.Contains(c))).FirstOrDefault();
                    if (nine != null && !ReplacementKey.Keys.Contains('g'))
                    {
                        ReplacementKey.Add('g', nine.Where(s => !combinedSignal.Contains(s)).Single());
                    }
                }
                return nine;
            }
        }
        List<string> AllSignals { get; set; }
        Dictionary<char, char> ReplacementKey { get; set; }
        List<string> OutputValues { get; set; }

        public SignalDecoder(List<string> signals, List<string> outputValues)
        {
            AllSignals = signals;
            ReplacementKey = new Dictionary<char, char>();
            OutputValues = outputValues;

            while (ReplacementKey.Count < 7)
            {
                string s = OneSignal;
                string s1 = TwoSignal;
                string s2 = ThreeSignal;
                string s4 = FourSignal;
                string s5 = FiveSignal;
                string s6 = SixSignal;
                string s7 = SevenSignal;
                string s8 = EightSignal;
                string s9 = NineSignal;
            }
        }

        public int Decode()
        {
            var defaultValues = new Dictionary<string, string>();
            defaultValues.Add("abcefg", "0");
            defaultValues.Add("cf", "1");
            defaultValues.Add("acdeg", "2");
            defaultValues.Add("acdfg", "3");
            defaultValues.Add("bcdf", "4");
            defaultValues.Add("abdfg", "5");
            defaultValues.Add("abdefg", "6");
            defaultValues.Add("acf", "7");
            defaultValues.Add("abcdefg", "8");
            defaultValues.Add("abcdfg", "9");

            if (ReplacementKey.Count() == 7)
            {
                var outputVal = "";
                foreach (var output in OutputValues)
                {
                    var replacedVals = new List<char>();
                    foreach (var c in output)
                    {
                        replacedVals.Add(ReplacementKey[c]);
                    }
                    replacedVals.Sort();

                    outputVal += defaultValues[new string(replacedVals.ToArray())];
                }
                return int.Parse(outputVal);
            }
            throw new Exception("ReplacementKey unfinsihed");
        }
    }
}
