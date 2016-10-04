namespace Katas.Model
{
    public class Board
    {
        public Piece AddPiece(int column, int row)
        {
            // TODO!
            var piece = new Piece();
            return piece;
        }
    }

    public class Piece
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public void MoveUpRight()
        {
            Row = 1;
            Column = 1;
        }

        public void MoveUpLeft()
        {
            Row = 1;
            Column = 6;
        }
    }
}
