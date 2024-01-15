using SharedKernel;

namespace Domain.Users
{
    public sealed record Email
    {
        public string EmailAddress { get; set; }
        public bool IsConfirmed { get; private set; }
        public bool IsSubscribedToNewsletter { get; private set; }

        private Email(string value) => EmailAddress = value;

        public static Result<Email> Create(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<Email>(EmailErrors.Empty);
            }

            if (email.Split('@').Length != 2)
            {
                return Result.Failure<Email>(EmailErrors.InvalidFormat);
            }

            return new Email(email);
        }
    }

    public static class EmailErrors
    {
        public static readonly Error Empty = new("Email.Empty", "Email is empty");
        public static readonly Error InvalidFormat = new("Email.InvalidFormat", "Email format is empty");
    }
}