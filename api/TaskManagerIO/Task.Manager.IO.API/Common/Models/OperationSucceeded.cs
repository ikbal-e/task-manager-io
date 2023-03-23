namespace TaskManagerIO.API.Common.Models;

public class OperationSucceeded
{
    public string Message { get; private set; }

    public OperationSucceeded(string message)
    {
        Message = message;
    }
}
