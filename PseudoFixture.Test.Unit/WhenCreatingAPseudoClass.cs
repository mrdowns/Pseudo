using NUnit.Framework;
using PseudoFixture.Test.Unit.Helpers;

namespace PseudoFixture.Test.Unit
{
    [TestFixture]
    public class WhenCreatingAPseudoClass
    {
        private ValueTypesDummy _dummy;

        [SetUp]
        public void EstablishContext()
        {
            _dummy = new Pseudo<ValueTypesDummy>().Create();
        }

        [Test]
        public void ItReturnsTheCorrectType()
        {
            Assert.That(_dummy.GetType().BaseType, Is.EqualTo(typeof(ValueTypesDummy)));
        }
    }
}