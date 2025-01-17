using System.Text;

namespace Suduko
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public class IntergerBoard
    {
        public Random random = new Random();

        #region Board Properties
        private int BlockSize { get; set; }
        private int BoardSize { get; set; }
        private int NumberOfCells { get; set; }
        private int NumberOfBlocks { get; set; }
        #endregion

        #region Board Grids
        public int[,] Grid { get; set; }

        public int[,] ShownPositions;
        public int[,] Solution;

        public int[,,] Possibilities { get; set; }

        private int[,] Easy;
        private int[,] Medium;
        private int[,] Hard;

        private int MissingCellsEasy;
        private int MissingCellsMedium;
        private int MissingCellsHard;

        #endregion

        #region Board Generation

        public IntergerBoard()
        {
            BlockSize = 3;
            BoardSize = BlockSize * BlockSize;
            NumberOfBlocks = BoardSize;
            NumberOfCells = BoardSize * BoardSize;
            //Grid = new int[,]{
            //    {6,7,9,3,8,4,5,2,1},
            //    {2,3,1,9,5,7,4,8,6},
            //    {4,5,8,6,1,2,9,3,7},
            //    {7,2,6,1,4,5,3,9,8},
            //    {9,1,4,8,3,6,7,5,2},
            //    {3,8,5,2,7,9,1,6,4},
            //    {5,6,2,7,9,1,8,4,3},
            //    {1,4,3,5,6,8,2,7,9},
            //    {8,9,7,4,2,3,6,1,5},
            //};
            Grid = new int[BoardSize, BoardSize];
            GenerateBoard();
        }
        public IntergerBoard(int size)
        {
            BlockSize = size;
            BoardSize = BlockSize * BlockSize;
            NumberOfBlocks = BoardSize;
            NumberOfCells = BoardSize * BoardSize;
            //Grid = new int[,]{
            //{6,7,9,3,8,4,5,2,1},
            //{2,3,1,9,5,7,4,8,6},
            //{4,5,8,6,1,2,9,3,7},
            //{7,2,6,1,4,5,3,9,8},
            //{9,1,4,8,3,6,7,5,2},
            //{3,8,5,2,7,9,1,6,4},
            //{5,6,2,7,9,1,8,4,3},
            //{1,4,3,5,6,8,2,7,9},
            //{8,9,7,4,2,3,6,1,5},
            //};
            Grid = new int[BoardSize, BoardSize];
            GenerateBoard();
            Console.WriteLine(VerifyBoard());
        }

        #endregion
        // Last CElls
        // Check single Row
        // Check single Column
        // Check single Block

        // Check only possible

        // Check by elimmination

        public bool SolveBoard()
        {
            int counter = 0;
            Console.WriteLine("Constraint:");
            do
            {
            }
            while(ConstraintSolve());
            Console.WriteLine();
            ConsoleDisplay();
            Console.WriteLine("Remainder:");
            do
            {
            }
            while(RemaningPossible());

            Console.WriteLine();
            ConsoleDisplay();
            Console.WriteLine("Elimination:");
            do
            {
            }
            while(EliminateSolve());
            return VerifyBoard();
        }

        public void Set(int row, int column, int cellValue)
        {
            Grid[row, column] = cellValue;
        }
        public int Get(int row, int column)
        {
            return Grid[row, column];
        }

        public bool SafeInsert(int row, int column, int cellValue)
        {
            if(row >= BoardSize ||  column >= BoardSize)
            {
                return false;
            }
            if(Grid[row, column] != 0)
            {
                return false;
            }
            if(IsSafe(row, column, cellValue))
            {
                Set(row, column, cellValue);
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool SafeInsert(int row, int column, int[] cellPValue)
        {
            if(row >= BoardSize ||  column >= BoardSize)
            {
                return false;
            }
            if(Grid[row, column] != 0)
            {
                return false;
            }
            try
            {

                int cellValue = cellPValue.Single();
                if(IsSafe(row, column, cellValue))
                {
                    Set(row, column, cellValue);
                    return true;
                }
            }
            catch
            {
                return false;

            }
            return false;

        }

        #region Board Solving Methods
        public bool ConstraintSolve()
        {
            bool inserted = false;
            for(int i = 0; i < BoardSize; i++)
            {
                for(int j = 0; j < BoardSize; j++)
                {
                    int[] cell = CellPossible(i, j);
                    if(cell != null && cell.Length == 1)
                    {
                        if(SafeInsert(i, j, cell[0]))
                        {
                            inserted = true;
                        }
                    }
                }
            }
            return inserted;
        }

        public bool EliminateSolve()
        {
            bool inserted = false;
            for(int i = 0; i < BoardSize; i++)
            {
                for(int j = 0; j < BoardSize; j++)
                {

                    if(SafeInsert(i, j, ElimRow(i, j)))
                    {
                        inserted = true;
                    }
                    else if(SafeInsert(i, j, ElimCol(i, j)))
                    {
                        inserted = true;
                    }
                    else if(SafeInsert(i, j, ElimBlock(i, j)))
                    {
                        inserted = true;
                    }
                }
            }
            return inserted;
        }

        public bool BackTrackingSolve(int row, int column)
        {
            if(row == BoardSize-1 && column ==BoardSize)
            {
                return true;
            }
            if(column == BoardSize)
            {
                row += 1;
                column = 0;
            }
            if(Grid[row, column] != 0)
            {
                column += 1;
                return BackTrackingSolve(row, column);
            }
            //int[] val = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] val = CellPossible(row,column);

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
            List<int> res = new List<int>();

            for(int column = 0; column < BoardSize; column++)
            {
                int cell = Grid[row, column];
                if(cell != 0)
                {
                    res.Add(Grid[row, column]);
                }
            }

            return [.. res];
        }

        public int[] GetColumn(int column)
        {
            List<int> res = new List<int>();

            for(int row = 0; row < BoardSize; row++)
            {
                int cell = Grid[row, column];
                if(cell != 0)
                {
                    res.Add(Grid[row, column]);
                }
            }
            return [.. res];
        }

        public int[] GetBlock(int row, int column)
        {
            List<int> res = new List<int>();

            int rowBlock = row/BlockSize*BlockSize;
            int columnBlock = column/BlockSize*BlockSize;

            for(int i = rowBlock; i < (rowBlock + BlockSize); i++)
            {
                for(int j = columnBlock; j < (columnBlock + BlockSize); j++)
                {

                    int cell = Grid[i, j];
                    if(cell != 0)
                    {
                        res.Add(cell);
                    }
                }
            }

            return [.. res];
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

                for(int i = 1; i <= BoardSize; i++)
                {
                    if(IsSafe(row, column, i))
                    {
                        possible.Add(i);
                    }
                }
                return possible.ToArray();
            }
            return null;
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

        public bool RemaningPossible()
        {
            bool inserted = false;
            for(int i = 0; i < BoardSize; i++)
            {
                for(int j = 0; j < BoardSize; j++)
                {
                    if(Grid[i, j] == 0)
                    {
                        if(SafeInsert(i, j, ElimRow(i, j)))
                        {
                            inserted = true;
                        }
                        else if(SafeInsert(i, j, ElimCol(i, j)))
                        {
                            inserted = true;
                        }
                        else if(SafeInsert(i, j, ElimBlock(i, j)))
                        {
                            inserted = true;
                        }
                    }
                }
            }
            return inserted;
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
                    int[] f = CellPossible(row, i);
                    if(f!=null)
                    {

                        hold.AddRange(f);
                    }
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
                    int[] f = CellPossible(col, i);
                    if(f!=null)
                    {

                        hold.AddRange(f);
                    }
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

            int[] currBlk = CellPossible(row, col);
            List<int> hold = new List<int>();

            int rowBlock = row/BlockSize*BlockSize;
            int columnBlock = col/BlockSize*BlockSize;

            for(int i = rowBlock; i < (rowBlock + BlockSize); i++)
            {
                for(int j = columnBlock; j < (columnBlock + BlockSize); j++)
                {
                    if(!(i == row && j== col))
                    {
                        int[] f = CellPossible(i,j);
                        if(f!=null)
                        {

                            hold.AddRange(f);
                        }
                    }
                }
            }
            List<int> res = new List<int>();
            foreach(int g in currBlk)
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
            bool result =  validRow && validColumn && validBlock && true;
            return result;
        }

        public bool CheckRow(int row, int cellValue)
        {
            int[] cells = GetRow(row);
            if(cells.Length !=BoardSize)
            {

                return !cells.Contains(cellValue);
            }
            else
            {
                return cells.Where(y => y == cellValue).Count()==1;
            }
        }

        public bool CheckColumn(int column, int cellValue)
        {
            int[] cells = GetColumn(column);
            if(cells.Length !=BoardSize)
            {

                return !cells.Contains(cellValue);
            }
            else
            {
                return cells.Where(y => y == cellValue).Count()==1;
            }
        }

        public bool CheckBlock(int row, int column, int cellValue)
        {
            int[] cells = GetBlock(row,column);
            if(cells.Length != BoardSize)
            {

                return !cells.Contains(cellValue);
            }
            else
            {
                return cells.Where(y => y == cellValue).Count()==1;
            }
        }

        public bool CheckBoard()
        {
            for(int row = 0; row < BoardSize; row++)
            {
                for(int column = 0; column < BoardSize; column++)
                {
                    int y = Grid[row, column];
                    if(ShownPositions[row, column] == 0)
                    {
                        return !IsSafe(row, column, y);
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

            //int[] values = FillValues();
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
                        Grid[i, j] = values[blockIndex++];
                    }
                }
            }
            BackTrackingSolve(0, 0);
            Console.WriteLine(VerifyBoard());
            Solution = Grid;
            SetBoard(24);
        }

        public bool VerifyBoard()
        {

            for(int row = 0; row < Grid.GetLength(0); row++)
            {
                for(int col = 0; col < Grid.GetLength(1); col++)
                {
                    if(!IsSafe(row, col, Grid[row, col]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void SetBoard(int clues)
        {

            //ShownPositions = new int[BoardSize, BoardSize];
            //ShownPositions.Initialize();
            ConsoleDisplay();
            for(int row = 0; row < (NumberOfCells -clues);)
            {

                int x = random.Next(BoardSize);
                int y = random.Next(BoardSize);
                if(Grid[x, y] != 0)
                {
                    Grid[x, y] = 0;
                    row++;
                }
            }
            for(int row = 0; row < BoardSize; row++)
            {
                for(int column = 0; column < BoardSize; column++)
                {
                    ShownPositions[row, column] = Grid[row, column];
                }
            }
        }

        public int[] FillValues()
        {
            int[] values = new int[BoardSize];
            for(int index = 1; index <= BoardSize; index++)
            {
                values[index-1] = index;
            }
            return values;
        }
        public void InitilizeGrid()
        {

            for(int row = 0; row < Grid.GetLength(0); row++)
            {
                for(int col = 0; col < Grid.GetLength(1); col++)
                {
                    Grid[row, col] = 0;
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
                        if(Grid[row, column] == ShownPositions[row, column])
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
                    sb.Append($" ");
                    if(Grid[row, column] == 0)
                    {
                        sb.Append($" ");
                    }
                    else
                    {
                        try
                        {

                            //if(Grid[row, column] == ShownPositions[row, column])
                            //{
                            //    sb.Append($"{Grid[row, column]}");
                            //}
                            //else if(Grid[row, column] == Solution[row, column])
                            //{
                            //    sb.Append($"{Grid[row, column]}");
                            //}
                            //else
                            //{
                            //    sb.Append(' ');
                            //}
                            sb.Append($"{Grid[row, column]}");
                        }
                        catch
                        {

                        }
                    }
                    //sb.Append($" ");
                    if(((column+1)%BlockSize) == 0 && (column +1)!=BoardSize)
                    {
                        sb.Append($" |");
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