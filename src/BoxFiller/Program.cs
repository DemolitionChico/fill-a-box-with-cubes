using BoxFiller.BoxProblem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoxFiller
{
    class Program
    {
        private static string[] endCmds = { "exit", "quit", "finish", "end" };
        //this solution is optimised, compared to the previous algorythm used to solve this case
        //if you want to test the other one, just comment the following line, and use the next one instead (needs to be recompiled)
        private static IBoxProblemSolution boxProblemSolver = new DivideBoxSolution();
        //private static IBoxProblemSolution boxProblemSolver = new ReserveSpacesSolution();

        static void Main(string[] args)
        {
            while (true)
            {
                string command = Console.ReadLine();
                if (string.IsNullOrEmpty(command))
                {
                    continue;
                }
                //type exit, quit, finish or end to exit
                if (endCmds.Contains(command.ToLower()))
                {
                    break;
                }
                try
                {
                    CmdLineInput deserializedInput = CmdLineInput.FromInput(command);
                    int solution = boxProblemSolver.GetMinimalBoxesAmount(deserializedInput.X, deserializedInput.Y, deserializedInput.Z, deserializedInput.PowersOfTwo.ToArray());
                    Console.WriteLine($">> Solution: {solution}");
                }
                catch(ArgumentException e)
                {
                    Console.WriteLine($"Provided input is not valid. {e.Message}");
                }
            }
            Console.WriteLine("Finished.");
        }
    }

    internal class CmdLineInput
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public IEnumerable<int> PowersOfTwo { get; }

        public CmdLineInput(int x, int y, int z, IEnumerable<int> powersOfTwo)
        {
            X = x;
            Y = y;
            Z = z;
            PowersOfTwo = powersOfTwo;
        }

        public static CmdLineInput FromInput(string inputLine)
        {
            string[] lineElems = inputLine.Split(' ');
            if (lineElems.Length < 4)
            {
                throw new ArgumentException("At least 4 numbers expected (x y z and at least one number of boxes)", nameof(inputLine));
            }
            int x, y, z;
            if (!int.TryParse(lineElems[0], out x) || !int.TryParse(lineElems[1], out y) || !int.TryParse(lineElems[2], out z))
            {
                throw new ArgumentException("Cannot parse box size", nameof(inputLine));
            }
            List<int> powersList = new List<int>();
            for (int i = 3; i < lineElems.Length; i++)
            {
                int pow;
                if (!int.TryParse(lineElems[i], out pow))
                {
                    throw new ArgumentException($"Cannot parse power of two (position {i})", nameof(inputLine));
                }
                powersList.Add(pow);
            }
            return new CmdLineInput(x, y, z, powersList);
        }
    }
}