using System;
using System.Collections.Generic;
using System.Text;
using Katas;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private List<string> _animalList;
        private Animalia _animalia;

        [SetUp]
        public void SetUp()
        {
            _animalList = new List<string>() { "pig", "goat", "tiger", "rat" };
            _animalia = new Animalia(_animalList);
        }

        [Test]
        public void Say_WithNoPrevious_OK()
        {
            Assert.True(_animalia.Say("pig"));
        }

        [Test]
        public void Say_FirstLetterMatchesLastOfPrevious_OK()
        {
            _animalia.Say("pig");
            Assert.True(_animalia.Say("goat"));
        }

        [Test]
        public void Say_FirstLetterDoesntMatchLastOfPrevious_NotOK()
        {
            _animalia.Say("pig");
            Assert.False(_animalia.Say("tiger"));
        }

        [Test]
        public void Say_PreviouslyUsedAnimal_NotOK()
        {
            _animalia.Say("tiger");
            _animalia.Say("rat");
            Assert.False(_animalia.Say("tiger"));
        }

        [Test]
        public void Say_CaseInsensitive()
        {
            _animalia.Say("tiger");
            Assert.True(_animalia.Say("RAT"));
        }

        [Test]
        public void Say_NotAnAnimal_NotOK()
        {
            Assert.False(_animalia.Say("car beep beep"));
        }
    }
}
