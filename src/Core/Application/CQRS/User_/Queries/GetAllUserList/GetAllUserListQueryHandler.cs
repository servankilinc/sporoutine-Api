using Application.CQRS.Auth_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Pagination;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.User_.Queries.GetAllUserList;

public class GetAllUserListQueryHandler : IRequestHandler<GetAllUserListQuery, Paginate<UserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetAllUserListQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Paginate<UserResponseDto>> Handle(GetAllUserListQuery request, CancellationToken cancellationToken)
    {
        Paginate<User>? result = await _userRepository.GetPaginatedListAsync(index: request.PagingRequest!.Page, size: request.PagingRequest.PageSize);
        var mappedItems = result.Items.Select(_mapper.Map<UserResponseDto>).ToList();

        return new Paginate<UserResponseDto>()
        {
            Items = mappedItems,
            Count = result.Count,
            Index = result.Index,
            Pages = result.Pages,
            Size = result.Size
        };
    }
}
