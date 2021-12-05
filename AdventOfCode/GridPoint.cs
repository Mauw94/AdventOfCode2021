namespace AdventOfCode2021
{
    public class GridPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Overlaps { get; set; }

        public GridPoint(int x, int y)
        {
            Overlaps = 1;
            X = x;
            Y = y;
        }
    }
}
