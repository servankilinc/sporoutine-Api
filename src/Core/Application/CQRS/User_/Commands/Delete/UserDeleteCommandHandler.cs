using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.User_.Commands.Delete;

public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand>
{
    private readonly IUserRepository _userRepository; 
    public UserDeleteCommandHandler(IUserRepository userRepository, IMapper mapper) => _userRepository = userRepository;

    public async Task Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteByFilterAsync(filter: u => u.Id == request.Id);
    }
}