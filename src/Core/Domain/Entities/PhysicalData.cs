using Domain.Models.Signings;

namespace Domain.Entities;

public class PhysicalData : IEntity
{
    public Guid UserId { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public float BodyMassIndex { get; set; }

    public User? User { get; set; }
}
