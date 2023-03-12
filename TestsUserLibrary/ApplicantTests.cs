using UserLibrary;
using UserLibrary.Exceptions;

namespace TestsUserLibrary
{
    [TestFixture]
    public class ApplicantTests
    {
        private List<User> _users = new();
        private List<Role> _roles = new();
        private List<WorkflowStep> _steps = new();
        private Applicant _applicant = null!;
        private Workflow _workflow = null!;

        [SetUp]
        public void SetUp()
        {
            _roles = new List<Role>
            {
                new("Interrogator"),
                new("Main Interrogator"),
                new("Tester"),
                new("Main Tester"),
                new("Admin")
            };
            _users = new List<User>
            {
                new("Alex Tihonov", _roles[0]),
                new("Stas Morozov", _roles[1]),
                new("Alis Ivanov", _roles[2]),
                new("Max Lavrov", _roles[3]),
                new("Evelin Li", _roles[4])
            };

            _steps = new List<WorkflowStep>();
            for (var i = 0; i < _users.Count; i++)
            {
                _steps.Add(new WorkflowStep(_users[i], i + 1));
            }

            for (var i = 0; i < _roles.Count; i++)
            {
                _steps.Add(new WorkflowStep(_roles[i], _users.Count + i + 1));
            }

            var role = new Role("Creating");
            var userApplicant = new User("Mark IM", role);
            _workflow = new Workflow(_steps);
            _applicant = new Applicant(userApplicant, _workflow, new Document("newName", "newSurname", DateTime.Today.AddDays(4500), "newPhone", "work"));
        }

        [Test]
        public void TestApprovedUser()
        {
            foreach (var user in _users)
            {
                _applicant.Approve(user);
            }

            for (var i = 0; i < _roles.Count; i++)
            {
                _applicant.Approve(_users[i]);
            }

            Assert.That(_applicant.Status, Is.EqualTo(ApplicantStatusEnum.Approved));
        }

        [Test]
        public void TryApproveWhenUserCannotApproveNegativeTest()
        {
            var action = () => _applicant.Approve(_users[1]);

            Assert.Catch<ForbiddenForUserException>(() => action.Invoke());
        }

        [Test]
        public void TryApproveAlreadyCompletedApplicant_NegativeTest()
        {
            foreach (var user in _users)
            {
                _applicant.Approve(user);
            }

            foreach (var role in _roles)
            {
                _applicant.Approve(_users.First(x => x.Role.Equals(role)));
            }

            Assert.That(_applicant.Status, Is.EqualTo(ApplicantStatusEnum.Approved));

            var action = () => _applicant.Approve(_users.First());

            Assert.Catch<WorkflowAlreadyCompletedException>(() => action.Invoke());
        }

        [Test]
        public void ApprovedAfterRejectedApplicantNegativeTest()
        {
            _applicant.Reject(_users.First());

            Assert.That(_applicant.Status, Is.EqualTo(ApplicantStatusEnum.Rejected));

            var action = () => _applicant.Approve(_users.First());

            Assert.Catch<WorkflowAlreadyRejectedException>(() => action.Invoke());
        }

        [Test]
        public void AddStepInWorkflowAfterUserApproved()
        {
            foreach (var user in _users)
            {
                _applicant.Approve(user);
            }

            for (var i = 0; i < _roles.Count; i++)
            {
                _applicant.Approve(_users[i]);
            }

            Assert.That(_applicant.Status, Is.EqualTo(ApplicantStatusEnum.Approved));
            _workflow.AddStep(_users[0]);
            Assert.That(_applicant.Status, Is.EqualTo(ApplicantStatusEnum.InProgress));
        }

        [Test]
        public void TestRejectedUser()
        {
            _applicant.Reject(_users[0]);
            Assert.That(_applicant.Status, Is.EqualTo(ApplicantStatusEnum.Rejected));
        }

        [Test]
        public void TestApprovedAfterRejectedUserNegativeTest()
        {
            _applicant.Reject(_users[0]);
            var action = () => _applicant.Approve(_users[1]);

            Assert.Catch<WorkflowAlreadyRejectedException>(() => action.Invoke());
        }

        [Test]
        public void ResetApprovedUser()
        {
            Assert.That(_workflow.CurrentStepNumber, Is.EqualTo(1));
            foreach (var t in _users)
            {
                _applicant.Approve(t);
            }

            Assert.That(_workflow.CurrentStepNumber, Is.Not.EqualTo(0));
            _workflow.Reset(_users[0]);
            Assert.That(_workflow.CurrentStepNumber, Is.EqualTo(1));
        }

        [Test]
        public void ResetWhenUserCannotApproveNegativeTest()
        {
            Assert.That(_workflow.CurrentStepNumber, Is.EqualTo(1));

            foreach (var user in _users)
            {
                _applicant.Approve(user);
            }

            foreach (var role in _roles)
            {
                _applicant.Approve(_users.First(x => x.Role.Equals(role)));
            }

            Assert.Multiple(() =>
            {
                Assert.That(_workflow.CurrentStepNumber, Is.EqualTo(_users.Count + _roles.Count));
                Assert.That(_applicant.Status, Is.EqualTo(ApplicantStatusEnum.Approved));
            });

            _workflow.Reset(_users.First());

            Assert.Multiple(() =>
            {
                Assert.That(_workflow.CurrentStepNumber, Is.EqualTo(_users.Count + _roles.Count));
                Assert.That(_applicant.Status, Is.EqualTo(ApplicantStatusEnum.Approved));
            });
        }

        [Test]
        public void GetStatusLogs()
        {
            foreach (var user in _users)
            {
                _applicant.Approve(user);
            }

            for (var i = 0; i < _roles.Count; i++)
            {
                _applicant.Approve(_users[i]);
            }

            foreach (var log in _workflow.Logs)
            {
                Console.WriteLine(log);
            }
        }
    }
}