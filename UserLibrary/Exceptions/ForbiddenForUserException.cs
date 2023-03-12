namespace UserLibrary.Exceptions;

public class ForbiddenForUserException : Exception
{
    public ForbiddenForUserException() : base("This user has no access to this action")
    {
    }
}