using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    public class Day8 : AdventBase
    {
        private List<string> _outputValues;
        private List<string> _segments;

        public Day8()
        {
            _outputValues = new List<string>();
            _segments = new List<string>();
            string[] input = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day8.txt");
            foreach (var line in input)
            {
                _segments.Add(line);
                var delimIndex = line.IndexOf('|');
                var output = line.Substring(delimIndex + 2, line.Length - delimIndex - 2);
                _outputValues.Add(output);
            }
        }

        public override void Solution1()
        {
            var occurence = 0;
            int[] digits = new int[] { 2, 4, 3, 7 };
            foreach (var outputValue in _outputValues)
            {
                var values = outputValue.Split(" ");
                foreach (var value in values)
                {
                    var len = value.Length;
                    for (int i = 0; i < digits.Length; i++)
                    {
                        if (len == digits[i])
                            occurence++;
                    }
                }
            }

            LogResults(8, 1, occurence.ToString());
        }

        public override void Solution2()
        {
            var totalCount = 0;

            var zero = string.Empty;
            var one = string.Empty;
            var two = string.Empty;
            var three = string.Empty;
            var four = string.Empty;
            var five = string.Empty;
            var six = string.Empty;
            var seven = string.Empty;
            var eight = string.Empty;
            var nine = string.Empty;

            foreach (var segment in _segments)
            {
                // Get the unique patterns and figure out the fixed patterns first.
                var delimIndex = segment.IndexOf("|");
                var uniquePatterns = segment[..(delimIndex - 1)].Split(" ");
                var digits = segment[(delimIndex + 2)..].Split(" ");
                one = uniquePatterns.First(x => x.Length == 2);
                four = uniquePatterns.First(x => x.Length == 4);
                seven = uniquePatterns.First(x => x.Length == 3);
                eight = uniquePatterns.First(x => x.Length == 7);

                // Figure out the other digits.
                var middle_topLeft = four.Except(one);
                var top = seven.Except(one);
                var bottom_bottomLeft = eight.Except(four).Except(top);
                nine = uniquePatterns.Where((x) => x.Length == 6 && x.Except(four).Except(top).Count() == 1).First();
                var bottom = nine.Except(four).Except(top);
                var bottomLeft = bottom_bottomLeft.Except(bottom);
                zero = uniquePatterns.Where((x) => x.Length == 6 && x.Except(seven).Except(bottom_bottomLeft).Count() == 1).First();
                var topLeft = zero.Except(seven).Except(bottom_bottomLeft);
                var middle = middle_topLeft.Except(topLeft);
                six = uniquePatterns.Where((x) => x.Length == 6 && x.Except(top).Except(middle_topLeft).Except(bottom).Except(bottomLeft).Count() == 1).First();
                var bottomRight = six.Except(top).Except(middle_topLeft).Except(bottom).Except(bottomLeft);
                var topRight = one.Except(bottomRight);
                two = new string(eight.Except(bottomRight).Except(topLeft).ToArray());
                three = new string(nine.Except(topLeft).ToArray());
                five = new string(six.Except(bottomLeft).ToArray());

                zero = string.Concat(zero.OrderBy(x => x));
                one = string.Concat(one.OrderBy(x => x));
                two = string.Concat(two.OrderBy(x => x));
                three = string.Concat(three.OrderBy(x => x));
                four = string.Concat(four.OrderBy(x => x));
                five = string.Concat(five.OrderBy(x => x));
                six = string.Concat(six.OrderBy(x => x));
                seven = string.Concat(seven.OrderBy(x => x));
                eight = string.Concat(eight.OrderBy(x => x));
                nine = string.Concat(nine.OrderBy(x => x));
                var matches = new Dictionary<string, int>();
                matches.Add(zero, 0);
                matches.Add(one, 1);
                matches.Add(two, 2);
                matches.Add(three, 3);
                matches.Add(four, 4);
                matches.Add(five, 5);
                matches.Add(six, 6);
                matches.Add(seven, 7);
                matches.Add(eight, 8);
                matches.Add(nine, 9);

                var allPatterns = new List<string> { zero, one, two, three, four, five, six, seven, eight, nine };

                var decodedDigit = string.Empty;
                foreach (var digit in digits)
                {
                    if (allPatterns.Contains(string.Concat(digit.OrderBy(x => x))))
                    {
                        decodedDigit += matches[string.Concat(digit.OrderBy(x => x))].ToString();
                    }
                }

                totalCount += int.Parse(decodedDigit);
            }

            LogResults(8, 2, totalCount.ToString());
        }
    }
}
