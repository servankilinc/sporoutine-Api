using Domain.Models.Signings;

namespace Domain.Entities;

public class RefreshToken : IEntity
{
    public Guid UserId { get; set; }
    public string CreatedIp { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public DateTime CreatedDate { get; set; }
    public int TTL { get; set; }

    public User? User { get; set; }
}
