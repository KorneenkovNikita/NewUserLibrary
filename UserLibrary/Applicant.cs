namespace UserLibrary
{
    public class Applicant
    {
        public User Author { get; }
        public Workflow Workflow { get; }
        public Document Document { get; }
        public Guid Id { get; }
        public ApplicantStatusEnum Status => CheckWorkflow();

        public Applicant(User user, Workflow workflow, Document document)
        {
            Author = user;
            Workflow = workflow;
            Document = document;
            Id = Guid.NewGuid();
        }

        public void Approve(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            Workflow.Approved(user);
        }

        public void Reject(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            
            Workflow.Rejected(user);
        }

        public void Reset(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            
            Workflow.Reset(user);
        }

        private ApplicantStatusEnum CheckWorkflow()
        {
            return Workflow.IsCompleted ? ApplicantStatusEnum.Approved : Workflow.IsRejected ? ApplicantStatusEnum.Rejected : ApplicantStatusEnum.InProgress;
        }
    }
}