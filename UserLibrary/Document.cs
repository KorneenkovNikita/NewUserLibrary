namespace UserLibrary
{
	public record Document
	{
		public string FirstName { get; init; }
		public string LastName { get; init; }
		public DateTime BirthDate { get; init; }
		public string Phone { get; init; }
		public string WorkExperience { get; init; }

		public Document(string firstName, string lastName, DateTime birthDate, string phone, string workExperience)
		{
			FirstName = firstName;
			LastName = lastName;
			BirthDate = birthDate;
			Phone = phone;
			WorkExperience = workExperience;
		}
	}
}
