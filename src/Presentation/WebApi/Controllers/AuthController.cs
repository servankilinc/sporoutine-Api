using Domain.Models.Auth;
using MediatR;
using Application.GlobalExceptionHandler.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.Auth_.Commands.Register;
using Application.CQRS.Auth_.Commands.Login.Models;
using Application.CQRS.Auth_.Commands.ConfirmEmail;
using Application.CQRS.Auth_.Commands.Login;
using Application.CQRS.Auth_.Commands.RefreshToken;
using Application.CQRS.Auth_.Commands.ResetPassword;
using Application.CQRS.Auth_.Commands.ConfirmPassworReset;
using Application.CQRS.Auth_.Commands.UpdatePassword;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator) => _mediator = mediator;
    

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterCommand registerCommand)
    { 
        await _mediator.Send(registerCommand);

        return Ok(); 
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestModel userLoginRequest)
    {
        var responseModel = await _mediator.Send(new UserLoginCommand() { UserLoginRequestModel = userLoginRequest, IpAddress = GetIpAddress() });
        SetRefreshTokenToCookie(responseModel.RefreshToken);
        return Ok(responseModel);
    }


    [HttpGet("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        if (Request.Cookies["refreshToken"] == null) throw new BusinessException("Refresh token could not found in cookie");
        var responseModel = await _mediator.Send(new RefreshTokenCommand() { RefreshToken = Request.Cookies["refreshToken"]!, IpAddress = GetIpAddress() });
        string ipAddress = GetIpAddress();

        SetRefreshTokenToCookie(responseModel.RefreshToken);
        return Ok(new { responseModel.RefreshToken, responseModel.AccessToken });
    }


    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
    {
        var result = await _mediator.Send(new ConfirmEmailCommand() { UserId = userId, Token = token });
  
        if (result.Success)
        {
            var successHtml = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/EmailConfirmation_success.html"));
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = 200,
                Content = successHtml
            };
        }
        var errorHtml = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/EmailConfirmation_error.html"));
        return new ContentResult
        {
            ContentType = "text/html",
            StatusCode = 400,
            Content = errorHtml
        };
    }



    [HttpPost("PasswordResetDemand")]
    public async Task<IActionResult> PasswordResetDemand([FromBody] ResetPasswordCommand resetPasswordCommand)
    {
        await _mediator.Send(resetPasswordCommand);

        return Ok();
    }


    [HttpGet("ConfirmPasswordReset")]
    public async Task<IActionResult> ConfirmPasswordReset(string token, Guid userId)
    {
        var result = await _mediator.Send(new ConfirmPassworResetCommand() { UserId = userId, Token = token});

        if (result.Success)
        {
            var successHtml = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PasswordResetConfirmation_success.html"));
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = 200,
                Content = successHtml
            };
        }
        var errorHtml = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PasswordResetConfirmation_error.html"));
        return new ContentResult
        {
            ContentType = "text/html",
            StatusCode = 400,
            Content = errorHtml
        };
    }

    [HttpPost("PasswordUpdate")]
    public async Task<IActionResult> PasswordUpdate([FromBody] UpdatePasswordCommand updatePasswordCommand)
    {
        await _mediator.Send(updatePasswordCommand);
        return Ok();
    }


    // HELPER METHODS ...
    private void SetRefreshTokenToCookie(RefreshTokenResponseDto refreshToken)
    {
        CookieOptions cookieOptions = new CookieOptions() { HttpOnly = true, Expires = refreshToken.Expiration };
        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }

    private string GetIpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"]!;
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;
    }
}
