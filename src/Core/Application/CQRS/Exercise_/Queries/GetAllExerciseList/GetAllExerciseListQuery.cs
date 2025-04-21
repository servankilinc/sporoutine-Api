using Application.CQRS.Exercise_.Models;
using Domain.Models.Pagination;
using MediatR;

namespace Application.CQRS.Exercise_.Queries.GetAllExerciseList;

public class GetAllExerciseListQuery : IRequest<Paginate<ExerciseResponseModel>>
{
    public PagingRequest? PagingRequest { get; set; }
}
