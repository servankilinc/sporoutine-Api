using Application.CQRS.Program_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetProgramListByUser;

public class GetProgramListByUserQueryHandler : IRequestHandler<GetProgramListByUserQuery, List<ProgramResponseDto>>
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;
    public GetProgramListByUserQueryHandler(IProgramRepository programRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _mapper = mapper;
    }

    public async Task<List<ProgramResponseDto>> Handle(GetProgramListByUserQuery request, CancellationToken cancellationToken)
    {
        var list = await _programRepository.GetAllAsync(filter: p => p.UserId == request.UserId);

        return list.Select(_mapper.Map<ProgramResponseDto>).ToList();
    }
}
