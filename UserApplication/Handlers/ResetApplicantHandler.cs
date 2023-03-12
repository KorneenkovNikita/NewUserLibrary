using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Command;

namespace UserApplication.Handlers
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
			var currentApplication = applicantRepository.GetById(command.ApplicantID);
			currentApplication.Reset(contextUser);
			UnitOfWork.Commit();
			return currentApplication.ID;
		}
	}
}
