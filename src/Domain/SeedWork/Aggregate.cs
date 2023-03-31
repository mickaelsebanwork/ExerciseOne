using System.Collections.Generic;

namespace Exercise_1.Domain.SeedWork
{
    public abstract class Aggregate : Entity, IAggregateRoot
    {
        private readonly Queue<IDomainEvent> _domainEvents = new();

        public IEnumerable<IDomainEvent> GetDomainEvents()
        {
            while (_domainEvents.Count > 0)
            {
                yield return _domainEvents.Dequeue();
            }
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Enqueue(domainEvent);
        }
    }
}