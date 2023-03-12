using UserApplication.Command;
using UserLibrary;

namespace UserApplication.Handlers
{
    public class CreateUserHandler
    {
        public IUnitOfWork UnitWork { get; init; }
        public CreateUserHandler(IUnitOfWork unitWork)
        {
            UnitWork = unitWork;
        }

        public Guid Handle(CreateUserCommand createUserCommand)
        {
            var roleRepository = UnitWork.GetRoleRepository();
            var currentRole = roleRepository.GetById(createUserCommand.RoleID);
            var newUser = new User(createUserCommand.Name, createUserCommand.Surname, currentRole);
            var userRepository = UnitWork.GetUserRepository();
            Guid newId = userRepository.CreateUser(newUser);
            UnitWork.Commit();
            return newId;
        }
    }
}