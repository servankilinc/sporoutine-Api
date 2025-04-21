using Application.CQRS.Auth_.Commands.Login.Models;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Auth_.Commands.Login;

public class UserLoginCommand : IRequest<UserLoginResponseModel>
{
    public UserLoginRequestModel UserLoginRequestModel { get; set; } = null!;
    public string IpAddress { get; set; } = null!;
}

public class UserLoginValidator : AbstractValidator<UserLoginCommand>
{
    public UserLoginValidator()
    {
        RuleFor(b => b.UserLoginRequestModel.Email).EmailAddress().NotEmpty();
        RuleFor(b => b.UserLoginRequestModel.Password).MinimumLength(6).NotEmpty();
        RuleFor(b => b.IpAddress).NotEmpty();
    }
}