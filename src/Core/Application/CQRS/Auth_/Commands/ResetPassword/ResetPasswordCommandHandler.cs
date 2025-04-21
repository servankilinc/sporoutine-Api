using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.EmailService;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.Auth_.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    
    public ResetPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }
    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        User? existUser = await _userManager.FindByEmailAsync(request.Email!);
        if (existUser == null) throw new BusinessException($"User could not found for {request.Email}");

        var confirmationToken = await _userManager.GeneratePasswordResetTokenAsync(existUser);

        string confirmationLink = $"http://localhost:5282/api/Auth/ConfirmPasswordReset?token={confirmationToken}&userId={existUser.Id}";

        await _emailService.SendMailAsync(new()
        {
            HtmlContent = $"Please <a href='{confirmationLink}'>click</a> to verify your password reset process.",
            RecipientEmailList = new List<string>() { existUser.Email! },
            Subject = "Verify Password Reset Process",
        });
    }
}
