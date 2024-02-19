using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedKernel
{
    public abstract class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; init; }


        private readonly List<IDomainEvent> _domainEvents = new();
        public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

        protected Entity(Guid id)
        {
            Id = id;
        }

        protected Entity()
        {

        }

        protected void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

    }
}
