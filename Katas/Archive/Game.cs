using System.Collections.Generic;
using System.Linq;

namespace Katas.Archive
{
    public class Game
    {
        private readonly Board _board;

        private readonly IList<Player> _players;

        public Game()
        {
            _board = new Board();

            _players = new List<Player>
            {
                new Player
                {
                    Type = PlayerType.Black,
                    Pieces = _board.CreateBlackPieces()
                },
                new Player
                {
                    Type = PlayerType.White,
                    Pieces = _board.CreateWhitePieces()
                }
            };
        }

        public Player Play()
        {
            var endCondition = false;

            Player winner = null;

            while (!endCondition)
            {

                foreach (var player in _players)
                {
                    Move(player);
                }

                endCondition = CheckGameEndCondition(out winner);
            }

            return winner;
        }

        private bool CheckGameEndCondition(out Player winner)
        {
            winner = _players.FirstOrDefault(x => x.Pieces.All(p => p.State == PieceState.Off));  // need to check the condition not possible moving
            return winner != null;
        }



        public void Move(Player player)
        {
            switch (player.Type)
            {
                case PlayerType.White:
                    _board.MoveWhitePlayer(player);
                    break;
                case PlayerType.Black:
                    _board.MoveBlackPlayer(player);
                    break;
            }
        }

    }
}
