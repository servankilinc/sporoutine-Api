using Application.CQRS.Exercise_.Dtos;
using Application.CQRS.Program_.Dtos;
using Application.CQRS.Region_.Dtos;

namespace Application.CQRS.Program_.Models;

public class ProgramExerciseInteractionModel
{
    public ExerciseResponseDto? Exercise { get; set; }
    public ProgramExerciseResponseDto? ProgramExercise { get; set; }
    public bool IsAdded { get; set; }
    public List<RegionResponseDto>? Regions { get; set; }
}

public class ProgramExerciseInteractionMultipleDayModel
{
    public ExerciseResponseDto? Exercise { get; set; }
    public List<ProgramExerciseResponseDto>? ProgramExercises { get; set; }
    public List<RegionResponseDto>? Regions { get; set; }
}