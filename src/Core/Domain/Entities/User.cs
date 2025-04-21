using Domain.Enums;
using Domain.Models.Signings;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>, IEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public AuthenticatorType AuthenticatorType { get; set; }

    public ICollection<RefreshToken>? RefreshTokens { get; set; }
    public ICollection<Program>? Programs { get; set; }
    public PhysicalData? PhysicalData { get; set; }
    public ICollection<WeightHistoryData>? WeightDataset { get; set; }
}