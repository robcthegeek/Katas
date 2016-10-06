using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public class Board
    {
        private const int MaxRequiredPlaces = 32;
        private const int MaxAllowedPieces = 24;

        public List<Place> Places { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public List<Piece> Pieces { get; private set; }

        public Board()
        {
            Rows = 8;
            Columns = 8;

            Pieces = new List<Piece>();

            Places = new List<Place>(
                Enumerable.Range(0, MaxRequiredPlaces)
                .Select(i => new Place()));
        }

        public void Reset()
        {
            Pieces = new List<Piece>();

            Pieces.AddRange(Enumerable.Range(0, MaxAllowedPieces)
                .Select(i => new Piece(i % 2 == 0 ? PieceColor.Black : PieceColor.White)));
        }
    }

    public class Place
    {

    }
}