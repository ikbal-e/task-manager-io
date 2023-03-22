namespace TaskManagerIO.API.Common.Models;

public class NotFoundError
{
    public string Message { get; private set; }

    public NotFoundError(string message)
    {
        Message = message;
    }
}
