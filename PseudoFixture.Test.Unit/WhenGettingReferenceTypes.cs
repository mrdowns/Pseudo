using NUnit.Framework;
using PseudoFixture.Test.Unit.Helpers;

namespace PseudoFixture.Test.Unit
{
    [TestFixture]
    public class WhenGettingReferenceTypes : SpecFor<ReferenceTypesDummy>
    {
        public override ReferenceTypesDummy When()
        {
            return new Pseudo<ReferenceTypesDummy>().Create();
        }

        [Test]
        public void ItRandomizesTheObjectsValueTypeProperties()
        {
            var id = Subject.Reference.Id;

            Assert.That(Subject.Reference.Id, Is.GreaterThan(0));
            Assert.That(Subject.Reference.Id, new IdempotentEqualityConstraint(id));
        }

        [Test]
        public void ItRandomizesTheObjectsStringProperties()
        {
            string name = Subject.Reference.Name;
            Assert.That(name, Is.Not.Null);
            Assert.That(name, Is.Not.EqualTo(string.Empty));
            Assert.That(name, new IdempotentEqualityConstraint(name));
        }
    }
}