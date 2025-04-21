namespace Application.CQRS.Auth_.Commands.ConfirmEmail;

public class ConfirmEmailCommandResponseModel
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;

    public ConfirmEmailCommandResponseModel()
    {
    }

    public ConfirmEmailCommandResponseModel(bool success)
    {
        Success = success;
    }

    public ConfirmEmailCommandResponseModel(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}
