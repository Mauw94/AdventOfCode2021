using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    class Cave
    {
        public bool IsSmall => Char.IsLower(Name.ToCharArray()[0]);
        public bool IsStart => Name == "start";
        public string Name { get; private set; }

        public Cave(string name)
        {
            Name = name;
        }
    }

    class CaveMap
    {
        public Dictionary<string, HashSet<string>> NeighbourList { get; } = new();
        public List<Cave> Caves { get; private set; }

        /// <summary>
        /// Sep up to create a Depth-first search structure.
        /// </summary>
        /// <param name="caves">All the caves available to search through.</param>
        /// <param name="neighbours">All neighbour pairs in the system.</param>
        public CaveMap(IEnumerable<Cave> caves, IEnumerable<Tuple<string, string>> neighbours)
        {
            Caves = caves.ToList();

            foreach (var cave in caves)
                AddCave(cave.Name);

            foreach (var neighbour in neighbours)
                AddNeighbour(neighbour);
        }

        /// <summary>
        /// Add all unique caves as key to the neighbour list.
        /// </summary>
        /// <param name="cave">Cave name.</param>
        public void AddCave(string cave)
        {
            NeighbourList[cave] = new HashSet<string>();
        }

        /// <summary>
        /// Add neighbours to each cave.
        /// </summary>
        /// <param name="neighbour">Neighbour.</param>
        public void AddNeighbour(Tuple<string, string> neighbour)
        {
            if (NeighbourList.ContainsKey(neighbour.Item1)
                && NeighbourList.ContainsKey(neighbour.Item2))
            {
                NeighbourList[neighbour.Item1].Add(neighbour.Item2);
                NeighbourList[neighbour.Item2].Add(neighbour.Item1);
            }
        }
    }

    class PathBuilder
    {
        public int PathCounter { get; private set; } = 0;

        private readonly Dictionary<string, List<int>> _pastVisits = new();
        private readonly CaveMap _map;
        private readonly int _part;
        private readonly bool _logging;

        /// <summary>
        /// Pathbuilder classes containing logic to calculate all the valid paths.
        /// </summary>
        /// <param name="map">The cavemap system.</param>
        /// <param name="part">Part 1 || part 2.</param>
        public PathBuilder(CaveMap map, int part, bool logging)
        {
            _map = map;
            _part = part;
            _logging = logging;

            foreach (var cave in _map.Caves)
                _pastVisits.Add(cave.Name, new());
        }

        /// <summary>
        /// Start path calculation.
        /// </summary>
        /// <returns>Amount of paths found.</returns>
        public int CalculatePaths()
        {
            // List used to log all the different paths in the console.
            List<string> pathList = new();
            pathList.Add("start");
            Search("start", pathList, 1);

            return PathCounter;
        }

        /// <summary>
        /// Calculate the different paths using the cavemap system.
        /// </summary>
        /// <param name="start">Start for every route.</param>
        /// <param name="pathList">Pathlist for logging.</param>
        /// <param name="steps">Steps.</param>
        void Search(string start, List<string> pathList, int steps)
        {
            var cave = _map.Caves.FirstOrDefault(c => c.Name == start);

            // Keep track of the small caves visits. "Big" caves can be ignored.
            if (cave.IsSmall)
                _pastVisits[cave.Name].Add(steps);

            foreach (var neighbour in _map.NeighbourList[start])
            {
                // Get the next neighbour of starting cave.
                var nextCave = _map.Caves.FirstOrDefault(c => c.Name == neighbour);

                // Path is done.
                if (nextCave.Name == "end")
                {
                    PathCounter++;
                    if (_logging)
                        Console.WriteLine(string.Join(" ", pathList));
                    continue;
                }

                if (nextCave.IsSmall)
                {
                    if (_pastVisits[nextCave.Name].Any())
                    {
                        // In p1 a small cave can only be visited once.
                        if (_part == 1)
                            continue;

                        // In p2 only ONE small cave can be visited more than once, max twice.
                        if (_pastVisits.Any(c => c.Value.Count > 1))
                            continue;
                    }
                }

                // Don't add start as another neighbour. Skip over it.
                if (nextCave.IsStart)
                    continue;

                // Search the next neighbours, neighbours.
                // Pathlist is only used for logging the paths to the console.
                pathList.Add(nextCave.Name);
                Search(neighbour, pathList, steps + 1);
                pathList.Remove(nextCave.Name);

                // Only keep past visits values smaller or equal our current steps.
                foreach (var visits in _pastVisits)
                {
                    _pastVisits[visits.Key] = visits.Value
                        .Where(s => s <= steps)
                        .ToList();
                }
            }

        }
    }

    public class Day12 : AdventBase
    {
        // All unique caves in the system.
        private readonly List<Cave> _caves;
        // The neighbour pairs in the system.
        private readonly List<Tuple<string, string>> _neighbours;

        /// <summary>
        /// Initialize day12.
        /// </summary>
        public Day12()
        {
            _caves = new();
            _neighbours = new();

            foreach (var caves in Common.GetInput(12))
                CreateCaves(caves);
        }

        /// <summary>
        /// Create the different unique cave systems and the neighbours bases on input.
        /// </summary>
        void CreateCaves(string input)
        {
            var caves = input.Split("-");
            var cave1 = new Cave(caves[0]);
            var cave2 = new Cave(caves[1]);
            AddNewCave(cave1);
            AddNewCave(cave2);

            _neighbours.Add(Tuple.Create(cave1.Name, cave2.Name));
        }

        /// <summary>
        /// Add new cave to the list.
        /// </summary>
        /// <param name="cave">Cave object.</param>
        void AddNewCave(Cave cave)
        {
            if (_caves.FirstOrDefault(c => c.Name == cave.Name) == null)
                _caves.Add(cave);
        }

        public override void Solution1()
        {
            var caveMap = new CaveMap(_caves, _neighbours);
            var pathBuilder = new PathBuilder(caveMap, 1, false);
            var paths = pathBuilder.CalculatePaths();

            LogResults(12, 1, paths);
        }

        public override void Solution2()
        {
            var caveMap = new CaveMap(_caves, _neighbours);
            var pathBuilder = new PathBuilder(caveMap, 2, false);
            var paths = pathBuilder.CalculatePaths();

            LogResults(12, 2, paths);
        }
    }
}
