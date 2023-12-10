namespace Lab3.exceptions;

public class TaskException : Exception
{
    public TaskException(string message)
        : base(message)
    {
    }
}