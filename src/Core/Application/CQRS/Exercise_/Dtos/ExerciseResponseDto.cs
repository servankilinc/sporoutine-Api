using Domain.Enums;
using Domain.Models.Signings;

namespace Application.CQRS.Exercise_.Dtos;

public class ExerciseResponseDto : IDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
    public string? Description { get; set; }
    public ExerciseType ExerciseType { get; set; }
}
