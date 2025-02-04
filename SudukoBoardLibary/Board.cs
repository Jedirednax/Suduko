namespace SudukoBoardLibary
{
    public class Board
    {
        public int BoardSize = 9;
        public int BlockSize = 3;
        public int NumberOfCells { get; set; }
        public int NumberOfBlocks { get; set; }
        private int[,] BlockIndex;
        public Cell[,] Grid;
        public List<Cell> openCells = new List<Cell>();

        public Board(int size)
        {

        }
        //public Cell this[int row, int col]
        //{
        //    get
        //    {
        //        return Grid[row, col];
        //    }
        //}

        public Board(int[,] board)
        {
            SetBlock(3);
            BoardSize = board.GetLength(0);
            Grid = new Cell[BoardSize, BoardSize];

            BlockSize = (int)Math.Sqrt(board.GetLength(0));
            BlockIndex = new int[BlockSize, BlockSize];

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
            /*
            for(int row = 0; row < board.GetLength(0); row++)
            {
                for(int col = 0; col < board.GetLength(1); col++)
                {
                    Grid[row, col].CellPossibilities = GetCellPossibilitiesPop(row, col);
                }
            }
            */
        }
        public Board(Cell[,] board)
        {

        }

        public void SetBlock(int size)
        {
            BlockIndex = new int[BlockSize, BlockSize];
            int Block = 0;
            for(int row = 0; row < size; row++)
            {
                for(int col = 0; col < size; col++)
                {
                    BlockIndex[row, col] = Block;
                    Block++;
                }
            }
        }

        public int GetBlockPos(int inRow, int inColumn)
        {
            int rowBlock = inRow/BlockSize;
            int columnBlock = inColumn/BlockSize;
            return BlockIndex[rowBlock, columnBlock];
        }

        /// <summary>
        /// Updates the cells Possibilities in the row, column and block, by removing it.
        /// <see cref="UpdateRowPossibilities(int, int)"/>
        /// <see cref="UpdateColumnPossibilities(int, int)"/>
        /// <see cref="UpdateBlockPossibilities(int, int, int)"/>
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to remove from th cells. </param>
        /// <returns> Returns hte total number of cells updated. </returns>
        public int UpdatePossibilities(int inRow, int inColumn, int cellValue)
        {
            int total = 0;
            total += UpdateRowPossibilities(inRow, cellValue);
            total += UpdateColumnPossibilities(inColumn, cellValue);
            total += UpdateBlockPossibilities(inRow, inColumn, cellValue);
            return total;
        }

        /// <summary>
        /// Updates the cells Possibilities in the row by removing it.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="cellValue"> Value to remove from th cells. </param>
        /// <returns> Returns the total number of cells updated. </returns>
        public int UpdateRowPossibilities(int inRow, int cellValue)
        {
            int numUpdated = 0;
            for(int col = 0; col < BoardSize; col++)
            {
                if(Grid[inRow, col].RemovePossibility(cellValue))
                {
                    numUpdated++;
                }
            }
            return numUpdated;
        }

        /// <summary>
        /// Updates the cells Possibilities in the column by removing it.
        /// </summary>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to remove from th cells. </param>
        /// <returns> Returns the total number of cells updated. </returns>
        public int UpdateColumnPossibilities(int inColumn, int cellValue)
        {
            int numUpdated = 0;
            for(int row = 0; row <BoardSize; row++)
            {
                if(Grid[row, inColumn].RemovePossibility(cellValue))
                {
                    numUpdated++;
                }
            }
            return numUpdated;
        }

        /// <summary>
        /// Updates the cells Possibilities in the block, by removing it.
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

            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
            {
                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
                {
                    if(Grid[outerRow, innerColumn].RemovePossibility(cellValue))
                    {
                        numUpdated++;
                    }
                }
            }
            return numUpdated;
        }

        /// <summary>
        /// Sets the values of the cell in the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellValue"> Value to set the cell. </param>
        public void Set(int inRow, int inColumn, int cellValue)
        {
            if(cellValue == 0)
            {
                openCells.Add(Grid[inRow, inColumn]);
            }
            else
            {
                openCells.Remove(Grid[inRow, inColumn]);
            }
            Grid[inRow, inColumn].CellValue = cellValue;
            UpdatePossibilities(inRow, inColumn, cellValue);

        }

        /// <summary>
        /// Gets the values of the selected cell in the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <returns> Returns the integer value of the selected cell. </returns>
        public int Get(int inRow, int inColumn)
        {
            return Grid[inRow, inColumn].CellValue;
            //return Grid[inRow, inColumn].CellValue;
        }

        /// <summary>
        /// Inserts a single value with checks to make sure it is within hte board and not overwriting a clue cell.
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
            if(Get(inRow, inColumn) != 0)
            {
                return false;
            }

            Set(inRow, inColumn, cellValue);

            return true;
        }

        /// <summary>
        /// Inserts a single value with checks to make sure it is within hte board and not overwriting a clue cell.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the selected cell. </param>
        /// <param name="cellPValue"> Value to set the cell. check as a list. </param>
        /// <returns> Return if the cells values was set. </returns>
        public bool SafeInsert(int inRow, int inColumn, List<int> cellPValue)
        {
            if(inRow >= BoardSize ||  inColumn >= BoardSize)
            {
                return false;
            }
            if(Get(inRow, inColumn) != 0)
            {
                return false;
            }

            if(cellPValue != null && cellPValue.Count == 1)
            {
                int cellValue = cellPValue[0];
                Set(inRow, inColumn, cellValue);
                return true;
            }
            else
            {
                return false;
            }
        }

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
        public List<Cell> GetUnPopulatedColumnCells(int inColumn)
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
        public List<Cell> GetUnPopulatedBlockCells(int inRow, int inColumn)
        {
            List<Cell> res = new List<Cell>();

            int rowBlock = inRow/BlockSize*BlockSize;
            int columnBlock = inColumn/BlockSize*BlockSize;

            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
            {
                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
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

        /// <summary>
        /// Gets all the Given and Populated cells <see cref="Cell"/> in the Row.
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
                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
                {
                    res.Add(Grid[outerRow, innerColumn]);
                }
            }
            return res;
        }

        /// <summary>
        /// Gets the possibilities associated with a cell.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of the values that can be safely inserted in to the cell. </returns>
        public List<int> GetCellPossibilities(int inRow, int inColumn)
        {
            if(inRow >= BoardSize ||  inColumn >= BoardSize)
            {
                return null;
            }
            Cell cell =  Grid[inRow, inColumn];

            if(cell.CellValue == 0)
            {
                return cell.CellPossibilities;
            }
            return null;
        }

        /// <summary>
        /// Gets all the possible values for each open cell in the selected inRow of the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <returns> Returns a list of the values that can be safely inserted in to the column's open cells. </returns>
        public List<List<int>> GetRowPossibilities(int inRow)
        {
            List<List < int >> ints = new List<List < int >>();
            for(int col = 0; col < BoardSize; col++)
            {
                List<int> possibleCells = GetCellPossibilities(inRow, col);
                if(possibleCells != null)
                {
                    ints.Add(possibleCells);
                }
            }
            return ints;
        }

        /// <summary>
        /// Gets all the possible values for each open cell in the selected column of the grid.
        /// </summary>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of the values that can be safely inserted in to the column's open cells. </returns>
        public List<List<int>> GetColumnPossibilities(int inColumn)
        {
            List<List<int>> ints = new List<List < int >>();
            for(int row = 0; row < BoardSize; row++)
            {
                List<int> possibleCells = GetCellPossibilities(row, inColumn);
                if(possibleCells != null)
                {
                    ints.Add(possibleCells);
                }
            }
            return ints;
        }

        /// <summary>
        /// Gets all the possible values for each open cell in the selected block of the grid.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of the values that can be safely inserted in to the block's open cells. </returns>
        public List<List<int>> GetBlockPossibilities(int inRow, int inColumn)
        {
            List<List<int>> ints = new List<List<int>>();

            int rowBlock = inRow/BlockSize*BlockSize;
            int columnBlock = inColumn/BlockSize*BlockSize;

            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
            {
                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
                {
                    List<int> possibleCells = GetCellPossibilities(outerRow, innerColumn);
                    if(possibleCells != null)
                    {
                        ints.Add(possibleCells);
                    }
                }
            }
            return ints;
        }

        /// <summary>
        /// Checks if the selected inRow contains the given value.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="cellValue"> Value to check in Targeted/Selected inRow. </param>
        /// <returns> Returns true if not present, returns false if present. </returns>
        public bool CheckRowContains(int inRow, int cellValue)
        {
            List<Cell> cell = GetRowCells(inRow);
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
            List<Cell> cell = GetColumnCells(inColumn);
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
            List<Cell> cell = GetBlockCells(inRow,inColumn);
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
        /// Checks that all the cells in the board meet the required constratints.
        /// </summary>
        /// <returns></returns>
        public bool VerifyBoard()
        {
            foreach(Cell cell in Grid)
            {
                if(!IsSafe(cell))
                {
                    return false;
                }
            }
            return true;
        }
    }
}