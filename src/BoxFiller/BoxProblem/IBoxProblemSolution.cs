namespace BoxFiller.BoxProblem
{
    public interface IBoxProblemSolution
    {
        int GetMinimalBoxesAmount(int boxX, int boxY, int boxZ, params int[] powersOfTwo);
    }
}
