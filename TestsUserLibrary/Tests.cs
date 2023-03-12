using UserLibrary;

namespace TestsUserLibrary
{
	[TestFixture]
	public class Tests
	{
		public List<User> users;
		public List<Role> roles;
		public List<WorkflowStep> steps;
		public Applicant applicant;
		public Workflow applicantWorkflow;

		[SetUp]
		public void SetUp()
		{
			roles = new()
			{
				new("Interrogator"),
				new("Main Interrogator"),
				new("Tester"),
				new("Main Tester"),
				new("Admin")
			};
			users = new()
			{
				new("Alex", "Tixonov", roles[0]),
				new("Stas", "Morozov", roles[1]),
				new("Alis", "Ivanov", roles[2]),
				new("Max", "Lavrov", roles[3]),
				new("Evelin", "Li", roles[4])
			};

			steps = new();
			for (int i = 0; i < users.Count; i++)
			{
				steps.Add(new(users[i], i));
			}
			for (int i = 0; i < roles.Count; i++)
			{
				steps.Add(new(roles[i], users.Count + i));
			}

			Role role = new Role("Creating");
			User userApplicant = new User("Mark", "IM", role);
			applicantWorkflow = new(steps);
			applicant = new(userApplicant, applicantWorkflow, new("newName", "newSurname", DateTime.Today.AddDays(4500), "newPhone", "work"));
		}

		[Test]
		public void TestApprovedUser()
		{
			for (int i = 0; i < users.Count; i++)
			{
				applicant.Approve(users[i]);
			}
			for (int i = 0; i < roles.Count; i++)
			{
				applicant.Approve(users[i]);
			}
			Assert.IsTrue(applicant.Status == ApplicantStatusEnum.Approved);
		}

		[Test]
		public void AddStepInWorkflowUserApproved()
		{
			for (int i = 0; i < users.Count; i++)
			{
				applicant.Approve(users[i]);
			}
			for (int i = 0; i < roles.Count; i++)
			{
				applicant.Approve(users[i]);
			}
			Assert.IsTrue(applicant.Status == ApplicantStatusEnum.Approved);
			applicantWorkflow.AddStep(users[0]);
			Assert.IsTrue(applicant.Status == ApplicantStatusEnum.InProgress);
		}

		[Test]
		public void AddStepInWorkflowUserNotApproved()
		{
			applicantWorkflow.AddStep(users[0]);
		}

		[Test]
		public void TestRejectedUser()
		{
			applicant.Reject(users[0]);
			Assert.IsTrue(applicant.Status == ApplicantStatusEnum.Rejected);
		}

		[Test]
		public void TestApprovedBeforeRejectedUser()
		{
			applicant.Reject(users[0]);
			string? str = string.Empty;
			Assert.Catch(() => applicant.Approve(users[1]));
		}

		[Test]
		public void ResetApprovedUser()
		{
			Assert.IsTrue(applicantWorkflow.CurrentStepNumber == 0);
			for (int i = 0; i < users.Count; i++)
			{
				applicant.Approve(users[i]);
			}
			Assert.IsTrue(applicantWorkflow.CurrentStepNumber != 0);
			applicantWorkflow.Reset(users[0]);
			Assert.IsTrue(applicantWorkflow.CurrentStepNumber == 0);
		}

		[Test]
		public void GetStatusLogs()
		{
			for (int i = 0; i < users.Count; i++)
			{
				applicant.Approve(users[i]);
			}
			for (int i = 0; i < roles.Count; i++)
			{
				applicant.Approve(users[i]);
			}

			foreach (var i in applicantWorkflow.Logs)
			{
				Console.WriteLine(i);
			}
		}
	}
}