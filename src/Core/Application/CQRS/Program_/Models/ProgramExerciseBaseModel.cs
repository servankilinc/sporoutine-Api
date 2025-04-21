using Domain.Enums;

namespace Application.CQRS.Program_.Models;

public class ProgramExerciseBaseModel
{
    public Guid ExerciseId { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
    public string? Description { get; set; }
    public ExerciseType ExerciseType { get; set; }
    // above fields coming from ProgramExercise entity upper fields from Exercsie entity
    public Guid ProgramExerciseId { get; set; }
    public DateTime? AddedDate { get; set; }
    public int Day { get; set; }
    public int NumberOfSets { get; set; }
    public int NumberOfRepetition { get; set; }
    public int Time { get; set; }
}
