namespace UserApplication.Command
{
	public class ApproveStepCommand
	{
		public Guid ApplicationId { get; init; }

		public ApproveStepCommand(Guid applicationId)
		{
			ApplicationId = applicationId;
		}
	}
}
