namespace PseudoFixture.Test.Unit
{
    public class ReferenceType
    {
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
        public ReferenceType CircularReference { get; set; }
    }
}
