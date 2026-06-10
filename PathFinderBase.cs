using System.Collections.Generic;

namespace AssessmentTask
{
    public abstract class PathFinderBase
    {
        protected readonly Position[] Directions =
        {
            new Position(-1, 0),
            new Position(1, 0),
            new Position(0, -1),
            new Position(0, 1)
        };

        public abstract PathResult FindShortestPath(Grid grid, Astronaut astronaut);

        protected List<Position> ReconstructPath(Position?[,] previous, Position start, Position finish)
        {
            List<Position> path = new List<Position>();

            Position current = finish;
            path.Add(current);

            while (current != start)
            {
                Position? previousPosition = previous[current.Row, current.Col];

                if (previousPosition == null)
                {
                    return new List<Position>();
                }

                current = previousPosition.Value;
                path.Add(current);
            }

            path.Reverse();

            return path;
        }
    }
}