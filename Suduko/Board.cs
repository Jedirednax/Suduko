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
        public Random random = new Random();

        #region Board Properties
        private int BlockSize { get; set; }
        private int BoardSize { get; set; }
        private int NumberOfCells { get; set; }
        private int NumberOfBlocks { get; set; }
        #endregion
        #region Board Values
        public int[,] Grid { get; set; }

        public int[,] Puzzle;
        public int[,] Solution; /*=
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
            { 0, 0, 0, 5, 0, 0, 0, 7, 0 },
            { 5, 0, 0, 0, 0, 8, 0, 2, 9 },
            { 0, 9, 8, 4, 0, 1, 3, 0, 5 },
            { 0, 2, 0, 7, 0, 4, 0, 0, 0 },
            { 0, 0, 5, 2, 0, 0, 0, 0, 7 },
            { 0, 0, 0, 0, 0, 0, 0, 5, 0 },
            { 4, 8, 0, 9, 0, 0, 7, 0, 0 },
            { 0, 0, 0, 8, 0, 2, 0, 0, 0 },
            { 0, 0, 0, 0, 4, 0, 2, 0, 0 },
        };

        /*
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
        };*/
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

            Solution = new int[BoardSize, BoardSize];
            for(int row = 0; row < BoardSize; row++)
            {
                for(int column = 0; column < BoardSize; column++)
                {
                    Solution[row, column] =0;
                }
            }
            //Puzzle = Grid;

            GenerateBoard();
            //for(int row = 0; row < BoardSize; row++)
            //{
            //    for(int column = 0; column < BoardSize; column++)
            //    {
            //        Grid[row, column] = Puzzle[row, column];
            //    }
            //}
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
                    //Puzzle = Easy;
                    break;
                case Difficulty.Medium:
                    //Puzzle = Medium;
                    break;
                case Difficulty.Hard:
                    //Puzzle = Hard;
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
            return false;
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
            if(row >= BoardSize ||  column >= BoardSize)
            {
                return false;
            }
            if(Puzzle[row, column] != 0)
            {
                return false;
            }
            if(IsSafe(row, column, cellValue))
            {
                Grid[row, column] = cellValue;
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ConstraintSolve()
        {
            int counter = 0;
            do
            {
                for(int i = 0; i < BoardSize; i++)
                {
                    for(int j = 0; j < BoardSize; j++)
                    {
                        int[] cell = CellPossible(i, j);
                        if(cell != null && cell.Length == 1)
                        {
                            SafeInsert(i, j, cell[0]);
                        }
                    }
                }
                counter++;
            }
            while(!CompareBoard()||counter<10);
        }

        public void EliminateSolve()
        {
            int count = 0;
            do
            {
                for(int i = 0; i < BoardSize; i++)
                {
                    for(int j = 0; j < BoardSize; j++)
                    {
                        if(Grid[i, j] == 0)
                        {

                            int[] row = ElimRow(i, j);
                            int[] col = ElimCol(i, j);
                            int[] blk = ElimBlock(i, j);
                            try
                            {
                                if(row != null && row.Length==1)
                                {
                                    Insert(i, j, row.First());
                                }
                            }
                            catch { }
                            try
                            {
                                if(col != null && col.Length==1)
                                {
                                    Insert(i, j, col.First());
                                }
                            }
                            catch { }
                            try
                            {
                                if(blk != null && blk.Length==1)
                                {
                                    Insert(i, j, blk.First());
                                }
                            }
                            catch { }
                        }
                    }
                }
                for(int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        int[] cell = CellPossible(i, j);
                        if(cell != null && cell.Length == 1)
                        {
                            SafeInsert(i, j, cell[0]);
                        }
                    }
                }
                count++;
            }
            while(!CompareBoard()||count<10);
        }

        public bool BackTrackingSolve(int row, int column)
        {
            if(row == BoardSize-1 && column ==BoardSize)
            {
                return true;
            }
            if(column == BoardSize)
            {
                row +=1;
                column = 0;
            }
            if(Grid[row, column] != 0)
            {
                column+=1;
                return BackTrackingSolve(row, column);
            }
            int[] val = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            random.Shuffle(val);
            foreach(int cell in val)
            {
                if(IsSafe(row, column, cell))
                {
                    Grid[row, column] = cell;
                    if(BackTrackingSolve(row, column+1))
                    {
                        return true;
                    }
                }
                Grid[row, column] = 0;
            }
            return false;

        }

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

        public int[] CellPossible(int row, int column)
        {
            if(row >= BoardSize ||  column >= BoardSize)
            {
                return null;
            }
            if(Grid[row, column] == 0)
            {
                List<int> possible = new List<int>();

                for(int i = 0; i < BoardSize; i++)
                {
                    if(IsSafe(row, column, i+1))
                    {
                        possible.Add(i+1);
                    }
                }
                return possible.ToArray();
            }
            else
            {
                return new int[BoardSize];
            }
        }

        public void LastCell()
        {

        }

        public void LastCellRow(int row)
        {

        }

        public void LastCellColumn(int column)
        {

        }

        public void LastCellBlock(int row, int column)
        {

        }

        public void RemaningPossible()
        {
            for(int i = 0; i < BoardSize; i++)
            {
                for(int j = 0; j < BoardSize; j++)
                {
                    if(Grid[i, j] == 0)
                    {
                        int[] row = ElimRow(i, j);
                        try
                        {
                            if(row != null && row.Length==1)
                            {
                                Insert(i, j, row.First());
                                break;
                            }
                        }
                        catch { }
                        int[] col = ElimCol(i, j);
                        try
                        {
                            if(col != null && col.Length==1)
                            {
                                Insert(i, j, col.First());
                                break;
                            }
                        }
                        catch { }
                        int[] blk = ElimBlock(i, j);
                        try
                        {
                            if(blk != null && blk.Length==1)
                            {
                                Insert(i, j, blk.First());
                                break;
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        public void NakedPair()
        {

        }

        public void HiddenPair()
        {

        }

        public void XWing()
        {

        }
        public void YWing()
        {
        }

        public void PointingPair()
        {

        }

        public void SwordFish()
        {

        }

        public int[] ElimRow(int row, int currCol)
        {
            int[] currRow = CellPossible(row, currCol);
            List<int> hold = new List<int>();
            for(int i = 0; i < BoardSize; i++)
            {
                if(i != currCol)
                {
                    hold.AddRange(CellPossible(row, i));
                }
            }
            List<int> res = new List<int>();
            foreach(int i in currRow)
            {
                if(!hold.Contains(i))
                {
                    res.Add(i);
                }
            }
            return res.ToArray();
        }

        public int[] ElimCol(int currRow, int col)
        {
            int[] currCol = CellPossible(currRow, col);
            List<int> hold = new List<int>();
            for(int i = 0; i < BoardSize; i++)
            {
                if(i != currRow)
                {
                    hold.AddRange(CellPossible(i, col));
                }
            }
            List<int> res = new List<int>();
            foreach(int i in currCol)
            {
                if(!hold.Contains(i))
                {
                    res.Add(i);
                }
            }
            return res.ToArray();
        }

        public int[] ElimBlock(int row, int col)
        {

            int[] currCol = CellPossible(row, col);
            List<int> hold = new List<int>();

            int rowBlock = row/BlockSize*BlockSize;
            int columnBlock = col/BlockSize*BlockSize;

            for(int i = rowBlock; i < (rowBlock + BlockSize); i++)
            {
                for(int j = columnBlock; j < (columnBlock + BlockSize); j++)
                {
                    if(!(i == row && j== col))
                    {
                        hold.AddRange(CellPossible(i, j));
                    }
                }
            }
            List<int> res = new List<int>();
            foreach(int g in currCol)
            {
                if(!hold.Contains(g))
                {
                    res.Add(g);
                }
            }
            return res.ToArray();
        }

        public void CheckElem(int row, int column)
        {

        }

        public bool IsSafe(int row, int column, int cellValue)
        {
            bool validRow = CheckRow(row, cellValue);
            bool validColumn = CheckColumn(column, cellValue);
            bool validBlock = CheckBlock(row, column, cellValue);
            bool result =  validRow && validColumn && validBlock &&true;
            return result;
        }

        public bool CheckRow(int row, int cellValue)
        {
            return !GetRow(row).Contains(cellValue);
        }

        public bool CheckColumn(int column, int cellValue)
        {
            return !GetColumn(column).Contains(cellValue);
        }

        public bool CheckBlock(int row, int column, int cellValue)
        {
            return !GetBlock(row, column).Contains(cellValue);
        }

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

        public void GenerateBoard()
        {

            int[] values = {1,2,3,4,5,6,7,8,9};

            for(int row = 0; row < BlockSize; row++)
            {

                int rowBlock = row*3/BlockSize*BlockSize;
                int columnBlock = row*3/BlockSize*BlockSize;
                int blockIndex = 0;
                random.Shuffle(values);
                for(int i = rowBlock; i < (rowBlock + BlockSize); i++)
                {
                    for(int j = columnBlock; j < (columnBlock + BlockSize); j++)
                    {
                        Solution[i, j] = values[blockIndex++];
                    }
                }
            }
            Grid = Solution;

            BackTrackingSolve(0, 0);
            SetBoard();
        }

        public void SetBoard()
        {
            for(int row = 0; row < 46;)
            {

                int x = random.Next(BoardSize);
                int y = random.Next(BoardSize);
                if(Grid[x, y] != 0)
                {
                    Grid[x, y] = 0;
                    row++;
                }
            }
            Puzzle = new int[BoardSize, BoardSize];
            for(int row = 0; row < BoardSize; row++)
            {
                for(int column = 0; column < BoardSize; column++)
                {
                    Puzzle[row, column]= Grid[row, column];
                }
            }
        }

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

/*
 
+-------+-------+-------+
| 0 0 0 | 5 0 0 | 0 0 0 |
| 5 0 0 | 0 0 8 | 0 2 0 |
| 0 9 8 | 4 0 1 | 0 0 5 |
+-------+-------+-------+
| 0 0 0 | 7 5 4 | 0 0 0 |
| 0 0 5 | 0 0 0 | 0 0 7 |
| 0 0 0 | 0 0 0 | 0 5 0 |
+-------+-------+-------+
| 4 8 0 | 0 0 5 | 7 0 0 |
| 0 0 0 | 8 0 0 | 5 0 0 |
| 0 5 0 | 0 0 0 | 2 0 0 |
+-------+-------+-------+
*/