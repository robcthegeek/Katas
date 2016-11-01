using System;
using System.Linq;

namespace Katas.Model
{
    public class BoardRenderer
    {
        private Board _board;

        public BoardRenderer(Board board)
        {
            if (board == null) throw new ArgumentNullException(nameof(board));

            _board = board;
        }

        public string Render()
        {
            var blackIndex = 0;

            var newBoard = string.Join(Environment.NewLine,
                                       Enumerable.Range(1, 8)
                                                 .Select(j => j % 2 == 0 ?
                                                  string.Join("", Enumerable.Range(1, 8).Select(column => column % 2 == 0 ? GetBlackSquare(ref blackIndex) : "▓")) :
                                                  string.Join("", Enumerable.Range(1, 8).Select(column => column % 2 == 0 ? "▓" : GetBlackSquare(ref blackIndex)))));

            return newBoard;
        }

        private string GetBlackSquare(ref int blackIndex)
        {
            var result = "▒";

            if (_board.Squares.Any() && _board.Squares[blackIndex].Piece != null)
            {

                switch (_board.Squares[blackIndex].Piece.Color)
                {
                    case PieceColor.White:
                        result = "●";
                        break;
                    case PieceColor.Black:
                        result = "○";
                        break;
                }
            }

            blackIndex++;
            return result;
        }
    }
}
