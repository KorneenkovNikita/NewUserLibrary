using UserApplication.Applicant.Command;

namespace UserApplication.Applicant.Handler
{
    public class CreateApplicantHandler
    {
        public IUnitOfWork UnitWork { get; init; }
        public IRequestContext RequestContext { get; init; }
		public CreateApplicantHandler(IUnitOfWork unitWork, IRequestContext requestContext)
        {
            UnitWork = unitWork;
            RequestContext = requestContext;
        }

        public Guid Handle(CreateApplicantCommand createApplicant)
        {
            var currentUser = RequestContext.GetCurrentUser();
            var newApplicant = new UserLibrary.Applicant(currentUser, createApplicant.ApplicantWorkflow, createApplicant.ApplicantDocument);
            var applicantRepository = UnitWork.GetApplicantRepository();
            var newId = applicantRepository.Create(newApplicant);
            UnitWork.Commit();
            return newId;
        }
    }
}
