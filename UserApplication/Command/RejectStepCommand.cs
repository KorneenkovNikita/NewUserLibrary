namespace UserApplication.Command
{
	public class RejectStepCommand
	{
		public Guid ApplicantId { get; init; }

		public RejectStepCommand(Guid applicantId)
		{
			ApplicantId = applicantId;
		}
	}
}
