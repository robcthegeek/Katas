namespace Katas.Model
{
    public class Board
    {
        public Piece AddPiece(int column, int row)
        {
            // TODO!
            var piece = new Piece(column, row);
            return piece;
        }
    }

    public class Piece
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public Piece(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public void MoveUpRight()
        {
            Row = 1;
            Column = 1;
        }

        public void MoveUpLeft()
        {
            if (Column == 0)
                throw new IllegalMoveException();

            Row = 1;
            Column = 6;
        }
    }
}
