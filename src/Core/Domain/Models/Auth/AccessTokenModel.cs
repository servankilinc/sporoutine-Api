namespace Domain.Models.Auth;

public class AccessTokenModel
{
    public AccessToken? AccessToken { get; set; }
    public IList<string>? Roles { get; set; }
}