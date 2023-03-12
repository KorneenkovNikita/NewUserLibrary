namespace UserLibrary
{
	public class StatusLogItem
	{
		public User User { get; }
		public DateTime LogDate { get; }
		public string Message { get; }

		public StatusLogItem(User user, string message)
		{
			User = user;
			Message = message;
			LogDate = DateTime.UtcNow;
		}
	}
}
