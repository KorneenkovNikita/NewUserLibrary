namespace UserApplication.Command
{
	internal class ResetApplicantCommand
	{
		public Guid ApplicantID { get; init; }

		public ResetApplicantCommand(Guid applicantID, Guid userID)
		{
			ApplicantID = applicantID;
		}
	}
}
