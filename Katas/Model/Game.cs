using System.Collections.Generic;
using System.Linq;

namespace Katas.Model
{
    public class Game
    {
        public Player WhitePlayer { get; private set; }
        public Player BlackPlayer { get; private set; }
        public IList<Piece> Pieces { get; private set; }

        public Board Board { get; private set; }

        public Game()
        {
            WhitePlayer = new Player
            {
                Type = PlayerType.White
            };

            BlackPlayer = new Player
            {
                Type = PlayerType.Black
            };

            Pieces = Enumerable.Range(0, 24)
                .Select(i => new Piece((i % 2 == 0 ? PieceColor.Black : PieceColor.White)))
                .ToList();

            Board = new Board();

        }

        public void Reset()
        {
            Board.Reset();
        }
    }
}
