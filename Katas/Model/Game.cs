using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas.Model
{
    public class Game
    {
        private readonly ICoinFlipper _flipper;
        public Player WhitePlayer { get; private set; }
        public Player BlackPlayer { get; private set; }
        public IList<Piece> Pieces { get; private set; }

        public Board Board { get; private set; }
        public Player NextPlayer { get; private set; }

        public Game(ICoinFlipper flipper)
        {
            _flipper = flipper;

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

        public void Start(Player player1, Player player2)
        {

            if (player1 == null) throw new ArgumentNullException(nameof(player1));
            if (player2 == null) throw new ArgumentNullException(nameof(player2));


            if (_flipper.PickPlayer() == PlayerType.Black)
            {
                BlackPlayer = player1;
                WhitePlayer = player2;

            }

            if (_flipper.PickPlayer() == PlayerType.White)
            {
                WhitePlayer = player1;
                BlackPlayer = player2;
            }

            NextPlayer = BlackPlayer;
        }

    }

    public interface ICoinFlipper
    {
        PlayerType PickPlayer();
    }
}
