namespace UserApplication.Repository
{
    public interface IUserRepository
    {
        Guid CreateUser(UserLibrary.User newUser);
        UserLibrary.User GetById(Guid id);
    }
}