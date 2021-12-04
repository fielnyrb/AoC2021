using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    class Day4
    {
        string[] bingoNumbers;
        List<Board> bingoBoards;

        public Day4()
        {
            List<string> input = File.ReadAllLines(@"input\day4.txt").ToList();
            //input = File.ReadAllLines(@"input\day4sample.txt").ToList();
            bingoBoards = new List<Board>();

            createStructures(input);

            bool isBingo = false;

            for (int i = 0; i < bingoNumbers.Count(); i++)
            {
                for (int j = 0; j < bingoBoards.Count; j++)
                {
                    bingoBoards[j].receiveNumber(bingoNumbers[i]);

                    //Am I the last winner?
                    int winnerCount = bingoBoards.Where(item => !item.hasWon()).Count();
                    if (winnerCount == 0)
                    {
                        Console.WriteLine("Has won: " + bingoNumbers[i]);
                        Console.WriteLine(bingoBoards[j].sumOfAllUnmarkedNumbers() * Int32.Parse(bingoNumbers[i]));
                        isBingo = true;
                        break;
                    }

                }
                for (int k = 0; k < bingoBoards.Count; k++)
                {
                    if (bingoBoards[k].hasWon())
                    {
                        //Part 1 was here:
                        /*Console.WriteLine("Has won: " + bingoNumbers[i]);
                        Console.WriteLine(bingoBoards[k].sumOfAllUnmarkedNumbers() * Int32.Parse(bingoNumbers[i]));
                        isBingo = true;
                        break;*/
                    }
                }
                if (isBingo)
                {
                    break;
                }
            }
        }

        private void createStructures(List<string> input)
        {
            bingoNumbers = input[0].Split(",");

            for (int i = 1; i < input.Count(); i++)
            {
                if (string.IsNullOrWhiteSpace(input[i]))
                {
                    bingoBoards.Add(new Board());
                    continue;
                }

                List<BoardNumber> row = new List<BoardNumber>();
                List<string> cells = input[i].Split(" ").ToList();
                cells.RemoveAll(item => string.IsNullOrWhiteSpace(item));

                for (int j = 0; j < cells.Count(); j++)
                {
                    row.Add(new BoardNumber(cells[j], false));
                }
                bingoBoards.Last().rows.Add(row);
            }
        }
    }

    class Board
    {
        public List<List<BoardNumber>> rows = new List<List<BoardNumber>>();

        public void receiveNumber(string number)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < rows[i].Count; j++)
                {
                    if (rows[i][j].number == number)
                    {
                        rows[i][j].isMarked = true;
                    }
                }
            }
        }
        public bool hasWon()
        {
            bool unmarkedExists = false;
            //rows
            for (int i = 0; i < rows.Count; i++)
            {
                unmarkedExists = rows[i].Exists(item =>
                {
                    return item.isMarked == false;
                });
                if (!unmarkedExists)
                {
                    return true;
                }
            }

            //columns
            for (int i = 0; i < 5; i++)
            {
                if (rows[0][i].isMarked &&
                    rows[1][i].isMarked &&
                    rows[2][i].isMarked &&
                    rows[3][i].isMarked &&
                    rows[4][i].isMarked)
                {
                    return true;
                }
            }

            return false;
        }

        public int sumOfAllUnmarkedNumbers()
        {
            int sum = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                sum += rows[i].Where(item => !item.isMarked).Sum(item => Int32.Parse(item.number));
            }
            return sum;
        }
    }

    public class BoardNumber
    {
        public string number;
        public bool isMarked;

        public BoardNumber(string num, bool marked)
        {
            this.number = num;
            this.isMarked = marked;
        }
    }
}
