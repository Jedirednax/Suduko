namespace Suduko
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(161, 55);

            int[,] InBoard = new int[,]{
            //{7,0,0,0,8,0,0,3,0},
            //{0,0,0,0,2,0,9,0,0},
            //{0,0,0,5,6,0,0,0,8},
            //{0,0,3,0,0,0,8,0,0},
            //{0,0,0,0,0,0,0,5,9},
            //{8,0,0,0,3,9,1,0,7},
            //{0,7,0,0,0,8,0,0,0},
            //{0,0,4,0,0,1,0,6,0},
            //{3,0,1,0,0,6,7,0,0},

           // {7,1,0,9,8,4,6,3,0,},
           // {0,0,8,1,2,3,9,7,0,},
           // {0,3,9,5,6,7,0,1,8,},
           // {0,0,3,0,0,5,8,0,6,},
           // {0,0,7,8,0,2,3,5,9,},
           // {8,0,0,6,3,9,1,0,7,},
           // {0,7,6,3,0,8,0,9,1,},
           // {0,8,4,0,0,1,0,6,3,},
           // {3,0,1,0,0,6,7,8,0,},

               {2,0,0,0,0,0,0,0,0},
               {0,0,0,4,0,0,1,0,6},
               {0,0,0,0,7,0,4,0,0},
               {0,0,5,0,0,0,6,0,0},
               {6,0,0,0,0,0,0,0,2},
               {0,7,8,0,2,0,0,0,9},
               {9,0,7,0,0,0,0,3,0},
               {0,0,0,1,0,0,7,0,0},
               {1,0,0,0,0,6,5,0,0},
            };

            // SudokuSolver solver = new SudokuSolver(InBoard);

            //solver.ColourBoardDisplay();
            // solver.SolveBoard();

            // solver.ColourBoardDisplay();
            // solver.SolveBoard();

            // solver.ColourBoardDisplay();

            //Console.WriteLine(solver.VerifyBoard());
        }
    }
}
/*
}
public static void ColourBoardDisplay(Cell[,] Grid, int[,] Clues, int[,] Solution)
{
    int BoardSize = Grid.GetLength(0);
    int BlockSize = (int)Math.Sqrt(Grid.GetLength(1));
    for(int row = 0; row < BoardSize; row++)
    {
        Console.WriteLine();
        if(row%BlockSize == 0)
        {
            Console.Write($"{RowSep(BlockSize)}\n|");
        }
        else
        {
            Console.Write($"|");
        }
        for(int column = 0; column < BoardSize; column++)
        {
            Console.Write($"{" "}");
            if(Grid[row, column].CellValue == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"{" "}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                if(Grid[row, column].CellValue == Clues[row, column])
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{Grid[row, column]}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if(Grid[row, column].CellValue == Solution[row, column])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{Grid[row, column]}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{Grid[row, column]}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            if(((column+1)%BlockSize) == 0)
            {
                Console.Write($"{" "}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"|");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
    Console.WriteLine($"\n{RowSep(BlockSize)}");
}

public static void ColourBoardDisplay(IntergerBoard board)
{
    for(int row = 0; row < board.BoardSize; row++)
    {
        Console.WriteLine();
        if(row%board.BlockSize == 0)
        {
            Console.Write($"{RowSep(board.BlockSize)}\n|");
        }
        else
        {
            Console.Write($"|");
        }
        for(int column = 0; column < board.BoardSize; column++)
        {
            Console.Write($"{" "}");
            if(board.Grid[row, column] == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"{" "}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                if(board.Grid[row, column] == board.Clues[row, column])
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{board.Grid[row, column]}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if(board.Grid[row, column] == board.Solution[row, column])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{board.Grid[row, column]}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{board.Grid[row, column]}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            if(((column+1)%board.BlockSize) == 0)
            {
                Console.Write($"{" "}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"|");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
    Console.WriteLine($"\n{RowSep(board.BlockSize)}");
}

public static string RowSep(int size)
{
    StringBuilder sb = new StringBuilder("+");

    for(int i = 0; i < size; i++)
    {
        for(int j = 0; j < size; j++)
        {
            sb.Append("--");
        }
        sb.Append("-+");
    }

    return sb.ToString();
}
}
}
/*
* Begginer = {
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* };
* Easy = {
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* {0,0,0,0,0,0,0,0,0},
* };
* Medium = {
* {9,8,0,0,0,4,7,0,3},
* {4,0,0,1,2,0,0,0,0},
* {0,0,2,0,0,8,0,0,1},
* {7,0,0,5,0,0,1,8,0},
* {0,5,3,7,1,0,0,0,2},
* {0,6,0,0,8,2,0,5,0},
* {3,4,0,0,0,0,2,0,9},
* {2,1,0,0,4,0,6,3,0},
* {6,0,8,0,0,0,0,0,0},
* };
* Hard = {
* {0,0,0,0,0,0,9,0,1},
* {0,0,0,0,6,3,0,2,0},
* {0,0,2,9,0,0,5,3,0},
* {7,0,1,0,2,0,0,0,5},
* {4,0,6,0,0,0,0,8,7},
* {0,0,0,0,0,0,0,0,0},
* {0,1,0,2,0,4,0,5,3},
* {0,2,4,0,5,0,0,1,0},
* {0,7,3,0,0,0,0,0,0},
* };
* Extreme = {
* {7,0,0,0,8,0,0,3,0},
* {0,0,0,0,2,0,9,0,0},
* {0,0,0,5,6,0,0,0,8},
* {0,0,3,0,0,0,8,0,0},
* {0,0,0,0,0,0,0,5,9},
* {8,0,0,0,3,9,1,0,7},
* {0,7,0,0,0,8,0,0,0},
* {0,0,4,0,0,1,0,6,0},
* {3,0,1,0,0,6,7,0,0},
* };
*/