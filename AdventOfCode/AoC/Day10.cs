using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    public class Day10 : AdventBase
    {
        private readonly List<string> _lines;

        private readonly Dictionary<char, char> _openClosePairs = new()
        {
            { '{', '}' },
            { '[', ']' },
            { '(', ')' },
            { '<', '>' }
        };

        public Day10()
        {
            _lines = Common.GetInput(10);
        }

        abstract record Result { }

        record Corrupted(char IllegalChar) : Result { }
        record Incomplete(char[] Missing) : Result { }
        record Complete : Result { }

        public override void Solution1()
        {
            int score = 0;

            foreach (var line in _lines)
            {
                var result = CheckLine(line);

                if (result is Corrupted corrupted)
                {
                    char illegal = corrupted.IllegalChar;

                    score += illegal switch
                    {
                        ')' => 3,
                        ']' => 57,
                        '}' => 1197,
                        '>' => 25137,
                        _ => throw new Exception($"{illegal} is not a correct char")
                    };
                }
            }

            LogResults(10, 1, score.ToString());
        }

        public override void Solution2()
        {
            List<long> scores = new();

            foreach (var line in _lines)
            {
                var result = CheckLine(line);

                if (result is Incomplete incomplete)
                {
                    char[] missing = incomplete.Missing;

                    scores.Add(Score(missing));
                }
            }

            scores.Sort();
            LogResults(10, 2, scores[scores.Count / 2].ToString());
        }

        Result CheckLine(string line)
        {
            // Stack is awesome; learning everyday! :)
            Stack<char> stack = new();

            foreach (char c in line.ToCharArray())
            {
                if ("{([<".Contains(c))
                {
                    stack.Push(c);
                }
                else
                {
                    // Take the first item from the stack, this should match the closing tag.
                    var opener = stack.Pop();
                    if (!Matching(opener, c))
                    {
                        return new Corrupted(c);
                    }
                }
            }

            // If there's objects left in the stack the line wasn't complete.
            if (stack.Any())
            {
                char[] missing = ReverseIncompleteStack(stack.ToArray());
                return new Incomplete(missing);
            }

            // Clean line.
            return new Complete();
        }

        bool Matching(char open, char close)
        {
            if (!_openClosePairs.ContainsKey(open))
                throw new Exception($"Dictionary does not contain this key {open}.");

            return _openClosePairs[open] == close;
        }

        char[] ReverseIncompleteStack(char[] incompletes)
        {
            List<char> missing = new();

            foreach (char c in incompletes)
            {
                missing.Add(_openClosePairs[c]);
            }

            return missing.ToArray(); ;
        }

        static long Score(char[] missing)
        {
            long score = 0;

            foreach (char c in missing)
            {
                score *= 5;
                score += c switch
                {
                    ')' => 1,
                    ']' => 2,
                    '}' => 3,
                    '>' => 4,
                    _ => throw new ArgumentOutOfRangeException($"Char {c} does not exist here.")
                };
            }

            return score;
        }
    }
}
