using Domain.Models.Auth;
using MediatR;

namespace Application.CQRS.Auth_.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<RefreshTokenCommandResponseModel>
{
    public string IpAddress { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
