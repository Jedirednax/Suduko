namespace SudukoBoardLibary
{
    /// <summary>
    /// Solves Suduko boards using basic to advanced Stratagies.
    /// </summary>
    public class SudokuSolverf
    {
        // TODO Add Naked pair solving
        // TODO Add Hidden Naked pair solving
        // TODO Add Naked tripple solving
        // TODO Add Hidden Naked tripple solving
        // TODO Add X-Wing
        // TODO Add Y-Wing
        // TODO Add SwordFish

        public SudokuSolverf()
        {

        }

        #region Solving Techiques
        #region Constraint

        /// <summary>
        /// Loops through the board's open cells and checks if there is only one possible value based off of the constraints.
        /// </summary>
        /// <returns> Returns true if was able to insert a value. </returns>
        public bool ConstraintSolve(Board board)
        {
            bool inserted = false;
            foreach(Cell va in board.openCells)
            {
                int outterRow = va.CellRow;
                int innerColumn = va.CellColumn;
                if(Get(outterRow, innerColumn) == 0)
                {
                    if(SafeInsert(outterRow, innerColumn, GetCellPossibilities(outterRow, innerColumn)))
                    {
                        inserted = true;
                        break;
                    }
                }
            }
            return inserted;
        }
        #endregion

        #region Elimination

        /// <summary>
        /// Checks for if a value can only be placed in a single cell based off of it is the only value in the inRow, column or block.
        /// If a single value is return in the Elim lists, then is is inserted. <see cref="SafeInsert(int, int, List{int})"/>
        /// </summary>
        /// <returns> Returns true if was able to insert a value. </returns>
        public bool EliminateSolve()
        {
            bool inserted = false;
            foreach(Cell va in openCells)
            {
                int outterRow = va.CellRow;
                int innerColumn = va.CellColumn;
                if(SafeInsert(outterRow, innerColumn, ElimRow(outterRow, innerColumn)))
                {
                    inserted = true;
                    break;
                }
                else if(SafeInsert(outterRow, innerColumn, ElimCol(outterRow, innerColumn)))
                {
                    inserted = true;
                    break;
                }
                else if(SafeInsert(outterRow, innerColumn, ElimBlock(outterRow, innerColumn)))
                {
                    inserted = true;
                    break;
                }
            }
            return inserted;
        }

        /// <summary>
        /// Loops through the selected inRow, and get all the possibilities, skipping the selected cell.
        /// Then loops though and checks if it has any values that are not contained in the remaining cells.
        /// THen if a single value is left returns those values.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="currCol"> Column of the Selected cell. </param>
        /// <returns> Returns a list of values the selected cell contains that are nor present in the other Cells as posibiites in the inRow. </returns>
        public List<int> ElimRow(int inRow, int currCol)
        {
            List < int > currRow = GetCellPossibilities(inRow, currCol);
            if(currRow!=null)
            {
                List<int> hold = new List<int>();
                for(int innerColumn = 0; innerColumn < BoardSize; innerColumn++)
                {
                    if(innerColumn != currCol)
                    {
                        List < int > possibleCells = GetCellPossibilities(inRow, innerColumn);
                        if(possibleCells!=null)
                        {
                            hold.AddRange(possibleCells);
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
                return res;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Loops through the selected column, and get all the possibilities, skipping the selected cell.
        /// Then loops though and checks if it has any values that are not contained in the remaining cells.
        /// THen if a single value is left returns those values.
        /// </summary>
        /// <param name="currRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of values the selected cell contains that are nor present in the other Cells as posibiites in the Column. </returns>
        public List<int> ElimCol(int currRow, int inColumn)
        {
            List < int > currCol = GetCellPossibilities(currRow, inColumn);
            if(currCol!=null)
            {
                List<int> hold = new List<int>();
                for(int innerRow = 0; innerRow < BoardSize; innerRow++)
                {
                    if(innerRow != currRow)
                    {
                        List < int > possibleCells = GetCellPossibilities(innerRow,inColumn);
                        if(possibleCells!=null)
                        {
                            hold.AddRange(possibleCells);
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
                return res;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Loops through the selected Block, and get all the possibilities, skipping the selected cell.
        /// Then loops though and checks if it has any values that are not contained in the remaining cells.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of values the selected cell contains that are nor present in the other Cells as posibiites in the block. </returns>
        public List<int> ElimBlock(int inRow, int inColumn)
        {
            List<int> currBlk = GetCellPossibilities(inRow, inColumn);
            if(currBlk!=null)
            {
                List<int> hold = new List<int>();
                int rowBlock = inRow/BlockSize*BlockSize;
                int columnBlock = inColumn/BlockSize*BlockSize;

                for(int outterRow = rowBlock; outterRow < rowBlock + BlockSize; outterRow++)
                {
                    for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
                    {
                        if(!(outterRow == inRow && innerColumn== inColumn))
                        {
                            List<int> possibleCells = GetCellPossibilities(outterRow,innerColumn);
                            if(possibleCells!=null)
                            {
                                hold.AddRange(possibleCells);
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
                return res;
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region BackTracking
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
            if(Get(row, column) != 0)
            {
                column += 1;
                return BackTrackingSolve(row, column);
            }

            foreach(int cell in GetCellPossibilities(row, column))
            {
                if(SafeInsert(row, column, cell))
                {
                    if(BackTrackingSolve(row, column+1))
                    {
                        return true;
                    }
                }
                Set(row, column, 0);
            }

            return false;
        }
        #endregion

        #region ObviousPairs
        public void ObviousPair()
        {
            foreach(Cell cl in openCells)
            {
                ObviousPairRow(cl.CellRow, cl.CellColumn);
            }
            foreach(Cell cl in openCells)
            {
                ObviousPairCol(cl.CellRow, cl.CellColumn);
            }
            foreach(Cell cl in openCells)
            {
                ObviousPairBlock(cl.CellRow, cl.CellColumn);
            }
        }
        public Cell FindPairRow(int inRow, int inColumn)
        {
            Cell mainCell = Grid[inRow, inColumn];
            Cell res = null;
            for(int col = 0; col <BoardSize; col++)
            {
                if(col != inColumn)
                {
                    if(mainCell.ComparePossibilities(Grid[inRow, col]))
                    {
                        res = Grid[inRow, col];
                        break;
                    }
                }
            }
            return res;
        }
        public Cell FindPairCol(int inRow, int inColumn)
        {

            Cell mainCell = Grid[inRow, inColumn];
            Cell res = null;
            for(int row = 0; row <BoardSize; row++)
            {
                if(row!=inRow)
                {
                    if(mainCell.ComparePossibilities(Grid[row, inColumn]))
                    {
                        res = Grid[row, inColumn];
                        break;
                    }
                }
            }
            return res;
        }
        public Cell FindPairBlk(int inRow, int inColumn)
        {

            Cell mainCell = Grid[inRow, inColumn];
            Cell res = null;
            int rowBlock = inRow/BlockSize*BlockSize;
            int columnBlock = inColumn/BlockSize*BlockSize;

            for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
            {
                for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
                {
                    if(outerRow != mainCell.CellRow && innerColumn != mainCell.CellColumn)
                    {
                        if(mainCell.ComparePossibilities(Grid[outerRow, innerColumn]))
                        {
                            res = Grid[outerRow, innerColumn];
                            break;
                        }
                    }
                }
            }
            return res;
        }
        public void ObviousPairRow(int inRow, int inColumn)
        {
            Cell mainCell = Grid[inRow, inColumn];
            Cell child = FindPairRow(inRow, inColumn);
            if(child != null)
            {
                for(int col = 0; col <BoardSize; col++)
                {
                    if(mainCell.CellColumn != col)
                    {
                        if(child.CellColumn != col)
                        {
                            foreach(int cellPosb in mainCell.CellPossibilities)
                            {
                                Grid[inRow, col].RemovePossibility(cellPosb);
                            }
                        }
                    }
                }
            }
        }

        public void ObviousPairCol(int inRow, int inColumn)
        {
            Cell mainCell = Grid[inRow, inColumn];
            Cell child = FindPairCol(inRow, inColumn);
            if(child!=null)
            {
                for(int row = 0; row<BoardSize; row++)
                {
                    if(mainCell.CellRow != row)
                    {
                        if(row != child.CellRow)
                        {
                            foreach(int cellPosb in mainCell.CellPossibilities)
                            {
                                Grid[row, inColumn].RemovePossibility(cellPosb);
                            }
                        }
                    }
                }
            }
        }
        public void ObviousPairBlock(int inRow, int inColumn)
        {
            Cell mainCell = Grid[inRow, inColumn];
            Cell child = FindPairBlk(inRow, inColumn);
            if(child!=null)
            {
                int rowBlock = inRow/BlockSize*BlockSize;
                int columnBlock = inColumn/BlockSize*BlockSize;

                for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
                {
                    for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
                    {
                        if(outerRow != mainCell.CellRow || innerColumn != mainCell.CellColumn)
                        {
                            if(outerRow != child.CellRow || innerColumn != child.CellColumn)
                            {
                                foreach(int cellPosb in mainCell.CellPossibilities)
                                {
                                    Grid[outerRow, innerColumn].RemovePossibility(cellPosb);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Hidden Paris
        public void HiddenPair()
        {

        }
        #endregion
        #region X-Wing
        public void XWing()
        {

        }
        #endregion
        #region Y-Wing
        public void YWing()
        {
        }
        #endregion
        #region PointingPairs

        /*
        public bool PointingPairCol(int inRow, int inColumn)
        {
            // Get starting cell
            Cell mainCell = Grid[inRow, inColumn];

            // Check cell is empty
            for(int col = 0; col <BoardSize; col++)
            {
                // checks not same cell

                if(col != inRow)
                {
                    // Checks starting cell with new cell, for matching possibilities

                    if(mainCell.ComparePosibilities(Grid[col, inRow]))
                    {
                        // loops through column

                        for(int removePos = 0; removePos < BoardSize; removePos++)
                        {
                            // makes sure, the found cell, and the main cells do not get removed.

                            if(removePos!= inRow && removePos!=col)
                            {
                                // removes values from the cell if is not part of the pair.

                                foreach(int cellPosb in mainCell.CellPossibilities)
                                {
                                    Grid[removePos, inColumn].RemovePossibility(cellPosb);
                                }
                            }
                        }
                        // once a appropriate pair is found the removal runs then exits the loop, as the pair is found.

                        return true;
                    }
                }
            }

            return false;

        }
        */

        #endregion
        #region SwordFish

        public void SwordFish()
        {

        }
        #endregion
        #endregion
        /// <summary>
        /// Runs the solving methods on the board/Grid.
        /// </summary>
        /// <returns> Returns if the board has been solved or not. </returns>
        public bool SolveBoard()
        {
            bool mainExit = true;
            int a = 0;
            bool Inserted = false;
            int counter = 0;
            do
            {
                counter = 0;
                do
                {
                    Inserted = ConstraintSolve();
                    counter++;
                    if(!Inserted)
                    {
                        a++;
                    }
                }
                while(Inserted);

                counter = 0;
                do
                {
                    Inserted = EliminateSolve();
                    counter++;
                    if(!Inserted)
                    {
                        a++;
                    }
                }
                while(Inserted);
                ObviousPair();
                mainExit = a<3;
            }
            while(mainExit);

            return false;//VerifyBoard();
        }
    }
}

//#region Values
///// <summary>
///// Gets the selected inRow's values from the grid.
///// </summary>
///// <param name="inRow"> Row of the Selected cell. </param>
///// <returns> Returns all the know values in the selected inRow. </returns>
//public List<int> GetRowValues(int inRow)
//{
//    List<int> res = new List<int>();

//    for(int innerColumn = 0; innerColumn < BoardSize; innerColumn++)
//    {
//        int cell = Get(inRow, innerColumn);
//        if(cell != 0)
//        {
//            res.Add(Get(inRow, innerColumn));
//        }
//    }

//    return res;
//}

///// <summary>
///// Gets the selected column's values from the grid.
///// </summary>
///// <param name="inColumn"> Column of the Selected cell. </param>
///// <returns> Returns all the know values in the selected column. </returns>
//public List<int> GetColumnValues(int inColumn)
//{
//    List<int> res = new List<int>();

//    for(int innerRow = 0; innerRow < BoardSize; innerRow++)
//    {
//        int cell = Get(innerRow, inColumn);
//        if(cell != 0)
//        {
//            res.Add(Get(innerRow, inColumn));
//        }
//    }
//    return res;
//}

///// <summary>
///// Gets the selected block's values from the grid.
///// </summary>
///// <param name="inRow"> Row of the Selected cell. </param>
///// <param name="inColumn"> Column of the Selected cell. </param>
///// <returns> Returns all the know values in the selected block. </returns>

//public List<int> GetBlockValues(int inRow, int inColumn)
//{
//    List<int> res = new List<int>();

//    int rowBlock = inRow/BlockSize*BlockSize;
//    int columnBlock = inColumn/BlockSize*BlockSize;

//    for(int outerRow = rowBlock; outerRow < rowBlock + BlockSize; outerRow++)
//    {
//        for(int innerColumn = columnBlock; innerColumn < columnBlock + BlockSize; innerColumn++)
//        {
//            int cell = Get(outerRow, innerColumn);
//            if(cell != 0)
//            {
//                res.Add(cell);
//            }
//        }
//    }
//    return res;
//}
//#endregion
//
//
//
//
///// <summary>
///// Gets all the possible values for a selected cell based off of the inRow, column and block of the selected cell.
///// </summary>
///// <param name="inRow"> Row of the Selected cell. </param>
///// <param name="inColumn"> Column of the Selected cell. </param>
///// <returns> Returns a list of the values that can be safely inserted in to a cell. </returns>
//public List<int> GetCellPossibilitiesPop(int inRow, int inColumn)
//{
//    if(inRow >= BoardSize ||  inColumn >= BoardSize)
//    {
//        return null;
//    }
//
//    if(Get(inRow, inColumn) == 0)
//    {
//        List<int> possible = new List<int>();
//
//        for(int i = 1; i <= BoardSize; i++)
//        {
//            if(IsSafe(inRow, inColumn, i))
//            {
//                possible.Add(i);
//            }
//        }
//        return possible;
//    }
//    return null;
//}
//

//public string ToArray()
//{
//StringBuilder sb = new StringBuilder();

//for(int row = 0; row < BoardSize; row++)
//{
//sb.AppendLine();
//sb.Append("{");
//for(int column = 0; column < BoardSize; column++)
//{
//sb.Append($"{Get(row, column)},");
//}
//sb.Append("},");

//}
//sb.AppendLine();
//return sb.ToString();
//}

//public string Compact()
//{
//StringBuilder sb = new StringBuilder();

//for(int row = 0; row < BoardSize; row++)
//{
//sb.AppendLine();
//for(int column = 0; column < BoardSize; column++)
//{
//sb.Append($" ");
//if(Get(row, column) == 0)
//{
//sb.Append($" ");
//}
//else
//{
//sb.Append($"{Get(row, column)}");
//}
//}
//}
//sb.AppendLine();
//return sb.ToString();
//}

//public static string RowSep(int size)
//{
//StringBuilder sb = new StringBuilder("+");

//for(int i = 0; i < size; i++)
//{
//for(int j = 0; j < size; j++)
//{
//sb.Append("--");
//}
//sb.Append("-+");
//}

//return sb.ToString();
//}

//public void ColourBoardDisplay()
//{
//for(int row = 0; row < BoardSize; row++)
//{
//if(row%BlockSize == 0)
//{
//Console.Write($"{RowSep(BlockSize*2)}\n|");
//}
//else
//{
//Console.Write($"|");
//}
//Console.WriteLine();
//for(int column = 0; column < BoardSize; column++)
//{
//Cell cell = Grid[row, column];
//Console.Write($"|");
//if(cell.IsGiven)
//{
//Console.ForegroundColor = ConsoleColor.Gray;
//}
//else
//{
//if(cell.IsPopulated)
//{
//if(cell.isCorrect())
//{

//Console.ForegroundColor = ConsoleColor.Green;
//}
//else
//{
//Console.ForegroundColor = ConsoleColor.Red;
//}
//}
//else
//{
//Console.ForegroundColor = ConsoleColor.Blue;
//}
//}
//Console.Write($"{cell.ToString(),-8}");
//Console.ForegroundColor = ConsoleColor.Gray;
//if(((column+1)%BlockSize) == 0)
//{
//Console.Write($"|");
//}
//}
//Console.Write($"|");
//Console.WriteLine();

//}
//Console.WriteLine($"\n{RowSep(BlockSize)}");
//}
