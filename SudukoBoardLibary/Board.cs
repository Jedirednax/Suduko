using System.Text;

namespace SudokuBoardLibrary
{
    public class Board
    {
        #region Properties
        public int BoardSize = 9;
        public int BlockSize = 3;
        public int NumberOfCells { get; set; }
        public int NumberOfBlocks { get; set; }
        private int[,] BlockIndex { get; set; }
        public Cell[,] Grid { get; set; }
        public List<Cell> openCells = new List<Cell>();

        public int[] digitCount = new int[9];
        #endregion
        //public Cell this[int row, int col]
        //{
        //    get
        //    {
        //        return Grid[row, col];
        //    }
        //}
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public Board(int size)
        {
            BlockSize = size;
            BoardSize = BlockSize*BlockSize;
            BlockIndex = SetCellBlock(BlockSize);
            Grid = new Cell[BoardSize, BoardSize];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        public Board(int[,] board)
        {
            BoardSize = board.GetLength(0);
            BlockSize = (int)Math.Sqrt(board.GetLength(0));
            BlockIndex = SetCellBlock(BlockSize);
            Grid = new Cell[BoardSize, BoardSize];

            for(int row = 0; row < board.GetLength(0); row++)
            {
                for(int col = 0; col < board.GetLength(1); col++)
                {
                    int cell = board[row, col];

                    Cell nCell = new Cell(row, col, cell);
                    if(cell == 0)
                    {
                        openCells.Add(nCell);
                    }
                    int blockPos = GetBlockPos(row, col);
                    nCell.CellBlock = blockPos;
                    Grid[row, col] = nCell;
                }
            }

            for(int row = 0; row < board.GetLength(0); row++)
            {
                for(int col = 0; col < board.GetLength(1); col++)
                {
                    Grid[row, col].CellPossibilities = GetCellPossibilitiesCalculation(row, col);
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        public Board(Cell[,] board)
        {
            BoardSize = board.GetLength(0);
            BlockSize = (int)Math.Sqrt(board.GetLength(0));
            BlockIndex = SetCellBlock(BlockSize);
            Grid = new Cell[BoardSize, BoardSize];

            for(int row = 0; row < board.GetLength(0); row++)
            {
                for(int col = 0; col < board.GetLength(1); col++)
                {
                    int cell = board[row, col].CellValue;

                    Cell nCell = new Cell(row, col, cell);
                    if(cell == 0)
                    {
                        openCells.Add(nCell);
                    }
                    int blockPos = GetBlockPos(row, col);
                    nCell.CellBlock = blockPos;
                    Grid[row, col] = nCell;
                }
            }
            for(int row = 0; row < board.GetLength(0); row++)
            {
                for(int col = 0; col < board.GetLength(1); col++)
                {
                    Grid[row, col].SetPossibilities(GetCellPossibilitiesCalculation(row, col));
                }
            }
        }
        #endregion

        /// <summary>
        /// Sets the values of the cell in the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to set the cell. </param>
        public bool Set(int inRow, int inColumn, int cellValue)
        {
            if(cellValue == 0)
            {
                openCells.Add(Grid[inRow, inColumn]);
            }
            else
            {
                openCells.Remove(Grid[inRow, inColumn]);
            }
            Grid[inRow, inColumn].Set(cellValue);
            UpdatePossibilities(inRow, inColumn, cellValue);
            return true;
        }

        /// <summary>
        /// Sets the values of the cell in the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to set the cell. </param>
        public bool Attempt(int inRow, int inColumn, int cellValue)
        {

            if(cellValue == 0)
            {
                openCells.Add(Grid[inRow, inColumn]);
            }
            else
            {
                openCells.Remove(Grid[inRow, inColumn]);
            }
            if(!Grid[inRow, inColumn].IsGiven)
            {
                Grid[inRow, inColumn].Set(cellValue);
                UpdatePossibilities(inRow, inColumn, cellValue);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets the values of the cell in the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to set the cell. </param>
        public void SetGiven(int inRow, int inColumn)
        {
            Grid[inRow, inColumn].SetGiven();

            openCells.Remove(Grid[inRow, inColumn]);
            //UpdatePossibilities(inRow, inColumn, Grid[inRow, inColumn].CellSolution);
        }

        /// <summary>
        /// Sets the values of the cell in the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to set the cell. </param>
        public void SetOpen(int inRow, int inColumn)
        {
            //var old = Grid[inRow, inColumn];
            //var f = old.CellValue;
            Grid[inRow, inColumn].SetOpen();
            openCells.Add(Grid[inRow, inColumn]);
            //UpdatePossibilities(inRow, inColumn, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetValidSolution()
        {
            if(VerifyBoard())
            {
                for(int row = 0; row < Grid.GetLength(0); row++)
                {
                    for(int col = 0; col < Grid.GetLength(1); col++)
                    {
                        Grid[row, col].SetSolution();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetBoard()
        {
            openCells = new List<Cell>();
            for(int row = 0; row < Grid.GetLength(0); row++)
            {
                for(int col = 0; col < Grid.GetLength(1); col++)
                {
                    if(!Grid[row, col].IsGiven)
                    {
                        Grid[row, col].Set(0);
                        openCells.Add(Grid[row, col]);
                    }
                }
            }
            foreach(var cell in openCells)
            {
                Grid[cell.CellRow, cell.CellColumn].SetPossibilities(
                    GetCellPossibilitiesCalculation(cell.CellRow, cell.CellColumn));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetFinal()
        {
            if(VerifyBoard())
            {
                for(int row = 0; row < Grid.GetLength(0); row++)
                {
                    for(int col = 0; col < Grid.GetLength(1); col++)
                    {
                        Grid[row, col].SetGiven();
                    }
                }
            }
        }

        /// <summary>
        /// Inserts a single value with checks to make sure it is within the board
        /// and not overwriting a clue cell.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to set the cell. </param>
        /// <returns> Return if the cells values was set. </returns>
        public bool SafeInsert(int inRow, int inColumn, int cellValue)
        {
            if(inRow >= BoardSize ||  inColumn >= BoardSize)
            {
                return false;
            }
            if(cellValue>BoardSize || cellValue<0)
            {
                return false;
            }
            if(GetCell(inRow, inColumn).IsPopulated)
            {
                return false;
            }

            Set(inRow, inColumn, cellValue);

            return true;
        }

        /// <summary>
        /// Inserts a single value with checks to make sure it is within the board,
        /// and not overwriting a clue cell.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellPValue"> Value to set the cell. check as a list. </param>
        /// <returns> Return if the cells values was set. </returns>
        public bool SafeInsert(int inRow, int inColumn, List<int>? cellPValue)
        {
            if(cellPValue!=null)
            {
                if(inRow >= BoardSize ||  inColumn >= BoardSize)
                {
                    return false;
                }

                if(GetCell(inRow, inColumn).IsPopulated)
                {
                    return false;
                }

                if(cellPValue != null && cellPValue.Count == 1)
                {
                    int cellValue = cellPValue[0];
                    if(cellValue>BoardSize || cellValue<0)
                    {
                        return false;
                    }
                    Set(inRow, inColumn, cellValue);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a smaller grid to represent the block number in a Grid.
        /// </summary>
        /// <param name="size"> The size of the blocks of the Grid. </param>
        /// <returns> Returns a populated map.</returns>
        public static int[,] SetCellBlock(int size)
        {
            int[,] blockIndex = new int[size, size];
            int Block = 0;
            for(int row = 0; row < size; row++)
            {
                for(int col = 0; col < size; col++)
                {
                    blockIndex[row, col] = Block;
                    Block++;
                }
            }
            return blockIndex;
        }

        /// <summary>
        /// Gets the values of the selected cell in the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <returns> Returns the integer value of the selected cell. </returns>
        public Cell GetCell(int inRow, int inColumn)
        {
            if(inRow >= BoardSize ||  inColumn >= BoardSize)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            return Grid[inRow, inColumn];
        }


        /// <summary>
        /// Gets the block number from values.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <returns> Returns the block number. </returns>
        public int GetBlockPos(int inRow, int inColumn)
        {
            int rowBlock = inRow/BlockSize;
            int columnBlock = inColumn/BlockSize;
            return BlockIndex[rowBlock, columnBlock];
        }

        #region CellPosibilities
        /// <summary>
        /// Calculates the cells possibilities.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of the values that can be safely inserted
        /// in to the cell. </returns>
        public List<int> GetCellPossibilitiesCalculation(int inRow, int inColumn)
        {
            if(inRow >= BoardSize ||  inColumn >= BoardSize)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }

            if(!GetCell(inRow, inColumn).IsPopulated)
            {
                List<int> possible = new List<int>();

                for(int i = 1; i <= BoardSize; i++)
                {
                    if(IsSafe(inRow, inColumn, i))
                    {
                        possible.Add(i);
                    }
                }
                return possible;
            }
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        /// <summary>
        /// Gets the possibilities associated with a cell.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of the values that can be safely inserted
        /// in to the cell. </returns>
        public List<int>? GetCellPossibilities(int inRow, int inColumn)
        {
            if(inRow >= BoardSize ||  inColumn >= BoardSize)
            {
                return null;
            }

            var cell = Grid[inRow, inColumn];
            if(!cell.IsPopulated)
            {

                return cell.CellPossibilities;
            }
            return null;
        }

        /// <summary>
        /// Gets all the possible values for each open cell in the selected inRow of the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <returns> Returns a list of the values that can be safely inserted
        /// in to the column's open cells. </returns>
        public List<int> GetRowPossibilities(int inRow)
        {
            List<int> possibilities = new List< int >();
            for(int col = 0; col < BoardSize; col++)
            {
                List<int>? possibleCells = GetCellPossibilities(inRow, col);
                if(possibleCells != null)
                {
                    possibilities.AddRange(possibleCells);
                }
            }
            return possibilities;
        }

        /// <summary>
        /// Gets all the possible values for each open cell in the selected column of the grid.
        /// </summary>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of the values that can be safely
        /// inserted in to the column's open cells. </returns>
        public List<int> GetColumnPossibilities(int inColumn)
        {
            List<int> possibilities = new List<int>();
            for(int row = 0; row < BoardSize; row++)
            {
                List<int>? possibleCells = GetCellPossibilities(row, inColumn);
                if(possibleCells != null)
                {
                    possibilities.AddRange(possibleCells);
                }
            }
            return possibilities;
        }

        /// <summary>
        /// Gets all the possible values for each open cell in the selected block of the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of the values that can be safely inserted
        /// in to the block's open cells. </returns>
        public List<int> GetBlockPossibilities(int inRow, int inColumn)
        {
            List<int> possibilities = new List<int>();

            int rowBlock = inRow/BlockSize*BlockSize;
            int columnBlock = inColumn/BlockSize*BlockSize;

            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
            {
                for(int innerColumn = columnBlock;
                    innerColumn < columnBlock + BlockSize; innerColumn++)
                {
                    List<int>? possibleCells = GetCellPossibilities(outerRow, innerColumn);
                    if(possibleCells != null)
                    {
                        possibilities.AddRange(possibleCells);
                    }
                }
            }
            return possibilities;
        }

        /// <summary>
        /// Updates the cells possibilities in the row,
        /// column and block, by removing it.
        /// <see cref="UpdateRowPossibilities(int, int)"/>
        /// <see cref="UpdateColumnPossibilities(int, int)"/>
        /// <see cref="UpdateBlockPossibilities(int, int, int)"/>
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to remove from th cells. </param>
        /// <returns> Returns hte total number of cells updated. </returns>
        public void UpdatePossibilities(int inRow, int inColumn, int cellValue)
        {
            UpdateRowPossibilities(inRow, cellValue);
            UpdateColumnPossibilities(inColumn, cellValue);
            UpdateBlockPossibilities(inRow, inColumn, cellValue);
            //return total;
        }

        /// <summary>
        /// Updates the cells possibilities in the row by removing it.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="cellValue"> Value to remove from th cells. </param>
        /// <returns> Returns the total number of cells updated. </returns>
        public int UpdateRowPossibilities(int inRow, int cellValue)
        {
            int numUpdated = 0;
            if(cellValue != 0)
            {
                //int numUpdated = 0;
                for(int col = 0; col < BoardSize; col++)
                {
                    if(Grid[inRow, col].RemovePossibilities(cellValue))
                    {
                        numUpdated++;
                    }
                }
            }
            else
            {
                for(int col = 0; col < BoardSize; col++)
                {
                    Grid[inRow, col].SetPossibilities(cellValue);
                }
            }
            return numUpdated;
        }

        /// <summary>
        /// Updates the cells possibilities in the column by removing it.
        /// </summary>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to remove from th cells. </param>
        /// <returns> Returns the total number of cells updated. </returns>
        public int UpdateColumnPossibilities(int inColumn, int cellValue)
        {
            int numUpdated = 0;
            if(cellValue != 0)
            {
                for(int row = 0; row <BoardSize; row++)
                {
                    if(Grid[row, inColumn].RemovePossibilities(cellValue))
                    {
                        numUpdated++;
                    }
                }
            }
            else
            {
                for(int row = 0; row <BoardSize; row++)
                {
                    Grid[row, inColumn].SetPossibilities(cellValue);
                }
            }
            return numUpdated;
        }

        /// <summary>
        /// Updates the cells possibilities in the block, by removing it.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to remove from th cells. </param>
        /// <returns> Returns the total number of cells updated. </returns>
        public int UpdateBlockPossibilities(int inRow, int inColumn, int cellValue)
        {
            int numUpdated = 0;
            int rowBlock = inRow/BlockSize*BlockSize;
            int columnBlock = inColumn/BlockSize*BlockSize;
            if(cellValue != 0)
            {
                for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
                {
                    for(int innerColumn = columnBlock;
                        innerColumn < columnBlock + BlockSize; innerColumn++)
                    {
                        if(Grid[outerRow, innerColumn].RemovePossibilities(cellValue))
                        {
                            numUpdated++;
                        }
                    }
                }
            }
            else
            {
                for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
                {
                    for(int innerColumn = columnBlock;
                        innerColumn < columnBlock + BlockSize; innerColumn++)
                    {
                        Grid[outerRow, innerColumn].SetPossibilities(cellValue);
                    }
                }
            }
            return numUpdated;
        }
        #endregion


        #region GetUnpopulated
        /// <summary>
        /// Gets all the Given and Populated cells <see cref="Cell"/> in the Row.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <returns> Returns all the Given and Populated cells <see cref="Cell"/>. </returns>
        public List<Cell> GetUnPopulatedRowCells(int inRow)
        {
            List<Cell> res = new List<Cell>();

            for(int innerColumn = 0; innerColumn < BoardSize; innerColumn++)
            {
                Cell cell = Grid[inRow, innerColumn];
                if(!cell.IsPopulated)
                {
                    res.Add(cell);
                }
            }

            return res;
        }

        /// <summary>
        /// Gets the selected column's values from the grid.
        /// </summary>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns all the know values in the selected column. </returns>
        public List<Cell> GetUnPopulatedColumnCells(int inColumn)
        {
            List<Cell> res = new List<Cell>();

            for(int innerRow = 0; innerRow < BoardSize; innerRow++)
            {
                Cell cell = Grid[innerRow, inColumn];
                if(!cell.IsPopulated)
                {
                    res.Add(cell);
                }
            }
            return res;
        }

        /// <summary>
        /// Gets the selected block's values from the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns all the know values in the selected block. </returns>
        public List<Cell> GetUnPopulatedBlockCells(int inRow, int inColumn)
        {
            List<Cell> res = new List<Cell>();

            int rowBlock = inRow/BlockSize*BlockSize;
            int columnBlock = inColumn/BlockSize*BlockSize;

            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
            {
                for(int innerColumn = columnBlock;
                    innerColumn < columnBlock + BlockSize; innerColumn++)
                {
                    Cell cell = Grid[outerRow, innerColumn];
                    if(!cell.IsPopulated)
                    {
                        res.Add(cell);
                    }
                }
            }
            return res;
        }
        #endregion
        #region GetPopulated
        /// <summary>
        /// Gets all the Given and Populated cells <see cref="Cell"/> in the Row.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <returns> Returns all the Given and Populated cells <see cref="Cell"/>. </returns>
        public List<Cell> GetPopulatedRowCells(int inRow)
        {
            List<Cell> res = new List<Cell>();

            for(int innerColumn = 0; innerColumn < BoardSize; innerColumn++)
            {
                Cell cell = Grid[inRow, innerColumn];
                if(cell.IsPopulated)
                {
                    res.Add(cell);
                }
            }

            return res;
        }

        /// <summary>
        /// Gets the selected column's values from the grid.
        /// </summary>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns all the know values in the selected column. </returns>
        public List<Cell> GetPopulatedColumnCells(int inColumn)
        {
            List<Cell> res = new List<Cell>();

            for(int innerRow = 0; innerRow < BoardSize; innerRow++)
            {
                Cell cell = Grid[innerRow, inColumn];
                if(cell.IsPopulated)
                {
                    res.Add(Grid[innerRow, inColumn]);
                }
            }
            return res;
        }

        /// <summary>
        /// Gets the selected block's values from the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns all the know values in the selected block. </returns>
        public List<Cell> GetPopulatedBlockCells(int inRow, int inColumn)
        {
            List<Cell> res = new List<Cell>();

            int rowBlock = inRow/BlockSize*BlockSize;
            int columnBlock = inColumn/BlockSize*BlockSize;

            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
            {
                for(int innerColumn = columnBlock;
                    innerColumn < columnBlock + BlockSize; innerColumn++)
                {
                    Cell cell = Grid[outerRow, innerColumn];
                    if(cell.IsPopulated)
                    {
                        res.Add(cell);
                    }
                }
            }
            return res;
        }
        #endregion
        #region GetFull
        /// <summary>
        /// Gets all thecells <see cref="Cell"/> Populated Or Not in the Row.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <returns> Returns all the Given and Populated cells <see cref="Cell"/>. </returns>
        public List<Cell> GetRowCells(int inRow)
        {
            List<Cell> res = new List<Cell>();

            for(int innerColumn = 0; innerColumn < BoardSize; innerColumn++)
            {
                res.Add(Grid[inRow, innerColumn]);
            }

            return res;
        }

        /// <summary>
        /// Gets the selected column's values from the grid.
        /// </summary>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns all the know values in the selected column. </returns>
        public List<Cell> GetColumnCells(int inColumn)
        {
            List<Cell> res = new List<Cell>();

            for(int innerRow = 0; innerRow < BoardSize; innerRow++)
            {
                res.Add(Grid[innerRow, inColumn]);
            }
            return res;
        }

        /// <summary>
        /// Gets the selected block's values from the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns all the know values in the selected block. </returns>
        public List<Cell> GetBlockCells(int inRow, int inColumn)
        {
            List<Cell> res = new List<Cell>();

            int rowBlock = inRow/BlockSize*BlockSize;
            int columnBlock = inColumn/BlockSize*BlockSize;

            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
            {
                for(int innerColumn = columnBlock;
                    innerColumn < columnBlock + BlockSize; innerColumn++)
                {
                    res.Add(Grid[outerRow, innerColumn]);
                }
            }
            return res;
        }
        #endregion

        #region GetSameValues

        #endregion
        #region CheckConstrains

        /// <summary>
        /// Checks if the selected inRow contains the given value.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="cellValue"> Value to check in Targeted/Selected inRow. </param>
        /// <returns> Returns true if not present, returns false if present. </returns>
        public bool CheckRowContains(int inRow, int cellValue)
        {

            List<Cell> cell = GetPopulatedRowCells(inRow);
            List<int> cells = new List<int>();
            foreach(Cell cellCell in cell)
            {
                cells.Add(cellCell.CellValue);
            }
            if(cells.Count != BoardSize)
            {
                return !cells.Contains(cellValue);
            }
            else
            {
                return cells.Where(y => y == cellValue).Count()==1;
            }
        }

        /// <summary>
        /// Checks if the selected column contains the given value.
        /// </summary>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <param name="cellValue"> Value to check in Targeted/Selected column. </param>
        /// <returns> Returns true if not present, returns false if present. </returns>
        public bool CheckColumnContains(int inColumn, int cellValue)
        {
            List<Cell> cell = GetPopulatedColumnCells(inColumn);
            List<int> cells = new List<int>();
            foreach(Cell cellCell in cell)
            {
                cells.Add(cellCell.CellValue);
            }
            if(cells.Count !=BoardSize)
            {
                return !cells.Contains(cellValue);
            }
            else
            {
                return cells.Where(y => y == cellValue).Count()==1;
            }
        }

        /// <summary>
        /// Checks if the selected block contains the given value.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <param name="cellValue"> Value to check in Selected block. </param>
        /// <returns> Returns true if not present, returns false if present. </returns>
        public bool CheckBlockContains(int inRow, int inColumn, int cellValue)
        {
            List<Cell> cell = GetPopulatedBlockCells(inRow,inColumn);
            List<int> cells = new List<int>();
            foreach(Cell cellCell in cell)
            {
                cells.Add(cellCell.CellValue);
            }
            if(cells.Count != BoardSize)
            {
                return !cells.Contains(cellValue);
            }
            else
            {
                return cells.Where(y => y == cellValue).Count()==1;
            }
        }

        /// <summary>
        /// Check the inRow, column and block, for the selected value.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <param name="cellValue"> Value to check against all constraints. </param>
        /// <returns> Returns true if not present, returns false if present. </returns>
        public bool IsSafe(int inRow, int inColumn, int cellValue)
        {
            if(!CheckRowContains(inRow, cellValue))
            {
                return false;
            }
            if(!CheckColumnContains(inColumn, cellValue))
            {
                return false;
            }
            if(!CheckBlockContains(inRow, inColumn, cellValue))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check the inRow, column and block, for the selected value.
        /// </summary>
        /// <param name="cell"> Cell to chick in grid. </param>
        /// <returns> Returns true if not present, returns false if present. </returns>
        public bool IsSafe(Cell cell)
        {
            //Debug.Assert(cell.CellValue!=0);
            if(cell.CellValue==0)
            {
                return false;
            }
            if(!CheckRowContains(cell.CellRow, cell.CellValue))
            {
                return false;
            }
            if(!CheckColumnContains(cell.CellColumn, cell.CellValue))
            {
                return false;
            }
            if(!CheckBlockContains(cell.CellRow, cell.CellColumn, cell.CellValue))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks that all the cells in the board meet the required constraints.
        /// </summary>
        /// <returns></returns>
        public bool VerifyBoard()
        {
            foreach(Cell cell in Grid)
            {
                if(!IsSafe(cell))
                {
                    //Debug.WriteLine(cell.PrintDebug());
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region Displays
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        public void ColourBoardDisplay()
        {
            for(int row = 0; row < BoardSize; row++)
            {
                if(row%BlockSize == 0)
                {
                    Console.Write($"{RowSep(BlockSize*2)}\n|");
                }
                else
                {
                    Console.Write($"|");
                }
                Console.WriteLine();
                for(int column = 0; column < BoardSize; column++)
                {
                    Cell cell = Grid[row, column];
                    Console.Write($"|");
                    if(cell.IsGiven)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        if(cell.IsPopulated)
                        {
                            if(cell.IsCorrect())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                    }
                    Console.Write($"{cell.ToString(),-4}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if(((column+1)%BlockSize) == 0)
                    {
                        Console.Write($"|");
                    }
                }
                Console.Write($"|");
                Console.WriteLine();

            }
            Console.WriteLine($"\n{RowSep(BlockSize)}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToArray()
        {
            StringBuilder sb = new StringBuilder();

            for(int row = 0; row < BoardSize; row++)
            {
                sb.AppendLine();
                sb.Append('{');
                for(int column = 0; column < BoardSize; column++)
                {
                    sb.Append($"{GetCell(row, column).CellValue},");
                }
                sb.Append("},");

            }
            sb.AppendLine();
            return sb.ToString();
        }
        #endregion
    }
}