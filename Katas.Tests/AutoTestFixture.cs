using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace Katas.Tests
{
    [TestFixture]
    public abstract class AutoTestFixture
    {
        protected IFixture Fixture { get; private set; }

        [SetUp]
        public void SetUp()
        {
            Fixture = new Fixture()
                .Customize(new AutoNSubstituteCustomization());
        }
    }
}