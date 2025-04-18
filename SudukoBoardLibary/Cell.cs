namespace SudokuBoardLibrary
{
    public class Cell
    {
        #region Properties
        private int cellRow;
        private int cellColumn;
        private int cellBlock;

        private int cellValue = 0;
        private int cellSolution = 0;

        private bool isGiven = false;
        private bool isPopulated = false;

        private List<int> cellPossibilities = new List<int>();

        public int CellRow
        {
            get => cellRow;
            set => cellRow=value;
        }
        public int CellColumn
        {
            get => cellColumn;
            set => cellColumn=value;
        }
        public int CellValue
        {
            get => cellValue;
            set
            {
                if(cellValue != value)
                {
                    cellValue=value;
                }
                IsPopulated = (value != 0);

            }
        }
        public int CellBlock
        {
            get => cellBlock;
            set => cellBlock=value;
        }
        public int CellSolution
        {
            get => cellSolution;
            set => cellSolution=value;
        }
        public bool IsGiven
        {
            get => isGiven;
            set => isGiven=value;
        }
        public bool IsPopulated
        {
            get => isPopulated;
            set => isPopulated=value;
        }
        public List<int> CellPossibilities
        {
            get
            {
                return cellPossibilities;
            }

            set
            {
                cellPossibilities=value;
            }
        }
        #endregion

        #region Constructors
        public Cell(int cellRow, int cellColumn)
        {
            CellRow=cellRow;
            CellColumn=cellColumn;
        }

        public Cell(int cellRow, int cellColumn, int cellValue) : this(cellRow, cellColumn)
        {
            CellValue=cellValue;
            CellSolution = CellValue;
            if(cellValue > 0)
            {
                IsGiven = true;
                IsPopulated = true;
            }
        }

        #endregion

        #region Sets
        public void Set(int setValue)
        {
            CellValue=setValue;
            if(cellValue > 0)
            {
                CellPossibilities.Clear();
            }
        }

        public void SetGiven()
        {
            IsGiven = true;
            CellValue = CellSolution;
            CellPossibilities = new List<int>();
        }

        public void SetGiven(int newValue)
        {

            CellValue=newValue;

            IsGiven = true;
            CellSolution = CellValue;
            CellPossibilities.Clear();
        }

        public void SetOpen()
        {

            CellValue = 0;
            IsGiven = false;
            //CellPossibilities.Clear();
        }

        public void SetOpen(int newValue)
        {
            CellValue = 0;
            IsGiven = false;
            CellPossibilities.Clear();
        }

        public void Reset()
        {
            CellValue = 0;
            CellPossibilities.Clear();
        }
        public void Reveal()
        {
            if(CellSolution!=0)
            {
                CellValue = CellSolution;
            }
        }
        public void SetSolution()
        {
            CellSolution = CellValue;
        }


        public bool SetPossibilities(int newPossibilities)
        {
            if(!IsPopulated)
            {
                if(CellPossibilities == null)
                {
                    CellPossibilities = new List<int>();
                }
                if(!CellPossibilities.Contains(newPossibilities))
                {

                    CellPossibilities.Add(newPossibilities);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SetPossibilities(List<int> newPossibilities)
        {
            if(!IsPopulated)
            {
                if(CellPossibilities == null)
                {
                    CellPossibilities = new List<int>();
                }

                foreach(var newPos in newPossibilities)
                {

                    if(!CellPossibilities.Contains(newPos))
                    {

                        CellPossibilities.Add(newPos);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemovePossibilities(int valueToRemove)
        {
            if(CellPossibilities == null || CellPossibilities.Count == 0)
            {
                return false;
            }
            return CellPossibilities.Remove(valueToRemove);
        }

        public bool RemovePossibilities(List<int> valuesToRemove)
        {
            bool removed = false;
            if(CellPossibilities == null || CellPossibilities.Count == 0)
            {
                return false;
            }

            foreach(int valueToRemove in valuesToRemove)
            {
                if(CellPossibilities.Remove(valueToRemove))
                {
                    removed = true;
                }
            }

            return removed;
        }
        #endregion

        #region Gets
        #endregion

        #region Checks
        public bool IsCorrect()
        {
            return CellValue == CellSolution;
        }
        public bool ComparePossibilitiesPair(Cell comp)
        {
            if(comp == null)
            {
                return false;
            }
            if(CellPossibilities == null)
            {
                return false;
            }
            if(comp.CellPossibilities == null)
            {
                return false;
            }
            if(CellPossibilities.Count !=2)
            {
                return false;
            }
            if(comp.CellPossibilities.Count !=2)
            {
                return false;
            }
            //if(CellPossibilities.Any(comp.CellPossibilities.Contains))

            return CellPossibilities.SequenceEqual(comp.CellPossibilities);
        }


        public bool ComparePosition(Cell comp)
        {
            if(comp== null)
            {
                return false;
            }
            if(CellRow != comp.CellRow)
            {
                return false;
            }
            if(CellColumn != comp.CellColumn)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Displays
        public override string ToString()
        {
            string f = "";
            if(CellPossibilities != null)
            {
                foreach(int po in CellPossibilities)
                {
                    f += $"{po}:";
                }
            }
            string alt ="";
            if(CellPossibilities != null && CellPossibilities.Count > 0)
            {

                alt = $"[{f}]";
            }
            else
            {
                alt = $"{CellValue}";
            }

            return $"{alt,-20}";
        }

        public string PrintDebug()
        {
            string f = "";
            if(CellPossibilities != null)
            {
                foreach(int po in CellPossibilities)
                {
                    f += $"{po}:";
                }
            }

            return $"Cell Debug:\n" +
                $"\tRow:{CellRow}\tColumn: {CellColumn}\tBlock:{CellBlock}\t\n" +
                $"\tValue\t{CellValue}:[{f}]:{CellSolution}\n" +
                $"\tGiven\t{IsGiven}\n" +
                $"\tIsPop\t{IsPopulated}";
        }
        #endregion
    }
}