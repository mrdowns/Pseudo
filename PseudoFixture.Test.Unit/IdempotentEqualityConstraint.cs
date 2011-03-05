using NUnit.Framework.Constraints;

namespace PseudoFixture.Test.Unit
{
    public class IdempotentEqualityConstraint : Constraint
    {
        private readonly object _expected;
        private readonly EqualConstraint _equalsConstraint;

        public IdempotentEqualityConstraint(object expected) : base(expected)
        {
            _expected = expected;
            _equalsConstraint = new EqualConstraint(expected);
        }

        public override bool Matches(object actual)
        {
            this.actual = actual;
            for(int i = 0; i < 3; i++)
            {
                if (!_equalsConstraint.Matches(actual)) return false;
            }
            return true;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            _equalsConstraint.WriteDescriptionTo(writer);
        }
    }
}