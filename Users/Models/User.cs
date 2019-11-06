using System;

namespace Users.Models
{
    public class User
    {
        private User() { }
        public User(Guid id, string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException(nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException(nameof(lastName));

            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
