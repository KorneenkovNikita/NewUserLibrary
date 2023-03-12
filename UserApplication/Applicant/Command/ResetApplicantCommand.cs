namespace UserApplication.Applicant.Command
{
	internal class ResetApplicantCommand
	{
		public Guid ApplicantId { get; init; }

		public ResetApplicantCommand(Guid applicantId)
		{
			ApplicantId = applicantId;
		}
	}
}
