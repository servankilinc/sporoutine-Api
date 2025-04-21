using Domain.Models.Signings;

namespace Application.CQRS.PhysicalData_.Dtos;

public class WeightHistoryDataResponseDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public float Weight { get; set; }
    public DateTime AddedDate { get; set; }
}
