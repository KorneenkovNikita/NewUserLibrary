using UserApplication.Command;

namespace UserApplication.Handlers
{
	public class ApproveStepHandler
	{
		public IUnitOfWork UnitOfWork { get; init; }
		public IRequestContext RequestContext { get; init; }
		public ApproveStepHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
		{
			UnitOfWork = unitOfWork;
			RequestContext = requestContext;
		}

		public void Handle(ApproveStepCommand command)
		{
			var applicantRepository = UnitOfWork.GetApplicantRepository();
			var currentApplicant = applicantRepository.GetById(command.ApplicationId);
			var currentUser = RequestContext.GetCurrentUser();
			currentApplicant.Approve(currentUser);
			UnitOfWork.Commit();
		}
	}
}
