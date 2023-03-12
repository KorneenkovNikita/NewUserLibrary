namespace UserLibrary
{
	public class Workflow
	{
		public ICollection<WorkflowStep> Steps { get; private set; }
		public ICollection<StatusLogItem> Logs { get; private set; }
		public int CurrentStepNumber { get; private set; }
		public bool IsCompleted { get; private set; }
		public bool IsRejected { get; private set; }
		public Guid ID { get; init; }

		public Workflow(ICollection<WorkflowStep> steps)
		{
			Steps = steps;
			Logs = new List<StatusLogItem>();
			IsCompleted = false;
			CurrentStepNumber = steps.Select(i => i.StepN).Min();
			ID = new Guid();
		}

		internal void Approved(User user)
		{
			if (IsCompleted) throw new Exception("All steps have been completed");
			if (IsRejected) throw new Exception("This workflow has been rejected");

			var currentStep = Steps.Where(i => i.StepN == CurrentStepNumber).First();
			if (currentStep.IsCanApprove(user))
			{
				CurrentStepNumber++;
				Logs.Add(new(user, $"step {currentStep.StepN} approved"));
				IsCompleted = currentStep.StepN == Steps.Select(i => i.StepN).Max();
			}
			else
			{
				throw new Exception("This user cannot approve the current step");
			}
		}

		internal void Rejected(User user)
		{
			if (IsCompleted) throw new Exception("All steps have been completed");
			if (IsRejected) throw new Exception("This workflow has been rejected");

			var currentStep = Steps.Where(i => i.StepN == CurrentStepNumber).First();
			if (currentStep.IsCanApprove(user))
			{
				Logs.Add(new(user, $"step {currentStep.StepN} rejected"));
				IsRejected = true;
			}
			else
			{
				throw new Exception("This user cannot approve the current step");
			}
		}

		public void AddStep(User user)
		{
			var newStep = new WorkflowStep(user, Steps.Select(i => i.StepN).Max() + 1);
			Steps.Add(newStep);
			IsCompleted = false;
		}

		public void AddStep(Role role)
		{
			var newStep = new WorkflowStep(role, Steps.Select(i => i.StepN).Max() + 1);
			Steps.Add(newStep);
			IsCompleted = false;
		}

		public void Reset(User user)
		{
			var currentStep = Steps.Where(i => i.StepN == CurrentStepNumber).First();

			if (currentStep.IsCanApprove(user))
			{
				IsRejected = false;
				IsCompleted = false;
				CurrentStepNumber = Steps.Select(i => i.StepN).Min();
			}
		}
	}
}
