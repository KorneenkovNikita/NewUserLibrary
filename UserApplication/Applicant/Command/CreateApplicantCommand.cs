using UserLibrary;

namespace UserApplication.Applicant.Command
{
	public class CreateApplicantCommand
	{
		public UserLibrary.Workflow ApplicantWorkflow { get; init; }
		public Document ApplicantDocument { get; init; }
		
		public CreateApplicantCommand(UserLibrary.Workflow workflow, Document document)
		{
			ApplicantDocument = document;
			ApplicantWorkflow = workflow;
		}
	}
}
