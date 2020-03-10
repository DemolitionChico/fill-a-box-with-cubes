using System;
using System.Collections.Generic;
using System.Linq;

namespace BoxFiller.BoxProblem
{
    internal class DivideBoxSolution : IBoxProblemSolution
    {
        private Box _bigBox;
        private List<Cube> _cubesList;

        public int GetMinimalBoxesAmount(int boxX, int boxY, int boxZ, params int[] powersOfTwo)
        {
            //large box initialization
            _bigBox = new Box(boxX, boxY, boxZ);
            _cubesList = new List<Cube>();
            for (int i = 0; i < powersOfTwo.Length; i++)
            {
                _cubesList.AddRange(Enumerable.Repeat(Cube.FromPowerOfTwo(i), powersOfTwo[i]));
            }
            //input validation - if the volume of all the cubes combined is lower than the volume of the big box, we know this is not solvable
            long sumOfVolumes = _cubesList.Sum(cube => cube.Volume);
            if (sumOfVolumes < _bigBox.Volume)
            {
                return -1;
            }
            int result = FitInBox(_bigBox);
            return result;
        }

        private int FitInBox(Box box)
        {
            int cubesUsed = 0;
            //get biggest cube possible to fit fit in this box
            int smallestBoxSide = Math.Min(Math.Min(box.X, box.Y), box.Z);
            Cube biggestCubeThatFits = _cubesList.Where(c => c.Side <= smallestBoxSide).OrderByDescending(x => x.Side).FirstOrDefault();
            //if there is none - that case is not solvable
            if(biggestCubeThatFits == null)
            {
                return -1;
            }
            //if there is any, remove the cube from the list as it is already used and increment the counter
            _cubesList.Remove(biggestCubeThatFits);
            cubesUsed++;
            //divide the box into 3 smaller boxes that together with the cube will fill the original one
            //watch the following box sizes - imagine than after placing a cube in the box, you divide the empty space left, creating 3 new boxes
            //create z box (cube.x, cube.x, z - cube.x)
            Box tempBox1 = new Box(biggestCubeThatFits.Side, biggestCubeThatFits.Side, box.Z - biggestCubeThatFits.X);
            //create y box (cube.x, y - cube.x, z)
            Box tempBox2 = new Box(biggestCubeThatFits.Side, box.Y - biggestCubeThatFits.X, box.Z);
            //create x box(x - cube.c, y, z)
            Box tempBox3 = new Box(box.X - biggestCubeThatFits.Side, box.Y, box.Z);
            int cubesUsedInZBox = 0, cubesUsedInYBox = 0, cubesUsedInXBox = 0;
            //try performing the same operation for smaller boxes recursively, until the "divided box" volume is 0
            if (tempBox1.Volume > 0)
            {
                cubesUsedInZBox = FitInBox(tempBox1);
                if (cubesUsedInZBox < 0)
                    return -1;
            }
            if(tempBox2.Volume > 0)
            {
                cubesUsedInYBox = FitInBox(tempBox2);
                if (cubesUsedInYBox < 0)
                    return -1;
            }
            if (tempBox3.Volume > 0)
            {
                cubesUsedInXBox = FitInBox(tempBox3);
                if (cubesUsedInXBox < 0)
                    return -1;
            }
            cubesUsed += cubesUsedInZBox + cubesUsedInYBox + cubesUsedInXBox;
            return cubesUsed;
        }
    }
}
