using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Enums;
using Domain.Entities;
using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.EmailService;

namespace Application.CQRS.Auth_.Commands.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public UserRegisterCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IEmailService emailService, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailService = emailService;
        _mapper = mapper;
    }


    public async Task Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var userToRegister = _mapper.Map<User>(request);
        userToRegister.AuthenticatorType = AuthenticatorType.Email;
        userToRegister.UserName = $"{request.Email}_{DateTime.UtcNow.ToString()}";
        var resultRegister = await _userManager.CreateAsync(userToRegister, request.Password);
        if (!resultRegister.Succeeded) throw new BusinessException(string.Join(separator: $" ", value: resultRegister.Errors.Select(e => e.Description).ToArray()));

        bool isRoleUserExist = await _roleManager.RoleExistsAsync("User");
        if (!isRoleUserExist) await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
        var resultRole = await _userManager.AddToRoleAsync(userToRegister, "User");

        var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(userToRegister);

        string confirmationLink = $"http://localhost:5282/api/Auth/ConfirmEmail?token={confirmationToken}&userId={userToRegister.Id}";


        Console.WriteLine($"confirmationLink ======> {confirmationLink}  \n");
        //await _emailService.SendMailAsync(new()
        //{
        //    HtmlContent = $"Please <a href='{confirmationLink}'>click</a> to confirm your e-mail address.",
        //    RecipientEmailList = new List<string>() { userToRegister.Email! },
        //    Subject = "Confirm e-mail",
        //});
    }
}
