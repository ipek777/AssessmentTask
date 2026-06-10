using System.Collections.Generic;

namespace AssessmentTask
{
    public class PathResult
    {
        public Astronaut Astronaut { get; }
        public bool IsSuccessful { get; }
        public int Distance { get; }
        public List<Position> Path { get; }

        public PathResult(Astronaut astronaut, bool isSuccessful, int distance, List<Position> path)
        {
            Astronaut = astronaut;
            IsSuccessful = isSuccessful;
            Distance = distance;
            Path = path;
        }
    }
}