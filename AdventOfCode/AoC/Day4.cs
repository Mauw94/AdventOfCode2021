using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    record BingoTile(int Number)
    {
        public bool Marked { get; set; } = false;
    }

    public class Day4 : AdventBase
    {
        public override void Solution1()
        {
            int boardCounter = 0;
            int columCounter = 0;

            List<string> fileContent = Common.GetInput(4);
            List<int> numbersToDraw = fileContent
                .First()
                .Split(",")
                .ToList()
                .Select(x => int.Parse(x))
                .ToList();

            Dictionary<int, BingoTile[,]> bingoBoards = new()
            {
                { boardCounter, new BingoTile[5, 5] }
            };

            for (int i = 2; i < fileContent.Count; i++)
            {
                var content = fileContent[i];

                if (content == "")
                {
                    boardCounter++;
                    bingoBoards.Add(boardCounter, new BingoTile[5, 5]);
                    columCounter = 0;
                    continue;
                }

                int[] rowNumbers = content.Split(" ").ToList()
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => int.Parse(x)).ToArray();

                for (int k = 0; k < rowNumbers.Length; k++)
                {
                    bingoBoards[boardCounter][columCounter, k] = new BingoTile(rowNumbers[k]);
                }
                columCounter++;
            }

            for (int i = 0; i < numbersToDraw.Count; i++)
            {
                foreach (var bingoBoard in bingoBoards)
                {
                    if (CheckForBingo(bingoBoard.Value, numbersToDraw[i]))
                    {
                        var winningNumber = numbersToDraw[i];
                        var sumUnmarked = SumAllUnmarkedNumbers(bingoBoard.Value);
                        i = numbersToDraw.Count - 1;

                        PrintBingoBoard(bingoBoard.Value);
                        LogResults(4, 1, (winningNumber * sumUnmarked));
                        break;
                    }
                }
            }
        }

        public override void Solution2()
        {
            List<string> fileContent = Common.GetInput(4);
            List<int> numbersToDraw = fileContent
                .First()
                .Split(",")
                .ToList()
                .Select(x => int.Parse(x))
                .ToList();

            int boardCounter = 0;
            int columCounter = 0;
            Dictionary<int, BingoTile[,]> bingoBoards = new();
            bingoBoards.Add(boardCounter, new BingoTile[5, 5]);

            for (int i = 2; i < fileContent.Count; i++)
            {
                var content = fileContent[i];

                if (content == "")
                {
                    boardCounter++;
                    bingoBoards.Add(boardCounter, new BingoTile[5, 5]);
                    columCounter = 0;
                    continue;
                }

                int[] rowNumbers = content.Split(" ").ToList()
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => int.Parse(x)).ToArray();

                for (int k = 0; k < rowNumbers.Length; k++)
                {
                    bingoBoards[boardCounter][columCounter, k] = new BingoTile(rowNumbers[k]);
                }
                columCounter++;
            }

            for (int i = 0; i < numbersToDraw.Count; i++)
            {
                foreach (var bingoBoard in bingoBoards.ToArray())
                {
                    var bingo = CheckForBingo(bingoBoard.Value, numbersToDraw[i]);
                    if (bingo && bingoBoards.Count > 1)
                    {
                        bingo = false;
                        if (bingoBoards.Count != 1)
                            bingoBoards.Remove(bingoBoard.Key);
                    }
                    if (bingo && bingoBoards.Count == 1)
                    {
                        var winningNumber = numbersToDraw[i];
                        var sumUnmarked = SumAllUnmarkedNumbers(bingoBoard.Value);
                        i = numbersToDraw.Count - 1;

                        PrintBingoBoard(bingoBoard.Value);
                        LogResults(4, 2, (winningNumber * sumUnmarked));
                        break;
                    }

                }
            }
        }

        private static int SumAllUnmarkedNumbers(BingoTile[,] board)
        {
            int sum = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (!board[i, j].Marked)
                        sum += board[i, j].Number;
                }
            }

            return sum;
        }

        private static bool CheckForBingo(BingoTile[,] board, int drawnNumber)
        {
            int rowLength = board.GetLength(0);
            int colLength = board.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (board[i, j].Number == drawnNumber)
                        board[i, j].Marked = true;

                    if (board[i, 0].Marked && board[i, 1].Marked && board[i, 2].Marked && board[i, 3].Marked && board[i, 4].Marked)
                    {
                        return true;
                    }
                    if (board[0, j].Marked && board[1, j].Marked && board[2, j].Marked && board[3, j].Marked && board[4, j].Marked)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void PrintBingoBoard(BingoTile[,] board)
        {
            int rowLength = board.GetLength(0);
            int colLength = board.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (board[i, j].Marked)
                        Console.Write(string.Format("{0} ", board[i, j].Number + "*"));
                    else
                        Console.Write(string.Format("{0} ", board[i, j].Number));
                }
                Console.Write(Environment.NewLine);
            }
            Console.Write(Environment.NewLine);
        }
    }

}
