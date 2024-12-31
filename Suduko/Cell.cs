namespace Suduko
{
    public class Cell : IComparable
    {
        public Cell()
        {
        }

        public int xPos { get; set; }
        public int yPos { get; set; }
        public double block { get; set; }

        public int value { get; set; } = 0;
        public int[] possibleValue { get; set; } = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public Cell(int xPos, int yPos, double block, int value)
        {
            this.xPos=xPos;
            this.yPos=yPos;
            this.block=block;
            this.value=value;
        }

        public override string ToString()
        {
            //return $"{Math.Round(block, 2),4}";
            return $"{value}";
            //return $"x:{xPos,2}|y:{yPos,2}|b:{Math.Round(block, 2),4}|v:{value,2}";
        }

        public override bool Equals(object? obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public int CompareTo(object? obj) => value.CompareTo(obj);
    }
}
/*{

}*/