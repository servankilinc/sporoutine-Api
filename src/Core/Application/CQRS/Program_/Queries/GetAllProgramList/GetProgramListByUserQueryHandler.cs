using Application.CQRS.Program_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Pagination;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetAllProgramList;

public class GetAllProgramListQueryHandler : IRequestHandler<GetAllProgramListQuery, Paginate<ProgramResponseDto>>
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;
    public GetAllProgramListQueryHandler(IProgramRepository programRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _mapper = mapper;
    }

    public async Task<Paginate<ProgramResponseDto>> Handle(GetAllProgramListQuery request, CancellationToken cancellationToken)
    {
        Paginate<Program> result = await _programRepository.GetPaginatedListAsync(
            orderBy: f => f.OrderBy(x => x.CreatedDate),
            index: request.PagingRequest!.Page, 
            size: request.PagingRequest!.PageSize
        );
        var mappedItems = result.Items.Select(_mapper.Map<ProgramResponseDto>).ToList();
        
        return new Paginate<ProgramResponseDto>()
        {
            Items = mappedItems,
            Count = result.Count,
            Index = result.Index,
            Pages = result.Pages,
            Size = result.Size
        };
    }
}
