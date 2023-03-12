namespace UserLibrary.Exceptions;

public class WorkflowAlreadyRejectedException : Exception
{
    public WorkflowAlreadyRejectedException() : base("Workflow already rejected")
    {
    }
}