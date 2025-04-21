using Application.CQRS.Program_.Dtos;
using Application.CQRS.Program_.Models;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
namespace Application.CQRS.Program_.Queries.GetProgramsImprovementsByUser;

public class GetProgramsImprovementsByUserQueryHandler : IRequestHandler<GetProgramsImprovementsByUserQuery, List<ProgramImprovementModel>>
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;
    public GetProgramsImprovementsByUserQueryHandler(IProgramRepository programRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _mapper = mapper;
    }

    public async Task<List<ProgramImprovementModel>> Handle(GetProgramsImprovementsByUserQuery request, CancellationToken cancellationToken)
    {
        var programs = await _programRepository.GetProgramsImprovementsByUserAsync(request.UserId, cancellationToken);


        var responseModel = programs.Select(p =>
            new ProgramImprovementModel()
            {
                Program = _mapper.Map<ProgramResponseDto>(p),
                ProgramExerciseFulfillments = p.ProgramExercises == null ? default : 
                    p.ProgramExercises.Select(pe =>
                        new ProgramExerciseFulfillmentModel()
                        {
                            ProgramExerciseId = pe.Id,
                            AddedDate = pe.AddedDate.GetClientLocalDate(),
                            Day = pe.Day,
                            ExerciseId = pe.ExerciseId,
                            CompletionCount = pe.Fulfillments != null ? pe.Fulfillments.Count : 0,
                            CompletionDateList = pe.Fulfillments != null ? pe.Fulfillments.Select(f => f.CompletionDate.GetClientLocalDate()).ToList() : default,
                            InCompletionCount = pe.Fulfillments != null ? FindMissedDays(
                                pe.AddedDate.GetClientLocalDate(),
                                pe.Fulfillments.Select(f => DateOnly.FromDateTime(f.CompletionDate.GetClientLocalDate())).ToHashSet()!,
                                pe.Day)
                                : default
                        }
                    ).ToList()
            }
        ).ToList();
         

        return responseModel;
    }

    private int FindMissedDays(DateTime addedDate, HashSet<DateOnly> completionList, int day)
    {
        // (1 - Monday, 7 - sunday)
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
