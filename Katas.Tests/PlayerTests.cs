using System;
using NUnit.Framework;

namespace Katas.Tests
{
    public class PlayerTests : AutoTestFixture
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void Ctor_NullEmptyOrWhitespaceName_ThrowsArgumentException(string value)
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new Player(value));

            Assert.That(ex.ParamName, Is.EqualTo("name"));
        }

        [Test]
        public void Ctor_NameGiven_SetsName()
        {
            var expected = "my-name";

            var player = new Player(expected);

            Assert.That(player.Name, Is.EqualTo(expected));
        }
    }
}