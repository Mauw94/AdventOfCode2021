using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day4 : IBase
    {
        public override void Solution1()
        {
            int boardCounter = 0;
            int columCounter = 0;
            List<string> fileContent = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day4.txt").ToList();
            List<int> numbersToDraw = fileContent[0].Split(",").ToList().Select(x => int.Parse(x)).ToList();
            Dictionary<int, BingoTile[,]> bingoBoards = new Dictionary<int, BingoTile[,]>
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
                        base.LogResults(4, 1, (winningNumber * sumUnmarked).ToString());
                        break;
                    }
                }
            }
            //foreach (var bingoBoard in bingoBoards)
            //    PrintBingoBoard(bingoBoard.Value);
        }

        public override void Solution2()
        {
            List<string> fileContent = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day4.txt").ToList();
            List<int> numbersToDraw = fileContent[0].Split(",").ToList().Select(x => int.Parse(x)).ToList();
            Dictionary<int, BingoTile[,]> bingoBoards = new Dictionary<int, BingoTile[,]>();
            int boardCounter = 0;
            int columCounter = 0;
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
                        //PrintBingoBoard(bingoBoard.Value);
                        if (bingoBoards.Count != 1)
                            bingoBoards.Remove(bingoBoard.Key);
                    }
                    if (bingo && bingoBoards.Count == 1)
                    {
                        var winningNumber = numbersToDraw[i];
                        var sumUnmarked = SumAllUnmarkedNumbers(bingoBoard.Value);
                        i = numbersToDraw.Count - 1;
                        PrintBingoBoard(bingoBoard.Value);
                        base.LogResults(4, 2, (winningNumber * sumUnmarked).ToString());
                        break;
                    }

                }
            }
        }

        private int SumAllUnmarkedNumbers(BingoTile[,] board)
        {
            int sum = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (!board[i, j]._marked)
                        sum += board[i, j]._number;
                }
            }

            return sum;
        }

        private bool CheckForBingo(BingoTile[,] board, int drawnNumber)
        {
            int rowLength = board.GetLength(0);
            int colLength = board.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (board[i, j]._number == drawnNumber)
                        board[i, j]._marked = true;

                    if (board[i, 0]._marked && board[i, 1]._marked && board[i, 2]._marked && board[i, 3]._marked && board[i, 4]._marked)
                    {
                        return true;
                    }
                    if (board[0, j]._marked && board[1, j]._marked && board[2, j]._marked && board[3, j]._marked && board[4, j]._marked)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void PrintBingoBoard(BingoTile[,] board)
        {
            int rowLength = board.GetLength(0);
            int colLength = board.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (board[i, j]._marked)
                        Console.Write(string.Format("{0} ", board[i, j]._number + "*"));
                    else
                        Console.Write(string.Format("{0} ", board[i, j]._number));
                }
                Console.Write(Environment.NewLine);
            }
            Console.Write(Environment.NewLine);
        }
    }

}
