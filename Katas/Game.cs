using System;

namespace Katas
{
    public class Game
    {
        public Player Player1 { get; }
        public Player Player2 { get; }
        public Board Board { get; }
        public GameStatus Status { get; }
        public Player WhitePlayer { get; }
        public Player BlackPlayer { get; }

        public Game(Player player1, Player player2)
        {
            if (player1 == null)
                throw new MissingPlayerException();
            if (player2 == null)
                throw new MissingPlayerException();

            Player1 = player1;
            Player2 = player2;

            Board = new Board();
            Status = GameStatus.Waiting;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}