using Application.CQRS.Program_.Dtos;

namespace Application.CQRS.Program_.Models;

public class ProgramDetailCurrentDayResponseModel
{
    public ProgramResponseDto? Program { get; set; }
    public List<FulfillmentsCurrentDayModel>? ExerciseList { get; set; }
}