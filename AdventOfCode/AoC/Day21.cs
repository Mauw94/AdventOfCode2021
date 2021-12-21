using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    class Dice
    {
        int _nextRoll = 1;
        int _rolls = 0;

        public int Roll()
        {
            int roll = _nextRoll;

            if (++_nextRoll > 100)
                _nextRoll = 1;

            _rolls++;

            return roll;
        }

        public int RollCount() => _rolls;
    }

    class Player
    {
        public int Position { get; set; }
        public int Score { get; set; }
        public bool Winner { get; private set; } = false;

        public Player(int position)
        {
            Position = position;
        }

        public void Move(int moves, int winningScore)
        {
            Position += moves % 10;
            if (Position > 10)
                Position -= 10;

            Score += Position;
            Winner = Score >= winningScore;
        }
    }

    public class Day21 : AdventBase
    {
        private List<Player> _players;

        public Day21()
        {
            var input = Common.GetInput(21);

            CreatePlayers(input, input.Count);
        }

        long Play(int winningScore)
        {
            Dice dice = new();

            while (true)
            {
                foreach (var player in _players)
                {
                    int total = dice.Roll() + dice.Roll() + dice.Roll();

                    player.Move(total, winningScore);

                    if (_players.Any(x => x.Winner))
                    {
                        var loser = _players.First(x => !x.Winner);
                        return loser.Score * dice.RollCount();
                    }
                }
            }
        }

        void CreatePlayers(List<string> input, int playerCount)
        {
            _players = new();

            for (int i = 0; i < playerCount; i++)
            {
                var startingPos = int.Parse(input[i].Substring(input[i].Length - 1, 1));
                var player = new Player(startingPos);
                _players.Add(player);
            }
        }

        public override void Solution1()
        {
            var result = Play(1000);

            LogResults(21, 1, result);
        }

        public override void Solution2()
        {
            base.Solution2();
        }
    }
}
