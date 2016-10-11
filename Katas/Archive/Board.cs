using System;
using System.Collections.Generic;
using Katas.Model;

namespace Katas.Archive
{
    public class Board
    {
        private readonly Square[,] _squares = new Square[8, 8];

        public Board()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _squares[i, j] = new Square();
                }
            }
        }

        public IList<Piece> CreateBlackPieces()
        {
            throw new System.NotImplementedException();
        }

        public IList<Piece> CreateWhitePieces()
        {
            throw new System.NotImplementedException();
        }

        internal void MoveWhitePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        internal void MoveBlackPlayer(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
