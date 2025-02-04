namespace SudukoBoardLibary
{
    public class SudukoBoardGenerator
    {

        public int BoardSize = 9;
        public int BlockSize = 3;
        public Cell[,] Grid;
        public static Random random = new Random();
        public int[,] Solution= new int[9,9];
        SudokuSolver solver;
        public int NumberOfCells = 75;
        public Cell[,] GenerateBoard()
        {
            int[] values = new int[BoardSize];

            for(int i = 1; i <= BoardSize; i++)
            {
                values[i-1] = i;
            }
            int[,] board = new int[9, 9];
            for(int row = 0; row < BlockSize; row++)
            {
                int rowBlock = row*3/BlockSize*BlockSize;
                int columnBlock = row*3/BlockSize*BlockSize;
                int blockIndex = 0;
                random.Shuffle(values);
                for(int i = rowBlock; i < rowBlock + BlockSize; i++)
                {
                    for(int j = columnBlock; j < columnBlock + BlockSize; j++)
                    {
                        board[i, j] = values[blockIndex++];
                    }
                }
            }
            //            Solution = new int[BoardSize, BoardSize];
            SudokuSolver solver = new SudokuSolver(board);
            solver.SolveBoard();
            solver.BackTrackingSolve(0, 0);
            return solver.Grid;
        }

        public Cell[,] SetBoard(int clues)
        {
            Cell[,] bo = GenerateBoard();
            List<(int,int)> clue = new List<(int, int)> ();

            for(int rowF = 0; rowF < NumberOfCells -clues;)
            {
                int x = random.Next(BoardSize);
                int y = random.Next(BoardSize);
                if(!bo[x, y].IsPopulated)
                {
                    bo[x, y].SetValue(0);
                    clue.Add((x, y));
                
                    if(solver.SolveBoard())
                    {
                        foreach((int, int) pos in clue)
                        {
                            Grid[pos.Item1, pos.Item2].SetValue( 0);
                        }
                        rowF++;
                    }
                    else
                    {
                        clue.Remove((x, y));
                    }
                }
            }
            return bo;
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
        public void InitilizeGrid(ref int[,] board)
        {
            for(int row = 0; row < Grid.GetLength(0); row++)
            {
                for(int col = 0; col < Grid.GetLength(1); col++)
                {
                    board[row, col] = 0;
                }
            }
        }

        /// <summary>
        /// This method takes Two board amd Sets teh values from pne to the other.
        /// </summary>
        /// <param name="to"> BoardGrid being set </param>
        /// <param name="from"> Board/Grid values being taken </param>
        public static void SetAToB(ref int[,] to, int[,] from)
        {
            for(int row = 0; row < to.GetLength(0); row++)
            {
                for(int col = 0; col < to.GetLength(1); col++)
                {
                    to[row, col] = from[row, col];
                }
            }
        }
    }
}
