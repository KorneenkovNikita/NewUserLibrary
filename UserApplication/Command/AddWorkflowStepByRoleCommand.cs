namespace UserApplication.Command
{
	public class AddWorkflowStepByRoleCommand
	{
		public Guid RoleId { get; init; }
		public Guid ApplicantId { get; init; }

		public AddWorkflowStepByRoleCommand(Guid roleId, Guid applicantId)
		{
			RoleId = roleId;
			ApplicantId = applicantId;
		}
	}
}
