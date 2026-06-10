using System;
using System.Collections.Generic;

namespace AssessmentTask
{
    public static class MapGenerator
    {
        private static readonly Random random = new Random();

        public static string[,] Generate(int rows, int cols, int astronautCount, int asteroidCount, int debrisCount)
        {
            if (astronautCount < 1 || astronautCount > 3)
            {
                throw new ArgumentException("Astronaut count must be between 1 and 3.");
            }

            if (asteroidCount < 0 || debrisCount < 0)
            {
                throw new ArgumentException("Asteroid and debris counts cannot be negative.");
            }

            int totalCells = rows * cols;
            int neededCells = astronautCount + 1 + asteroidCount + debrisCount;

            if (neededCells > totalCells)
            {
                throw new ArgumentException("Too many objects for the selected map size.");
            }

            string[,] cells = new string[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    cells[row, col] = "0";
                }
            }

            List<Position> positions = new List<Position>();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    positions.Add(new Position(row, col));
                }
            }

            Shuffle(positions);

            int index = 0;

            for (int i = 1; i <= astronautCount; i++)
            {
                Position position = positions[index];
                cells[position.Row, position.Col] = $"S{i}";
                index++;
            }

            Position finishPosition = positions[index];
            cells[finishPosition.Row, finishPosition.Col] = "F";
            index++;

            for (int i = 0; i < asteroidCount; i++)
            {
                Position position = positions[index];
                cells[position.Row, position.Col] = "X";
                index++;
            }

            for (int i = 0; i < debrisCount; i++)
            {
                Position position = positions[index];
                cells[position.Row, position.Col] = "D";
                index++;
            }

            return cells;
        }

        private static void Shuffle(List<Position> positions)
        {
            for (int i = positions.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(i + 1);

                Position temp = positions[i];
                positions[i] = positions[randomIndex];
                positions[randomIndex] = temp;
            }
        }
    }
}