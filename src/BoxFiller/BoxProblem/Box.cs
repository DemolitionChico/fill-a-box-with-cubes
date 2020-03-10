namespace BoxFiller.BoxProblem
{
    internal class Box
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public long Volume { get; }

        public Box(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Volume = x * y * z;
        }
    }
}
