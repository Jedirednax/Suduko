//using System.Text;

//namespace SudokuBoardLibrary
//{
//    /// <summary>
//    /// Solves Suduko boards using basic to advanced Stratagies.
//    /// </summary>
//    public class SudokuSolver
//    {
//        // TODO Add Naked pair solving
//        // TODO Add Hidden Naked pair solving
//        // TODO Add Naked tripple solving
//        // TODO Add Hidden Naked tripple solving
//        // TODO Add X-Wing
//        // TODO Add Y-Wing
//        // TODO Add SwordFish

//        public int BoardSize = 9;
//        public int BlockSize = 3;
//        private int[,] BlockIndex;

//        public Cell[,] Grid;
//        //public int[,] Clues;
//        public int[,] Solution= new int[,]
//        {
//            //{ 7,1,2,9,8,4,6,3,5,},
//            //{ 5,6,8,1,2,3,9,7,4,},
//            //{ 4,3,9,5,6,7,2,1,8,},
//            //{ 1,9,3,7,4,5,8,2,6,},
//            //{ 6,4,7,8,1,2,3,5,9,},
//            //{ 8,2,5,6,3,9,1,4,7,},
//            //{ 2,7,6,3,5,8,4,9,1,},
//            //{ 9,8,4,2,7,1,5,6,3,},
//            //{ 3,5,1,4,9,6,7,8,2,},

//            {2,3,4,8,6,1,9,7,5,},
//            {7,8,9,4,5,3,1,2,6,},
//            {5,1,6,2,7,9,4,8,3,},
//            {3,2,5,9,1,8,6,4,7,},
//            {6,9,1,3,4,7,8,5,2,},
//            {4,7,8,6,2,5,3,1,9,},
//            {9,6,7,5,8,4,2,3,1,},
//            {8,5,3,1,9,2,7,6,4,},
//            {1,4,2,7,3,6,5,9,8,},
//        };
//        public List<Cell> openCells = new List<Cell>();

//        public SudokuSolver(int[,] board)
//        {
//            SetBlock(3);
//            Grid = new Cell[board.GetLength(0), board.GetLength(1)];

//            BlockSize = (int)Math.Sqrt(board.GetLength(0));
//            BlockIndex = new int[BlockSize, BlockSize];

//            for(int row = 0; row < board.GetLength(0); row++)
//            {
//                for(int col = 0; col < board.GetLength(1); col++)
//                {
//                    int cell = board[row, col];

//                    Cell nCell = new Cell(row, col, cell);
//                    if(cell == 0)
//                    {
//                        openCells.Add(nCell);
//                    }
//                    int blockPos = GetBlockPos(row, col);
//                    nCell.CellBlock = blockPos;
//                    //nCell.CellSolution = Solution[row, col];
//                    Grid[row, col] = nCell;
//                }
//            }
//            for(int row = 0; row < board.GetLength(0); row++)
//            {
//                for(int col = 0; col < board.GetLength(1); col++)
//                {
//                    Grid[row, col].CellPossibilities = GetCellPossibilitiesPop(row, col);
//                }
//            }
//        }
//        public SudokuSolver(Cell[,] board)
//        {
//            SetBlock(3);
//            Grid =board;

//            BlockSize = (int)Math.Sqrt(board.GetLength(0));
//            BlockIndex = new int[BlockSize, BlockSize];

//        }
//        public void SetBlock(int size)
//        {
//            BlockIndex = new int[BlockSize, BlockSize];
//            int Block = 0;
//            for(int row = 0; row < size; row++)
//            {
//                for(int col = 0; col < size; col++)
//                {
//                    BlockIndex[row, col] = Block;
//                    Block++;
//                }
//            }
//        }
//        public int GetBlockPos(int inRow, int inColumn)
//        {
//            int rowBlock = inRow/BlockSize;
//            int columnBlock = inColumn/BlockSize;
//            return BlockIndex[rowBlock, columnBlock];
//        }

//        public void UpdatePossibilities(int inRow, int inColumn, int cellValue)
//        {
//            UpdateRowPossibilities(inRow, cellValue);
//            UpdateColumnPossibilities(inColumn, cellValue);
//            UpdateBlockPossibilities(inRow, inColumn, cellValue);
//        }

//        public void UpdateRowPossibilities(int inRow, int cellValue)
//        {
//            for(int col = 0; col < BoardSize; col++)
//            {
//                Grid[inRow, col].RemovePossibility(cellValue);
//            }
//        }

//        public void UpdateColumnPossibilities(int inColumn, int cellValue)
//        {
//            for(int row = 0; row <BoardSize; row++)
//            {
//                Grid[row, inColumn].RemovePossibility(cellValue);
//            }
//        }

//        public void UpdateBlockPossibilities(int inRow, int inColumn, int cellValue)
//        {
//            int rowBlock = inRow/BlockSize*BlockSize;
//            int columnBlock = inColumn/BlockSize*BlockSize;

//            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
//            {
//                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
//                {
//                    Grid[outerRow, innerColumn].RemovePossibility(cellValue);
//                }
//            }
//        }

//        /// <summary>
//        /// Sets the values of the cell in the grid.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the selected cell. </param>
//        /// <param name="cellValue"> Value to set the cell. </param>
//        public void Set(int inRow, int inColumn, int cellValue)
//        {
//            if(cellValue == 0)
//            {
//                openCells.Add(Grid[inRow, inColumn]);
//            }
//            else
//            {
//                openCells.Remove(Grid[inRow, inColumn]);
//            }
//            Grid[inRow, inColumn].CellValue = cellValue;
//            UpdatePossibilities(inRow, inColumn, cellValue);

//        }

//        /// <summary>
//        /// Gets the values of the selected cell in the grid.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the selected cell. </param>
//        /// <returns> Returns the integer value of the selected cell. </returns>
//        public int Get(int inRow, int inColumn)
//        {
//            return Grid[inRow, inColumn].CellValue;
//            //return Grid[inRow, inColumn].CellValue;
//        }

//        #region Inserts
//        /// <summary>
//        /// Inserts a single value with checks to make sure it is within hte board and not overwriting a clue cell.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the selected cell. </param>
//        /// <param name="cellValue"> Value to set the cell. </param>
//        /// <returns> Return if the cells values was set. </returns>
//        public bool SafeInsert(int inRow, int inColumn, int cellValue)
//        {
//            if(inRow >= BoardSize ||  inColumn >= BoardSize)
//            {
//                return false;
//            }
//            if(Get(inRow, inColumn) != 0)
//            {
//                return false;
//            }

//            Set(inRow, inColumn, cellValue);

//            return true;
//        }
//        /// <summary>
//        /// Inserts a single value with checks to make sure it is within hte board and not overwriting a clue cell.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the selected cell. </param>
//        /// <param name="cellPValue"> Value to set the cell. check as a list. </param>
//        /// <returns> Return if the cells values was set. </returns>
//        public bool SafeInsert(int inRow, int inColumn, List<int> cellPValue)
//        {
//            if(inRow >= BoardSize ||  inColumn >= BoardSize)
//            {
//                return false;
//            }
//            if(Get(inRow, inColumn) != 0)
//            {
//                return false;
//            }

//            if(cellPValue != null && cellPValue.Count == 1)
//            {
//                int cellValue = cellPValue[0];
//                Set(inRow, inColumn, cellValue);
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        #endregion

//        #region Gets
//        #region Values
//        /// <summary>
//        /// Gets the selected inRow's values from the grid.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <returns> Returns all the know values in the selected inRow. </returns>
//        public List<int> GetRowValues(int inRow)
//        {
//            List<int> res = new List<int>();

//            for(int innerColumn = 0; innerColumn < BoardSize; innerColumn++)
//            {
//                int cell = Get(inRow, innerColumn);
//                if(cell != 0)
//                {
//                    res.Add(Get(inRow, innerColumn));
//                }
//            }

//            return res;
//        }

//        /// <summary>
//        /// Gets the selected column's values from the grid.
//        /// </summary>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns all the know values in the selected column. </returns>
//        public List<int> GetColumnValues(int inColumn)
//        {
//            List<int> res = new List<int>();

//            for(int innerRow = 0; innerRow < BoardSize; innerRow++)
//            {
//                int cell = Get(innerRow, inColumn);
//                if(cell != 0)
//                {
//                    res.Add(Get(innerRow, inColumn));
//                }
//            }
//            return res;
//        }

//        /// <summary>
//        /// Gets the selected block's values from the grid.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns all the know values in the selected block. </returns>

//        public List<int> GetBlockValues(int inRow, int inColumn)
//        {
//            List<int> res = new List<int>();

//            int rowBlock = inRow/BlockSize*BlockSize;
//            int columnBlock = inColumn/BlockSize*BlockSize;

//            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
//            {
//                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
//                {
//                    int cell = Get(outerRow, innerColumn);
//                    if(cell != 0)
//                    {
//                        res.Add(cell);
//                    }
//                }
//            }
//            return res;
//        }
//        #endregion
//        #region Unpopulated Cells
//        /// <summary>
//        /// Gets all the Given and Populated cells <see cref="Cell"/> in the Row.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <returns> Returns all the Given and Populated cells <see cref="Cell"/>. </returns>
//        public List<Cell> GetUnPopulatedRowCells(int inRow)
//        {
//            List<Cell> res = new List<Cell>();

//            for(int innerColumn = 0; innerColumn < BoardSize; innerColumn++)
//            {
//                Cell cell = Grid[inRow, innerColumn];
//                if(cell.IsPopulated)
//                {
//                    res.Add(cell);
//                }
//            }

//            return res;
//        }
//        /// <summary>
//        /// Gets the selected column's values from the grid.
//        /// </summary>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns all the know values in the selected column. </returns>
//        public List<Cell> GetUnPopulatedColumnCells(int inColumn)
//        {
//            List<Cell> res = new List<Cell>();

//            for(int innerRow = 0; innerRow < BoardSize; innerRow++)
//            {
//                Cell cell = Grid[innerRow, inColumn];
//                if(cell.IsPopulated)
//                {
//                    res.Add(Grid[innerRow, inColumn]);
//                }
//            }
//            return res;
//        }

//        /// <summary>
//        /// Gets the selected block's values from the grid.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns all the know values in the selected block. </returns>

//        public List<Cell> GetUnPopulatedBlockCells(int inRow, int inColumn)
//        {
//            List<Cell> res = new List<Cell>();

//            int rowBlock = inRow/BlockSize*BlockSize;
//            int columnBlock = inColumn/BlockSize*BlockSize;

//            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
//            {
//                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
//                {
//                    Cell cell = Grid[outerRow, innerColumn];
//                    if(cell.IsPopulated)
//                    {
//                        res.Add(cell);
//                    }
//                }
//            }
//            return res;
//        }
//        #endregion
//        #region GetAllCellsin
//        /// <summary>
//        /// Gets all the Given and Populated cells <see cref="Cell"/> in the Row.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <returns> Returns all the Given and Populated cells <see cref="Cell"/>. </returns>
//        public List<Cell> GetRowCells(int inRow)
//        {
//            List<Cell> res = new List<Cell>();

//            for(int innerColumn = 0; innerColumn < BoardSize; innerColumn++)
//            {
//                res.Add(Grid[inRow, innerColumn]);
//            }

//            return res;
//        }
//        /// <summary>
//        /// Gets the selected column's values from the grid.
//        /// </summary>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns all the know values in the selected column. </returns>
//        public List<Cell> GetColumnCells(int inColumn)
//        {
//            List<Cell> res = new List<Cell>();

//            for(int innerRow = 0; innerRow < BoardSize; innerRow++)
//            {
//                res.Add(Grid[innerRow, inColumn]);
//            }
//            return res;
//        }

//        /// <summary>
//        /// Gets the selected block's values from the grid.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns all the know values in the selected block. </returns>

//        public List<Cell> GetBlockCells(int inRow, int inColumn)
//        {
//            List<Cell> res = new List<Cell>();

//            int rowBlock = inRow/BlockSize*BlockSize;
//            int columnBlock = inColumn/BlockSize*BlockSize;

//            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
//            {
//                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
//                {
//                    res.Add(Grid[outerRow, innerColumn]);
//                }
//            }
//            return res;
//        }
//        #endregion
//        #region GetPossibilities

//        /// <summary>
//        /// Gets all the possible values for a selected cell based off of the inRow, column and block of the selected cell.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns a list of the values that can be safely inserted in to a cell. </returns>
//        public List<int> GetCellPossibilitiesPop(int inRow, int inColumn)
//        {
//            if(inRow >= BoardSize ||  inColumn >= BoardSize)
//            {
//                return null;
//            }

//            if(Get(inRow, inColumn) == 0)
//            {
//                List<int> possible = new List<int>();

//                for(int i = 1; i <= BoardSize; i++)
//                {
//                    if(IsSafe(inRow, inColumn, i))
//                    {
//                        possible.Add(i);
//                    }
//                }
//                return possible;
//            }
//            return null;
//        }

//        public List<int> GetCellPossibilities(int inRow, int inColumn)
//        {
//            if(inRow >= BoardSize ||  inColumn >= BoardSize)
//            {
//                return null;
//            }
//            Cell cell =  Grid[inRow, inColumn];

//            if(cell.CellValue == 0)
//            {
//                return cell.CellPossibilities;
//            }
//            return null;
//        }
//        /// <summary>
//        /// Gets all the possible values for each open cell in the selected inRow of the grid.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <returns> Returns a list of the values that can be safely inserted in to the column's open cells. </returns>
//        public List<List<int>> GetRowPossibilities(int inRow)
//        {
//            List<List < int >> ints = new List<List < int >>();
//            for(int col = 0; col < BoardSize; col++)
//            {
//                List<int> possibleCells = GetCellPossibilities(inRow, col);
//                if(possibleCells != null)
//                {
//                    ints.Add(possibleCells);
//                }
//            }
//            return ints;
//        }

//        /// <summary>
//        /// Gets all the possible values for each open cell in the selected column of the grid.
//        /// </summary>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns a list of the values that can be safely inserted in to the column's open cells. </returns>
//        public List<List<int>> GetColumnPossibilities(int inColumn)
//        {
//            List<List<int>> ints = new List<List < int >>();
//            for(int row = 0; row < BoardSize; row++)
//            {
//                List<int> possibleCells = GetCellPossibilities(row, inColumn);
//                if(possibleCells != null)
//                {
//                    ints.Add(possibleCells);
//                }
//            }
//            return ints;
//        }

//        /// <summary>
//        /// Gets all the possible values for each open cell in the selected block of the grid.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns a list of the values that can be safely inserted in to the block's open cells. </returns>
//        public List<List<int>> GetBlockPossibilities(int inRow, int inColumn)
//        {
//            List<List<int>> ints = new List<List<int>>();

//            int rowBlock = inRow/BlockSize*BlockSize;
//            int columnBlock = inColumn/BlockSize*BlockSize;

//            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
//            {
//                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
//                {
//                    List<int> possibleCells = GetCellPossibilities(outerRow, innerColumn);
//                    if(possibleCells != null)
//                    {
//                        ints.Add(possibleCells);
//                    }
//                }
//            }
//            return ints;
//        }
//        #endregion
//        #endregion
//        #region Checks

//        /// <summary>
//        /// Checks if the selected inRow contains the given value.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="cellValue"> Value to check in Targeted/Selected inRow. </param>
//        /// <returns> Returns true if not present, returns false if present. </returns>
//        public bool CheckRowContains(int inRow, int cellValue)
//        {
//            List<int> cells = GetRowValues(inRow);
//            if(cells.Count != BoardSize)
//            {
//                return !cells.Contains(cellValue);
//            }
//            else
//            {
//                return cells.Where(y => y == cellValue).Count()==1;
//            }
//        }

//        /// <summary>
//        /// Checks if the selected column contains the given value.
//        /// </summary>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <param name="cellValue"> Value to check in Targeted/Selected column. </param>
//        /// <returns> Returns true if not present, returns false if present. </returns>
//        public bool CheckColumnContains(int inColumn, int cellValue)
//        {
//            List<int> cells = GetColumnValues(inColumn);
//            if(cells.Count !=BoardSize)
//            {
//                return !cells.Contains(cellValue);
//            }
//            else
//            {
//                return cells.Where(y => y == cellValue).Count()==1;
//            }
//        }

//        /// <summary>
//        /// Checks if the selected block contains the given value.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <param name="cellValue"> Value to check in Selected block. </param>
//        /// <returns> Returns true if not present, returns false if present. </returns>
//        public bool CheckBlockContains(int inRow, int inColumn, int cellValue)
//        {
//            List<int> cells = GetBlockValues(inRow,inColumn);
//            if(cells.Count != BoardSize)
//            {
//                return !cells.Contains(cellValue);
//            }
//            else
//            {
//                return cells.Where(y => y == cellValue).Count()==1;
//            }
//        }

//        /// <summary>
//        /// Check the inRow, column and block, for the selected value.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <param name="cellValue"> Value to check against all constraints. </param>
//        /// <returns> Returns true if not present, returns false if present. </returns>
//        public bool IsSafe(int inRow, int inColumn, int cellValue)
//        {
//            if(!CheckRowContains(inRow, cellValue))
//            {
//                return false;
//            }
//            if(!CheckColumnContains(inColumn, cellValue))
//            {
//                return false;
//            }
//            if(!CheckBlockContains(inRow, inColumn, cellValue))
//            {
//                return false;
//            }

//            return true;
//        }

//        /// <summary>
//        /// Check the inRow, column and block, for the selected value.
//        /// </summary>
//        /// <param name="cell"> Cell to chick in grid. </param>
//        /// <returns> Returns true if not present, returns false if present. </returns>
//        public bool IsSafe(Cell cell)
//        {
//            if(!CheckRowContains(cell.CellRow, cell.CellValue))
//            {
//                return false;
//            }
//            if(!CheckColumnContains(cell.CellColumn, cell.CellValue))
//            {
//                return false;
//            }
//            if(!CheckBlockContains(cell.CellRow, cell.CellColumn, cell.CellValue))
//            {
//                return false;
//            }
//            return true;
//        }

//        public bool VerifyBoard()
//        {
//            foreach(Cell cell in Grid)
//            {
//                if(!IsSafe(cell))
//                {
//                    return false;
//                }
//            }
//            return true;
//        }
//        #endregion

//        #region Solving Techiques
//        #region Constraint

//        /// <summary>
//        /// Loops through the board's open cells and checks if there is only one possible value based off of the constraints.
//        /// </summary>
//        /// <returns> Returns true if was able to insert a value. </returns>
//        public bool ConstraintSolve()
//        {
//            bool inserted = false;
//            foreach(Cell va in openCells)
//            {
//                int outterRow = va.CellRow;
//                int innerColumn = va.CellColumn;
//                if(Get(outterRow, innerColumn) == 0)
//                {
//                    if(SafeInsert(outterRow, innerColumn, GetCellPossibilities(outterRow, innerColumn)))
//                    {
//                        inserted = true;
//                        break;
//                    }
//                }
//            }
//            return inserted;
//        }
//        #endregion

//        #region Elimination

//        /// <summary>
//        /// Checks for if a value can only be placed in a single cell based off of it is the only value in the inRow, column or block.
//        /// If a single value is return in the Elim lists, then is is inserted. <see cref="SafeInsert(int, int, List{int})"/>
//        /// </summary>
//        /// <returns> Returns true if was able to insert a value. </returns>
//        public bool EliminateSolve()
//        {
//            bool inserted = false;
//            foreach(Cell va in openCells)
//            {
//                int outterRow = va.CellRow;
//                int innerColumn = va.CellColumn;
//                if(SafeInsert(outterRow, innerColumn, ElimRow(outterRow, innerColumn)))
//                {
//                    inserted = true;
//                    break;
//                }
//                else if(SafeInsert(outterRow, innerColumn, ElimCol(outterRow, innerColumn)))
//                {
//                    inserted = true;
//                    break;
//                }
//                else if(SafeInsert(outterRow, innerColumn, ElimBlock(outterRow, innerColumn)))
//                {
//                    inserted = true;
//                    break;
//                }
//            }
//            return inserted;
//        }

//        /// <summary>
//        /// Loops through the selected inRow, and get all the possibilities, skipping the selected cell.
//        /// Then loops though and checks if it has any values that are not contained in the remaining cells.
//        /// THen if a single value is left returns those values.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="currCol"> Column of the Selected cell. </param>
//        /// <returns> Returns a list of values the selected cell contains that are nor present in the other Cells as posibiites in the inRow. </returns>
//        public List<int> ElimRow(int inRow, int currCol)
//        {
//            List < int > currRow = GetCellPossibilities(inRow, currCol);
//            if(currRow!=null)
//            {
//                List<int> hold = new List<int>();
//                for(int innerColumn = 0; innerColumn < BoardSize; innerColumn++)
//                {
//                    if(innerColumn != currCol)
//                    {
//                        List < int > possibleCells = GetCellPossibilities(inRow, innerColumn);
//                        if(possibleCells!=null)
//                        {
//                            hold.AddRange(possibleCells);
//                        }
//                    }
//                }
//                List<int> res = new List<int>();
//                foreach(int i in currRow)
//                {
//                    if(!hold.Contains(i))
//                    {
//                        res.Add(i);
//                    }
//                }
//                return res;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// Loops through the selected column, and get all the possibilities, skipping the selected cell.
//        /// Then loops though and checks if it has any values that are not contained in the remaining cells.
//        /// THen if a single value is left returns those values.
//        /// </summary>
//        /// <param name="currRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns a list of values the selected cell contains that are nor present in the other Cells as posibiites in the Column. </returns>
//        public List<int> ElimCol(int currRow, int inColumn)
//        {
//            List < int > currCol = GetCellPossibilities(currRow, inColumn);
//            if(currCol!=null)
//            {
//                List<int> hold = new List<int>();
//                for(int innerRow = 0; innerRow < BoardSize; innerRow++)
//                {
//                    if(innerRow != currRow)
//                    {
//                        List < int > possibleCells = GetCellPossibilities(innerRow,inColumn);
//                        if(possibleCells!=null)
//                        {
//                            hold.AddRange(possibleCells);
//                        }
//                    }
//                }
//                List<int> res = new List<int>();
//                foreach(int i in currCol)
//                {
//                    if(!hold.Contains(i))
//                    {
//                        res.Add(i);
//                    }
//                }
//                return res;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// Loops through the selected Block, and get all the possibilities, skipping the selected cell.
//        /// Then loops though and checks if it has any values that are not contained in the remaining cells.
//        /// </summary>
//        /// <param name="inRow"> Row of the Selected cell. </param>
//        /// <param name="inColumn"> Column of the Selected cell. </param>
//        /// <returns> Returns a list of values the selected cell contains that are nor present in the other Cells as posibiites in the block. </returns>
//        public List<int> ElimBlock(int inRow, int inColumn)
//        {
//            List<int> currBlk = GetCellPossibilities(inRow, inColumn);
//            if(currBlk!=null)
//            {
//                List<int> hold = new List<int>();
//                int rowBlock = inRow/BlockSize*BlockSize;
//                int columnBlock = inColumn/BlockSize*BlockSize;

//                for(int outterRow = rowBlock; outterRow < rowBlock + BlockSize; outterRow++)
//                {
//                    for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
//                    {
//                        if(!(outterRow == inRow && innerColumn== inColumn))
//                        {
//                            List<int> possibleCells = GetCellPossibilities(outterRow,innerColumn);
//                            if(possibleCells!=null)
//                            {
//                                hold.AddRange(possibleCells);
//                            }
//                        }
//                    }
//                }
//                List<int> res = new List<int>();

//                foreach(int g in currBlk)
//                {
//                    if(!hold.Contains(g))
//                    {
//                        res.Add(g);
//                    }
//                }
//                return res;
//            }
//            else
//            {
//                return null;
//            }
//        }
//        #endregion
//        #region BackTracking
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
//            if(Get(row, column) != 0)
//            {
//                column += 1;
//                return BackTrackingSolve(row, column);
//            }

//            foreach(int cell in GetCellPossibilities(row, column))
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
//        #endregion
//        #region NakedPairs
//        public void ObvPair()
//        {
//            foreach(Cell cl in openCells)
//            {
//                ObviousPairRow(cl.CellRow, cl.CellColumn);
//            }
//            foreach(Cell cl in openCells)
//            {
//                ObviousPairCol(cl.CellRow, cl.CellColumn);
//            }
//            foreach(Cell cl in openCells)
//            {
//                ObviousPairBlock(cl.CellRow, cl.CellColumn);
//            }
//        }
//        public Cell FindPairRow(int inRow, int inColumn)
//        {
//            Cell mainCell = Grid[inRow, inColumn];
//            Cell res = null;
//            for(int col = 0; col <BoardSize; col++)
//            {
//                if(col != inColumn)
//                {
//                    if(mainCell.ComparePossibilities(Grid[inRow, col]))
//                    {
//                        res = Grid[inRow, col];
//                        break;
//                    }
//                }
//            }
//            return res;
//        }
//        public Cell FindPairCol(int inRow, int inColumn)
//        {

//            Cell mainCell = Grid[inRow, inColumn];
//            Cell res = null;
//            for(int row = 0; row <BoardSize; row++)
//            {
//                if(row!=inRow)
//                {
//                    if(mainCell.ComparePossibilities(Grid[row, inColumn]))
//                    {
//                        res = Grid[row, inColumn];
//                        break;
//                    }
//                }
//            }
//            return res;
//        }
//        public Cell FindPairBlk(int inRow, int inColumn)
//        {

//            Cell mainCell = Grid[inRow, inColumn];
//            Cell res = null;
//            int rowBlock = inRow/BlockSize*BlockSize;
//            int columnBlock = inColumn/BlockSize*BlockSize;

//            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
//            {
//                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
//                {
//                    if(outerRow != mainCell.CellRow && innerColumn != mainCell.CellColumn)
//                    {
//                        if(mainCell.ComparePossibilities(Grid[outerRow, innerColumn]))
//                        {
//                            res = Grid[outerRow, innerColumn];
//                            break;
//                        }
//                    }
//                }
//            }
//            return res;
//        }
//        public void ObviousPairRow(int inRow, int inColumn)
//        {
//            Cell mainCell = Grid[inRow, inColumn];
//            Cell child = FindPairRow(inRow, inColumn);
//            if(child != null)
//            {
//                for(int col = 0; col <BoardSize; col++)
//                {
//                    if(mainCell.CellColumn != col)
//                    {
//                        if(child.CellColumn != col)
//                        {
//                            foreach(int cellPosb in mainCell.CellPossibilities)
//                            {
//                                Grid[inRow, col].RemovePossibility(cellPosb);
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        public void ObviousPairCol(int inRow, int inColumn)
//        {
//            Cell mainCell = Grid[inRow, inColumn];
//            Cell child = FindPairCol(inRow, inColumn);
//            if(child!=null)
//            {
//                for(int row = 0; row<BoardSize; row++)
//                {
//                    if(mainCell.CellRow != row)
//                    {
//                        if(row != child.CellRow)
//                        {
//                            foreach(int cellPosb in mainCell.CellPossibilities)
//                            {
//                                Grid[row, inColumn].RemovePossibility(cellPosb);
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        public void ObviousPairBlock(int inRow, int inColumn)
//        {
//            Cell mainCell = Grid[inRow, inColumn];
//            Cell child = FindPairBlk(inRow, inColumn);
//            if(child!=null)
//            {
//                int rowBlock = inRow/BlockSize*BlockSize;
//                int columnBlock = inColumn/BlockSize*BlockSize;

//                for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
//                {
//                    for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
//                    {
//                        if(outerRow != mainCell.CellRow || innerColumn != mainCell.CellColumn)
//                        {
//                            if(outerRow != child.CellRow || innerColumn != child.CellColumn)
//                            {
//                                foreach(int cellPosb in mainCell.CellPossibilities)
//                                {
//                                    Grid[outerRow, innerColumn].RemovePossibility(cellPosb);
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        #endregion

//        #region Hidden Paris
//        public void HiddenPair()
//        {

//        }
//        #endregion
//        #region X-Wing
//        public void XWing()
//        {

//        }
//        #endregion
//        #region Y-Wing
//        public void YWing()
//        {
//        }
//        #endregion
//        #region PointingPairs

//        /*
//        public bool PointingPairCol(int inRow, int inColumn)
//        {
//            // GetCell starting cell
//            Cell mainCell = Grid[inRow, inColumn];

//            // Check cell is empty
//            for(int col = 0; col <BoardSize; col++)
//            {
//                // checks not same cell

//                if(col != inRow)
//                {
//                    // Checks starting cell with new cell, for matching possibilities

//                    if(mainCell.ComparePosibilities(Grid[col, inRow]))
//                    {
//                        // loops through column

//                        for(int removePos = 0; removePos < BoardSize; removePos++)
//                        {
//                            // makes sure, the found cell, and the main cells do not get removed.

//                            if(removePos!= inRow && removePos!=col)
//                            {
//                                // removes values from the cell if is not part of the pair.

//                                foreach(int cellPosb in mainCell.CellPossibilities)
//                                {
//                                    Grid[removePos, inColumn].RemovePossibility(cellPosb);
//                                }
//                            }
//                        }
//                        // once a appropriate pair is found the removal runs then exits the loop, as the pair is found.

//                        return true;
//                    }
//                }
//            }

//            return false;

//        }
//        */

//        #endregion
//        #region SwordFish

//        public void SwordFish()
//        {

//        }
//        #endregion
//        #endregion
//        /// <summary>
//        /// Runs the solving methods on the board/Grid.
//        /// </summary>
//        /// <returns> Returns if the board has been solved or not. </returns>
//        public bool SolveBoard()
//        {
//            bool mainExit = true;
//            int a = 0;
//            bool Inserted = false;
//            int counter = 0;
//            do
//            {
//                counter = 0;
//                do
//                {
//                    Inserted = ConstraintSolve();
//                    counter++;
//                    if(!Inserted)
//                    {
//                        a++;
//                    }
//                }
//                while(Inserted);

//                counter = 0;
//                do
//                {
//                    Inserted = EliminateSolve();
//                    counter++;
//                    if(!Inserted)
//                    {
//                        a++;
//                    }
//                }
//                while(Inserted);
//                //ObvPair();
//                mainExit = a<3;
//            }
//            while(mainExit);

//            return false;//VerifyBoard();
//        }
//        public string ToArray()
//        {
//            StringBuilder sb = new StringBuilder();

//            for(int row = 0; row < BoardSize; row++)
//            {
//                sb.AppendLine();
//                sb.Append("{");
//                for(int column = 0; column < BoardSize; column++)
//                {
//                    sb.Append($"{Get(row, column)},");
//                }
//                sb.Append("},");

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
//                    if(Get(row, column) == 0)
//                    {
//                        sb.Append($" ");
//                    }
//                    else
//                    {
//                        sb.Append($"{Get(row, column)}");
//                    }
//                }
//            }
//            sb.AppendLine();
//            return sb.ToString();
//        }

//        public static string RowSep(int size)
//        {
//            StringBuilder sb = new StringBuilder("+");

//            for(int i = 0; i < size; i++)
//            {
//                for(int j = 0; j < size; j++)
//                {
//                    sb.Append("--");
//                }
//                sb.Append("-+");
//            }

//            return sb.ToString();
//        }

//        public void ColourBoardDisplay()
//        {
//            for(int row = 0; row < BoardSize; row++)
//            {
//                if(row%BlockSize == 0)
//                {
//                    Console.Write($"{RowSep(BlockSize*2)}\n|");
//                }
//                else
//                {
//                    Console.Write($"|");
//                }
//                Console.WriteLine();
//                for(int column = 0; column < BoardSize; column++)
//                {
//                    Cell cell = Grid[row, column];
//                    Console.Write($"|");
//                    if(cell.IsGiven)
//                    {
//                        Console.ForegroundColor = ConsoleColor.Gray;
//                    }
//                    else
//                    {
//                        if(cell.IsPopulated)
//                        {
//                            if(cell.isCorrect())
//                            {

//                                Console.ForegroundColor = ConsoleColor.Green;
//                            }
//                            else
//                            {
//                                Console.ForegroundColor = ConsoleColor.Red;
//                            }
//                        }
//                        else
//                        {
//                            Console.ForegroundColor = ConsoleColor.Blue;
//                        }
//                    }
//                    Console.Write($"{cell.ToString(),-8}");
//                    Console.ForegroundColor = ConsoleColor.Gray;
//                    if(((column+1)%BlockSize) == 0)
//                    {
//                        Console.Write($"|");
//                    }
//                }
//                Console.Write($"|");
//                Console.WriteLine();

//            }
//            Console.WriteLine($"\n{RowSep(BlockSize)}");
//        }
//    }
//}
/////*public Cell FindPairRow(int inRow, int inColumn)
////        {
////            /**/
////Cell mainCell = board.GetCell(inRow, inColumn);
////Cell res = null;
////var hold = board.GetUnPopulatedRowCells(inRow);
////hold.Remove(mainCell);
////
////foreach(var g in hold)
////{
////    if(mainCell.ComparePossibilitiesPair(g))
////    {
////        return g;
////    }
////}
////
/////*
////for(int col = 0; col <board.BoardSize; col++)
////{
////    if(col != inColumn)
////    {
////        if(board.GetCell(inRow, col).ComparePossibilitiesPair(mainCell))
////        {
////            res = board.GetCell(inRow, col);
////            return res;
////        }
////    }
////}
/////**/
////return res;
////        }
////        public Cell FindPairCol(int inRow, int inColumn)
////{
////    Cell mainCell = board.GetCell(inRow, inColumn);
////    Cell res = null;
////    var hold = board.GetUnPopulatedColumnCells(inColumn);
////    hold.Remove(mainCell);
////
////    foreach(var g in hold)
////    {
////        if(mainCell.ComparePossibilitiesPair(g))
////        {
////            return g;
////        }
////    }
////    /*
////    Cell mainCell = board.GetCell(inRow,inColumn);
////    Cell res = null;
////    for(int row = 0; row <board.BoardSize; row++)
////    {
////        if(row!=inRow)
////        {
////            if(mainCell.ComparePossibilitiesPair(board.GetCell(row, inColumn)))
////            {
////                res = board.GetCell(row, inColumn);
////                return res;
////
////            }
////        }
////    }
////    */
////    return res;
////}
////public Cell FindPairBlk(int inRow, int inColumn)
////{
////    Cell mainCell = board.GetCell(inRow, inColumn);
////    Cell res = null;
////    var hold = board.GetUnPopulatedBlockCells(inRow,inColumn);
////    hold.Remove(mainCell);
////
////    foreach(var g in hold)
////    {
////        if(mainCell.ComparePossibilitiesPair(g))
////        {
////            return g;
////        }
////    }
////    /*
////    Cell mainCell = board.GetCell(inRow, inColumn);
////    Cell res = null;
////    int rowBlock = inRow/board.BlockSize*board.BlockSize;
////    int columnBlock = inColumn/board.BlockSize*board.BlockSize;
////
////    for(int outerRow = rowBlock; outerRow < rowBlock + board.BlockSize; outerRow++)
////    {
////        for(int innerColumn = columnBlock; innerColumn < columnBlock + board.BlockSize; innerColumn++)
////        {
////            if(outerRow != mainCell.CellRow && innerColumn != mainCell.CellColumn)
////            {
////                if(mainCell.ComparePossibilitiesPair(board.GetCell(outerRow, innerColumn)))
////                {
////                    res = board.GetCell(outerRow, innerColumn);
////                    return res;
////
////                }
////            }
////        }
////    }
////    */
////    return res;
////}