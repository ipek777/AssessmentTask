namespace AssessmentTask
{
    public readonly record struct Position(int Row, int Col);

    public class Astronaut
    {
        public string Name { get; }
        public Position StartPosition { get; }

        public Astronaut(string name, Position startPosition)
        {
            Name = name;
            StartPosition = startPosition;
        }
    }
}