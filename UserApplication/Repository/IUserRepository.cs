using UserLibrary;

namespace UserApplication.Repository
{
    public interface IUserRepository
    {
        public Guid CreateUser(User newUser);
        public User GetById(Guid id);
    }
}