namespace UserApplication.User.Command
{
    public class CreateUserCommand
    {
        public string FullName { get; init; }
        public Guid RoleId { get; init; }

		public CreateUserCommand(string fullName, Guid roleId)
        {
            FullName = fullName;
            RoleId = roleId;
        }
    }
}
