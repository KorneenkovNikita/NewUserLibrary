using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApplication.Command
{
	public class AddWorkflowStepByUserCommand
	{
		public Guid UserId { get; init; }
		public Guid ApplicantId { get; init; }

		public AddWorkflowStepByUserCommand(Guid userId, Guid applicantId)
		{
			UserId = userId;
			ApplicantId = applicantId;
		}
	}
}
