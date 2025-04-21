using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.Repositories;
using Application.Services.TokenService;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Auth_.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResponseModel>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly TokenService _tokenService;
    private readonly IMapper _mapper;
    public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, TokenService tokenService, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<RefreshTokenCommandResponseModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenListByIp = await _refreshTokenRepository.GetAllAsync(filter: r => r.CreatedIp == request.IpAddress, include: r => r.Include(r => r.User!));
        if (refreshTokenListByIp == null || refreshTokenListByIp.Count == 0) throw new BusinessException("Not found any refresh token");

        var refreshToken = refreshTokenListByIp.Where(r => r.Token == request.RefreshToken).SingleOrDefault();
        if (refreshToken == null) throw new BusinessException("Invalid refresh token");
        User? user = refreshToken.User;
        if (user == null) throw new BusinessException("User does not read in token");

        if (refreshToken.Expiration < DateTime.UtcNow) throw new BusinessException("Refresh token over");


        RefreshTokenCommandResponseModel responseModel = new();

        if (refreshToken.TTL <= 0)
        {
            var newRefreshToken = _tokenService.GenerateNewRefreshToken(user, request.IpAddress);
            var updatedRefreshToken = await _refreshTokenRepository.UpdateAsync(newRefreshToken, cancellationToken);
            responseModel.RefreshToken = _mapper.Map<RefreshTokenResponseDto>(updatedRefreshToken);
        }
        else
        {
            refreshToken.TTL -= 1;
            var updatedRefreshToken = await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);
            responseModel.RefreshToken = _mapper.Map<RefreshTokenResponseDto>(updatedRefreshToken);
        }

        responseModel.AccessToken = await _tokenService.CreateAccessTokenAsync(user);

        return responseModel;
    }
}
