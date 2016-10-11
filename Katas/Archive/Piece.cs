using Katas.Model;

namespace Katas.Archive
{
    public class Piece
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsKing { get; set; }
        public PieceState State { get; set; }

        public PieceColor Color { get; set; }


    }

    public enum PieceState
    {
        On,
        Off
    }
}
