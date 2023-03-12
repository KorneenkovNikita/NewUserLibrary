namespace UserApplication
{
	public interface IRequestContext
	{
		UserLibrary.User GetCurrentUser();
	}
}
