using Application.CQRS.Exercise_.Commands.Create;
using Application.CQRS.Exercise_.Commands.Update;
using Application.CQRS.Exercise_.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.CQRS.Exercise_.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Exercise, ExerciseResponseDto>();

        // Create
        CreateMap<ExerciseCreateCommand, Exercise>();

        // Update
        CreateMap<ExerciseUpdateCommand, Exercise>();
    }
}
