using Application.CQRS.Auth_.Commands.Register;
using Application.CQRS.Auth_.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Auth;

namespace Application.CQRS.Auth_.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Register
        CreateMap<UserRegisterCommand, User>();

        // Login
        CreateMap<User, UserResponseDto>();
        CreateMap<RefreshToken, RefreshTokenResponseDto>();
    }
}
