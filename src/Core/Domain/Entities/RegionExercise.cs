using Domain.Models.Signings;

namespace Domain.Entities;

public class RegionExercise : IEntity
{
    public Guid RegionId { get; set; }
    public Guid ExerciseId { get; set; }

    public Region? Region { get; set; }
    public Exercise? Exercise { get; set; }
}
