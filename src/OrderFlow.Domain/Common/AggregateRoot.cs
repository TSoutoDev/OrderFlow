namespace OrderFlow.Domain.Common
{
    public class AggregateRoot : Entity
    {
        protected AggregateRoot() { }

        protected AggregateRoot(Guid id) : base(id) { }
    }
}