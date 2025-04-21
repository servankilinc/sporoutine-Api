using Application.CQRS.PhysicalData_.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.CQRS.PhysicalData_.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PhysicalData, PhysicalDataResponseDto>();
        CreateMap<WeightHistoryData, WeightHistoryDataResponseDto>();
    }
}
