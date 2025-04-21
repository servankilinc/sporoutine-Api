using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Services.TokenService;
using Domain.Models.Auth;
using Application.Services.EmailService;
using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.Repositories;
using Application.CQRS.Auth_.Commands.Login.Models;
using Application.CQRS.Auth_.Dtos;
using System.Net;

namespace Application.CQRS.Auth_.Commands.Login;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginResponseModel>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    public UserLoginCommandHandler(IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager, TokenService tokenService, IEmailService emailService, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userManager = userManager;
        _tokenService = tokenService;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<UserLoginResponseModel> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userManager.FindByEmailAsync(request.UserLoginRequestModel.Email);
        if (existUser == null) throw new BusinessException($"User could not found for {request.UserLoginRequestModel.Email}");

        bool passwordStatus = await _userManager.CheckPasswordAsync(existUser, request.UserLoginRequestModel.Password);
        if (!passwordStatus) throw new BusinessException("Password does not correct");

        if (!existUser.EmailConfirmed)
        {
            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(existUser);
            string encodedToken = WebUtility.UrlEncode(confirmationToken);
            string confirmationLink = $"http://localhost:5282/api/Auth/ConfirmEmail?userId={existUser.Id}&token={encodedToken}";

            await _emailService.SendMailAsync(new()
            {
                HtmlContent = $"Please <a href='{confirmationLink}'>click</a> to confirm your e-mail address.",
                RecipientEmailList = new List<string>() { request.UserLoginRequestModel.Email },
                Subject = "Confirm e-mail",
            });

            throw new BusinessException("Email_Confirmed_Error");
        }

        AccessTokenModel accessToken = await _tokenService.CreateAccessTokenAsync(existUser);

        // refresh token
        var generatedRefreshToken = _tokenService.GenerateNewRefreshToken(existUser, request.IpAddress);
        bool isRefreshTokenExist = await _refreshTokenRepository.IsExistAsync(filter: rt => rt.UserId == existUser.Id && rt.CreatedIp == request.IpAddress);
        var refreshToken = isRefreshTokenExist ?
            await _refreshTokenRepository.UpdateAsync(generatedRefreshToken, cancellationToken) :
            await _refreshTokenRepository.AddAsync(generatedRefreshToken, cancellationToken);
        // refresh token


        UserLoginResponseModel responseModel = new()
        {
            AccessToken = accessToken,
            User = _mapper.Map<UserResponseDto>(existUser),
            RefreshToken = _mapper.Map<RefreshTokenResponseDto>(refreshToken)
        };

        return responseModel;
    }
}