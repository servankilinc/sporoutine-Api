using Domain.Models.Signings;

namespace Application.CQRS.Program_.Dtos;

public class ProgramExerciseResponseDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProgramId { get; set; }
    public Guid ExerciseId { get; set; }
    public DateTime AddedDate { get; set; }
    public int Day { get; set; }
    public int NumberOfSets { get; set; }
    public int NumberOfRepetition { get; set; }
    public int Time { get; set; }
}
