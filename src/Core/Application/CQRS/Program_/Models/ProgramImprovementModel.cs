using Application.CQRS.Program_.Dtos;

namespace Application.CQRS.Program_.Models;

public class ProgramImprovementModel
{
    public ProgramResponseDto? Program { get; set; }
    public List<ProgramExerciseFulfillmentModel>? ProgramExerciseFulfillments { get; set; }
}


public class ProgramExerciseFulfillmentModel
{
    public Guid ProgramExerciseId { get; set; }
    public Guid ExerciseId { get; set; } 
    public DateTime? AddedDate { get; set; }
    public int Day { get; set; }
    public int CompletionCount { get; set; }
    public int InCompletionCount { get; set; }
    public List<DateTime>? CompletionDateList { get; set; }
}
