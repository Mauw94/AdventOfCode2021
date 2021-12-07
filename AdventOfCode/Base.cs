using System;

namespace AdventOfCode
{
    public class Base
    {
        public virtual void Solution1() { }
        public virtual void Solution2() { }

        public virtual void LogResults(int day, int solution, string result)
        {
            Console.WriteLine("Day: " + day.ToString() + "\tSolution: " + solution.ToString() + "\tResult: " + result + "\n");
        }
    }
}
