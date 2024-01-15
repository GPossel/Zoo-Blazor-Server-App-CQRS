namespace Domain.Users
{
    public sealed record Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }

        public Name(string firstName, string lastName, string? middleName = "")
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }
    }
}
