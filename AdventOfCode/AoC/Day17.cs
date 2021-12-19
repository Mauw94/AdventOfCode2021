using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.AoC
{
    public class Day17 : AdventBase
    {
        readonly int _right;
        readonly int _left;
        readonly int _bottom;
        readonly int _top;
        public Day17()
        {
            var input = Common.GetInput(17).First();
            var numbers = Regex
                .Matches(input, @"-?\d+")
                .Select(m => m.Value)
                .Select(int.Parse)
                .ToArray();

            _left = numbers[0];
            _right = numbers[1];
            _bottom = numbers[2];
            _top = numbers[3];
        }
        (int Highest, int Hits) Simulate()
        {
            const int steps = 100_000;

            int i = 0;
            int highest = 0;
            int hits = 0;
            int dx = 1;
            int dy = _bottom;

            while (++i <= steps)
            {
                (bool hit, int h) = Fire(dx, dy);

                if (hit)
                {
                    hits++;
                    if (highest < h)
                        highest = h;
                }

                if (++dx > _right)
                {
                    dx = 1;
                    dy++;
                }
            }

            return (highest, hits);
        }

        (bool hit, int highest) Fire(int dx, int dy)
        {
            int x = 0;
            int y = 0;
            int highest = y;

            while (true)
            {
                x += dx;
                y += dy;

                if (highest < y)
                    highest = y;

                if (CheckTargetArea(x, y)) return (true, highest);
                if (NoHit(x, y)) return (false, highest);

                if (dx > 0) dx--;

                dy--;
            }
        }

        bool CheckTargetArea(int x, int y) => x >= _left
                                           && x <= _right
                                           && y >= _bottom
                                           && y <= _top;

        bool NoHit(int x, int y) => x > _right || y < _bottom;

        public override void Solution1()
        {
            LogResults(17, 1, Simulate().Highest);
        }

        public override void Solution2()
        {
            LogResults(17, 1, Simulate().Hits);
        }
    }
}
