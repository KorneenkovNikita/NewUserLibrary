namespace UserLibrary
{
	public record StatusLogItem
	{
		public User User { get; init; }
		public DateTime LogDate { get; init; }
		public string Message { get; init; }

		public StatusLogItem(User user, string message)
		{
			User = user;
			Message = message;
			LogDate = DateTime.UtcNow;
		}
	}
}
