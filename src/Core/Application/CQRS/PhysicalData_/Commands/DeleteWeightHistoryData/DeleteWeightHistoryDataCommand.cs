using FluentValidation;
using MediatR;

namespace Application.CQRS.PhysicalData_.Commands.DeleteWeightHistoryData;

public class DeleteWeightHistoryDataCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteWeightHistoryDataCommandValidator : AbstractValidator<DeleteWeightHistoryDataCommand>
{
    public DeleteWeightHistoryDataCommandValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}
