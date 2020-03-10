using System;
using System.Collections.Generic;
using System.Linq;

namespace BoxFiller.BoxProblem
{
    [Obsolete]
    internal class ReserveSpacesSolution : IBoxProblemSolution
    {
        private Box _bigBox;
        private Stack<Cube> _cubesStack;
        // big box mapped to small 1x1x1 cubes, numbered in the following order: x-> y-> z. true value means that the cubic space is empty, false - it's occupied
        private bool[] _bigBoxCubesArray;

        public int GetMinimalBoxesAmount(int boxX, int boxY, int boxZ, params int[] powersOfTwo)
        {
            //large box initialization
            _bigBox = new Box(boxX, boxY, boxZ);
            List<Cube> cubes = new List<Cube>();
            for (int i = 0; i < powersOfTwo.Length; i++)
            {
                cubes.AddRange(Enumerable.Repeat(Cube.FromPowerOfTwo(i), powersOfTwo[i]));
            }
            //input validation - if the volume of all cubes is lower than the volume of the box, we know this is not solvable
            long sumOfVolumes = cubes.Sum(cube => cube.Volume);
            if (sumOfVolumes < _bigBox.Volume)
            {
                return -1;
            }
            //stack up cubes, so that the biggest are on the top
            _cubesStack = new Stack<Cube>();
            foreach (Cube cube in cubes.OrderBy(c => c.Volume))
            {
                _cubesStack.Push(cube);
            }
            _bigBoxCubesArray = Enumerable.Repeat(true, (int)_bigBox.Volume).ToArray();
            int result = CountResult();
            return result;
        }

        private bool CheckIfCanFitCube(int cubeSize)
        {
            return _bigBox.X >= cubeSize && _bigBox.Y >= cubeSize && _bigBox.Z >= cubeSize;
        }

        private int GetDiagonalPoint(int startPoint, int cubeSize)
        {
            //based on the index and the size of a cube (cube space number), calculate the diagonal cube index
            int startPointZ = startPoint / (_bigBox.X * _bigBox.Y);
            int startPointY = (startPoint - (startPointZ * _bigBox.X * _bigBox.Y)) / _bigBox.X;
            int startPointX = (startPoint - (startPointZ * _bigBox.X * _bigBox.Y)) % _bigBox.X;
            if (startPointX + cubeSize > _bigBox.X || startPointY + cubeSize > _bigBox.Y || startPointZ + cubeSize > _bigBox.Z)
            {
                return -1;
            }
            return startPoint + (cubeSize - 1) * (_bigBox.X * _bigBox.Y + _bigBox.X + 1);
        }

        private void PlaceCubeInArray(int startPoint, int cubeSize)
        {
            //starting from the "startPoint", which is the index of the first cube space in the "big box", 
            //reserve all cubic spacer required to fit the cube with given size
            for (int z = 0; z < cubeSize; z++)
            {
                for (int y = 0; y < cubeSize; y++)
                {
                    for (int x = 0; x < cubeSize; x++)
                    {
                        _bigBoxCubesArray[startPoint + x + (y * _bigBox.X) + z * (_bigBox.X * _bigBox.Y)] = false;
                    }
                }
            }
        }

        private int CountResult()
        {
            int counter = 0;
            Cube currentCube = null;
            //if there are any cubes left or there are any empty spaces
            while (_cubesStack.Any() && _bigBoxCubesArray.Any(x => true))
            {
                //pick the biggest cube (top of the stack)
                currentCube = _cubesStack.Pop();
                //pick another cube if this one is too big for the box to fit
                if (!CheckIfCanFitCube(currentCube.Side))
                {
                    continue;
                }
                //iterate over cubic spaces in the big box
                for (int i = 0; i < _bigBoxCubesArray.Length; i++)
                {
                    //if the space is occupied, proceed to the next one
                    if (!_bigBoxCubesArray[i])
                    { continue; }
                    //otherwise, find the diagonal index and check whether it is occupied
                    int diagonalPt = GetDiagonalPoint(i, currentCube.Side);
                    if (diagonalPt < 0 || !_bigBoxCubesArray[diagonalPt])
                    { continue; }
                    //if not, reserve cubic spaces and increment the counter of used cubes
                    PlaceCubeInArray(i, currentCube.Side);
                    counter++;
                    break;
                }
            }
            return counter;
        }
    }
}
