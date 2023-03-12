namespace UserLibrary.Exceptions;

public class WorkflowAlreadyCompletedException : Exception
{
    public WorkflowAlreadyCompletedException() : base("Workflow already completed")
    {
    }
}