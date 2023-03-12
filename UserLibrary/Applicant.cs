namespace UserLibrary
{
	public class Applicant
	{
		public User Author { get; init; }
		public Workflow Workflow { get; init; }
		public Document Document { get; init; }
		public Guid ID { get; init; }
		public ApplicantStatusEnum Status 
		{
			get => CheckWorkflow();
		}

		public Applicant(User user, Workflow workflow, Document document)
		{
			Author = user;
			Workflow = workflow;
			Document = document;
			ID = new Guid();
		}

		public void Approve(User user)
		{
			Workflow.Approved(user);
		}

		public void Reject(User user)
		{
			Workflow.Rejected(user);
		}

		public void Reset(User user)
		{
			Workflow.Reset(user);
		}

		private ApplicantStatusEnum CheckWorkflow()
		{
			if (Workflow.IsCompleted)
			{
				return ApplicantStatusEnum.Approved;
			}
			else if (Workflow.IsRejected)
			{
				return ApplicantStatusEnum.Rejected;
			}
			else
			{
				return ApplicantStatusEnum.InProgress;
			}
		}
	}
}
