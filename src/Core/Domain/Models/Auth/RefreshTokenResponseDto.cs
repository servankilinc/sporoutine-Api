using Domain.Models.Signings;

namespace Domain.Models.Auth;

public class RefreshTokenResponseDto : IDto
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public DateTime CreatedDate { get; set; }
    public int TTL { get; set; }
}