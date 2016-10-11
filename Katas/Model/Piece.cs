using Katas;
using Katas.Archive;
using Katas.Model;

public class Piece
{
    public PieceColor Color { get; private set; }

    public Piece(PieceColor color)
    {
        Color = color;
    }
}