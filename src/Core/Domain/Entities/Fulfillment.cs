using Domain.Models.Signings;

namespace Domain.Entities;

public class Fulfillment : IEntity
{ 
    public Guid ProgramExerciseId { get; set; }
    public DateTime CompletionDate { get; set; }
    public DateOnly CompletionDateIndexer { get; set; }

    public ProgramExercise? ProgramExercise { get; set; }
}
