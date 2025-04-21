using Domain.Models.Signings;

namespace Domain.Entities;

public class Program : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedDate { get; set; }

    public User? User { get; set; }
    public ICollection<ProgramExercise>? ProgramExercises { get; set; }
}
