namespace Katas.Model
{
    public class Board
    {
        public Piece AddPiece(int column, int row)
        {
            // TODO!
            var piece = new Piece(PieceColor.Black);
            return piece;
        }

        public Square Square(int location)
        {
            return new Square { Piece = new Piece(PieceColor.Black) };
        }
    }
}