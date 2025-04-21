using Domain.Models.Signings;

namespace Application.CQRS.Program_.Dtos;

public class ProgramResponseDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedDate { get; set; }
}