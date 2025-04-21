using Application.CQRS.Auth_.Dtos;
using Domain.Models.Auth;

namespace Application.CQRS.Auth_.Commands.Login.Models;

public class UserLoginResponseModel
{
    public UserResponseDto User { get; set; } = null!;
    public AccessTokenModel AccessToken { get; set; } = null!;
    public RefreshTokenResponseDto RefreshToken { get; set; } = null!;
}
