using System;

namespace Katas
{
    public class Game
    {
        private readonly ICoinFlipper _flipper;

        public Player Player1 { get; }
        public Player Player2 { get; }
        public Board Board { get; }
        public GameStatus Status { get; private set; }
        public Player WhitePlayer { get; private set; }
        public Player BlackPlayer { get; private set; }
        public Player NextPlayer { get; private set; }

        public Game(Player player1, Player player2, ICoinFlipper flipper)
        {
            if (player1 == null)
                throw new MissingPlayerException();
            if (player2 == null)
                throw new MissingPlayerException();
            if (flipper == null)
                throw new ArgumentNullException(nameof(flipper));

            Player1 = player1;
            Player2 = player2;

            _flipper = flipper;

            Board = new Board();
            Status = GameStatus.Waiting;
        }

        public void Start()
        {
            var flip = _flipper.Flip();

            BlackPlayer = flip == CoinSide.Heads ? Player1 : Player2;
            WhitePlayer = flip == CoinSide.Heads ? Player2 : Player1;

            Status = GameStatus.Playing;

            NextPlayer = BlackPlayer;
        }
    }
}