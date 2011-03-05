using NUnit.Framework;

namespace PseudoFixture.Test.Unit.Helpers
{
    public abstract class SpecFor<T>
    {
        [SetUp]
        public void SetUp()
        {
            Given();
            Subject = When();
        }

        protected T Subject { get; private set; }

        public virtual void Given(){}

        public abstract T When();
    }
}