namespace UserLibrary
{
	public class User
	{
		public string Name { get; init; }
		public Guid ID { get; init; }
		public Role Role { get; init; }

		public User(string name, string surname, Role role)
		{
			Role = role;
			Name = name;
			ID = new Guid();
		}

		public override bool Equals(object? obj)
		{
			if (obj is User user)
			{
				return string.Equals(user.Name, Name) && Equals(user.ID, ID) && Equals(user.Role, Role);
			}
			return false;
		}

		public override string ToString()
		{
			return $"User name: {Name}\n{Role}";
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}