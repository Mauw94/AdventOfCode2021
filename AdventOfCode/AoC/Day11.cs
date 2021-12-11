using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    class Octopus
    {
        public int Energy { get; set; }
        public bool Flashed { get; set; }

        public Octopus(int energy, bool flashed = false)
        {
            Energy = energy;
            Flashed = flashed;
        }
    }

    class Cavern
    {
        public Octopus[][] Octopi { get; set; }
        public int FlashedCounter { get; private set; }

        private readonly int _rows;
        private readonly int _columns;

        public Cavern()
        {
            Octopi = Common.GetInput(11)
                .Select(ToOctoArray)
                .ToArray();

            _rows = Octopi.Length;
            _columns = Octopi[0].Length;
        }

        public void Step()
        {
            ResetFlashed();
            IncreaseEnergyLevels();

            bool flashed;
            do
            {
                flashed = false;

                for (int row = 0; row < _rows; row++)
                    for (int column = 0; column < _columns; column++)
                    {
                        var octo = CurrentOcto(row, column);

                        if (octo.Flashed)
                            continue;

                        if (octo.Energy <= 9)
                            continue;

                        flashed = true;
                        octo.Flashed = true;
                        FlashedCounter++;

                        foreach (var neighbour in Neighbours(row, column))
                        {
                            if (neighbour.Flashed)
                                continue;

                            neighbour.Energy++;
                        }
                    }

            } while (flashed);

            // Reset all flashed octopi energy back to 0.
            for (int row = 0; row < _rows; row++)
                for (int column = 0; column < _columns; column++)
                {
                    var octo = CurrentOcto(row, column);
                    if (octo.Flashed)
                        octo.Energy = 0;
                }
        }

        Octopus[] ToOctoArray(string digits)
        {
            return digits.ToCharArray().Select(c => new Octopus(c - 48)).ToArray();
        }

        void IncreaseEnergyLevels()
        {
            for (int row = 0; row < _rows; row++)
                for (int column = 0; column < _columns; column++)
                {
                    CurrentOcto(row, column).Energy++;
                }
        }

        void ResetFlashed()
        {
            for (int row = 0; row < _rows; row++)
                for (int column = 0; column < _columns; column++)
                {
                    CurrentOcto(row, column).Flashed = false;
                }
        }

        Octopus CurrentOcto(int row, int col)
        {
            return Octopi[row][col];
        }

        IEnumerable<Octopus> Neighbours(int row, int col)
        {
            List<(int r, int c)> neighbours = new()
            {
                (row - 1, col - 1),
                (row - 1, col),
                (row - 1, col + 1),

                (row, col - 1),
                (row, col + 1),

                (row + 1, col - 1),
                (row + 1, col),
                (row + 1, col + 1),
            };

            return neighbours
                .Where(o => o.r >= 0 && o.r < _rows
                    && o.c >= 0 && o.c < _columns)
                .Select(o => Octopi[o.r][o.c]);
        }

        int OctoCount() => Octopi.Length * Octopi[0].Length;
        int FlashCount() => Octopi.SelectMany(x => x.Where(o => o.Flashed)).Count();
        public bool AllFlashed() => FlashCount() == OctoCount();
    }

    public class Day11 : AdventBase
    {
        private readonly int _steps;
        public Day11()
        {
            _steps = 100;
        }

        public override void Solution1()
        {
            var cavern = new Cavern();

            for (int i = 0; i < _steps; i++)
                cavern.Step();

            LogResults(11, 1, cavern.FlashedCounter.ToString());
        }

        public override void Solution2()
        {
            var cavern = new Cavern();
            var steps = 0;

            do
            {
                cavern.Step();
                steps++;
            } while (!cavern.AllFlashed());

            LogResults(11, 2, steps.ToString());
        }
    }
}
