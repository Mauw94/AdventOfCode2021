using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day2 : IBase
    {
        public Day2()
        {
        }

        public override void Solution1()
        {
            int depth = 0;
            int horizontalPos = 0;
            List<string> fileContent = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day2.txt").ToList();

            foreach (var content in fileContent)
            {
                var space = content.IndexOf(" ");
                var move = content.Substring(0, space);
                var amount = int.Parse(content.Substring(space + 1, 1));

                if (move == "forward")
                    horizontalPos += amount;
                if (move == "up")
                    depth -= amount;
                if (move == "down")
                    depth += amount;
            }

            base.LogResults(2, 1, (depth * horizontalPos).ToString());
        }

        public override void Solution2()
        {
            int depth = 0;
            int horizontalPos = 0;
            int aim = 0;
            List<string> fileContent = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day2.txt").ToList();

            foreach (var content in fileContent)
            {
                var space = content.IndexOf(" ");
                var move = content.Substring(0, space);
                var amount = int.Parse(content.Substring(space + 1, 1));

                if (move == "forward")
                {
                    horizontalPos += amount;
                    if (aim != 0)
                        depth += amount * aim;
                }
                if (move == "up")
                    aim -= amount;
                if (move == "down")
                    aim += amount;
            }

            base.LogResults(2, 2, (depth * horizontalPos).ToString());
        }
    }
}
