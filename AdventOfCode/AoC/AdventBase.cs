using System;

namespace AdventOfCode2021.AoC
{
    public class AdventBase : IAdventBase
    {
        public virtual void Solution1()
        {
            throw new NotImplementedException();
        }

        public virtual void Solution2()
        {
            throw new NotImplementedException();
        }

        public virtual void LogResults(int day, int solution, string result)
        {
            Console.WriteLine("Day: " + day.ToString() + "\tSolution: " + solution.ToString() + "\tResult: " + result + "\n");
        }
    }
}
