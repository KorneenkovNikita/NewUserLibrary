namespace UserApplication.Workflow.Command
{
	public class AddWorkflowStepByUserCommand
	{
		public Guid UserId { get; init; }
		public Guid ApplicantId { get; init; }

		public AddWorkflowStepByUserCommand(Guid userId, Guid applicantId)
		{
			UserId = userId;
			ApplicantId = applicantId;
		}
	}
}
