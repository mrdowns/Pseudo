using System;
using NUnit.Framework;

namespace PseudoFixture.Test.Unit
{
    [TestFixture]
    public class WhenGettingValueTypes_ItReturnsRandomAndIdempotentValues
    {
        private Random _random;
        public ValueTypesDummy Dummy { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            _random = NonRandom.Random;

            Dummy = new Pseudo<ValueTypesDummy>(NonRandom.Random).Create();
        }

        [Test]
        public void Int()
        {
            AssertIdempotentEquality(() => Dummy.Int, _random.Next());
        }

        [Test]
        public void String()
        {
            AssertIdempotentEquality(() => Dummy.String, _random.Next().ToString());
        }

        [Test]
        public void Byte()
        {
            var b = new byte[1];
            _random.NextBytes(b);

            AssertIdempotentEquality(() => Dummy.Byte, b[0]);
        }

        [Test]
        public void Long()
        {
            AssertIdempotentEquality(() => Dummy.Long, _random.Next());
        }

        [Test]
        public void Short()
        {
            AssertIdempotentEquality(() => Dummy.Short, _random.Next(short.MaxValue));
        }

        [Test]
        public void Char()
        {
            AssertIdempotentEquality(() => Dummy.Char,  (char)_random.Next(33, 126));
        }

        [Test]
        public void Float()
        {
            double expected = _random.NextDouble();
            Assert.That(Math.Abs(Dummy.Float - expected), Is.LessThanOrEqualTo(.0001));
            Assert.That(Math.Abs(Dummy.Float - expected), Is.LessThanOrEqualTo(.0001));
        }

        [Test]
        public void Double()
        {
            double expected = _random.NextDouble();
            Assert.That(Math.Abs(Dummy.Double - expected), Is.LessThanOrEqualTo(.0001));
            Assert.That(Math.Abs(Dummy.Double - expected), Is.LessThanOrEqualTo(.0001));
        }

        [Test]
        public void BoolReturnsFalse()
        {
            AssertIdempotentEquality(() => Dummy.Bool, false);
        }

        private static void AssertIdempotentEquality<T>(Func<T> method, T expected)
        {
            Assert.That(method(), Is.EqualTo(expected));
            Assert.That(method(), Is.EqualTo(expected));
            Assert.That(method(), Is.EqualTo(expected));
        }

        internal class NonRandom
        {
            public static Random Random { get { return new Random(123); } }
        }
    }
}