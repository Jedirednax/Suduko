using System.Diagnostics;

namespace SudokuBoardLibrary
{
    /// <summary>
    /// Solves Sudoku boards using basic to advanced Strategies.
    /// </summary>
    public class SudokuSolver
    {
        public Board board;

        // Current Hidden Pairs

        // Done Add Hidden pair solving
        // TODO Add Remaing in Block
        // TODO Add Hidden triple solving
        // TODO Add X-Wing
        // TODO Add XY-Wing
        // TODO Add Y-Wing
        // TODO Add SwordFish

        public SudokuSolver(Board board)
        {
            this.board=board;
        }

        #region Solving Techiques
        #region Constraint

        /// <summary>
        /// Loops through the board's open cells and checks if there is only,
        /// one possible value based off of the constraints.
        /// </summary>
        /// <returns> Returns true if was able to insert a value. </returns>
        public bool ConstraintSolve()
        {
            bool inserted = false;
            foreach(Cell va in board.openCells)
            {
                int outerRow = va.CellRow;
                int innerColumn = va.CellColumn;
                if(!board.GetCell(outerRow, innerColumn).IsPopulated)
                {
                    var tempPos = board.GetCellPossibilities(outerRow, innerColumn);
                    if(tempPos != null)
                    {

                        if(board.SafeInsert(outerRow, innerColumn, tempPos))
                        {
                            inserted = true;
                            break;
                        }
                    }
                }
            }
            Debug.WriteLineIf(inserted, "Constraint Solver");
            return inserted;
        }
        #endregion

        #region Elimination

        /// <summary>
        /// Checks for if a value can only be placed in a single cell, 
        /// based off of it is the only value in the inRow, column or block.
        /// If a single value is return in the Elim lists,
        /// then is is inserted. <see cref="SafeInsert(int, int, List{int})"/>
        /// </summary>
        /// <returns> Returns true if was able to insert a value. </returns>
        public bool EliminateSolve()
        {
            bool inserted = false;
            foreach(Cell va in board.openCells)
            {
                int outerRow = va.CellRow;
                int innerColumn = va.CellColumn;
                if(board.SafeInsert(outerRow, innerColumn, ElimRow(outerRow, innerColumn)))
                {
                    inserted = true;
                    break;
                }
                else if(board.SafeInsert(outerRow, innerColumn, ElimCol(outerRow, innerColumn)))
                {
                    inserted = true;
                    break;
                }
                else if(board.SafeInsert(outerRow, innerColumn, ElimBlock(outerRow, innerColumn)))
                {
                    inserted = true;
                    break;
                }
            }
            Debug.WriteLineIf(inserted, "Eliminate Solver");

            return inserted;
        }

        /// <summary>
        /// Loops through the selected inRow, and get all the possibilities,
        /// skipping the selected cell.
        /// Then loops though and checks if it has any values.
        /// If they are not contained in the remaining cells.
        /// Then if a single value is left returns those values.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="currCol"> Column of the Selected cell. </param>
        /// <returns> Returns a list of values the selected cell contains,
        /// that are nor present in the other Cells as possibilities in the inRow. </returns>
        public List<int>? ElimRow(int inRow, int currCol)
        {
            List<int>? currRow = board.GetCellPossibilities(inRow, currCol);
            if(currRow!=null)
            {
                List<int> hold = new List<int>();
                for(int innerColumn = 0; innerColumn < board.BoardSize; innerColumn++)
                {
                    if(innerColumn != currCol)
                    {
                        List<int>? possibleCells = board.GetCellPossibilities(inRow, innerColumn);
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
        /// Loops through the selected column, and get all the possibilities,
        /// skipping the selected cell.
        /// Then loops though and checks if it has any values
        /// that are not contained in the remaining cells.
        /// THen if a single value is left returns those values.
        /// </summary>
        /// <param name="currRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of values the selected cell contains
        /// that are nor present in the other Cells as possibilities in the Column. </returns>
        public List<int>? ElimCol(int currRow, int inColumn)
        {
            List < int >? currCol = board.GetCellPossibilities(currRow, inColumn);
            if(currCol!=null)
            {
                List<int> hold = new List<int>();
                for(int innerRow = 0; innerRow < board.BoardSize; innerRow++)
                {
                    if(innerRow != currRow)
                    {
                        List<int>? possibleCells = board.GetCellPossibilities(innerRow,inColumn);
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
        /// Loops through the selected Block, and get all the possibilities,
        /// skipping the selected cell.
        /// Then loops though and checks if it has any values
        /// that are not contained in the remaining cells.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns a list of values the selected cell contains
        /// that are nor present in the other Cells as possibilities in the block. </returns>
        public List<int>? ElimBlock(int inRow, int inColumn)
        {
            List<int>? currBlk = board.GetCellPossibilities(inRow, inColumn);
            if(currBlk!=null)
            {
                List<int> hold = new List<int>();
                int rowBlock = inRow/board.BlockSize*board.BlockSize;
                int columnBlock = inColumn/board.BlockSize*board.BlockSize;

                for(int outerRow = rowBlock; outerRow < rowBlock + board.BlockSize; outerRow++)
                {
                    for(int innerColumn = columnBlock;
                        innerColumn < columnBlock + board.BlockSize; innerColumn++)
                    {
                        if(!(outerRow == inRow && innerColumn== inColumn))
                        {
                            List<int>? possibleCells =
                                board.GetCellPossibilities(outerRow,innerColumn);
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
        /// <summary>
        /// Helper method to call backtracking solve with out specifying cords.
        /// </summary>
        /// <returns>  </returns>
        public bool BackTrackingSolve()
        {
            return BackTrackingSolve(0, 0);
        }
        /// <summary>
        /// A recursive method to solve a board using backtracking,
        /// with cell possibilities,
        /// and will generate a solution,
        /// even with "Unsolvable pairs".
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if a cell was possible </returns>
        public bool BackTrackingSolve(int inRow, int inColumn)
        {
            if(inRow == board.BoardSize-1 && inColumn ==board.BoardSize)
            {
                return true;
            }
            if(inColumn == board.BoardSize)
            {
                inRow += 1;
                inColumn = 0;
            }
            if(board.GetCell(inRow, inColumn).IsGiven)
            {
                inColumn += 1;
                return BackTrackingSolve(inRow, inColumn);
            }

            foreach(int cell in board.GetCellPossibilitiesCalculation(inRow, inColumn))
            {
                if(board.SafeInsert(inRow, inColumn, cell))
                {
                    if(BackTrackingSolve(inRow, inColumn+1))
                    {
                        return true;
                    }
                }
                board.Set(inRow, inColumn, 0);
            }


            return false;
        }
        #endregion

        #region ObviousPairs
        /// <summary>
        /// Checks the board for two cells that only certain values.
        /// That can be entered in and no where else.
        /// Like two cells both only contains 5 and 7,
        /// so the rest cannot be 5 or 7 in the block row or column.
        /// </summary>
        /// <returns> Returns if a cell was filled. </returns>
        public bool ObviousPair()
        {
            bool inserted = false;
            foreach(Cell cl in board.openCells)
            {
                if(cl.CellPossibilities==null)
                {
                    return false;
                }
                if(cl.CellPossibilities.Count==2)
                {

                    if(ObviousPairRow(cl.CellRow, cl.CellColumn))
                    {
                        inserted = true;
                    }
                    if(ObviousPairCol(cl.CellRow, cl.CellColumn))
                    {
                        inserted = true;
                    }
                    if(ObviousPairBlock(board, cl.CellRow, cl.CellColumn))
                    {
                        inserted = true;
                    }
                }

            }
            Debug.WriteLineIf(inserted, "Obvious Pair set");

            return inserted;
        }

        /// <summary>
        /// Checks for obvious pairs in a selected row.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if a possibility was found or entered. </returns>
        public bool ObviousPairRow(int inRow, int inColumn)
        {
            Cell mainCell = board.GetCell(inRow, inColumn);
            //Cell? res = null;
            var hold = board.GetUnPopulatedRowCells(inRow);
            hold.Remove(mainCell);

            foreach(var g in hold)
            {
                if(mainCell.ComparePossibilitiesPair(g))
                {
                    hold.Remove(g);
                    foreach(var t in hold)
                    {
                        t.RemovePossibilities(mainCell.CellPossibilities);
                    }
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// Checks for obvious pairs in a selected column.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if a possibility was found or entered. </returns>
        public bool ObviousPairCol(int inRow, int inColumn)
        {
            Cell mainCell = board.GetCell(inRow, inColumn);
            //Cell res = null;
            var hold = board.GetUnPopulatedColumnCells(inColumn);
            hold.Remove(mainCell);

            foreach(var g in hold)
            {
                if(mainCell.ComparePossibilitiesPair(g))
                {
                    hold.Remove(g);
                    foreach(var t in hold)
                    {
                        t.RemovePossibilities(mainCell.CellPossibilities);
                    }
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// Checks for obvious pairs in a selected block.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if a possibility was found or entered. </returns>
        public bool ObviousPairBlock(Board board, int inRow, int inColumn)
        {
            Cell mainCell = board.GetCell(inRow, inColumn);
            var hold = board.GetUnPopulatedBlockCells(inRow,inColumn);
            hold.Remove(mainCell);

            foreach(var g in hold)
            {
                if(mainCell.ComparePossibilitiesPair(g))
                {
                    hold.Remove(g);
                    foreach(var t in hold)
                    {
                        t.RemovePossibilities(mainCell.CellPossibilities);
                    }
                    return true;
                }
            }
            return false;

        }
        #endregion

        #region ObviousTriples

        /// <summary>
        /// Checks the board for three cells that only certain values.
        /// can be entered in and no where else.
        /// like three cells that can only contain 5, 8 and 7,
        /// so the rest cannot be 5,8  or 7 in the block row or column.
        /// </summary>
        /// <returns> Returns if a cell was filled. </returns>
        public bool ObviousTrip()
        {
            bool inserted = false;
            foreach(Cell cl in board.openCells)
            {
                if(cl.CellPossibilities==null)
                {
                    return false;
                }
                if(ObviousTripRow(cl.CellRow, cl.CellColumn))
                {
                    inserted = true;
                }
                else if(ObviousDisTripRow(cl.CellRow, cl.CellColumn))
                {
                    inserted = true;
                }

                if(ObviousTripCol(cl.CellRow, cl.CellColumn))
                {
                    inserted = true;
                }
                else if(ObviousDisTripCol(cl.CellRow, cl.CellColumn))
                {
                    inserted = true;
                }

                if(ObviousTripBlock(cl.CellRow, cl.CellColumn))
                {
                    inserted = true;
                }
                else if(ObviousDisTripBlock(cl.CellRow, cl.CellColumn))
                {
                    inserted = true;
                }
            }
            Debug.WriteLineIf(inserted, "Obvious Tripple set");

            return inserted;
        }
        #region Tripple
        /// <summary>
        /// Checks if the cells contain the tree same possible values only.
        /// </summary>
        /// <param name="main"> the cell to compare to another cell. </param>
        /// <param name="comp"> the Cell being compared. </param>
        /// <returns> Returns if the cells have the same three possible values. </returns>
        public bool ComparePossibilitiesTripple(Cell main, Cell comp)
        {
            if(main == null ||comp ==null)
            {
                return false;
            }
            if(main.CellPossibilities == null)
            {
                return false;
            }
            if(comp.CellPossibilities == null)
            {
                return false;
            }
            if(main.CellPossibilities.Count >3)
            {
                return false;
            }
            if(comp.CellPossibilities.Count >3)
            {
                return false;
            }
            foreach(var ce in comp.CellPossibilities)
            {

                if(!main.CellPossibilities.Contains(ce))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks for obvious triples in a selected row.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if a possibility was found or entered. </returns>
        public bool ObviousTripRow(int inRow, int inColumn)
        {
            Cell mainCell = board.GetCell(inRow, inColumn);

            var hold = board.GetUnPopulatedRowCells(inRow);
            hold.Remove(mainCell);
            var temp = new List<Cell>();
            foreach(var g in hold)
            {
                if(ComparePossibilitiesTripple(mainCell, g))
                {
                    temp.Add(g);
                }
            }
            bool res = false;
            if(temp.Count()==2)
            {
                foreach(var g in temp)
                {
                    hold.Remove(g);
                }
                foreach(var t in hold)
                {
                    res = t.RemovePossibilities(mainCell.CellPossibilities);
                }
                return true;
            }
            return res;
        }

        /// <summary>
        /// Checks for obvious triples in a selected column.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if a possibility was found or entered. </returns>
        public bool ObviousTripCol(int inRow, int inColumn)
        {
            Cell mainCell = board.GetCell(inRow, inColumn);

            var hold = board.GetUnPopulatedColumnCells(inColumn);

            hold.Remove(mainCell);
            var temp = new List<Cell>();
            foreach(var g in hold)
            {
                if(ComparePossibilitiesTripple(mainCell, g))
                {
                    temp.Add(g);
                }
            }
            bool res = false;
            if(temp.Count()==2)
            {
                foreach(var g in temp)
                {
                    hold.Remove(g);
                }
                foreach(var t in hold)
                {
                    res = t.RemovePossibilities(mainCell.CellPossibilities);
                }
                return true;
            }
            return res;
        }

        /// <summary>
        /// Checks for obvious triples in a selected block.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if a possibility was found or entered. </returns>
        public bool ObviousTripBlock(int inRow, int inColumn)
        {
            Cell mainCell = board.GetCell(inRow, inColumn);

            var hold = board.GetUnPopulatedBlockCells(inRow,inColumn);
            var temp = new List<Cell>();

            hold.Remove(mainCell);
            foreach(var g in hold)
            {
                if(ComparePossibilitiesTripple(mainCell, g))
                {
                    temp.Add(g);
                }
            }
            bool res = false;
            if(temp.Count()==2)
            {
                foreach(var g in temp)
                {
                    hold.Remove(g);
                }
                foreach(var t in hold)
                {
                    res = t.RemovePossibilities(mainCell.CellPossibilities);
                }
                return true;
            }
            return res;
        }
        #endregion
        #region DistributedTripple
        /// <summary>
        /// Checks a row for three values across three cells,
        /// and if they are distributed in sets of two across a row.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns>  </returns>
        public bool ObviousDisTripRow(int inRow, int inColumn)
        {
            bool res = false;

            Cell mainCell = board.GetCell(inRow, inColumn);
            if(mainCell.CellPossibilities.Count == 2)
            {

                var hold = board.GetUnPopulatedBlockCells(inRow,inColumn);
                var temp = new List<Cell>();
                temp.Add(mainCell);
                hold.Remove(mainCell);
                HashSet<int> ints = new HashSet<int>();
                foreach(var g in mainCell.CellPossibilities)
                {
                    ints.Add(g);
                }
                foreach(var g in hold)
                {

                    if(g.CellPossibilities.Count==2)
                    {
                        foreach(var t in g.CellPossibilities)
                        {
                            if(ints.Contains(t))
                            {
                                //ints.Add((int)t);
                                foreach(var k in g.CellPossibilities)
                                {
                                    ints.Add(k);
                                }
                                if(ints.Count >3)
                                {
                                    return false;
                                }
                                temp.Add(g);
                                break;
                            }
                        }
                    }

                }
                if(temp.Count()==3)
                {

                    foreach(var g in temp)
                    {
                        hold.Remove(g);
                    }
                    foreach(var t in hold)
                    {
                        res = t.RemovePossibilities(mainCell.CellPossibilities);
                    }
                    return true;
                }
            }
            return res;
        }
        /// <summary>
        /// Checks a row for three values across three cells,
        /// and if they are distributed in sets of two across a row.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns>  </returns>
        public bool ObviousDisTripCol(int inRow, int inColumn)
        {
            bool res = false;

            Cell mainCell = board.GetCell(inRow, inColumn);
            if(mainCell.CellPossibilities.Count == 2)
            {

                var hold = board.GetUnPopulatedBlockCells(inRow,inColumn);
                var temp = new List<Cell>();
                temp.Add(mainCell);
                hold.Remove(mainCell);
                HashSet<int> ints = new HashSet<int>();
                foreach(var g in mainCell.CellPossibilities)
                {
                    ints.Add(g);
                }
                foreach(var g in hold)
                {

                    if(g.CellPossibilities.Count==2)
                    {
                        foreach(var t in g.CellPossibilities)
                        {
                            if(ints.Contains(t))
                            {
                                //ints.Add((int)t);
                                foreach(var k in g.CellPossibilities)
                                {
                                    ints.Add(k);
                                }
                                if(ints.Count >3)
                                {
                                    return false;
                                }
                                temp.Add(g);
                                break;
                            }
                        }
                    }

                }
                if(temp.Count()==3)
                {

                    foreach(var g in temp)
                    {
                        hold.Remove(g);
                    }
                    foreach(var t in hold)
                    {
                        res = t.RemovePossibilities(mainCell.CellPossibilities);
                    }
                    return true;
                }
            }
            return res;
        }
        /// <summary>
        /// Checks a row for three values across three cells,
        /// and if they are distributed in sets of two across a row.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns>  </returns>
        public bool ObviousDisTripBlock(int inRow, int inColumn)
        {
            bool res = false;

            Cell mainCell = board.GetCell(inRow, inColumn);
            if(mainCell.CellPossibilities.Count == 2)
            {
                var hold = board.GetUnPopulatedBlockCells(inRow,inColumn);
                var temp = new List<Cell>();
                temp.Add(mainCell);
                hold.Remove(mainCell);
                HashSet<int> ints = new HashSet<int>();
                foreach(var g in mainCell.CellPossibilities)
                {
                    ints.Add(g);
                }
                foreach(var g in hold)
                {
                    if(g.CellPossibilities.Count==2)
                    {
                        foreach(var t in g.CellPossibilities)
                        {
                            if(ints.Contains(t))
                            {
                                foreach(var k in g.CellPossibilities)
                                {
                                    ints.Add(k);
                                }
                                if(ints.Count >3)
                                {
                                    return false;
                                }
                                temp.Add(g);
                                break;
                            }
                        }
                    }
                }
                if(temp.Count()==3)
                {
                    foreach(var g in temp)
                    {
                        hold.Remove(g);
                    }
                    foreach(var t in hold)
                    {
                        res = t.RemovePossibilities(mainCell.CellPossibilities);
                    }
                    return true;
                }
            }
            return res;
        }
        #endregion
        #endregion

        #region Hidden Paris
        /// <summary>
        /// Checks the board for two cells that only certain values.
        /// That can be entered in and no where else.
        /// Like two cells both only contains 5 and 7,
        ///
        /// get the test cell
        /// check what values it contains
        /// Then search for if any other cells contain those values, check that only one other cell has those values.

        /// so the rest cannot be 5 or 7 in the block row or column.
        /// </summary>
        /// <returns> Returns if a cell was filled. </returns>
        public bool HiddenPair()
        {

            bool inserted = false;

            //Debug.WriteLine(t.PrintDebug());

            foreach(Cell cl in board.openCells)
            {
                if(cl.CellPossibilities==null)
                {
                    return false;
                }
                if(cl.CellPossibilities.Count==2)
                {


                    if(HiddenPairRow(cl.CellRow, cl.CellColumn))
                    {
                        inserted = true;
                    }
                    if(HiddenPairCol(cl.CellRow, cl.CellColumn))
                    {
                        inserted = true;
                    }
                    if(HiddenPairBlock(cl.CellRow, cl.CellColumn))
                    {
                        inserted = true;
                    }
                }


            }
            Debug.WriteLineIf(inserted, "Hidden Pair set");

            return inserted;
        }

        /// <summary>
        /// Checks for obvious pairs in a selected row.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if a possibility was found or entered. </returns>
        public bool HiddenPairRow(int inRow, int inColumn)
        {
            var posis = new int[9] { 0,0,0,0,0,0,0,0,0};
            var openCElls = board.GetUnPopulatedRowCells(inRow);
            foreach(var t in openCElls)
            {
                if(t.CellPossibilities.Count > 0)
                {
                    foreach(var g in t.CellPossibilities)
                    {
                        posis[g-1]++;
                    }
                }
            }

            var vals = board.GetRowPossibilities(inRow);
            var ToSave = new List<int>();
            for(int i = 0; i < board.BoardSize; i++)
            {
                if(posis[i] == 2)
                {
                    ToSave.Add(i+1);
                    //Debug.WriteLine(posis[i], "ToKeep");
                    //vals.Remove(i+1);
                    vals.RemoveAll(x => x ==i+1);
                    //    Debug.WriteLine(i+1, "ToAlter");
                }
                //  else
                //  {
                //
                //      Debug.WriteLine(posis[i], "ToLarge");
                //  }
            }
            //foreach(var t in ToSave)
            //{
            //    Debug.WriteLine(t, "Save");
            //}

            var saftyList = new List<Cell>();
            if(ToSave.Count > 2)
            {

                foreach(var f in openCElls)
                {
                    //  Debug.WriteLine(f.PrintDebug());
                    if(f.CellPossibilities.Contains(ToSave[0]) || f.CellPossibilities.Contains(ToSave[1]))
                    {
                        saftyList.Add(f);
                    }
                    //    Debug.WriteLine(f.PrintDebug());
                }

            }

            if(saftyList.Count ==2)
            {
                foreach(var f in saftyList)
                {

                    f.RemovePossibilities(vals);
                }
            }

            return false;

        }

        /// <summary>
        /// Checks for obvious pairs in a selected column.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if a possibility was found or entered. </returns>
        public bool HiddenPairCol(int inRow, int inColumn)
        {
            var posis = new int[9] { 0,0,0,0,0,0,0,0,0};
            var openCElls = board.GetUnPopulatedColumnCells(inColumn);
            foreach(var t in openCElls)
            {
                if(t.CellPossibilities.Count > 0)
                {
                    foreach(var g in t.CellPossibilities)
                    {
                        posis[g-1]++;
                    }
                }
            }

            var vals = board.GetColumnPossibilities(inColumn);
            var ToSave = new List<int>();
            for(int i = 0; i < board.BoardSize; i++)
            {
                if(posis[i] == 2)
                {
                    ToSave.Add(i+1);
                    vals.RemoveAll(x => x ==i+1);
                }
            }

            var saftyList = new List<Cell>();
            if(ToSave.Count ==2)
            {
                foreach(var f in openCElls)
                {
                    if(f.CellPossibilities.Contains(ToSave[0]) || f.CellPossibilities.Contains(ToSave[1]))
                    {
                        saftyList.Add(f);
                    }
                }
            }

            if(saftyList.Count ==2)
            {
                foreach(var f in saftyList)
                {
                    f.RemovePossibilities(vals);
                }
            }

            return false;

        }

        /// <summary>
        /// Checks for obvious pairs in a selected block.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if a possibility was found or entered. </returns>
        public bool HiddenPairBlock(int inRow, int inColumn)
        {
            var posis = new int[9] { 0,0,0,0,0,0,0,0,0};
            var openCElls = board.GetUnPopulatedBlockCells(inRow, inColumn);
            foreach(var t in openCElls)
            {
                if(t.CellPossibilities.Count > 0)
                {
                    foreach(var g in t.CellPossibilities)
                    {
                        posis[g-1]++;
                    }
                }
            }

            var vals = board.GetBlockPossibilities(inRow, inColumn);
            var ToSave = new List<int>();
            for(int i = 0; i < board.BoardSize; i++)
            {
                if(posis[i] == 2)
                {
                    ToSave.Add(i+1);
                    vals.RemoveAll(x => x ==i+1);
                }
            }

            var saftyList = new List<Cell>();
            if(ToSave.Count ==2)
            {
                foreach(var f in openCElls)
                {
                    if(f.CellPossibilities.Contains(ToSave[0]) || f.CellPossibilities.Contains(ToSave[1]))
                    {
                        saftyList.Add(f);
                    }
                }
            }

            if(saftyList.Count ==2)
            {
                foreach(var f in saftyList)
                {
                    f.RemovePossibilities(vals);
                }
            }

            return false;


        }
        #endregion

        #region PointingPairs
        /// <summary>
        /// Checks values do not eliminate a row column or block by having only a certain value,
        /// present on only one row or column in a block.
        /// </summary>
        /// <returns>  </returns>
        public bool PointingPair()
        {
            bool inserted = false;
            foreach(var t in board.openCells)
            {
                if(
                PointingPairRow(t.CellRow, t.CellColumn))
                {
                    inserted = true;
                }
                if(PointingPairCol(t.CellRow, t.CellColumn))
                {
                    inserted = true;
                }
            }
            return inserted;
        }

        /// <summary>
        /// Checks the columns for pointing pairs.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if an instance of pointing pairs was found. </returns>
        public bool PointingPairCol(int inRow, int inColumn)
        {
            Cell startCell = board.GetCell(inRow, inColumn);

            // GetCell cells block.
            // Loop through other columns of block.
            var fullBlock = board.GetBlockPossibilities(inRow, inColumn);

            HashSet<int> uniqueValues = new HashSet<int>();
            foreach(int cellPosb in fullBlock)
            {
                uniqueValues.Add(cellPosb);
            }

            // Check if column has possibilities.
            int blockSize = board.BlockSize;
            int rowOffset = inRow/board.BlockSize*board.BlockSize;
            int colOffset = inColumn/board.BlockSize*board.BlockSize;
            int co = 0;
            for(int blkRow = rowOffset; blkRow < (rowOffset + blockSize); blkRow++)
            {
                var j = board.GetCellPossibilities(blkRow, inColumn);
                if(j != null)
                {
                    // If they don't remove from current column.
                    foreach(int f in j)
                    {
                        fullBlock.Remove(f);
                    }
                    co++;
                }
            }
            if(co < 2)
            {
                return false;
            }

            // Check oif the contain any values form current column.
            int missingValue = 0;
            foreach(var g in uniqueValues)
            {
                if(!fullBlock.Contains(g))
                {
                    missingValue = g;
                    break;
                }
            }

            if(missingValue > 0)
            {
                for(int row = 0; row < board.Grid.GetLength(0); row++)
                {
                    if(board.GetCell(row, inColumn).CellBlock != startCell.CellBlock)
                    {
                        board.Grid[row, inColumn].RemovePossibilities(missingValue);
                    }
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks the columns for pointing pairs.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if an instance of pointing pairs was found. </returns>
        public bool PointingPairRow(int inRow, int inColumn)
        {
            Cell startCell = board.GetCell(inRow, inColumn);

            // GetCell cells block.
            // Loop through other columns of block.
            var fullBlock = board.GetBlockPossibilities(inRow, inColumn);

            HashSet<int> uniqueValues = new HashSet<int>();
            foreach(int cellPosb in fullBlock)
            {
                uniqueValues.Add(cellPosb);
            }

            // CHeck if column has possibilities.
            int blockSize = board.BlockSize;
            int rowOffset = inRow/board.BlockSize*board.BlockSize;
            int colOffset = inColumn/board.BlockSize*board.BlockSize;
            int ro = 0;
            for(int blkCol = colOffset; blkCol <(colOffset + blockSize); blkCol++)
            {
                var j = board.GetCellPossibilities(inRow, blkCol);
                if(j != null)
                {
                    // If they don't remove from current column.
                    foreach(int f in j)
                    {
                        fullBlock.Remove(f);
                    }
                    ro++;
                }
            }
            if(ro<2)
            {
                return false;
            }

            // Check oif the contain any values form current column.
            int missingValue = 0;
            foreach(var g in uniqueValues)
            {
                if(!fullBlock.Contains(g))
                {
                    missingValue = g;
                    break;
                }
            }

            if(missingValue > 0)
            {
                for(int col = 0; col < board.Grid.GetLength(0); col++)
                {
                    if(board.GetCell(inRow, col).CellBlock != startCell.CellBlock)
                    {
                        board.Grid[inRow, col].RemovePossibilities(missingValue);
                    }
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks the columns for pointing pairs.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if an instance of pointing triples was found. </returns>
        public bool PointingTripleCol(int inRow, int inColumn)
        {
            Cell startCell = board.GetCell(inRow, inColumn);

            // GetCell cells block.
            // Loop through other columns of block.
            var fullBlock = board.GetBlockPossibilities(inRow, inColumn);

            HashSet<int> uniqueValues = new HashSet<int>();
            foreach(int cellPosb in fullBlock)
            {
                uniqueValues.Add(cellPosb);
            }

            // Check if column has possibilities.
            int blockSize = board.BlockSize;
            int rowOffset = inRow/board.BlockSize*board.BlockSize;
            int colOffset = inColumn/board.BlockSize*board.BlockSize;
            int co = 0;
            for(int blkRow = rowOffset; blkRow < (rowOffset + blockSize); blkRow++)
            {
                var j = board.GetCellPossibilities(blkRow, inColumn);
                if(j != null)
                {
                    // If they don't remove from current column.
                    foreach(int f in j)
                    {
                        fullBlock.Remove(f);
                    }
                    co++;
                }
            }
            if(co < 2)
            {
                return false;
            }

            // Check oif the contain any values form current column.
            int missingValue = 0;
            foreach(var g in uniqueValues)
            {
                if(!fullBlock.Contains(g))
                {
                    missingValue = g;
                    break;
                }
            }

            if(missingValue > 0)
            {
                for(int row = 0; row < board.Grid.GetLength(0); row++)
                {
                    if(board.GetCell(row, inColumn).CellBlock != startCell.CellBlock)
                    {
                        board.Grid[row, inColumn].RemovePossibilities(missingValue);
                    }
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks the columns for pointing pairs.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if an instance of pointing triples was found. </returns>
        public bool PointingTripleRow(int inRow, int inColumn)
        {
            Cell startCell = board.GetCell(inRow, inColumn);

            // GetCell cells block.
            // Loop through other columns of block.
            var fullBlock = board.GetBlockPossibilities(inRow, inColumn);

            HashSet<int> uniqueValues = new HashSet<int>();
            foreach(int cellPosb in fullBlock)
            {
                uniqueValues.Add(cellPosb);
            }

            // CHeck if column has possibilities.
            int blockSize = board.BlockSize;
            int rowOffset = inRow/board.BlockSize*board.BlockSize;
            int colOffset = inColumn/board.BlockSize*board.BlockSize;
            int ro = 0;
            for(int blkCol = colOffset; blkCol <(colOffset + blockSize); blkCol++)
            {
                var j = board.GetCellPossibilities(inRow, blkCol);
                if(j != null)
                {
                    // If they don't remove from current column.
                    foreach(int f in j)
                    {
                        fullBlock.Remove(f);
                    }
                    ro++;
                }
            }
            if(ro<2)
            {
                return false;
            }

            // Check oif the contain any values form current column.
            int missingValue = 0;
            foreach(var g in uniqueValues)
            {
                if(!fullBlock.Contains(g))
                {
                    missingValue = g;
                    break;
                }
            }

            if(missingValue > 0)
            {
                for(int col = 0; col < board.Grid.GetLength(0); col++)
                {
                    if(board.GetCell(inRow, col).CellBlock != startCell.CellBlock)
                    {
                        board.Grid[inRow, col].RemovePossibilities(missingValue);
                    }
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks values do not eliminate a row column or block by having only a certain value,
        /// present on only one row or column in a block.
        /// </summary>
        /// <returns>  </returns>
        public bool RemainingBlocks()
        {
            foreach(var t in board.openCells)
            {
                RemainingBlockRow(t.CellRow, t.CellColumn);
                RemainingBlockColumn(t.CellRow, t.CellColumn);
            }
            return false;
        }
        /// <summary>
        /// Checks the columns for pointing pairs.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if an instance of pointing pairs was found. </returns>
        public bool RemainingBlockRow(int inRow, int inColumn)
        {
            Cell startCell = board.GetCell(inRow, inColumn);

            var cellBlock = startCell.CellBlock;
            var tempList = new HashSet<int>();
            foreach(var outtter in board.GetUnPopulatedRowCells(startCell.CellRow))
            {
                if(outtter.CellBlock != startCell.CellBlock)
                {
                    foreach(var cp in outtter.CellPossibilities)
                    {
                        tempList.Add(cp);
                    }
                }
            }
            var tempHOld = new List<int>();
            var hold = startCell.CellPossibilities;
            foreach(var g in hold)
            {
                if(!tempList.Contains(g))
                {
                    tempHOld.Add(g);
                }
            }


            //foreach(var f in tempHOld)
            //{
            //    Debug.WriteLine(f);
            //
            //}



            var blockOther = board.GetUnPopulatedBlockCells(inRow,inColumn);

            //blockOther.RemoveAll(x => x.CellRow == inRow);
            foreach(var cl in blockOther)
            {
                if(cl.CellRow != startCell.CellRow)
                {
                    cl.RemovePossibilities(tempHOld);
                }
            }
            return false;
        }
        /// <summary>
        /// Checks the columns for pointing pairs.
        /// </summary>
        /// <param name="inRow"> Row of the Selected cell. </param>
        /// <param name="inColumn"> Column of the Selected cell. </param>
        /// <returns> Returns if an instance of pointing pairs was found. </returns>
        public bool RemainingBlockColumn(int inRow, int inColumn)
        {
            Cell startCell = board.GetCell(inRow, inColumn);

            var cellBlock = startCell.CellBlock;
            var tempList = new HashSet<int>();
            foreach(var outtter in board.GetUnPopulatedColumnCells(startCell.CellColumn))
            {
                if(outtter.CellBlock != startCell.CellBlock)
                {
                    foreach(var cp in outtter.CellPossibilities)
                    {
                        tempList.Add(cp);
                    }
                }
            }
            var tempHOld = new List<int>();
            var hold = startCell.CellPossibilities;
            foreach(var g in hold)
            {
                if(!tempList.Contains(g))
                {
                    tempHOld.Add(g);
                }
            }


            //foreach(var f in tempHOld)
            //{
            //    Debug.WriteLine(f);
            //
            //}



            var blockOther = board.GetUnPopulatedBlockCells(inRow,inColumn);

            //blockOther.RemoveAll(x => x.CellRow == inRow);
            foreach(var cl in blockOther)
            {
                if(cl.CellColumn != startCell.CellColumn)
                {
                    cl.RemovePossibilities(tempHOld);
                }
            }
            return false;
        }
        #endregion
        #region XY-Wing
        public bool XYWing()
        {
            foreach(var t in board.openCells)
            {
                XYWingRowCol(t.CellRow, t.CellColumn);

            }
            return false;
        }

        public void XYWingRowCol(int inRow, int inColumn)
        {
            //(2,6);
            // Get starting cell

            Cell startCell = board.GetCell(inRow, inColumn);
            Debug.WriteLine($"{inRow}:{inColumn}\n {startCell}", "StartCells");
            if(startCell.CellPossibilities.Count == 2)
            {
                Debug.WriteLine("Checking Rows");
                // Set x and y values to reference
                int X = startCell.CellPossibilities[0];
                int Y = startCell.CellPossibilities[1];

                // Holder for value to be removed.
                int Z = 0;

                Cell? rowCell = startCell;
                Cell? columnCell = startCell;

                // Check each Column in the row for matching values
                for(int i = 0; i < 9; i++)
                {
                    if(i != inColumn)
                    {
                        //row value contating Y
                        var tempCell = board.GetCell(inRow, i);


                        if(tempCell.CellPossibilities != null && tempCell.CellPossibilities.Count == 2 && tempCell != startCell)
                        {
                            Debug.WriteLine(tempCell.PrintDebug(), "Loop A");
                            if(tempCell.CellPossibilities.Contains(X) ||
                                tempCell.CellPossibilities.Contains(Y))
                            {

                                Debug.WriteLine(tempCell.PrintDebug(), "Loop B");
                                rowCell = tempCell;
                                break;
                            }
                        }
                    }
                }

                Debug.WriteLine("");
                Debug.WriteLine("Checking Columns");

                // Check each row in the column for matching values
                for(int i = 0; i < 9; i++)
                {
                    if(i != inRow)
                    {

                        // column value contating X
                        var tempCell = board.GetCell(i, inColumn);


                        if(tempCell.CellPossibilities != null && tempCell.CellPossibilities.Count == 2 && tempCell != rowCell && tempCell != startCell)
                        {
                            if(tempCell.CellPossibilities != rowCell.CellPossibilities)
                            {

                                Debug.WriteLine(tempCell.PrintDebug(), "Loop A");
                                if(tempCell.CellPossibilities.Contains(X) ||
                                    tempCell.CellPossibilities.Contains(Y))
                                {

                                    Debug.WriteLine(tempCell.PrintDebug(), "Loop B");
                                    columnCell = tempCell;
                                    break;
                                }
                            }
                        }
                    }

                }

                var targetCell = board.GetCell(columnCell.CellRow,rowCell.CellColumn);

                int valRem = 0;
                //var allPosisbilitiess = columnCell.CellPossibilities
                HashSet<int> allPos = new HashSet<int>();
                foreach(var h in rowCell.CellPossibilities)
                {

                    allPos.Add(h);
                }
                foreach(var h in columnCell.CellPossibilities)
                {

                    allPos.Add(h);
                }

                foreach(var h in allPos)
                {
                    if(rowCell.CellPossibilities.Contains(h) && columnCell.CellPossibilities.Contains(h))
                    {
                        valRem = h;
                        break;
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("");
                Debug.WriteLine(valRem);
                Debug.WriteLine(targetCell.PrintDebug(), "Target being changed");

                targetCell.RemovePossibilities(valRem);
            }
            Console.ReadLine();
            board.ColourBoardDisplay();
        }







        public void XYWingBlockRow(int inRow, int inColumn)
        {

        }
        public void XYWingBlockCol(int inRow, int inColumn)
        {

        }


        #endregion
        #region X-Wing
        public void XWing()
        {

        }

        public void XWingRow()
        {

        }

        public void XWingColumn()
        {

        }
        #endregion

        #region Y-Wing
        public void YWing()
        {
        }
        #endregion


        #region SwordFish

        public void SwordFish()
        {

        }
        #endregion
        #endregion
        /// <summary>
        /// Updates the possibilities of the cells on the board for analysis techniques.
        /// </summary>
        public void UpdatePos()
        {
            PointingPair();
            ObviousPair();
            ObviousTrip();
            HiddenPair();
            RemainingBlocks();
            XYWing();

        }

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
            UpdatePos();
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
                    else
                    {
                        UpdatePos();
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
                    else
                    {
                        UpdatePos();
                    }
                }
                while(Inserted);

                // Debug.WriteLine("asd");
                //PointingPair();
                //ObviousPair();
                //ObviousTrip();
                UpdatePos();

                mainExit = a<=6;
            }
            while(mainExit);
            //HiddenPair();
            return board.VerifyBoard();
        }
    }
}
