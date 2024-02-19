using Infrastructure.Entities.Users;
using SharedKernel;

namespace Infrastructure.Users
{
    // Dependent (child)
    public sealed class NameDbo : Entity
    {
        public Guid UserId { get; set; } // Required foreign key property
        public UserDbo User { get; set; } = null!; // Required reference navigation to principal
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }

        public NameDbo(Guid id) : base(id)
        {
        }

        public static NameDbo Create(string firstName, string lastName, string? middleName = "")
        {
            var name = new NameDbo(Guid.NewGuid())
            {
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName
            };

            return name;
        }
    }
}
