using UserLibrary;

namespace UserApplication.Command
{
	public class CreateApplicantComand
	{
		public Workflow ApplicantWorkflow { get; init; }
		public Document ApplicantDocument { get; init; }
		
		public CreateApplicantComand(Workflow workflow, Document document)
		{
			ApplicantDocument = document;
			ApplicantWorkflow = workflow;
		}
	}
}
