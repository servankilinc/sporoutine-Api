using Application.CQRS.Program_.Commands.AddExerciseToProgram;
using Application.CQRS.Program_.Commands.Save;
using Application.CQRS.Program_.Commands.Update;
using Application.CQRS.Program_.Commands.UpdateExerciseOfProgram;
using Application.CQRS.Program_.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.CQRS.Program_.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Program, ProgramResponseDto>();

        // Save
        CreateMap<SaveProgramCommand, Program>();

        // Update
        CreateMap<UpdateProgramCommand, Program>();

        // Program Exercise Processes ...
        CreateMap<ProgramExercise, ProgramExerciseResponseDto> ();

        CreateMap<AddExerciseToProgramCommand, ProgramExercise>();

        CreateMap<UpdateExerciseOfProgramCommand, ProgramExercise>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProgramExerciseId));
    }
}
