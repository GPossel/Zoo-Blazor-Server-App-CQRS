using Infrastructure.Entities.Followers;
using Infrastructure.Users;
using SharedKernel;

namespace Infrastructure.Entities.Users
{
    public sealed class UserDbo : Entity
    {
        public EmailDbo Email { get; set; } = null!;
        public NameDbo Name { get; set; } = null!;
        public bool HasPublicProfile { get; set; }

        public ICollection<FollowerDbo> Followers { get; set; } = new List<FollowerDbo>();

        public UserDbo()
        {

        }

        public UserDbo(EmailDbo email, NameDbo name, bool hasPublicProfile)
        {
            Email = email;
            Name = name;
            HasPublicProfile = hasPublicProfile;
        }

        public UserDbo(Guid id, EmailDbo email, NameDbo name, bool hasPublicProfile)
            : base(id)
        {
            Email = email;
            Name = name;
            HasPublicProfile = hasPublicProfile;
        }

        public static UserDbo Create(EmailDbo email, NameDbo name, bool hasPublicProfile)
        {
            var user = new UserDbo(Guid.NewGuid(), email, name, hasPublicProfile);

            user.Raise(new UserCreatedDomainEvent(user.Id));
            return user;
        }

    }
}