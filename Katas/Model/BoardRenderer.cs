using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var oddRow = string.Join("", Enumerable.Range(1, 8).Select(column => column % 2 == 0 ? "▓" : "▒"));
            var evenRow = string.Join("", Enumerable.Range(1, 8).Select(column => column % 2 == 0 ? "▒" : "▓"));

            return string.Join(
                Environment.NewLine,
                Enumerable.Range(1, 8)
                    .Select(j => j % 2 == 0 ? evenRow : oddRow));

        }
    }
}
