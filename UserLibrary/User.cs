namespace UserLibrary
{
    public class User
    {
        public string FullName { get; }
        public Guid Id { get; }
        public Role Role { get; }

        public User(string fullName, Role role)
        {
            Role = role;
            FullName = fullName;
            Id = Guid.NewGuid();
        }

        public override bool Equals(object? obj)
        {
            if (obj is User user)
            {
                return string.Equals(user.FullName, FullName) && Equals(user.Id, Id) && Equals(user.Role, Role);
            }

            return false;
        }

        public override string ToString()
        {
            return $"User fullName: {FullName}\n User role: {Role}";
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + FullName.GetHashCode() + Role.GetHashCode();
        }
    }
}