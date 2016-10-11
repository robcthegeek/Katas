using Katas.Model;
using System.Collections.Generic;

namespace Katas.Archive
{
    public class Player
    {
        public IList<Piece> Pieces { get; set; }

        public PlayerType Type { get; set; }

    }

    public enum PlayerType
    {
        White,
        Black
    }
}
