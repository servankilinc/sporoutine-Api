using Application.CQRS.Auth_.Dtos;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.User_.Queries.GetUserInfo;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserResponseDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public GetUserInfoQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        _mapper = mapper;
        _userManager = userManager;
    }    

    public async Task<UserResponseDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userManager.FindByIdAsync(request.Id.ToString());
        return _mapper.Map<UserResponseDto>(user);
    }
}
