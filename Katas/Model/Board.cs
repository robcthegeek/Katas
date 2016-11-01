using System.Collections.Generic;
using System.Linq;

namespace Katas.Model
{
    public class Board
    {
        public IList<Square> Squares { get; private set; }

        public Board()
        {
            Squares = new List<Square>(32);
        }

        /// <summary>
        /// Returns the <see cref="Square"/> at the given location.
        /// NOTE: Locations are base-1 (from 1 to 32).
        /// </summary>
        public Square Square(int location)
        {
            return Squares[location - 1];
        }

        public void Reset()
        {
            Squares = Enumerable.Range(0, 32)
                                .Select(i =>
                                {
                                    if (i < 12)
                                        return new Square { Piece = new Piece(PieceColor.Black) };
                                    if (i > 19)
                                        return new Square { Piece = new Piece(PieceColor.White) };

                                    return new Square();
                                })
                                .ToList();
        }
    }
}