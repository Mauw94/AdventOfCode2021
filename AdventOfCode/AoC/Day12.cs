using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    class Cave
    {
        public bool IsSmall { get; set; }
        public bool IsVisited { get; set; }
        public bool IsStart { get; set; }
        public string Name { get; private set; }
        public int VisitCount { get; set; } = 0;

        public Cave(string name, bool isSmall)
        {
            IsSmall = isSmall;
            Name = name;

            CheckForStart();
        }

        bool CheckForStart() => IsStart = (Name == "start");
    }

    class CaveMap
    {
        public Dictionary<string, HashSet<string>> NeighbourList { get; } = new();
        public List<Cave> Caves { get; private set; }
        public int CaveCount() => Caves.Count;

        public CaveMap(IEnumerable<Cave> caves, IEnumerable<Tuple<string, string>> neighbours)
        {
            Caves = caves.ToList();

            foreach (var cave in caves)
                AddCave(cave.Name);

            foreach (var neighbour in neighbours)
                AddNeighbour(neighbour);
        }

        public void AddCave(string cave)
        {
            NeighbourList[cave] = new HashSet<string>();
        }

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

    class Path
    {
        public int PathCounter { get; private set; }
        private Cave _uniqueSmallCave;

        public Path()
        {
            PathCounter = 0;
        }

        public void CalculatePaths(CaveMap map)
        {
            List<string> pathList = new();
            pathList.Add("start");

            Calculate("start", "end", map, pathList);
        }

        void Calculate(string start, string end, CaveMap map, List<string> pathList)
        {
            if (start == end)
            {
                PathCounter++;
                Console.WriteLine(string.Join(" ", pathList));
                return;
            }

            var cave = map.Caves.First(c => c.Name == start);

            if (cave.IsSmall)
                cave.IsVisited = true;

            cave.VisitCount++;

            foreach (var neighbour in map.NeighbourList[start])
            {
                var nb = map.Caves.FirstOrDefault(c => c.Name == neighbour);
                if (!nb.IsVisited)
                {

                    pathList.Add(nb.Name);
                    Calculate(neighbour, end, map, pathList);
                    pathList.Remove(nb.Name);
                }
            }

            cave.IsVisited = false;
        }
    }

    public class Day12 : AdventBase
    {
        private readonly List<Cave> _caves;
        private readonly List<Tuple<string, string>> _edges;
        private readonly List<string> _usedNames;

        public Day12()
        {
            _caves = new();
            _usedNames = new();
            _edges = new();
            List<string> input = Common.GetInput(12);
            foreach (var caves in input)
            {
                CreateCaves(caves);
            }
        }

        void CreateCaves(string input)
        {
            var caves = input.Split("-");
            var cave1 = new Cave(caves[0], !char.IsUpper(caves[0].ToCharArray()[0]));
            var cave2 = new Cave(caves[1], !char.IsUpper(caves[1].ToCharArray()[0]));
            AddNewCave(caves[0], cave1);
            AddNewCave(caves[1], cave2);

            _edges.Add(Tuple.Create(cave1.Name, cave2.Name));
        }

        void AddNewCave(string caveName, Cave cave)
        {
            if (!_usedNames.Contains(caveName))
            {
                _caves.Add(cave);
                _usedNames.Add(caveName);
            }
        }

        public override void Solution1()
        {
            var caveMap = new CaveMap(_caves, _edges);
            var path = new Path();
            path.CalculatePaths(caveMap);

            LogResults(12, 1, path.PathCounter.ToString());
        }

        public override void Solution2()
        {
            base.Solution2();
        }
    }
}
