using System.IO;
using System.Linq;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var day1 = new Day1();
            day1.Solution1();
            day1.Solution2();

            var day2 = new Day2();
            day2.Solution1();
            day2.Solution2();

            var day3 = new Day3();
            day3.Solution1();
            day3.Solution2();

            var day4 = new Day4();
            day4.Solution1();
            day4.Solution2();
        }
    }
}
