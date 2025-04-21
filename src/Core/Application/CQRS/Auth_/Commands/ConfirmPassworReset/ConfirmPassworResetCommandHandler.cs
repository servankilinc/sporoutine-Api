using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.Auth_.Commands.ConfirmPassworReset;

public class ConfirmPassworResetCommandHandler : IRequestHandler<ConfirmPassworResetCommand, ConfirmPassworResetCommandResponseModel>
{
    private readonly UserManager<User> _userManager;
    public ConfirmPassworResetCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ConfirmPassworResetCommandResponseModel> Handle(ConfirmPassworResetCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) return new ConfirmPassworResetCommandResponseModel(false ,$"Unable to load user with ID '{request.UserId}'.");

        var result = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", request.Token);

        if (!result) return new ConfirmPassworResetCommandResponseModel(false, "Token is not valid");

        return new ConfirmPassworResetCommandResponseModel(true);
    }
}
