namespace UserLibrary
{
    public class WorkflowStep
    {
        public User? ApprovedUser { get; }
        public Role? ApprovedRole { get; }
        public int StepN { get; }

        public WorkflowStep(User user, int stepN)
        {
            ApprovedUser = user ?? throw new ArgumentNullException(nameof(user));
            ApprovedRole = null;
            StepN = stepN;
        }

        public WorkflowStep(Role role, int stepN)
        {
            ApprovedUser = null;
            ApprovedRole = role ?? throw new ArgumentNullException(nameof(role));
            StepN = stepN;
        }

        public bool IsCanApprove(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            return ApprovedUser is not null ? Equals(ApprovedUser, user) : Equals(ApprovedRole, user.Role);
        }

        public override bool Equals(object? obj)
        {
            return obj switch
            {
                null => throw new ArgumentNullException(nameof(obj)),
                WorkflowStep equalStep => Equals(equalStep.ApprovedRole, ApprovedRole) && Equals(equalStep.ApprovedUser, ApprovedUser) &&
                                          equalStep.StepN == StepN,
                _ => false
            };
        }

        public override int GetHashCode()
        {
            var hash = StepN;
            if (ApprovedRole is not null)
            {
                hash += ApprovedRole.GetHashCode();
            }

            if (ApprovedUser is not null)
            {
                hash += ApprovedUser.GetHashCode();
            }

            return hash;
        }
    }
}