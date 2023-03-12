namespace UserLibrary
{
    public class Role
    {
        public string Name { get; }
        public Guid Id { get; }

        public Role(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Role role)
            {
                return string.Equals(Name, role.Name, StringComparison.OrdinalIgnoreCase) && Equals(Id, role.Id);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}