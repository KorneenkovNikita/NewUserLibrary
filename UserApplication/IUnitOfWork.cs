using UserApplication.Repository;

namespace UserApplication
{
	public interface IUnitOfWork
	{
		IRoleRepository GetRoleRepository();

		IUserRepository GetUserRepository();

		IApplicantRepository GetApplicantRepository();

		void Commit();
	}
}