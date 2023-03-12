namespace UserApplication.Repository
{
    public interface IApplicantRepository
    {
        Guid Create(UserLibrary.Applicant applicant);

        UserLibrary.Applicant GetById(Guid id);
    }
}