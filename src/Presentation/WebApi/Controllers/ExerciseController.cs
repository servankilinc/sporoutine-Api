using Application.CQRS.Exercise_.Commands.Create;
using Application.CQRS.Exercise_.Commands.Delete;
using Application.CQRS.Exercise_.Commands.Update;
using Application.CQRS.Exercise_.Queries.GetAllExerciseList;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Models.Pagination;
using Application.CQRS.Exercise_.Queries.GetExerciseListByRegion;
using Application.CQRS.Exercise_.Queries.GetExerciseListByProgram;
using Application.CQRS.Exercise_.Commands.RemoveRegionFromExercise;
using Application.CQRS.Exercise_.Commands.AddRegionToExercise;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExerciseController : ControllerBase
{
    private readonly IMediator _mediator;
    public ExerciseController(IMediator mediator) => _mediator = mediator;


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PagingRequest? pagingRequest)
    {
        var response = await _mediator.Send(new GetAllExerciseListQuery() { PagingRequest = pagingRequest });
        return Ok(response);
    }

    [HttpGet("GetAllByProgram")]
    public async Task<IActionResult> GetAllByProgram([FromQuery] GetExerciseListByProgramQuery exerciseListByProgram)
    {
        var response = await _mediator.Send(exerciseListByProgram);
        return Ok(response);
    }

    [HttpGet("GetAllByRegion")]
    public async Task<IActionResult> GetAllByRegion([FromQuery] GetExerciseListByRegionQuery exerciseListByRegionQuery)
    {
        var response = await _mediator.Send(exerciseListByRegionQuery);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ExerciseCreateCommand exerciseCreate)
    {
        var response = await _mediator.Send(exerciseCreate);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ExerciseUpdateCommand exerciseUpdate)
    {
        var response = await _mediator.Send(exerciseUpdate);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] ExerciseDeleteCommand exerciseDeleteCommand)
    {
        await _mediator.Send(exerciseDeleteCommand);
        return Ok();
    }



    [HttpPost("AddRegionToExercise")]
    public async Task<IActionResult> AddRegionToExercise(AddRegionToExerciseCommand addRegionToExerciseCommand)
    {
        await _mediator.Send(addRegionToExerciseCommand);
        return Ok();
    }

    [HttpPost("RemoveRegionFromExercise")]
    public async Task<IActionResult> RemoveRegionFromExercise(RemoveRegionFromExerciseCommand removeRegionFromExerciseCommand)
    {
        await _mediator.Send(removeRegionFromExerciseCommand);
        return Ok();
    }
}
