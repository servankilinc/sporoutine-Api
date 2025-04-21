using Application.Services.Repositories;
using MediatR;

namespace Application.CQRS.Program_.Commands.Delete;

public class DeleteProgramCommandHandler : IRequestHandler<DeleteProgramCommand>
{
    private readonly IProgramRepository _programRepository;
    public DeleteProgramCommandHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
    {
        await _programRepository.DeleteByFilterAsync(filter: f => f.Id == request.ProgramId);
    }
}
