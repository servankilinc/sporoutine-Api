using Application.CQRS.Region_.Dtos;
using Domain.Enums;

namespace Application.CQRS.Exercise_.Models;

public class ProgramExerciseResponseModel
{
    public Guid ExerciseId { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
    public string? Description { get; set; }
    public ExerciseType ExerciseType { get; set; }

    public Guid ProgramExerciseId { get; set; }
    public DateTime? AddedDate { get; set; }
    public int Day { get; set; }
    public int NumberOfSets { get; set; }
    public int NumberOfRepetition { get; set; }
    public int Time { get; set; }

    public List<RegionResponseDto>? Regions { get; set; }
}
