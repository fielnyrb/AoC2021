using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    class Day5
    {
        int[][] oceanFloor = new int[1000][];
        public Day5()
        {
            List<string> input = File.ReadAllLines(@"input\day5.txt").ToList();
            //input = File.ReadAllLines(@"input\day5sample.txt").ToList();
            prepareStructure(input);
            Console.WriteLine(countOverlap());
        }

        void prepareStructure(List<string> input)
        {

            for (int j = 0; j < 1000; j++)
            {
                oceanFloor[j] = new int[1000];
            }

            for (int i = 0; i < input.Count; i++)
            {
                List<string> singleLine = input[i].Split(' ').ToList();

                List<string> coord1 = singleLine[0].Split(',').ToList();
                List<string> coord2 = singleLine[2].Split(',').ToList();
                int x1 = Int32.Parse(coord1[0]);
                int y1 = Int32.Parse(coord1[1]);
                int x2 = Int32.Parse(coord2[0]);
                int y2 = Int32.Parse(coord2[1]);

                if (!(x1 == x2 || y1 == y2))
                {
                    layDownLineOfVentsDiagonals(x1, x2, y1, y2);
                }
                else if (x1 != x2)
                {
                    layDownLineOfVents(x1, x2, y1, true);
                }
                else if (y1 != y2)
                {
                    layDownLineOfVents(y1, y2, x1, false);
                }

            }

            void layDownLineOfVentsDiagonals(int x1, int x2, int y1, int y2)
            {
                oceanFloor[x1][y1]++;
                while (x1 != x2 && y1 != y2)
                {
                    if(x1 != x2) {
                        if(x1 < x2) x1++;
                        if(x1 > x2) x1--;
                    }

                    if (y1 != y2)
                    {
                        if (y1 < y2) y1++;
                        if (y1 > y2) y1--;
                    }

                    oceanFloor[x1][y1]++;
                }
            }


            void layDownLineOfVents(int n1, int n2, int invariant, bool variesAlongX)
            {
                int smallerNumber = n1 < n2 ? n1 : n2;
                int largerNumber = n1 > n2 ? n1 : n2;

                while (smallerNumber <= largerNumber)
                {
                    if (variesAlongX)
                    {
                        oceanFloor[smallerNumber][invariant]++;
                    }
                    else
                    {
                        oceanFloor[invariant][smallerNumber]++;
                    }
                    smallerNumber++;
                }
            }
        }

        int countOverlap()
        {
            int sum = 0;

            for (int i = 0; i < 1000; i++)
            {
                int overlaps = oceanFloor[i].Where(item => item > 1).Count();
                sum += overlaps;
            }

            return sum;
        }
    }
}
