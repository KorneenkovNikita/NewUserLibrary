using UserLibrary;

namespace UserApplication.Repository
{
    public interface IRoleRepository
    {
        Role GetById(Guid idRole);
    }
}
