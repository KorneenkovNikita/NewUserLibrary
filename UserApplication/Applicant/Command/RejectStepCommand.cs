namespace UserApplication.Applicant.Command
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
