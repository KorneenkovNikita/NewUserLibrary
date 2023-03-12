namespace UserLibrary
{
	public class Role
	{
		public string Name { get; init; }
		public Guid ID { get; init; }

		public Role(string name)
		{
			Name = name;
			ID = new Guid();
		}

		public override bool Equals(object? obj)
		{
			if (obj is Role role)
			{
				return string.Equals(Name, role.Name, StringComparison.OrdinalIgnoreCase) && Equals(ID, role.ID);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return $"Role name: {Name}";
		}
	}
}
