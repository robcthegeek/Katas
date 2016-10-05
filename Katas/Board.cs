namespace Katas
{
    public class Board
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Board()
        {
            Rows = 8;
            Columns = 8;
        }
    }
}