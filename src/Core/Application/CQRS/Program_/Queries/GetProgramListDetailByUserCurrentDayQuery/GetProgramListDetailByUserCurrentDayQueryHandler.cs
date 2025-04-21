using Application.CQRS.Program_.Dtos;
using Application.CQRS.Program_.Models;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetProgramListDetailByUserCurrentDayQuery;

public class GetProgramListDetailByUserCurrentDayQueryHandler : IRequestHandler<GetProgramListDetailByUserCurrentDayQuery, List<ProgramDetailCurrentDayResponseModel>>
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;
    public GetProgramListDetailByUserCurrentDayQueryHandler(IProgramRepository programRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _mapper = mapper;
    }

    public async Task<List<ProgramDetailCurrentDayResponseModel>> Handle(GetProgramListDetailByUserCurrentDayQuery request, CancellationToken cancellationToken)
    {
        var programList = await _programRepository.GetAllProgramExercisesDetailCurrentDayAsync(request.UserId, cancellationToken);

        var model = programList.Select(p =>
            new ProgramDetailCurrentDayResponseModel()
            {
                Program = _mapper.Map<ProgramResponseDto>(p),
                ExerciseList = p.ProgramExercises!.Select(pe => new FulfillmentsCurrentDayModel()
                {
                    ExerciseId = pe.Exercise!.Id,
                    Name = pe.Exercise.Name,
                    Content = pe.Exercise.Content,
                    Description = pe.Exercise.Description,
                    ExerciseType = pe.Exercise.ExerciseType,
                    ProgramExerciseId = pe.Id,
                    AddedDate = pe.AddedDate.GetClientLocalDate(),
                    Day = pe.Day,
                    NumberOfSets = pe.NumberOfSets,
                    NumberOfRepetition = pe.NumberOfRepetition,
                    Time = pe.Time,
                    // business fields ...
                    CompletionStatus = pe.Fulfillments != null ? pe.Fulfillments.Any(f => DateOnly.FromDateTime(f.CompletionDate.GetClientLocalDate()) == DateOnly.FromDateTime(DateTime.UtcNow.GetClientLocalDate()) ) : default,
                    CompletionCount = pe.Fulfillments != null ? pe.Fulfillments.Count : 0,
                    InCompletionCount = pe.Fulfillments != null ? FindMissedDays(
                        pe.AddedDate.GetClientLocalDate(), 
                        pe.Fulfillments.Select(f => DateOnly.FromDateTime(f.CompletionDate.GetClientLocalDate())).ToHashSet()!, 
                        pe.Day) : default,

                }).ToList()
            }
        ).ToList();

        return model;
    }

    private int FindMissedDays(DateTime addedDate, HashSet<DateOnly> completionList, int day)
    {
        int addedDateIndex = ((int)addedDate.DayOfWeek + 6) % 7 + 1;

        var now = DateTime.UtcNow.GetClientLocalDate();

        int dayDifference = (day - addedDateIndex + 7) % 7;

        DateTime firstCheckDate = addedDate.AddDays(dayDifference);

        int missedDayCount = 0;
        for (DateTime i = firstCheckDate.Date; i <= now.Date; i = i.AddDays(7))
        {
            if (!completionList.Contains(DateOnly.FromDateTime(i))) missedDayCount++;
        }
        return missedDayCount;
    }
}