using Moq;
using UserApplication;
using UserApplication.Command;
using UserApplication.Handlers;
using UserApplication.Repository;
using UserLibrary;

namespace TestUserApplication
{
    [TestFixture]
	public class TestsHandlers
	{
		[Test]
		public void TestAddWorkflowStepUsingUser()
		{
			var workflow = new Workflow(new List<WorkflowStep>() { new(new Role("Role1"), 1), new(new Role("Role2"), 1) });
			var document = new Document("TestName", "TestLastName", DateTime.Now, "TestPhone", "Test");
			var applicant = new Applicant(new("TestName", "TestSurname", new("Director")), workflow, document);
			var user = new User("Name", "Surname", new("Specialist"));

			var applicantMock = new Mock<IApplicantRepository>(MockBehavior.Strict);
			var userMock = new Mock<IUserRepository>(MockBehavior.Strict);
			var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
			var addStepHandler = new AddWorkflowStepHandler(unitOfWorkMock.Object);
			var addStepCommand = new AddWorkflowStepByUserCommand(user.ID, applicant.ID);

			applicantMock.Setup(m => m.GetById(applicant.ID)).Returns(applicant);
			userMock.Setup(m => m.GetById(user.ID)).Returns(user);
			unitOfWorkMock.Setup(m => m.GetApplicantRepository()).Returns(applicantMock.Object);
			unitOfWorkMock.Setup(m => m.GetUserRepository()).Returns(userMock.Object);
			unitOfWorkMock.Setup(m => m.Commit());
			userMock.Setup(m => m.GetById(user.ID)).Returns(user);

			addStepHandler.Handle(addStepCommand);

			applicantMock.Verify(m => m.GetById(It.IsAny<Guid>()));
			unitOfWorkMock.Verify(m => m.GetApplicantRepository());
			unitOfWorkMock.Verify(m => m.GetUserRepository());
			unitOfWorkMock.Verify(m => m.Commit());
			userMock.Verify(m => m.GetById(It.IsAny<Guid>()));
		}

		[Test]
		public void TestAddWorkflowStepUsingRole()
		{
			var workflow = new Workflow(new List<WorkflowStep>() { new(new Role("Role1"), 1), new(new Role("Role2"), 1) });
			var document = new Document("TestName", "TestLastName", DateTime.Now, "TestPhone", "Test");
			var applicant = new Applicant(new("TestName", "TestSurname", new("Director")), workflow, document);
			var role = new Role("Specialist");

			var applicantMock = new Mock<IApplicantRepository>(MockBehavior.Strict);
			var roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);
			var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
			var addStepHandler = new AddWorkflowStepHandler(unitOfWorkMock.Object);
			var addStepCommand = new AddWorkflowStepByRoleCommand(role.ID, applicant.ID);

			applicantMock.Setup(m => m.GetById(applicant.ID)).Returns(applicant);
			roleMock.Setup(m => m.GetById(role.ID)).Returns(role);
			unitOfWorkMock.Setup(m => m.GetApplicantRepository()).Returns(applicantMock.Object);
			unitOfWorkMock.Setup(m => m.GetRoleRepository()).Returns(roleMock.Object);
			unitOfWorkMock.Setup(m => m.Commit());
			roleMock.Setup(m => m.GetById(role.ID)).Returns(role);

			addStepHandler.Handle(addStepCommand);

			applicantMock.Verify(m => m.GetById(It.IsAny<Guid>()));
			unitOfWorkMock.Verify(m => m.GetApplicantRepository());
			unitOfWorkMock.Verify(m => m.GetRoleRepository());
			unitOfWorkMock.Verify(m => m.Commit());
			roleMock.Verify(m => m.GetById(It.IsAny<Guid>()));
		}
		
		[Test]
		public void TestApproveStep()
		{
			var role = new Role("Role1");
			var workflow = new Workflow(new List<WorkflowStep>() { new(new Role("Role1"), 1) });
			var document = new Document("TestName", "TestLastName", DateTime.Now, "TestPhone", "Test");
			var applicant = new Applicant(new("TestName", "TestSurname", new("Director")), workflow, document);
			var user = new User("Name", "Surname", role);

			var applicantMock = new Mock<IApplicantRepository>(MockBehavior.Strict);
			var requestMock = new Mock<IRequestContext>(MockBehavior.Strict);
			var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
			var approveStepHandler = new ApproveStepHandler(unitOfWorkMock.Object, requestMock.Object);
			var approveStepCommand = new ApproveStepCommand(applicant.ID);

			applicantMock.Setup(m => m.GetById(It.Is<Guid>((x) => x == applicant.ID))).Returns(applicant);
			requestMock.Setup(m => m.GetCurrentUser()).Returns(user);
			unitOfWorkMock.Setup(m => m.GetApplicantRepository()).Returns(applicantMock.Object);
			unitOfWorkMock.Setup(m => m.Commit());

			approveStepHandler.Handle(approveStepCommand);

			applicantMock.Verify(m => m.GetById(It.IsAny<Guid>()));
			unitOfWorkMock.Verify(m => m.GetApplicantRepository());
			unitOfWorkMock.Verify(m => m.Commit());
			requestMock.Verify(m => m.GetCurrentUser());
		}

		[Test]
		public void TestRejectedStep()
		{
			var role = new Role("Role1");
			var workflow = new Workflow(new List<WorkflowStep>() { new(new Role("Role1"), 1) });
			var document = new Document("TestName", "TestLastName", DateTime.Now, "TestPhone", "Test");
			var applicant = new Applicant(new("TestName", "TestSurname", new("Director")), workflow, document);
			var user = new User("Name", "Surname", role);

			var applicantMock = new Mock<IApplicantRepository>(MockBehavior.Strict);
			var requestMock = new Mock<IRequestContext>(MockBehavior.Strict);
			var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
			var rejectStepHandler = new RejectStepHandler(unitOfWorkMock.Object, requestMock.Object);
			var rejectStepCommand = new RejectStepCommand(applicant.ID);

			applicantMock.Setup(m => m.GetById(applicant.ID)).Returns(applicant);
			requestMock.Setup(m => m.GetCurrentUser()).Returns(user);
			unitOfWorkMock.Setup(m => m.GetApplicantRepository()).Returns(applicantMock.Object);
			unitOfWorkMock.Setup(m => m.Commit());

			rejectStepHandler.Handle(rejectStepCommand);

			applicantMock.Verify(m => m.GetById(It.IsAny<Guid>()));
			unitOfWorkMock.Verify(m => m.GetApplicantRepository());
			unitOfWorkMock.Verify(m => m.Commit());
			requestMock.Verify(m => m.GetCurrentUser());
		}

		[Test]
		public void CreateApplicantHandler()
		{
			var role = new Role("Role1");
			var workflow = new Workflow(new List<WorkflowStep>() { new(new Role("Role1"), 1) });
			var document = new Document("TestName", "TestLastName", DateTime.Now, "TestPhone", "Test");
			var user = new User("Name", "Surname", role);

			var applicantMock = new Mock<IApplicantRepository>(MockBehavior.Strict);
			var requestMock = new Mock<IRequestContext>(MockBehavior.Strict);
			var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
			var createApplicantHandler = new CreateApplicantHandler(unitOfWorkMock.Object, requestMock.Object);
			var createApplicantCommand = new CreateApplicantComand(workflow, document);

			applicantMock.Setup(m => m.Create(It.Is<Applicant>((param) => param is Applicant))).Returns(It.Is<Guid>((param) => true)); // <-- соответствие входящему параметру
			requestMock.Setup(m => m.GetCurrentUser()).Returns(user);
			unitOfWorkMock.Setup(m => m.GetApplicantRepository()).Returns(applicantMock.Object);
			unitOfWorkMock.Setup(m => m.Commit());

			createApplicantHandler.Handle(createApplicantCommand);

			unitOfWorkMock.Verify(m => m.GetApplicantRepository());
			unitOfWorkMock.Verify(m => m.Commit());
			requestMock.Verify(m => m.GetCurrentUser());
		}

		[Test]
		public void CreateUserHandler()
		{
			var role = new Role("Role1");
			var user = new User("Name", "Surname", role);

			var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
			var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
			var roleRepositoryMock = new Mock<IRoleRepository>(MockBehavior.Strict);
			var createUserHandler = new CreateUserHandler(unitOfWorkMock.Object);
			var createUserCommand = new CreateUserCommand("Name", "Surname", role.ID);

			roleRepositoryMock.Setup(m => m.GetById(role.ID)).Returns(role);
			userRepositoryMock.Setup(m => m.CreateUser(user)).Returns(user.ID);
			unitOfWorkMock.Setup(m => m.GetRoleRepository()).Returns(roleRepositoryMock.Object);
			unitOfWorkMock.Setup(m => m.GetUserRepository()).Returns(userRepositoryMock.Object);
			unitOfWorkMock.Setup(m => m.Commit());

			createUserHandler.Handle(createUserCommand);

			unitOfWorkMock.Verify(m => m.GetUserRepository());
			unitOfWorkMock.Verify(m => m.GetRoleRepository());
			unitOfWorkMock.Verify(m => m.Commit());
			roleRepositoryMock.Verify(m => m.GetById(It.IsAny<Guid>()));
			userRepositoryMock.Verify(m => m.CreateUser(It.IsAny<User>()));
		}

		[Test]
		public void ResetApplicantHandler()
		{

		}
	}
}