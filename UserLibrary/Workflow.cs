using UserLibrary.Exceptions;

namespace UserLibrary
{
    public class Workflow
    {
        public ICollection<WorkflowStep> Steps { get; }
        public ICollection<StatusLogItem> Logs { get; }
        public int CurrentStepNumber { get; private set; }
        public bool IsCompleted { get; private set; }
        public bool IsRejected { get; private set; }
        public Guid Id { get; }

        public Workflow(ICollection<WorkflowStep> steps)
        {
            if (steps.Count == 0)
            {
                throw new ArgumentException("Steps should not be empty", nameof(steps));
            }
            
            Steps = steps;
            Logs = new List<StatusLogItem>();
            IsCompleted = false;
            CurrentStepNumber = steps.Min(x=>x.StepN);
            Id = Guid.NewGuid();
        }

        internal void Approved(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            if (IsCompleted) throw new WorkflowAlreadyCompletedException();
            if (IsRejected) throw new WorkflowAlreadyRejectedException();

            var currentStep = Steps.First(i => i.StepN == CurrentStepNumber);
            if (currentStep.IsCanApprove(user))
            {
                IsCompleted = currentStep.StepN == Steps.Max(i => i.StepN);
                CurrentStepNumber = IsCompleted ? CurrentStepNumber : CurrentStepNumber + 1;
                Logs.Add(new StatusLogItem(user, $"step {currentStep.StepN} approved"));
            }
            else
            {
                throw new ForbiddenForUserException();
            }
        }

        internal void Rejected(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            if (IsCompleted) throw new WorkflowAlreadyCompletedException();
            if (IsRejected) throw new WorkflowAlreadyRejectedException();

            var currentStep = Steps.First(i => i.StepN == CurrentStepNumber);
            if (currentStep.IsCanApprove(user))
            {
                Logs.Add(new StatusLogItem(user, $"step {currentStep.StepN} rejected"));
                IsRejected = true;
            }
            else
            {
                throw new ForbiddenForUserException();
            }
        }

        public void AddStep(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            var newStep = new WorkflowStep(user, Steps.Select(i => i.StepN).Max() + 1);
            Steps.Add(newStep);
            IsCompleted = false;
        }

        public void AddStep(Role role)
        {
            if (role is null) throw new ArgumentNullException(nameof(role));

            var newStep = new WorkflowStep(role, Steps.Max(i => i.StepN) + 1);
            Steps.Add(newStep);
            IsCompleted = false;
        }

        public void Reset(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            var currentStep = Steps.First(i => i.StepN == CurrentStepNumber);

            if (!currentStep.IsCanApprove(user))
            {
                return;
            }

            IsRejected = false;
            IsCompleted = false;
            CurrentStepNumber = Steps.Min(i => i.StepN);
        }
    }
}