namespace UserLibrary
{
	public class Document
	{
		public string FirstName { get; }
		public string LastName { get; }
		public DateTime BirthDate { get; }
		public string Phone { get; }
		public string WorkExperience { get; }

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
