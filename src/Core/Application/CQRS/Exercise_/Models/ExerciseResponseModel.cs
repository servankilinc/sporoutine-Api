using Application.CQRS.Exercise_.Dtos;
using Application.CQRS.Region_.Dtos;

namespace Application.CQRS.Exercise_.Models;

public class ExerciseResponseModel
{
    public ExerciseResponseDto Exercise { get; set; } = null!;
    public List<RegionResponseDto>? Regions { get; set; }
}
