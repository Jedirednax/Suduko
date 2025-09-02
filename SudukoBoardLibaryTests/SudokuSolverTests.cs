namespace SudokuBoardLibrary.Tests
{
    [TestClass()]
    public class SudokuSolverTests
    {
        [TestMethod()]
        public void SudokuSolverTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ConstraintSolveTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void EliminateSolveTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ElimRowTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ElimColTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ElimBlockTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void BackTrackingSolveTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void BackTrackingSolveTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObviousPairTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObviousPairRowTest()
        {
            int row = 1;
            int col = 1;
            var tripValues = (new int[] { 5, 7 });

            int[,] InBoard = BoardTypes.NakedPairInARow;

            Board board = new Board(InBoard);
            SudokuSolver solver = new SudokuSolver(board);

            solver.ObviousPairRow(row, col);
            var cells = board.GetRowCells(row);

            //CollectionAssert.IsNotSubsetOf(cells[0].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[1].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[2].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[3].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[4].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[5].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[6].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[7].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[8].CellPossibilities, tripValues);
        }

        [TestMethod()]
        public void ObviousPairColTest()
        {
            int row = 2;
            int col = 3;
            var tripValues = (new int[] { 4, 7 });

            int[,] InBoard = BoardTypes.NakedPairInAColumn;

            Board board = new Board(InBoard);
            SudokuSolver solver = new SudokuSolver(board);

            solver.ObviousPairCol(row, col);
            var cells = board.GetColumnCells(col);

            CollectionAssert.IsNotSubsetOf(cells[0].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[1].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[2].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[3].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[4].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[5].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[6].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[7].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[8].CellPossibilities, tripValues);
        }

        [TestMethod()]
        public void ObviousPairBlockTest()
        {
            int row = 0;
            int col = 7;
            var tripValues = (new int[] { 5, 8 });

            int[,] InBoard = BoardTypes.NakedPairInABlock;

            Board board = new Board(InBoard);
            SudokuSolver solver = new SudokuSolver(board);

            solver.ObviousTripBlock(row, col);
            var cells = board.GetBlockCells(row,col);

            //CollectionAssert.IsNotSubsetOf(cells[0].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[1].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[2].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[5].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[3].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[4].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[6].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[7].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[8].CellPossibilities, tripValues);
        }

        [TestMethod()]
        public void ObviousTripTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ComparePossibilitiesTrippleTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObviousTripRowTest()
        {
            int row = 6;
            int col = 3;
            var tripValues = (new int[] { 1, 4, 7 });

            int[,] InBoard = BoardTypes.NakedTripletInARow;

            Board board = new Board(InBoard);
            SudokuSolver solver = new SudokuSolver(board);

            solver.ObviousTripRow(row, col);
            var cells = board.GetRowCells(row);

            CollectionAssert.IsNotSubsetOf(cells[0].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[1].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[2].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[3].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[4].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[5].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[6].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[7].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[8].CellPossibilities, tripValues);
        }

        [TestMethod()]
        public void ObviousTripColTest()
        {
            int row = 3;
            int col = 5;
            var tripValues = (new int[] { 2,8,9 });

            int[,] InBoard = BoardTypes.NakedTripletInAColumn;

            Board board = new Board(InBoard);
            SudokuSolver solver = new SudokuSolver(board);

            solver.ObviousTripCol(row, col);
            var cells = board.GetColumnCells(col);

            //CollectionAssert.IsNotSubsetOf(cells[0].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[1].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[2].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[3].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[4].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[5].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[6].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[7].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[8].CellPossibilities, tripValues);
        }

        [TestMethod()]
        public void ObviousTripBlockTest()
        {
            int row = 5;
            int col = 6;
            var tripValues = (new int[] { 7,5,3 });

            int[,] InBoard = BoardTypes.NakedTripletInABlock;

            Board board = new Board(InBoard);
            SudokuSolver solver = new SudokuSolver(board);

            solver.ObviousTripBlock(row, col);
            var cells = board.GetBlockCells(row,col);

            CollectionAssert.IsNotSubsetOf(cells[0].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[1].CellPossibilities, tripValues);
            //CollectionAssert.IsNotSubsetOf(cells[2].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[3].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[4].CellPossibilities, tripValues);
            CollectionAssert.IsNotSubsetOf(cells[5].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[6].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[7].CellPossibilities, tripValues);
            CollectionAssert.IsSubsetOf(cells[8].CellPossibilities, tripValues);
        }

        [TestMethod()]
        public void ObviousDisTripRowTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObviousDisTripColTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObviousDisTripBlockTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void HiddenPairTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void HiddenPairRowTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void HiddenPairColTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void HiddenPairBlockTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PointingPairTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PointingPairColTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PointingPairRowTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void XWingTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void YWingTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SwordFishTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UpdatePosTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SolveBoardTest()
        {
            throw new NotImplementedException();
        }
    }
}