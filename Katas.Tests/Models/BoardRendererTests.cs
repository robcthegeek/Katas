using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Katas.Model;
using NUnit.Framework;

namespace Katas.Tests.Models
{
    [TestFixture]
    public class BoardRendererTests
    {
        [Test]
        public void Ctor_NullBoard_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new BoardRenderer(null));
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

        // TODO (RC): Actually start rendering the pieces from the board!
        // Remember that the Board only tracks pieces on Black places!
    }
}
