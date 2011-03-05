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
            Assert.That(Subject.Reference.Name, Is.Not.Null);
            Assert.That(Subject.Reference.Name, Is.Not.EqualTo(string.Empty));
            Assert.That(Subject.Reference.Name, new IdempotentEqualityConstraint(Subject.Reference.Name));
        }

        [Test]
        public void ItHandlesCircularReferences()
        {
            var circularId = Subject.Reference.CircularReference.CircularReference.CircularReference.CircularReference.Id;

            Assert.That(circularId, Is.GreaterThan(0));
            Assert.That(Subject.Reference.CircularReference.CircularReference.CircularReference.CircularReference.Id, 
                new IdempotentEqualityConstraint(circularId));
        }

        [Test]
        public void ItReturnsADifferentObjectForEachLevelOfCircularReference()
        {
            Assert.That(
                Subject.Reference.CircularReference.Id, 
                Is.Not.EqualTo(Subject.Reference.CircularReference.CircularReference.Id));
        }
    }
}