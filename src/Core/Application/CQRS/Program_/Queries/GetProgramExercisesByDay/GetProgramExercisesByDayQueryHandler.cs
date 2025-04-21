using Application.CQRS.Program_.Models;
using Application.Services.Repositories;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetProgramExercisesByDay;

public class GetProgramExercisesByDayQueryHandler : IRequestHandler<GetProgramExercisesByDayQuery, List<ProgramExerciseBaseModel>>
{
    private readonly IProgramRepository _programRepository;
    public GetProgramExercisesByDayQueryHandler(IProgramRepository programRepository) => _programRepository = programRepository; 
    

    public async Task<List<ProgramExerciseBaseModel>> Handle(GetProgramExercisesByDayQuery request, CancellationToken cancellationToken)
    {
        var programExercises = await _programRepository.GetProgramExercisesByDayAsync(request.ProgramId, request.Day, cancellationToken);

        var exerciseList = programExercises.Select(pe =>
            new ProgramExerciseBaseModel()
            {
                ExerciseId = pe.Exercise!.Id,
                Name = pe.Exercise.Name,
                Content = pe.Exercise.Content,
                Description = pe.Exercise.Description,
                ExerciseType = pe.Exercise.ExerciseType,
                ProgramExerciseId = pe.Id,
                AddedDate = pe.AddedDate,
                Day = pe.Day,
                NumberOfSets = pe.NumberOfSets,
                NumberOfRepetition = pe.NumberOfRepetition,
                Time = pe.Time,                
            }
        ).ToList();

        return exerciseList;
    }
}