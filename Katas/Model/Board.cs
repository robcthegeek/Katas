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
}