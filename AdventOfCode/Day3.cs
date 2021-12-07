using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day3 : Base
    {
        public override void Solution1()
        {
            List<string> fileContent = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day3.txt").ToList();
            List<int> first = new List<int>();
            List<int> second = new List<int>();
            List<int> third = new List<int>();
            List<int> fourth = new List<int>();
            List<int> fifth = new List<int>();
            List<int> sixth = new List<int>();
            List<int> seventh = new List<int>();
            List<int> eight = new List<int>();
            List<int> ninth = new List<int>();
            List<int> tenth = new List<int>();
            List<int> eleventh = new List<int>();
            List<int> twelvth = new List<int>();
            int gamma = 0;
            int epsilon = 0;

            // 001000010101
            foreach (var content in fileContent)
            {
                var chars = content.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    var n = int.Parse(chars[i].ToString());
                    if (i == 0)
                        first.Add(n);
                    if (i == 1)
                        second.Add(n);
                    if (i == 2)
                        third.Add(n);
                    if (i == 3)
                        fourth.Add(n);
                    if (i == 4)
                        fifth.Add(n);
                    if (i == 5)
                        sixth.Add(n);
                    if (i == 6)
                        seventh.Add(n);
                    if (i == 7)
                        eight.Add(n);
                    if (i == 8)
                        ninth.Add(n);
                    if (i == 9)
                        tenth.Add(n);
                    if (i == 10)
                        eleventh.Add(n);
                    if (i == 11)
                        twelvth.Add(n);
                }
            }

            int[] gammaBits = new int[12];
            gammaBits[0] = GetMostSignificant(first);
            gammaBits[1] = GetMostSignificant(second);
            gammaBits[2] = GetMostSignificant(third);
            gammaBits[3] = GetMostSignificant(fourth);
            gammaBits[4] = GetMostSignificant(fifth);
            gammaBits[5] = GetMostSignificant(sixth);
            gammaBits[6] = GetMostSignificant(seventh);
            gammaBits[7] = GetMostSignificant(eight);
            gammaBits[8] = GetMostSignificant(ninth);
            gammaBits[9] = GetMostSignificant(tenth);
            gammaBits[10] = GetMostSignificant(eleventh);
            gammaBits[11] = GetMostSignificant(twelvth);

            string binaryGamma = string.Empty;
            string binaryEpsilon = string.Empty;
            for (int i = 0; i < gammaBits.Length; i++)
            {
                binaryGamma += gammaBits[i].ToString();
                binaryEpsilon += ReverseBit(gammaBits[i]).ToString();
            }
            gamma = Convert.ToInt32(binaryGamma, 2);
            epsilon = Convert.ToInt32(binaryEpsilon, 2);

            base.LogResults(3, 1, (gamma * epsilon).ToString());
        }

        public override void Solution2()
        {
            List<string> fileContent = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day3.txt").ToList();
            int globalOxygenCounter = 0;
            StartCalculations(fileContent, globalOxygenCounter, true);

            List<string> fileContent2 = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day3.txt").ToList();
            int globalCO2Counter = 0;
            StartCalculations(fileContent2, globalCO2Counter, false);
        }

        private int GetMostSignificant(List<int> list)
        {
            if (list.Where(x => x == 1).Count() > list.Where(x => x == 0).Count())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private int ReverseBit(int bit)
        {
            if (bit == 0) return 1;
            return 0;
        }

        private void StartCalculations(List<string> fileContent, int global, bool oxygen)
        {
            Dictionary<int, List<int>> bits = new Dictionary<int, List<int>>();

            for (int i = 0; i < fileContent.Count; i++)
            {
                var input = fileContent[i];
                for (int j = 0; j < input.Length; j++)
                {
                    if (!bits.ContainsKey(j))
                        bits.Add(j, new List<int>());

                    bits[j].Add(int.Parse(input[j].ToString()));
                }
            }

            if (oxygen)
            {
                if (fileContent.Count == 1)
                {
                    base.LogResults(3, 2, "Oxyen generator rating: " + Convert.ToInt32(fileContent[0], 2));
                    return;
                }
                DetermineOxygenRating(bits, fileContent, global);
            }
            else
            {
                if (fileContent.Count == 1)
                {
                    base.LogResults(3, 2, "CO2 scrubber rating: " + Convert.ToInt32(fileContent[0], 2));
                    return;
                }
                DetermineCO2Rating(bits, fileContent, global);
            }
        }

        private void DetermineOxygenRating(Dictionary<int, List<int>> bits, List<string> fileContent, int counter)
        {
            var newBits = new Dictionary<int, List<int>>();

            var list = bits[counter];
            var zerosCount = list.Where(x => x == 0).Count();
            var onesCount = list.Where(x => x == 1).Count();

            List<string> newFileContent = new List<string>();

            if (onesCount > zerosCount)
            {
                newFileContent = fileContent.Where(x => x.Substring(counter, 1) == "1").Select(x => x).ToList();
            }
            else if (zerosCount > onesCount)
            {
                newFileContent = fileContent.Where(x => x.Substring(counter, 1) == "0").Select(x => x).ToList();
            }
            else
            {
                newFileContent = fileContent.Where(x => x.Substring(counter, 1) == "1").Select(x => x).ToList();
            }

            counter++;

            StartCalculations(newFileContent, counter, true);
        }

        private void DetermineCO2Rating(Dictionary<int, List<int>> bits, List<string> fileContent, int counter)
        {
            var newBits = new Dictionary<int, List<int>>();

            var list = bits[counter];
            var zerosCount = list.Where(x => x == 0).Count();
            var onesCount = list.Where(x => x == 1).Count();

            List<string> newFileContent = new List<string>();

            if (onesCount > zerosCount)
            {
                newFileContent = fileContent.Where(x => x.Substring(counter, 1) == "0").Select(x => x).ToList();
            }
            else if (zerosCount > onesCount)
            {
                newFileContent = fileContent.Where(x => x.Substring(counter, 1) == "1").Select(x => x).ToList();
            }
            else
            {
                newFileContent = fileContent.Where(x => x.Substring(counter, 1) == "0").Select(x => x).ToList();
            }

            counter++;

            StartCalculations(newFileContent, counter, false);
        }
    }
}

