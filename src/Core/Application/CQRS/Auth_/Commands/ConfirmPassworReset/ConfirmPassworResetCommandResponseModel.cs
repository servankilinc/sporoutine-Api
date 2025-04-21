namespace Application.CQRS.Auth_.Commands.ConfirmPassworReset;

public class ConfirmPassworResetCommandResponseModel
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;

    public ConfirmPassworResetCommandResponseModel()
    {
    }

    public ConfirmPassworResetCommandResponseModel(bool success)
    {
        Success = success;
    }

    public ConfirmPassworResetCommandResponseModel(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}
