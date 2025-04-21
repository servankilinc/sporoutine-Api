using Domain.Models.Signings;

namespace Domain.Entities;

public class WeightHistoryData : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public float Weight { get; set; }
    public DateTime AddedDate { get; set; }

    public User? User { get; set; }
}