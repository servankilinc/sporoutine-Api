namespace Application.GlobalExceptionHandler.CustomExceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }
}
