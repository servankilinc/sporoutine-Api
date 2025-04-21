using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Application.CQRS.Auth_.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ConfirmEmailCommandResponseModel>
{
    private readonly UserManager<User> _userManager;
    public ConfirmEmailCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ConfirmEmailCommandResponseModel> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) return new ConfirmEmailCommandResponseModel(false, $"Unable to load user with ID '{request.UserId}'.");

        string decodedToken = WebUtility.UrlDecode(request.Token);
        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
        return new ConfirmEmailCommandResponseModel(result.Succeeded);
    }
}
