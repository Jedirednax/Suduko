//using System.Diagnostics;
//using System.Text;

//namespace SudokuBoardLibrary
//{
//    public enum Difficulty
//    {
//        Easy,
//        Medium,
//        Hard
//    }
//    public class IntergerBoard
//    {

//        public List<Cell> cells = new List<Cell>();
//        public Random random = new Random();

//        public int BlockSize { get; set; }
//        public int BoardSize { get; set; }
//        public int NumberOfCells { get; set; }
//        public int NumberOfBlocks { get; set; }

//        public int[,] Grid;/*=
//            { { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//            };/**/

//        public int[,] Clues=
//              { { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 0,0,0,0,0,0,0,0,0},
//            };/**/
//        public int[,] Solution;/* =
//              { {  0, 8, 0, 7, 0, 9, 3, 4, 0,},
//                {  0, 0, 0, 8, 0, 0, 0, 0, 0,},
//                {  0, 0, 9, 1, 4, 0, 0, 5, 0,},
//                {  0, 9, 0, 2, 0, 8, 4, 0, 6,},
//                {  3, 2, 4, 5, 6, 7, 0, 8, 0,},
//                {  0, 0, 8, 9, 0, 4, 2, 0, 5,},
//                {  0, 0, 0, 6, 7, 5, 0, 9, 0,},
//                {  9, 5, 1, 4, 8, 2, 7, 6, 3,},
//                {  8, 0, 0, 3, 9, 1, 5, 2, 4,},
//            };/**/

//        public int[,,] Possibilities { get; set; }

//        private int[,] Easy;
//        private int[,] Medium;
//        private int[,] Hard;

//        private int MissingCellsEasy;
//        private int MissingCellsMedium;
//        private int MissingCellsHard;

//        public IntergerBoard()
//        {
//            BlockSize = 3;
//            BoardSize = BlockSize * BlockSize;
//            NumberOfBlocks = BoardSize;
//            NumberOfCells = BoardSize * BoardSize;

//            Grid = new int[BoardSize, BoardSize];
//            int index = 0;
//            for(int row = 0; row < Grid.GetLength(0); row++)
//            {
//                for(int col = 0; col < Grid.GetLength(1); col++)
//                {
//                    Grid[row, col] = Clues[row, col];
//                    if(Grid[row, col] == 0)
//                    {
//                        // Cell cl = new Cell(row, col, Grid[row, col], CellPossible(row, col));
//                        // cl.CellIndex = index++;
//                        // cells.Add(cl);

//                    }
//                }
//            }

//            //SetAToB(ref Grid, Clues);
//        }
//        public IntergerBoard(int size)
//        {
//            BlockSize = size;
//            BoardSize = BlockSize * BlockSize;
//            NumberOfBlocks = BoardSize;
//            NumberOfCells = BoardSize * BoardSize;

//            Grid = new int[BoardSize, BoardSize];

//            GenerateBoard();

//            Solution = new int[BoardSize, BoardSize];
//            SetAToB(ref Solution, Grid);
//        }
//        public IntergerBoard(int size, int clues)
//        {
//            BlockSize = size;
//            BoardSize = BlockSize * BlockSize;
//            NumberOfBlocks = BoardSize;
//            NumberOfCells = BoardSize * BoardSize;

//            Grid = new int[BoardSize, BoardSize];

//            GenerateBoard();

//            Solution = new int[BoardSize, BoardSize];
//            SetAToB(ref Solution, Grid);

//            SetBoard(clues);
//        }
//        public IntergerBoard(int[,] puzzle)
//        {
//            BlockSize = (int)Math.Sqrt(puzzle.GetLength(0));
//            BoardSize = puzzle.GetLength(0);
//            NumberOfBlocks = BoardSize;
//            NumberOfCells = BoardSize * BoardSize;

//            Grid = new int[BoardSize, BoardSize];
//            SetAToB(ref Grid, puzzle);
//            Clues = new int[BoardSize, BoardSize];
//            SetAToB(ref Clues, Grid);
//            Solution = new int[BoardSize, BoardSize];
//            int index = 0;
//            for(int row = 0; row < Grid.GetLength(0); row++)
//            {
//                for(int col = 0; col < Grid.GetLength(1); col++)
//                {
//                    Grid[row, col] = Clues[row, col];
//                    if(Grid[row, col] == 0)
//                    {
//                        //Cell cl = new Cell(row, col, Grid[row, col], CellPossible(row, col));
//                        //cl.CellIndex = index++;
//                        //cells.Add(cl);

//                    }
//                }
//            }
//        }

//        public bool SolveBoard()
//        {
//            //Console.WriteLine("Constraint:");
//            do
//            {
//            }
//            while(ConstraintSolve());
//            //Console.WriteLine("Elimination:");
//            do
//            {
//            }
//            while(EliminateSolve());

//            do
//            {
//            }
//            while(ConstraintSolve());
//            do
//            {
//            }
//            while(EliminateSolve());

//            // Diffuculty
//            return VerifyBoard();
//        }
//        public bool Insert(int row, int column, int cellValue)
//        {

//            if(row >= BoardSize ||  column >= BoardSize)
//            {
//                return false;
//            }
//            Debug.WriteLine($"({row}:{column}):{cellValue}");
//            if(Clues[row, column] == 0)
//            {
//                Debug.WriteLine($"T{Grid[row, column]}:{Clues[row, column]}");
//                Grid[row, column] = cellValue;
//                return true;
//            }
//            else
//            {
//                Debug.WriteLine($"F{Grid[row, column]}:{Clues[row, column]}");
//                return false;
//            }
//        }
//        public void Set(int row, int column, int cellValue)
//        {
//            Grid[row, column] = cellValue;

//        }
//        public int GetCell(int row, int column)
//        {
//            return Grid[row, column];
//        }

//        public bool SafeInsert(int row, int column, int cellValue)
//        {
//            if(row >= BoardSize ||  column >= BoardSize)
//            {
//                return false;
//            }
//            if(Grid[row, column] != 0)
//            {
//                return false;
//            }
//            //if(IsSafe(row, column, cellValue))
//            //{
//            Set(row, column, cellValue);
//            //}
//            //else
//            //{
//            //    return false;
//            //}
//            return true;
//        }
//        public bool SafeInsert(int row, int column, int[] cellPValue)
//        {
//            if(row >= BoardSize ||  column >= BoardSize)
//            {
//                return false;
//            }
//            if(Grid[row, column] != 0)
//            {
//                return false;
//            }

//            if(cellPValue != null && cellPValue.Length == 1)
//            {
//                int cellValue = cellPValue[0];
//                //if(IsSafe(row, column, cellValue))
//                //{
//                Set(row, column, cellValue);
//                return true;
//                //}
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public bool ConstraintSolve()
//        {
//            bool inserted = false;
//            for(int i = 0; i < BoardSize; i++)
//            {
//                for(int j = 0; j < BoardSize; j++)
//                {
//                    if(Grid[i, j] == 0)
//                    {
//                        if(SafeInsert(i, j, CellPossible(i, j)))
//                        {
//                            inserted = true;
//                        }
//                    }
//                }
//            }
//            return inserted;
//        }

//        public bool EliminateSolve()
//        {
//            bool inserted = false;
//            for(int i = 0; i < BoardSize; i++)
//            {
//                for(int j = 0; j < BoardSize; j++)
//                {
//                    if(SafeInsert(i, j, ElimRow(i, j)))
//                    {
//                        inserted = true;
//                    }
//                    else if(SafeInsert(i, j, ElimCol(i, j)))
//                    {
//                        inserted = true;
//                    }
//                    else if(SafeInsert(i, j, ElimBlock(i, j)))
//                    {
//                        inserted = true;
//                    }
//                }
//            }
//            return inserted;
//        }

//        public bool BackTrackingSolve(int row, int column)
//        {
//            if(row == BoardSize-1 && column ==BoardSize)
//            {
//                return true;
//            }
//            if(column == BoardSize)
//            {
//                row += 1;
//                column = 0;
//            }
//            if(Grid[row, column] != 0)
//            {
//                column += 1;
//                return BackTrackingSolve(row, column);
//            }

//            foreach(int cell in CellPossible(row, column))
//            {
//                if(SafeInsert(row, column, cell))
//                {
//                    if(BackTrackingSolve(row, column+1))
//                    {
//                        return true;
//                    }
//                }
//                Set(row, column, 0);
//            }

//            return false;
//        }

//        public void Reveal()
//        {
//            for(int row = 0; row < BoardSize; row++)
//            {
//                for(int column = 0; column < BoardSize; column++)
//                {
//                    Grid[row, column] = Solution[row, column];
//                }
//            }
//        }

//        public void LastCell()
//        {

//        }

//        public void LastCellRow(int row)
//        {

//        }

//        public void LastCellColumn(int column)
//        {

//        }

//        public void LastCellBlock(int row, int column)
//        {

//        }

//        public void NakedPair()
//        {

//        }
//        public void NakedPairRow(int inRow)
//        {
//            List<List<int>> posb = new List<List < int >>();
//            for(int col = 0; col < BoardSize; col++)
//            {
//                int[] h = CellPossible(inRow, col);
//                if(h != null)
//                {
//                    foreach(int cl in h)
//                    {
//                        Console.Write(cl);
//                    }
//                    Console.WriteLine();
//                    if(h.Length ==2)
//                    {
//                        posb.Add(h.ToList());
//                    }
//                }
//            }
//            List<List<int>> temp = new List<List<int>>();
//            foreach(List<int> cl in posb)
//            {

//                for(int col = 0; col < BoardSize; col++)
//                {
//                    int[] p = CellPossible(inRow, col);
//                    if(p != null)
//                    {

//                        List<int> g = p.ToList();
//                        if(posb.Contains(g))
//                        {
//                            temp.Add(g);
//                        }
//                    }
//                }
//            }
//            foreach(List<int> cl in temp)
//            {
//                Console.WriteLine();
//                foreach(int item in cl)
//                {

//                    Console.Write(item);
//                }
//            }
//        }
//        public void NakedPairCol(int inCol)
//        {
//            List<List<int>> posb = new List<List < int >>();
//            for(int col = 0; col < BoardSize; col++)
//            {
//                int[] h = CellPossible(col, inCol);
//                if(h != null)
//                {
//                    foreach(int cl in h)
//                    {
//                        Console.Write(cl);
//                    }
//                    Console.WriteLine();
//                    if(h.Length ==2)
//                    {
//                        posb.Add(h.ToList());
//                    }
//                }
//            }
//            List<List<int>> temp = new List<List<int>>();
//            foreach(List<int> cl in posb)
//            {

//                for(int col = 0; col < BoardSize; col++)
//                {
//                    int[] p = CellPossible(col,inCol);
//                    if(p != null)
//                    {

//                        List<int> g = p.ToList();
//                        if(posb.Contains(g))
//                        {
//                            temp.Add(g);
//                        }
//                    }
//                }
//            }
//            foreach(List<int> cl in temp)
//            {
//                Console.WriteLine();
//                foreach(int item in cl)
//                {

//                    Console.Write(item);
//                }
//            }
//        }
//        public void NakedPairBlock(int inRow, int inCol)
//        {

//        }

//        public void HiddenPair()
//        {

//        }

//        public void XWing()
//        {

//        }
//        public void YWing()
//        {
//        }

//        public void PointingPair()
//        {

//        }

//        public void SwordFish()
//        {

//        }

//        public List<int[]> GetRowPosible(int inRow)
//        {
//            List<int[]> ints = new List<int[]>();
//            for(int col = 0; col < BoardSize; col++)
//            {
//                int[] h = CellPossible(inRow, col);
//                if(h != null)
//                {

//                    ints.Add(h);
//                }
//            }
//            return ints;
//        }
//        public List<int[]> GetColumnPosible(int inCol)
//        {
//            List<int[]> ints = new List<int[]>();
//            for(int row = 0; row < BoardSize; row++)
//            {
//                int[] h = CellPossible(row, inCol);
//                if(h != null)
//                {
//                    ints.Add(h);
//                }
//            }
//            return ints;
//        }
//        public List<int[]> GetBlockPosible(int inRow, int inCol)
//        {
//            List<int[]> ints = new List<int[]>();

//            int rowBlock = inRow/BlockSize*BlockSize;
//            int columnBlock = inCol/BlockSize*BlockSize;

//            for(int i = rowBlock; i < rowBlock + BlockSize; i++)
//            {
//                for(int j = columnBlock; j < columnBlock + BlockSize; j++)
//                {

//                    int[] h = CellPossible(i, j);
//                    if(h != null)
//                    {

//                        ints.Add(h);
//                    }
//                }
//            }
//            return ints;
//        }
//        public int[] ElimRow(int row, int currCol)
//        {

//            int[] currRow = CellPossible(row, currCol);
//            List<int> hold = new List<int>();
//            for(int i = 0; i < BoardSize; i++)
//            {
//                if(i != currCol)
//                {

//                    int[] f = CellPossible(row, i);
//                    if(f!=null)
//                    {
//                        hold.AddRange(f);
//                    }
//                }
//            }
//            List<int> res = new List<int>();
//            if(currRow!=null)
//            {
//                foreach(int i in currRow)
//                {
//                    if(!hold.Contains(i))
//                    {
//                        res.Add(i);
//                    }
//                }
//            }
//            return res.ToArray();

//        }

//        public int[] ElimCol(int currRow, int col)
//        {

//            int[] currCol = CellPossible(currRow, col);
//            List<int> hold = new List<int>();
//            for(int i = 0; i < BoardSize; i++)
//            {
//                if(i != currRow)
//                {

//                    int[] f = CellPossible(i,col);
//                    if(f!=null)
//                    {
//                        hold.AddRange(f);
//                    }
//                }
//            }
//            List<int> res = new List<int>();
//            if(currCol != null)
//            {
//                foreach(int i in currCol)
//                {
//                    if(!hold.Contains(i))
//                    {
//                        res.Add(i);
//                    }
//                }
//            }
//            return res.ToArray();
//        }

//        public int[] ElimBlock(int row, int col)
//        {

//            int[] currBlk = CellPossible(row, col);
//            List<int> hold = new List<int>();

//            int rowBlock = row/BlockSize*BlockSize;
//            int columnBlock = col/BlockSize*BlockSize;

//            for(int i = rowBlock; i < rowBlock + BlockSize; i++)
//            {
//                for(int j = columnBlock; j < columnBlock + BlockSize; j++)
//                {
//                    if(!(i == row && j== col))
//                    {

//                        int[] f = CellPossible(i,j);
//                        if(f!=null)
//                        {
//                            hold.AddRange(f);
//                        }
//                    }
//                }
//            }
//            List<int> res = new List<int>();
//            if(currBlk != null)
//            {
//                foreach(int g in currBlk)
//                {
//                    if(!hold.Contains(g))
//                    {
//                        res.Add(g);
//                    }
//                }
//            }
//            return res.ToArray();

//        }

//        public int[] GetRow(int row)
//        {
//            List<int> res = new List<int>();

//            for(int column = 0; column < BoardSize; column++)
//            {
//                int cell = Grid[row, column];
//                if(cell != default)
//                {
//                    res.Add(Grid[row, column]);
//                }
//            }

//            return [.. res];
//        }

//        public int[] GetColumn(int column)
//        {
//            List<int> res = new List<int>();

//            for(int row = 0; row < BoardSize; row++)
//            {
//                int cell = Grid[row, column];
//                if(cell != 0)
//                {
//                    res.Add(Grid[row, column]);
//                }
//            }
//            return [.. res];
//        }

//        public int[] GetBlock(int row, int column)
//        {
//            List<int> res = new List<int>();

//            int rowBlock = row/BlockSize*BlockSize;
//            int columnBlock = column/BlockSize*BlockSize;

//            for(int i = rowBlock; i < rowBlock + BlockSize; i++)
//            {
//                for(int j = columnBlock; j < columnBlock + BlockSize; j++)
//                {
//                    int cell = Grid[i, j];
//                    if(cell != 0)
//                    {
//                        res.Add(cell);
//                    }
//                }
//            }
//            return [.. res];
//        }

//        public int[] CellPossible(int row, int column)
//        {
//            if(row >= BoardSize ||  column >= BoardSize)
//            {
//                return null;
//            }
//            if(Grid[row, column] == 0)
//            {
//                List<int> possible = new List<int>();

//                for(int i = 1; i <= BoardSize; i++)
//                {
//                    if(IsSafe(row, column, i))
//                    {
//                        possible.Add(i);
//                    }
//                }
//                //if(!possible.Any())
//                //{
//                //    throw new NoValuesPossibleException("No values can be safly inserted in to this cell.");
//                //}
//                return possible.ToArray();
//            }
//            return null;
//        }

//        public bool CheckRow(int row, int cellValue)
//        {
//            int[] cells = GetRow(row);
//            if(cells.Length != BoardSize)
//            {
//                return !cells.Contains(cellValue);
//            }
//            else
//            {
//                return cells.Where(y => y == cellValue).Count()==1;
//            }
//        }

//        public bool CheckColumn(int column, int cellValue)
//        {
//            int[] cells = GetColumn(column);
//            if(cells.Length !=BoardSize)
//            {
//                return !cells.Contains(cellValue);
//            }
//            else
//            {
//                return cells.Where(y => y == cellValue).Count()==1;
//            }
//        }

//        public bool CheckBlock(int row, int column, int cellValue)
//        {
//            int[] cells = GetBlock(row,column);
//            if(cells.Length != BoardSize)
//            {
//                return !cells.Contains(cellValue);
//            }
//            else
//            {
//                return cells.Where(y => y == cellValue).Count()==1;
//            }
//        }
//        public bool IsSafe(int row, int column, int cellValue)
//        {
//            //bool validRow = CheckRow(row, cellValue);
//            //bool validColumn = CheckColumn(column, cellValue);
//            //bool validBlock = CheckBlock(row, column, cellValue);
//            //bool result =  validRow && validColumn && validBlock && true;
//            //return result;
//            if(!CheckRow(row, cellValue))
//            {
//                return false;
//            }
//            if(!CheckColumn(column, cellValue))
//            {
//                return false;
//            }
//            if(!CheckBlock(row, column, cellValue))
//            {
//                return false;
//            }
//            return true;
//        }
//        public bool CheckBoard()
//        {
//            for(int row = 0; row < BoardSize; row++)
//            {
//                for(int column = 0; column < BoardSize; column++)
//                {
//                    int y = Grid[row, column];
//                    if(Clues[row, column] == 0)
//                    {
//                        return !IsSafe(row, column, y);
//                    }
//                }
//            }
//            return true;
//        }

//        public bool CompareBoard()
//        {
//            for(int row = 0; row < BoardSize; row++)
//            {
//                for(int column = 0; column < BoardSize; column++)
//                {
//                    if(Grid[row, column] != Solution[row, column])
//                    {
//                        return false;
//                    }
//                }
//            }
//            return true;
//        }

//        public void GenerateBoard()
//        {
//            int[] values = new int[BoardSize];

//            for(int i = 1; i <= BoardSize; i++)
//            {
//                values[i-1] = i;
//            }
//            for(int row = 0; row < BlockSize; row++)
//            {
//                int rowBlock = row*3/BlockSize*BlockSize;
//                int columnBlock = row*3/BlockSize*BlockSize;
//                int blockIndex = 0;
//                random.Shuffle(values);
//                for(int i = rowBlock; i < rowBlock + BlockSize; i++)
//                {
//                    for(int j = columnBlock; j < columnBlock + BlockSize; j++)
//                    {
//                        Set(i, j, values[blockIndex++]);
//                    }
//                }
//            }
//            Solution = new int[BoardSize, BoardSize];
//            SetAToB(ref Solution, Grid);

//            Clues = new int[BoardSize, BoardSize];
//            SetAToB(ref Clues, Grid);
//            SolveBoard();
//            BackTrackingSolve(0, 0);

//        }

//        public bool VerifyBoard()
//        {
//            int[] vals = new int[BoardSize];
//            for(int i = 1; i <= BoardSize; i++)
//            {
//                vals[i-1] = i;
//            }
//            for(int row = 0; row < Grid.GetLength(0); row++)
//            {
//                if(!GetRow(row).Order().SequenceEqual(vals))
//                {
//                    return false;
//                }
//                if(!GetColumn(row).Order().SequenceEqual(vals))
//                {
//                    return false;
//                }
//            }

//            for(int row = 0; row < BlockSize; row++)
//            {
//                for(int col = 0; col < BlockSize; col++)
//                {
//                    if(!GetBlock(row*BlockSize, col*BlockSize).Order().SequenceEqual(vals))
//                    {
//                        return false;
//                    }
//                }
//            }

//            return true;
//        }

//        public void SetBoard(int clues)
//        {
//            List<(int,int)> clue = new List<(int, int)> ();
//            Clues = new int[BoardSize, BoardSize];

//            for(int rowF = 0; rowF < NumberOfCells -clues;)
//            {
//                int x = random.Next(BoardSize);
//                int y = random.Next(BoardSize);
//                if(Grid[x, y] != 0)
//                {
//                    Grid[x, y] = 0;
//                    clue.Add((x, y));

//                    if(SolveBoard())
//                    {
//                        foreach((int, int) pos in clue)
//                        {
//                            Grid[pos.Item1, pos.Item2] = 0;
//                        }
//                        rowF++;
//                    }
//                    else
//                    {
//                        clue.Remove((x, y));
//                    }
//                }
//            }
//            SetAToB(ref Clues, Grid);
//        }

//        public int[] FillValues()
//        {
//            int[] values = new int[BoardSize];
//            for(int index = 1; index <= BoardSize; index++)
//            {
//                values[index-1] = index;
//            }
//            return values;
//        }
//        public void InitilizeGrid(ref int[,] board)
//        {
//            for(int row = 0; row < Grid.GetLength(0); row++)
//            {
//                for(int col = 0; col < Grid.GetLength(1); col++)
//                {
//                    board[row, col] = 0;
//                }
//            }
//        }

//        /// <summary>
//        /// This method takes Two board amd Sets teh values from pne to the other.
//        /// </summary>
//        /// <param name="to"> BoardGrid being set </param>
//        /// <param name="from"> Board/Grid values being taken </param>
//        public static void SetAToB(ref int[,] to, int[,] from)
//        {
//            for(int row = 0; row < to.GetLength(0); row++)
//            {
//                for(int col = 0; col < to.GetLength(1); col++)
//                {
//                    to[row, col] = from[row, col];
//                }
//            }
//        }

//        public override string ToString()
//        {
//            StringBuilder sb = new StringBuilder();
//            string rowSep = "+";
//            for(int i = 0; i <= BoardSize; i++)
//            {
//                rowSep += "--";
//            }

//            for(int row = 0; row < BoardSize; row++)
//            {
//                sb.AppendLine();
//                if(row%BlockSize == 0 && row != 0)
//                {
//                    sb.Append($"{rowSep}\n");
//                }

//                for(int column = 0; column < BoardSize; column++)
//                {
//                    sb.Append($" ");
//                    if(Grid[row, column] == 0)
//                    {
//                        sb.Append($" ");
//                    }
//                    else
//                    {

//                        //if(Grid[row, column] == Clues[row, column])
//                        //{
//                        //    sb.Append($"{Grid[row, column]}");
//                        //}
//                        //else if(Grid[row, column] == Solution[row, column])
//                        //{
//                        //    sb.Append($"{Grid[row, column]}");
//                        //}
//                        //else
//                        //{
//                        //    sb.Append(' ');
//                        //}
//                        sb.Append($"{Grid[row, column]}");

//                    }
//                    //sb.Append($" ");
//                    if((column+1)%BlockSize == 0 && column +1!=BoardSize)
//                    {
//                        sb.Append($" |");
//                    }
//                }
//            }
//            sb.AppendLine();
//            return sb.ToString();
//        }

//        public string Compact()
//        {
//            StringBuilder sb = new StringBuilder();

//            for(int row = 0; row < BoardSize; row++)
//            {
//                sb.AppendLine();
//                for(int column = 0; column < BoardSize; column++)
//                {
//                    sb.Append($" ");
//                    if(Grid[row, column] == 0)
//                    {
//                        sb.Append($"0");
//                    }
//                    else
//                    {
//                        sb.Append($"{Grid[row, column]}");
//                    }
//                    sb.Append(",");
//                }
//            }
//            sb.AppendLine();
//            return sb.ToString();
//        }
//        public string RowSep()
//        {
//            StringBuilder sb = new StringBuilder("+");

//            for(int i = 0; i < BlockSize; i++)
//            {
//                for(int j = 0; j < BlockSize; j++)
//                {
//                    sb.Append("--");
//                }
//                sb.Append("-+");
//            }

//            return sb.ToString();
//        }
//    }
//}