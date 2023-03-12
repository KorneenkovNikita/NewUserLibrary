using UserApplication.Applicant.Command;

namespace UserApplication.Applicant.Handler
{
	internal class ResetApplicantHandler
	{
		public IUnitOfWork UnitOfWork { get; init; }
		public IRequestContext RequestContext { get; init; }

		public ResetApplicantHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
		{
			UnitOfWork = unitOfWork;
			RequestContext = requestContext;
		}

		public Guid Handler(ResetApplicantCommand command)
		{
			var contextUser = RequestContext.GetCurrentUser();
			var applicantRepository = UnitOfWork.GetApplicantRepository();
			var currentApplication = applicantRepository.GetById(command.ApplicantId);
			currentApplication.Reset(contextUser);
			UnitOfWork.Commit();
			return currentApplication.Id;
		}
	}
}
