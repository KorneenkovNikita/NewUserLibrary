namespace UserLibrary
{
	public class WorkflowStep
	{
		public User? ApprovedUser { get; init; }
		public Role? ApprovedRole {get; init; }
		public int StepN { get; init; }

		public WorkflowStep(User user, int stepN)
		{
			ApprovedUser = user ?? throw new ArgumentNullException("object cannot be null");
			ApprovedRole = null;
			StepN = stepN;
		}

		public WorkflowStep(Role role, int stepN)
		{
			ApprovedUser = null;
			ApprovedRole = role ?? throw new ArgumentNullException("object cannot be null");
			StepN = stepN;
		}

		public bool IsCanApprove(User user)
		{
			if (user is not null)
			{
				if (ApprovedUser is not null) return Equals(ApprovedUser, user);
				else return Equals(ApprovedRole, user.Role);
			}
			else
			{
				throw new NullReferenceException("object cannot be null");
			}
		}

		public override bool Equals(object? obj)
		{
			if (obj is WorkflowStep equalStep)
			{
				return Equals(equalStep.ApprovedRole, ApprovedRole)
					&& Equals(equalStep.ApprovedUser, ApprovedUser)
					&& equalStep.StepN == StepN;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}