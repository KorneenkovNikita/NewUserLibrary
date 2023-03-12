using UserLibrary;

namespace UserApplication
{
	public interface IRequestContext
	{
		public User GetCurrentUser();
	}
}
