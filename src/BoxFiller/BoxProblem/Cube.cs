using System;

namespace BoxFiller.BoxProblem
{
    internal class Cube : Box
    {
        public int Side { get; }

        public Cube(int side)
            : base(side, side, side) { Side = side; }

        public static Cube FromPowerOfTwo(int powerOfTwo)
        {
            int side = (int)Math.Pow(2, powerOfTwo);
            return new Cube(side);
        }
    }
}
