using UserApplication.Command;

namespace UserApplication.Handlers
{
	public class RejectStepHandler
	{
		public IUnitOfWork UnitOfWork { get; init; }
		public IRequestContext RequestContext { get; init; }
		
		public RejectStepHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
		{
			UnitOfWork = unitOfWork;
			RequestContext = requestContext;
		}

		public void Handle(RejectStepCommand command)
		{
			var applicantRepository = UnitOfWork.GetApplicantRepository();
			var currentApplication = applicantRepository.GetById(command.ApplicantId);
			var currentUser = RequestContext.GetCurrentUser();

			currentApplication.Reject(currentUser);
			UnitOfWork.Commit();
		}
	}
}
