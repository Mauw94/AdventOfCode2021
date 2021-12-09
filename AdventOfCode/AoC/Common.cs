using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    public static class Common
    {
        public static List<string> GetInput(int day)
        {
            var fileName = $"day{day}.txt";
            var inputPath = "C:\\Projects\\AdventOfCode2021\\input\\";
            var file = inputPath + fileName;

            if (File.Exists(file))
                return File.ReadAllLines(file).ToList();

            throw new Exception($"Couldn't file for day: {day}");
        }
    }
}
