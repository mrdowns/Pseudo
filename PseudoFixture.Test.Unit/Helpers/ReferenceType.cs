namespace PseudoFixture.Test.Unit.Helpers
{
    public class ReferenceType
    {
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
        public virtual ReferenceType CircularReference { get; set; }
    }
}
