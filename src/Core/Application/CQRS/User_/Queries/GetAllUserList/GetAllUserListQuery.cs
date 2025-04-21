using Application.CQRS.Auth_.Dtos;
using Domain.Models.Pagination;
using MediatR;

namespace Application.CQRS.User_.Queries.GetAllUserList;

public class GetAllUserListQuery : IRequest<Paginate<UserResponseDto>>
{
    public PagingRequest? PagingRequest { get; set; }
}
