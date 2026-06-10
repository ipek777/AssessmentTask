using System;
using System.Collections.Generic;

namespace AssessmentTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Generate random map? (y/n): ");
                string choice = Console.ReadLine() ?? "";

                Console.Write("Map rows: ");
                int rows = int.Parse(Console.ReadLine()!);

                Console.Write("Map columns: ");
                int cols = int.Parse(Console.ReadLine()!);

                if (rows < 2 || rows > 100 || cols < 2 || cols > 100)
                {
                    throw new ArgumentException("Rows and columns must be between 2 and 100.");
                }

                string[,] cells;
                bool generatedMap = choice.ToLower() == "y";

                if (generatedMap)
                {
                    Console.Write("Number of astronauts: ");
                    int astronautCount = int.Parse(Console.ReadLine()!);

                    Console.Write("Number of asteroids: ");
                    int asteroidCount = int.Parse(Console.ReadLine()!);

                    Console.Write("Number of debris cells: ");
                    int debrisCount = int.Parse(Console.ReadLine()!);

                    cells = MapGenerator.Generate(rows, cols, astronautCount, asteroidCount, debrisCount);
                }
                else
                {
                    Console.WriteLine("Cosmic map:");
                    cells = ReadGrid(rows, cols);
                }

                Grid grid = new Grid(cells);

                if (generatedMap)
                {
                    Console.WriteLine();
                    Console.WriteLine("Generated cosmic map:");
                    grid.Print();
                }

                if (grid.FinishPosition.Row == -1 || grid.FinishPosition.Col == -1)
                {
                    throw new ArgumentException("The map must contain a Space Station marked with F.");
                }

                if (grid.Astronauts.Count == 0)
                {
                    throw new ArgumentException("The map must contain at least one astronaut.");
                }

                // PathFinderBase pathFinder = new BfsPathFinder();
                PathFinderBase pathFinder = new DijkstraPathFinder();

                List<PathResult> results = new List<PathResult>();

                foreach (Astronaut astronaut in grid.Astronauts)
                {
                    PathResult result = pathFinder.FindShortestPath(grid, astronaut);
                    results.Add(result);
                }

                results.Sort((first, second) =>
                {
                    if (!first.IsSuccessful && !second.IsSuccessful)
                    {
                        return 0;
                    }

                    if (first.IsSuccessful && second.IsSuccessful)
                    {
                        return first.Distance.CompareTo(second.Distance);
                    }

                    return first.IsSuccessful ? 1 : -1;
                });

                Console.WriteLine();

                foreach (PathResult result in results)
                {
                    if (!result.IsSuccessful)
                    {
                        Console.WriteLine($"Mission failed — Astronaut {result.Astronaut.Name} lost in space!");
                    }
                    else
                    {
                        Console.WriteLine($"Astronaut {result.Astronaut.Name} - Shortest path cost: {result.Distance}");
                        grid.PrintWithPath(result);
                        Console.WriteLine();
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Input error: numeric values must be valid numbers.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        private static string[,] ReadGrid(int rows, int cols)
        {
            string[,] cells = new string[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                string line = Console.ReadLine() ?? "";
                string[] symbols = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (symbols.Length != cols)
                {
                    throw new ArgumentException($"Row {row + 1} must contain exactly {cols} symbols.");
                }

                for (int col = 0; col < cols; col++)
                {
                    cells[row, col] = symbols[col];
                }
            }

            return cells;
        }
    }
}