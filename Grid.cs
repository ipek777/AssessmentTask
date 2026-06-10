using System;
using System.Collections.Generic;

namespace AssessmentTask
{
    public class Grid
    {
        public string[,] Cells { get; }
        public int Rows { get; }
        public int Cols { get; }

        public List<Astronaut> Astronauts { get; }
        public Position FinishPosition { get; private set; }

        public Grid(string[,] cells)
        {
            Cells = cells;
            Rows = cells.GetLength(0);
            Cols = cells.GetLength(1);

            Astronauts = new List<Astronaut>();
            FinishPosition = new Position(-1, -1);

            FindImportantPositions();
        }

        private void FindImportantPositions()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    string symbol = Cells[row, col];

                    if (symbol == "S1" || symbol == "S2" || symbol == "S3")
                    {
                        Astronauts.Add(new Astronaut(symbol, new Position(row, col)));
                    }
                    else if (symbol == "F")
                    {
                        FinishPosition = new Position(row, col);
                    }
                }
            }
        }

        public bool IsInside(Position position)
        {
            return position.Row >= 0 &&
                   position.Row < Rows &&
                   position.Col >= 0 &&
                   position.Col < Cols;
        }

        public bool IsWalkable(Position position)
        {
            if (!IsInside(position))
            {
                return false;
            }

            string symbol = Cells[position.Row, position.Col];

            return symbol != "X";
        }

        public string[,] CopyCells()
        {
            string[,] copy = new string[Rows, Cols];

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    copy[row, col] = Cells[row, col];
                }
            }

            return copy;
        }
        public void PrintWithPath(PathResult result)
        {
            string[,] mapToPrint = CopyCells();

            for (int i = 1; i < result.Path.Count - 1; i++)
            {
                Position position = result.Path[i];

                string currentSymbol = mapToPrint[position.Row, position.Col];

                if (currentSymbol != "F" && !currentSymbol.StartsWith("S"))
                {
                    mapToPrint[position.Row, position.Col] = "*";
                }
            }

            PrintCells(mapToPrint);
        }
        public void Print()
        {
            PrintCells(Cells);
        }
        private void PrintCells(string[,] cells)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Console.Write(cells[row, col]);

                    if (col < Cols - 1)
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }
        public int GetMovementCost(Position position)
        {
            string symbol = Cells[position.Row, position.Col];

            if (symbol == "D")
            {
                return 2;
            }

            return 1;
        }
    }
}