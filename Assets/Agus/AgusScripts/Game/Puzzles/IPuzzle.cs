namespace Game.Puzzles
{
    public interface IPuzzle
    {
        void Activate();
        void Deactivate();
        bool IsSolved { get; }
    }
}
