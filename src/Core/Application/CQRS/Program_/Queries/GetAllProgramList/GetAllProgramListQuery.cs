using Application.CQRS.Program_.Dtos;
using Domain.Models.Pagination;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetAllProgramList;

public class GetAllProgramListQuery : IRequest<Paginate<ProgramResponseDto>>
{
    public PagingRequest? PagingRequest { get; set; }
}
