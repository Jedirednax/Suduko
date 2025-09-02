﻿namespace SudokuBoardLibrary
{
    public class SudukoBoardGenerator
    {

        public int BoardSize;
        public int BlockSize;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Cell[,] Grid;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public static Random random = new Random();
        //public int[,] Solution= new int[9,9];

        //public int NumberOfCells = 75;
        public Board GenerateBoard(int size = 9)
        {
            int[] values = new int[size];
            BoardSize = size;
            BlockSize = (int)Math.Sqrt(size);
            for(int i = 1; i <= BoardSize; i++)
            {
                values[i-1] = i;
            }

            int[,] boardDiagonal = new int[BoardSize, BoardSize];
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
                        boardDiagonal[i, j] = values[blockIndex++];
                    }
                }
            }
            var board = new Board(boardDiagonal);
            SudokuSolver solver = new SudokuSolver(board);
            solver.BackTrackingSolve();
            board.SetValidSolution();
            board.SetFinal();


            return board;
        }


        public Board SetBoard(int clues)
        {
            // test number of clues 50;
            BoardSize = 9;
            //Board board = GenerateBoard();
            // For testing the Seting of clues on the board wors
            Board board = new Board(new int[,]{
            {1,2,3,4,5,6,7,8,9},
            {4,5,6,7,8,9,1,2,3},
            {7,8,9,1,2,3,4,5,6},
            {9,1,2,3,4,5,6,7,8},
            {3,4,5,6,7,8,9,1,2},
            {6,7,8,9,1,2,3,4,5},
            {8,9,1,2,3,4,5,6,7},
            {2,3,4,5,6,7,8,9,1},
            {5,6,7,8,9,1,2,3,4},
                            });

            Stack<Cell> stack = new Stack<Cell>();

            SudokuSolver solver = new SudokuSolver(board);
            var openCell = new List<Cell>();
            foreach(var item in board.Grid)
            {
                openCell.Add(item);
            }
            var tempSh = openCell.ToArray();
            random.Shuffle(tempSh);
            Stack<Cell> use = new Stack<Cell>();
            foreach(var item in tempSh)
            {
                use.Push(item);
            }
            do
            {
                var rCell = use.Pop();
                board.SetOpen(rCell.CellRow, rCell.CellColumn);
                board.ResetBoard();
                if(solver.SolveBoard())
                {
                    //Debug.Assert(board.VerifyBoard());
                    stack.Push(rCell);
                }
                else
                {
                    rCell.SetGiven();

                    board.SetGiven(rCell.CellRow, rCell.CellColumn);
                    //use.Prepend(rCell);
                }

            } while(stack.Count<=(BoardSize*BoardSize) - clues && use.Count>0);
            board.ResetBoard();
            return board;
        }
    }
}
/*
{0,7,0,0,0,0,6,0,5},
{0,3,0,6,2,0,0,0,0},
{0,0,0,4,3,5,0,8,7},
{0,2,0,0,0,6,0,9,3},
{0,0,6,0,0,3,1,0,4},
{0,0,7,9,4,0,0,0,6},
{0,4,8,3,0,0,5,6,1},
{0,1,0,0,6,4,0,7,0},
{0,0,9,0,1,0,0,4,0},
*/