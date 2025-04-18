//namespace SudokuBoardLibrary
//{
//    public class Cell
//    {
//        #region Properties
//        #region private
//        private int cellRow;
//        private int cellColumn;
//        private int cellBlock;

//        private int cellIndex;

//        private int cellValue;
//        private int cellSolution;
//        private bool isGiven;
//        private bool isPopulated;
//        private List<int>? cellPossibilities;
//        #endregion
//        #region SettersGetters
//        public int CellRow { get => cellRow; set => cellRow=value; }
//        public int CellColumn { get => cellColumn; set => cellColumn=value; }
//        public int CellBlock { get => cellBlock; set => cellBlock=value; }
//        public int CellIndex { get => cellIndex; set => cellIndex=value; }

//        public int CellValue
//        {
//            get => cellValue;

//            set
//            {
//                if(!IsGiven)
//                {
//                    cellValue=value;
//                    if(value == 0)
//                    {
//                        IsPopulated = false;
//                    }
//                    else if(value > 0)
//                    {
//                        IsPopulated = true;
//                    }
//                }
//                else
//                {
//                    cellValue=value;
//                    if(value == 0)
//                    {
//                        IsPopulated = false;
//                    }
//                    else if(value > 0)
//                    {
//                        IsPopulated = true;
//                    }
//                }
//            }
//        }
//        public int CellSolution { get => cellSolution; set => cellSolution=value; }
//        public bool IsGiven
//        {
//            get => isGiven;
//            set => isGiven=value;
//        }

//        public bool IsPopulated
//        {
//            get => isPopulated;
//            set => isPopulated=value;
//        }
//        public List<int>? CellPossibilities
//        {
//            get
//            {
//                if(!IsPopulated)
//                {

//                    return cellPossibilities;
//                }
//                else
//                {
//                    return null;
//                }
//            }
//            set => cellPossibilities=value;
//        }// = new List<int>();
//        #endregion
//        #endregion

//        #region Constructors
//        public Cell(int row, int col)
//        {
//            IsGiven=false;
//            CellRow=row;
//            CellColumn=col;
//            CellBlock=0;
//            CellValue=0;

//        }
//        public Cell(int row, int col, int value)
//        {
//            if(value ==0)
//            {
//                IsGiven=false;
//                CellSolution=0;
//            }
//            else
//            {
//                IsGiven=true;
//                CellSolution=value;
//            }
//            CellRow=row;
//            CellColumn=col;
//            CellBlock=0;
//            CellValue=value;
//        }

//        public Cell(int row, int col, int value, int block)
//        {
//            if(value ==0)
//            {
//                IsGiven=false;
//                CellSolution=0;
//            }
//            else
//            {
//                IsGiven=true;
//                CellSolution=value;
//            }
//            CellRow=row;
//            CellColumn=col;
//            CellBlock=block;
//            CellValue=value;
//        }
//        public void SetSolution(int solValue)
//        {
//            CellSolution=solValue;
//        }

//        public void SetValue(int value)
//        {
//            CellValue=value;
//        }
//        #endregion
//        public bool RemovePossibility(int valueToRemove)
//        {
//            if(CellPossibilities==null)
//            {
//                return false;
//            }
//            CellPossibilities.Remove(valueToRemove);
//            return true;
//        }
//        public bool RemovePossibility(ICollection<int> valuesToRemove)
//        {
//            if(CellPossibilities==null)
//            {
//                return false;
//            }
//            foreach(int valueToRemove in valuesToRemove)
//            {
//                CellPossibilities.Remove(valueToRemove);
//            }
//            return true;
//        }
//        public bool ComparePosition(Cell other)
//        {
//            return CellRow == other.CellRow && CellColumn == other.CellColumn;
//        }
//        public bool ComparePossibilities(Cell other)
//        {
//            if(VerifyCell(other))
//            {

//                if(CellPossibilities == null || other.CellPossibilities == null)
//                {
//                    return false;
//                }
//                if(CellPossibilities.Count == 2 && other.CellPossibilities.Count ==2)
//                {

//                    if(CellPossibilities[0] == other.CellPossibilities[0]
//                    && CellPossibilities[1] == other.CellPossibilities[1])
//                    {
//                        return true;
//                    }
//                }
//            }

//            return false;
//        }

//        public bool VerifyCell(Cell other)
//        {
//            if(other != null)
//            {
//                if(!IsPopulated && !other.IsPopulated)
//                {
//                    if(!ComparePosition(other))
//                    {
//                        return true;
//                    }
//                }
//            }
//            return false;
//        }

//        public bool isCorrect()
//        {
//            return CellSolution == CellValue;
//        }
//        public override bool Equals(object? obj)
//        {
//            if(obj == null)
//            {
//                return false;
//            }

//            if(obj is not Cell)
//            {
//                return false;
//            }
//            Cell? compObj = obj as Cell;
//            if(compObj == null)
//            {
//                return false;
//            }

//            if(CellRow != compObj.CellRow || CellColumn != compObj.CellColumn)
//            {
//                return false;
//            }

//            return true;
//        }

//        public override int GetHashCode() => base.GetHashCode();

//        public string PrintDebug()
//        {
//            string f = "";
//            if(CellPossibilities != null)
//            {
//                foreach(int po in CellPossibilities)
//                {
//                    f += $"{po}:";
//                }
//            }

//            return $"Cell Debug:\n" +
//                $"\tRow:{CellRow}\tColumn: {CellColumn}\tBlock:{CellBlock}\t\n" +
//                $"\tValue\t{CellValue}:[{f}]\n" +
//                $"\tGiven\t{IsGiven}\n" +
//                $"\tIsPop\t{IsPopulated}";
//        }

//        public override string ToString()
//        {
//            string f = "";
//            if(CellPossibilities != null)
//            {
//                foreach(int po in CellPossibilities)
//                {
//                    f += $"{po}:";
//                }
//            }
//            string alt ="";
//            if(CellValue != 0)
//            {
//                alt = $"{CellValue}";
//            }
//            else
//            {
//                alt = $"[{f}]";
//            }
//            return $"{alt,-16}";
//        }
//    }
//}