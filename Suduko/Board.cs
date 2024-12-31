using System.Text;

namespace Suduko
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public class Board
    {

        #region Board Properties
        private int BlockSize { get; set; }
        private int BoardSize { get; set; }
        private int NumberOfCells { get; set; }
        private int NumberOfBlocks { get; set; }
        #endregion
        #region Board Values
        public int[,] Grid { get; set; }

        public int[,] Puzzle;
        public int[,] Solution =
        {
            { 3, 4, 6, 5, 2, 9, 8, 7, 1 },
            { 5, 7, 1, 3, 6, 8, 4, 2, 9 },
            { 2, 9, 8, 4, 7, 1, 3, 6, 5 },
            { 6, 2, 9, 7, 5, 4, 1, 8, 3 },
            { 8, 1, 5, 2, 9, 3, 6, 4, 7 },
            { 7, 3, 4, 1, 8, 6, 9, 5, 2 },
            { 4, 8, 2, 9, 3, 5, 7, 1, 6 },
            { 9, 6, 7, 8, 1, 2, 5, 3, 4 },
            { 1, 5, 3, 6, 4, 7, 2, 9, 8 },
        };
        public int[,] Easy =
        {
            { 3, 4, 6, 5, 0, 0, 8, 7, 1 },
            { 5, 7, 0, 0, 6, 8, 0, 2, 9 },
            { 2, 9, 8, 4, 7, 1, 3, 6, 5 },
            { 0, 2, 9, 7, 5, 4, 0, 8, 0 },
            { 8, 1, 5, 2, 9, 3, 6, 0, 0 },
            { 7, 0, 0, 1, 0, 6, 0, 5, 2 },
            { 4, 8, 2, 9, 3, 5, 7, 1, 6 },
            { 0, 0, 7, 8, 0, 2, 0, 0, 0 },
            { 0, 0, 0, 6, 4, 0, 2, 0, 8 },
        };
        public int[,] Medium =
        {
            { 0, 4, 0, 5, 0, 9, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 8, 4, 0, 9 },
            { 2, 9, 0, 0, 7, 0, 3, 0, 5 },
            { 6, 2, 9, 7, 0, 0, 1, 8, 0 },
            { 0, 0, 5, 2, 0, 3, 6, 0, 0 },
            { 0, 0, 0, 1, 8, 0, 0, 0, 2 },
            { 0, 8, 0, 9, 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 2, 5, 0, 4 },
            { 0, 5, 0, 6, 4, 7, 2, 0, 0},
        };
        public int[,] Hard =
        {
            { 0, 0, 0, 5, 0, 0, 0, 0, 0 },
            { 5, 0, 0, 0, 0, 8, 0, 2, 0 },
            { 0, 9, 8, 4, 0, 1, 0, 0, 5 },
            { 0, 0, 0, 7, 0, 4, 0, 0, 0 },
            { 0, 0, 5, 0, 0, 0, 0, 0, 7 },
            { 0, 0, 0, 0, 0, 0, 0, 5, 0 },
            { 4, 8, 0, 0, 0, 0, 7, 0, 0 },
            { 0, 0, 0, 8, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 2, 0, 0 },
        };

        #endregion

        #region Board Generation
        /// <summary>
        /// Generic constructor
        /// </summary>
        /// <param name="Size"></param>
        public Board(int Size = 3)
        {
            BlockSize = Size;
            BoardSize = BlockSize * BlockSize;
            NumberOfBlocks = BoardSize;

            NumberOfCells = BlockSize * BoardSize;
            Puzzle = Easy;

            Grid = new int[BoardSize, BoardSize];
            for(int row = 0; row < BoardSize; row++)
            {
                for(int column = 0; column < BoardSize; column++)
                {
                    Grid[row, column] = Puzzle[row, column];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="diff"></param>
        /// <param name="Size"></param>
        public Board(Difficulty diff, int Size = 3)
        {
            BlockSize = Size;
            BoardSize = BlockSize * BlockSize;
            NumberOfBlocks = BoardSize;

            NumberOfCells = BlockSize * BoardSize;
            switch(diff)
            {
                case Difficulty.Easy:
                    Puzzle = Easy;
                    break;
                case Difficulty.Medium:
                    Puzzle = Medium;
                    break;
                case Difficulty.Hard:
                    Puzzle = Hard;
                    break;
            }

            Grid = new int[BoardSize, BoardSize];
            for(int row = 0; row < BoardSize; row++)
            {
                for(int column = 0; column < BoardSize; column++)
                {
                    Grid[row, column] = Puzzle[row, column];
                }
            }
        }
        #endregion
        #region Board Solving

        /// <summary>
        /// Takes board position, is not a puzzle set, sets the grid to the new value.
        /// </summary>
        /// <param name="row"> Row to insert value. </param>
        /// <param name="column"> Column to insert value. </param>
        /// <param name="cellValue"> Value to be inserted. </param>
        /// <returns> Returns if the value can be inserted. </returns>
        public bool Insert(int row, int column, int cellValue)
        {
            if(Puzzle[row, column] == 0)
            {
                Grid[row, column] = cellValue;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Takes board position, is not a puzzle set, sets the grid to the new value.
        /// Only insertes value if it is safe to do so.
        /// </summary>
        /// <param name="row"> Row to insert value. </param>
        /// <param name="column"> Column to insert value. </param>
        /// <param name="cellValue"> Value to be inserted. </param>
        /// <returns> Returns if the value can be inserted. </returns>
        public bool SafeInsert(int row, int column, int cellValue)
        {
            if(Puzzle[row, column] == 0)
            {
                if(IsSafe(row, column, cellValue))
                {
                    Grid[row, column] = cellValue;
                    return true;
                }
                else { return false; }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ConstraintSolve()
        {
            Console.ReadLine();
            do
            {
                for(int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        int[] cell = CellPossible(i, j);
                        if(cell != null && cell.Length == 1)
                        {
                            Console.Clear();
                            ConsoleDisplay();
                            SafeInsert(i, j, cell[0]);
                            Thread.Sleep(250);
                        }
                    }
                }
            }
            while(!CompareBoard());
        }

        /// <summary>
        /// 
        /// </summary>
        public void EliminateSolve()
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {

                }
            }
            ConsoleDisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        public void BackTrackingSove()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void BruteSolve()
        {
            do
            {
                for(int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        for(int k = 1; k <= 9; k++)
                        {
                            SafeInsert(i, j, k);
                        }
                    }
                }
            }
            while(!CompareBoard());
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reveal()
        {
            for(int row = 0; row < BoardSize; row++)
            {
                for(int column = 0; column < BoardSize; column++)
                {
                    Grid[row, column] = Solution[row, column];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns> </returns>
        public int[] GetRow(int row)
        {
            int[] res = new int[BoardSize];
            for(int column = 0; column < BoardSize; column++)
            {
                if(Grid[row, column] != 0)
                {
                    res[column] = Grid[row, column];
                }
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <returns> </returns>
        public int[] GetColumn(int column)
        {
            int[] res = new int[BoardSize];
            for(int row = 0; row < BoardSize; row++)
            {
                if(Grid[row, column] != 0)
                {
                    res[row] = Grid[row, column];
                }
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns> </returns>
        public int[] GetBlock(int row, int column)
        {
            int[] res = new int[BoardSize];

            int rowBlock = row/BlockSize*BlockSize;
            int columnBlock = column/BlockSize*BlockSize;
            int blockIndex = 0;

            for(int i = rowBlock; i < (rowBlock + BlockSize); i++)
            {
                for(int j = columnBlock; j < (columnBlock + BlockSize); j++)
                {
                    if(Grid[i, j] != 0)
                    {
                        res[blockIndex++] = Grid[i, j];
                    }
                    else
                    {
                        res[blockIndex++] = 0;
                    }
                }
            }
            return res;
        }

        #region Checks

        /// <summary>
        /// Gets a list of values that can be entered in to the cell based off of contrsints
        /// <see cref="CheckRow(int, int)"/>
        /// <see cref="CheckColumn(int, int)"/>
        /// <see cref="CheckBlock(int, int, int)"/>
        /// </summary>
        /// <param name="row"> The row if the cell to check </param>
        /// <param name="column"> The column of the cell to check </param>
        /// <returns> returns an array of posisble values that can beentered </returns>
        public int[] CellPossible(int row, int column)
        {
            if(Grid[row, column] == 0)
            {
                List<int> possible = new List<int>();

                for(int i = 1; i<=BoardSize; i++)
                {
                    if(IsSafe(row, column, i))
                    {
                        possible.Add(i);
                    }
                }
                return possible.ToArray();
            }
            else
            {
                return new int[9];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void CheckElem(int row, int column)
        {

        }

        /// <summary>
        /// Returns is the all three constraints are met for a valid number
        /// </summary>
        /// <param name="row"> The row if the cell to check. </param>
        /// <param name="column"> The column of the cell to check. </param>
        /// <param name="cellValue"> The value being checked. </param>
        /// <returns> Returns true if all there cinditions are true, else returns false. </returns>
        public bool IsSafe(int row, int column, int cellValue)
        {
            bool validRow = CheckRow(row, cellValue);
            bool validColumn = CheckColumn(column, cellValue);
            bool validBlock = CheckBlock(row, column, cellValue);
            bool result =  validRow && validColumn && validBlock &&true;
            //Debug.WriteLine($"|({row}:{column})={cellValue} | R {validRow,-6} | C {validColumn,-6}| B {validBlock,-6}", "IsSafe");
            return result;
        }

        /// <summary>
        /// Checks if the selected row contains the passed in value.
        /// </summary>
        /// <param name="row"> The row to check. </param>
        /// <param name="cellValue"> Value to look for. </param>
        /// <returns> if the row contains the value returns false. </returns>
        public bool CheckRow(int row, int cellValue)
        {
            return !GetRow(row).Contains(cellValue);
        }

        /// <summary>
        /// Checks if the selected column contains the passed in value.
        /// </summary>
        /// <param name="column"> The column to check. </param>
        /// <param name="cellValue"> Value to look for. </param>
        /// <returns> if the row contains the value returns false. </returns> 
        public bool CheckColumn(int column, int cellValue)
        {
            return !GetColumn(column).Contains(cellValue);
        }

        /// <summary>
        /// Checks if the selected block contains the passed in value.
        /// </summary>
        /// <param name="row"> The row to check. </param>
        /// <param name="column"> The column to check. </param>
        /// <param name="cellValue"> Value to look for. </param>
        /// <returns> If the block contains the value returns false. </returns>
        public bool CheckBlock(int row, int column, int cellValue)
        {
            return !GetBlock(row, column).Contains(cellValue);
        }

        /// <summary>
        /// Campars the board to the solution board.
        /// </summary>
        /// <returns> </returns>
        public bool CheckBoard()
        {
            for(int row = 0; row < BoardSize; row++)
            {
                for(int column = 0; column < BoardSize; column++)
                {
                    int y = Grid[row, column];
                    if(Puzzle[row, column] == 0)
                    {
                        bool v = IsSafe(row, column, y);
                        if(!v)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns> If the grid board is the same as the Solution board. </returns>
        public bool CompareBoard()
        {
            for(int row = 0; row < BoardSize; row++)
            {
                for(int column = 0; column < BoardSize; column++)
                {
                    if(Grid[row, column] != Solution[row, column])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
        #endregion

        #region Display

        public void ConsoleDisplay()
        {
            for(int row = 0; row < BoardSize; row++)
            {
                Console.WriteLine();
                if(row%BlockSize == 0)
                {
                    Console.Write($"{RowSep()}\n|");
                }
                else
                {
                    Console.Write($"|");
                }
                for(int column = 0; column < BoardSize; column++)
                {
                    Console.Write($"{" "}");
                    if(Grid[row, column] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write($"{" "}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        if(Grid[row, column] == Puzzle[row, column])
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write($"{Grid[row, column]}");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else if(Grid[row, column] == Solution[row, column])
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
            Console.WriteLine($"\n{RowSep()}");
        }

        public void GridDisplay()
        {
            Console.WriteLine();
            const int padding = 1;
            Console.Write(RowSep());
            for(int row = 0; row < BoardSize; row++)
            {
                if(row%BlockSize == 0 && row != 0)
                {
                    Console.WriteLine();
                    Console.Write(RowSep());
                }
                Console.WriteLine();
                for(int column = 0; column < BoardSize; column++)
                {
                    if((column%BlockSize) == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write($"| ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write($"0");
                    Console.Write($"{" "}");
                }
                Console.Write($"|");
            }
            Console.WriteLine();
            Console.Write(RowSep());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string rowSep = "+";
            for(int i = 0; i <= BoardSize; i++)
            {
                rowSep += "--";
            }

            for(int row = 0; row < BoardSize; row++)
            {
                sb.AppendLine();
                if(row%BlockSize == 0 && row != 0)
                {
                    sb.Append($"{rowSep}\n");
                }

                for(int column = 0; column < BoardSize; column++)
                {
                    if(Grid[row, column] == 0)
                    {
                        sb.Append($"   ");
                    }
                    else
                    {
                        if(Grid[row, column] == Puzzle[row, column])
                        {
                            sb.Append($"{Grid[row, column]}");
                        }
                        else if(Grid[row, column] == Solution[row, column])
                        {
                            sb.Append($"{Grid[row, column]}");
                        }
                    }
                    if(((column+1)%BlockSize) == 0 && (column +1)!=BoardSize)
                    {
                        sb.Append($"|");
                    }
                }
            }
            sb.AppendLine();
            return sb.ToString();
        }
        public string RowSep()
        {
            StringBuilder sb = new StringBuilder("+");

            for(int i = 0; i < BlockSize; i++)
            {
                for(int j = 0; j < BlockSize; j++)
                {
                    sb.Append("--");
                }
                sb.Append("-+");
            }

            return sb.ToString();
        }
        #endregion
    }
}