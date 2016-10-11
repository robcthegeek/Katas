using System.Collections.Generic;

namespace Katas.Model
{
    public class Game
    {
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
        }
        public Player WhitePlayer { get; private set; }
        public Player BlackPlayer { get; private set; }

        public IList<Piece> Pieces { get; private set; }

    }
}
