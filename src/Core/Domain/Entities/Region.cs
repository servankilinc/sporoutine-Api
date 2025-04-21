using Domain.Models.Signings;

namespace Domain.Entities;

public class Region : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }

    public ICollection<RegionExercise>? RegionExercises { get; set; }
}
