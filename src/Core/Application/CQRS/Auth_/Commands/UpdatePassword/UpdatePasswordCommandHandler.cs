using Application.GlobalExceptionHandler.CustomExceptions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.Auth_.Commands.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand>
{
    private readonly UserManager<User> _userManager;
    public UpdatePasswordCommandHandler(UserManager<User> userManager) => _userManager = userManager;
    
    public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        User? existUser = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (existUser == null) throw new BusinessException($"Unable to load user with ID '{request.UserId}'.");
        var result = await _userManager.ResetPasswordAsync(existUser, request.Token, request.NewPassword);
        if (!result.Succeeded) throw new BusinessException(result.Errors.Select(e=> e.Description).FirstOrDefault()!);
    }
}
