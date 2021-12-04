namespace AdventOfCode
{
    public class BingoTile
    {
        public bool _marked;
        public int _number;

        public BingoTile(int number, bool marked = false)
        {
            this._marked = marked;
            this._number = number;
        }
    }
}
