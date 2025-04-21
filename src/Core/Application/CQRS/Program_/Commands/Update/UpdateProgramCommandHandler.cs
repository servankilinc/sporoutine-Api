using Application.CQRS.Program_.Dtos;
using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Program_.Commands.Update;

public class UpdateProgramCommandHandler : IRequestHandler<UpdateProgramCommand, ProgramResponseDto>
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;
    public UpdateProgramCommandHandler(IProgramRepository programRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _mapper = mapper;
    }

    public async Task<ProgramResponseDto> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
    {
        var existProgram = await _programRepository.GetAsync(filter: f => f.Id == request.Id);
        if (existProgram == null) throw new BusinessException("The program could not be found to update");
        var programToUpdate = _mapper.Map(request, existProgram);
        var updatedProgram = await _programRepository.UpdateAsync(programToUpdate);
        return _mapper.Map<ProgramResponseDto>(updatedProgram);
    }
}
