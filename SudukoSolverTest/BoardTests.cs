namespace SudukoBoardLibary.Tests
{
    [TestClass()]
    public class BoardTests
    {

        public static Board EmptyBoard;
        public static Board Populated;
        public static Board Unsolved;
        public static Board Solved;
        public static Board Broken;

        [TestMethod()]
        public void SafeInsertTest()
        {
            Assert.Fail();
        }
    }
}

/*
{1,2,3,4,5,6,7,8,9}
{4,5,6,7,8,9,1,2,3}
{7,8,9,1,2,3,4,5,6}
{9,1,2,3,4,5,6,7,8}
{3,4,5,6,7,8,9,1,2}
{6,7,8,9,1,2,3,4,5}
{8,9,1,2,3,4,5,6,7}
{2,3,4,5,6,7,8,9,1}
{5,6,7,8,9,1,2,3,4}
*/