using Domain.Models.Signings;

namespace Application.CQRS.PhysicalData_.Dtos;

public class PhysicalDataResponseDto : IDto
{
    public Guid UserId { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public float BodyMassIndex => Height == 0 ? 0 : Weight / (Height * Height / (100 * 100));
}
