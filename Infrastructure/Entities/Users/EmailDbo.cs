using Infrastructure.Entities.Users;
using SharedKernel;

namespace Infrastructure.Users
{
    // Dependent (child)
    public sealed class EmailDbo : Entity
    {
        public Guid UserId { get; set; } // Required foreign key property
        public UserDbo User { get; set; } = null!; // Required reference navigation to principal
        public string EmailAddress { get; set; }
        public bool IsConfirmed { get; private set; }
        public bool IsSubscribedToNewsletter { get; private set; }

        private EmailDbo(string value) => EmailAddress = value;

        public EmailDbo(Guid id) : base(id) { }

        public EmailDbo(Guid id, string value) : base(id)
        {
            EmailAddress = value;
        }

        public static Result<EmailDbo> Create(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<EmailDbo>(EmailErrors.Empty);
            }

            if (email.Split('@').Length != 2)
            {
                return Result.Failure<EmailDbo>(EmailErrors.InvalidFormat);
            }

            return new EmailDbo(Guid.NewGuid(), email);
        }
    }

    public static class EmailErrors
    {
        public static readonly Error Empty = new("Email.Empty", "Email is empty");
        public static readonly Error InvalidFormat = new("Email.InvalidFormat", "Email format is empty");
    }
}