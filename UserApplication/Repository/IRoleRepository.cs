using UserLibrary;

namespace UserApplication.Repository
{
    public interface IRoleRepository
    {
        public Role GetById(Guid idRole);
    }
}
