using Domain.Models.Signings;

namespace Domain.Entities;

public class ProgramExercise : IEntity
{
    public Guid Id { get; set; }
    public Guid ProgramId { get; set; }
    public Guid ExerciseId { get; set; }
    public DateTime AddedDate { get; set; }
    public int Day { get; set; }
    public int NumberOfSets { get; set; }
    public int NumberOfRepetition { get; set; }
    public int Time { get; set; }

    public Program? Program { get; set; }
    public Exercise? Exercise { get; set; }
    public ICollection<Fulfillment>? Fulfillments { get; set; }
}
