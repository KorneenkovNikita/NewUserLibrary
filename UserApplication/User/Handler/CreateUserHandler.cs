using UserApplication.User.Command;

namespace UserApplication.User.Handler
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
            var currentRole = roleRepository.GetById(createUserCommand.RoleId);
            var newUser = new UserLibrary.User(createUserCommand.FullName, currentRole);
            var userRepository = UnitWork.GetUserRepository();
            var newId = userRepository.CreateUser(newUser);
            UnitWork.Commit();
            return newId;
        }
    }
}