namespace Application.CQRS.Program_.Models;

public class FulfillmentsCurrentDayModel : ProgramExerciseBaseModel
{
    public int CompletionCount { get; set; }
    public int InCompletionCount { get; set; }
    public bool CompletionStatus { get; set; }
}
