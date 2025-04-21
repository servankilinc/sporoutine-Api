using Domain.Models.Auth;

namespace Application.CQRS.Auth_.Commands.RefreshToken;

public class RefreshTokenCommandResponseModel
{
    public AccessTokenModel AccessToken { get; set; } = null!;
    public RefreshTokenResponseDto RefreshToken { get; set; } = null!;
}
