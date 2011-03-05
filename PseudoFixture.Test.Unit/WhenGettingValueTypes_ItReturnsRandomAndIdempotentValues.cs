using System;
using NUnit.Framework;
using PseudoFixture.Test.Unit.Helpers;

namespace PseudoFixture.Test.Unit
{
    [TestFixture]
    public class WhenGettingValueTypes_ItReturnsRandomAndIdempotentValues : SpecFor<ValueTypesDummy>
    {
        private const int SEED = 123;

        private Random _random;

        public override void Given()
        {
            _random = new Random(SEED);
        }

        public override ValueTypesDummy When()
        {
            return new Pseudo<ValueTypesDummy>(new Random(SEED)).Create();
        }

        [Test]
        public void Int()
        {
            Assert.That(Subject.Int, new IdempotentEqualityConstraint(_random.Next()));
        }

        [Test]
        public void Byte()
        {
            var b = new byte[1];
            _random.NextBytes(b);

            Assert.That(Subject.Byte, new IdempotentEqualityConstraint(b[0]));
        }

        [Test]
        public void Long()
        {
            Assert.That(Subject.Long, new IdempotentEqualityConstraint((long) _random.Next()));
        }

        [Test]
        public void Short()
        {
            Assert.That(Subject.Short, new IdempotentEqualityConstraint(_random.Next(short.MaxValue)));
        }

        [Test]
        public void Char()
        {
            Assert.That(Subject.Char, new IdempotentEqualityConstraint((char) _random.Next(33, 126)));
        }

        [Test]
        public void Float()
        {
            double expected = _random.NextDouble();
            Assert.That(Math.Abs(Subject.Float - expected), Is.LessThanOrEqualTo(.0001));
            Assert.That(Math.Abs(Subject.Float - expected), Is.LessThanOrEqualTo(.0001));
        }

        [Test]
        public void Double()
        {
            double expected = _random.NextDouble();
            Assert.That(Math.Abs(Subject.Double - expected), Is.LessThanOrEqualTo(.0001));
            Assert.That(Math.Abs(Subject.Double - expected), Is.LessThanOrEqualTo(.0001));
        }

        [Test]
        public void BoolReturnsFalse()
        {
            Assert.That(Subject.Bool, new IdempotentEqualityConstraint(false));
        }
    }
}