using Application.CQRS.Auth_.Commands.Register;
using Application.CQRS.Auth_.Dtos;
using Application.CQRS.User_.Commands.Update;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Auth;

namespace Application.CQRS.User_.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Update
        CreateMap<UserUpdateCommand, User>();
    }
}
