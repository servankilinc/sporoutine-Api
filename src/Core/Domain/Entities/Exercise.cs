using Domain.Enums;
using Domain.Models.Signings;

namespace Domain.Entities;

public class Exercise : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
    public string? Description { get; set; }
    public ExerciseType ExerciseType { get; set; }

    public ICollection<RegionExercise>? RegionExercises { get; set; }
    public ICollection<ProgramExercise>? ProgramExercises { get; set; }
}
