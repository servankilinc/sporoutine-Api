using Application.CQRS.Program_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Program_.Commands.Save;

public class UpdateProgramCommandHandler : IRequestHandler<SaveProgramCommand, ProgramResponseDto>
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;
    public UpdateProgramCommandHandler(IProgramRepository programRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _mapper = mapper;
    }
    public async Task<ProgramResponseDto> Handle(SaveProgramCommand request, CancellationToken cancellationToken)
    {
        var programToSave = _mapper.Map<Program>(request);
        programToSave.CreatedDate = DateTime.UtcNow;
        var insertedProgram = await _programRepository.AddAsync(programToSave);
        return _mapper.Map<ProgramResponseDto>(insertedProgram);
    }
}
