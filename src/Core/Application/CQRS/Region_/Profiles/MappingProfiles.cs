using Application.CQRS.Region_.Commands.Create;
using Application.CQRS.Region_.Commands.Update;
using Application.CQRS.Region_.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.CQRS.Region_.Profiles;
 
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Region, RegionResponseDto>();

        // Create
        CreateMap<RegionCreateCommand, Region>();

        // Update
        CreateMap<RegionUpdateCommand, Region>();
    }
}