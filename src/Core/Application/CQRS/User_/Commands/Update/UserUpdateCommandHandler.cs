using Application.CQRS.Auth_.Dtos;
using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.User_.Commands.Update;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserUpdateCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<UserResponseDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        User existUser = await _userRepository.GetAsync(filter: u => u.Id == request.Id);
        if (existUser == null) throw new BusinessException("The data could not found to update");
        User userToUpdate = _mapper.Map(request, existUser);
        User updatedUser = await _userRepository.UpdateAsync(userToUpdate, cancellationToken);
        return _mapper.Map<UserResponseDto>(updatedUser);
    }
}