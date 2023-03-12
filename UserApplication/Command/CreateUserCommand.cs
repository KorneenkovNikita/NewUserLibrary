namespace UserApplication.Command
{
    public class CreateUserCommand
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public Guid RoleID { get; init; }

		public CreateUserCommand(string name, string surname, Guid roleId)
        {
            Name = name;
            Surname = surname;
            RoleID = roleId;
        }
    }
}
