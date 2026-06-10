using System.Collections.Generic;

namespace AssessmentTask
{
    public class DijkstraPathFinder : PathFinderBase
    {
        public override PathResult FindShortestPath(Grid grid, Astronaut astronaut)
        {
            int[,] distances = new int[grid.Rows, grid.Cols];
            Position?[,] previous = new Position?[grid.Rows, grid.Cols];
            PriorityQueue<Position, int> queue = new PriorityQueue<Position, int>();

            for (int row = 0; row < grid.Rows; row++)
            {
                for (int col = 0; col < grid.Cols; col++)
                {
                    distances[row, col] = int.MaxValue;
                }
            }

            Position start = astronaut.StartPosition;
            Position finish = grid.FinishPosition;

            distances[start.Row, start.Col] = 0;
            queue.Enqueue(start, 0);

            while (queue.Count > 0)
            {
                queue.TryDequeue(out Position current, out int currentDistance);

                if (currentDistance > distances[current.Row, current.Col])
                {
                    continue;
                }

                if (current == finish)
                {
                    List<Position> path = ReconstructPath(previous, start, finish);
                    int totalCost = distances[finish.Row, finish.Col];

                    return new PathResult(astronaut, true, totalCost, path);
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

                    int newDistance = currentDistance + grid.GetMovementCost(next);

                    if (newDistance < distances[next.Row, next.Col])
                    {
                        distances[next.Row, next.Col] = newDistance;
                        previous[next.Row, next.Col] = current;
                        queue.Enqueue(next, newDistance);
                    }
                }
            }

            return new PathResult(astronaut, false, -1, new List<Position>());
        }
    }
}