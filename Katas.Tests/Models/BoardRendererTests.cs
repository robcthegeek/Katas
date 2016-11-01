using Katas.Model;
using NUnit.Framework;
using System;

namespace Katas.Tests.Models
{
    [TestFixture]
    public class BoardRendererTests
    {
        [Test]
        public void Ctor_NullBoard_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new BoardRenderer(null));
        }

        [Test]
        public void Render_ANewBoard_AcceptBoardInConstructor()
        {
            var boardRenderer = new BoardRenderer(new Board());

            var expected =
                "▒▓▒▓▒▓▒▓" + Environment.NewLine +
                "▓▒▓▒▓▒▓▒" + Environment.NewLine +
                "▒▓▒▓▒▓▒▓" + Environment.NewLine +
                "▓▒▓▒▓▒▓▒" + Environment.NewLine +
                "▒▓▒▓▒▓▒▓" + Environment.NewLine +
                "▓▒▓▒▓▒▓▒" + Environment.NewLine +
                "▒▓▒▓▒▓▒▓" + Environment.NewLine +
                "▓▒▓▒▓▒▓▒";

            var squares = boardRenderer.Render();

            Assert.That(squares, Is.EqualTo(expected));
        }

        [Test]
        public void Render_ANewBoard_MapBoardSquares()
        {
            var board = new Board();
            board.Reset();

            var boardRenderer = new BoardRenderer(board);

            var expected =
               "○▓○▓○▓○▓" + Environment.NewLine +
               "▓○▓○▓○▓○" + Environment.NewLine +
               "○▓○▓○▓○▓" + Environment.NewLine +
               "▓▒▓▒▓▒▓▒" + Environment.NewLine +
               "▒▓▒▓▒▓▒▓" + Environment.NewLine +
               "▓●▓●▓●▓●" + Environment.NewLine +
               "●▓●▓●▓●▓" + Environment.NewLine +
               "▓●▓●▓●▓●";

            var squares = boardRenderer.Render();

            Assert.That(squares, Is.EqualTo(expected));
        }

    }
}
