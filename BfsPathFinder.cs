using System.Collections.Generic;

namespace AssessmentTask
{
    public class BfsPathFinder : PathFinderBase
    {
        public override PathResult FindShortestPath(Grid grid, Astronaut astronaut)
        {
            Queue<Position> queue = new Queue<Position>();
            bool[,] visited = new bool[grid.Rows, grid.Cols];
            Position?[,] previous = new Position?[grid.Rows, grid.Cols];

            Position start = astronaut.StartPosition;
            Position finish = grid.FinishPosition;

            queue.Enqueue(start);
            visited[start.Row, start.Col] = true;

            while (queue.Count > 0)
            {
                Position current = queue.Dequeue();

                if (current == finish)
                {
                    List<Position> path = ReconstructPath(previous, start, finish);
                    int distance = path.Count - 1;

                    return new PathResult(astronaut, true, distance, path);
                }

                foreach (Position direction in Directions)
                {
                    Position next = new Position(
                        current.Row + direction.Row,
                        current.Col + direction.Col
                    );

                    if (!grid.IsWalkable(next))
                    {
                        continue;
                    }

                    if (visited[next.Row, next.Col])
                    {
                        continue;
                    }

                    visited[next.Row, next.Col] = true;
                    previous[next.Row, next.Col] = current;
                    queue.Enqueue(next);
                }
            }

            return new PathResult(astronaut, false, -1, new List<Position>());
        }
    }
}