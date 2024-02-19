namespace Domain.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public Email Email { get; private set; }
        public Name Name { get; private set; }
        public bool HasPublicProfile { get; set; }

    }
}
