using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Models.Pagination;
using Application.CQRS.Program_.Commands.AddExerciseToProgram;
using Application.CQRS.Program_.Commands.Delete;
using Application.CQRS.Program_.Commands.RemoveExerciseFromProgram;
using Application.CQRS.Program_.Commands.Save;
using Application.CQRS.Program_.Commands.Update;
using Application.CQRS.Program_.Queries.GetAllProgramList;
using Application.CQRS.Program_.Queries.GetProgramListByUser; 
using Application.CQRS.Program_.Commands.ExerciseCompleted;
using Application.CQRS.Program_.Commands.ExerciseNotCompleted;
using Application.CQRS.Program_.Queries.GetProgramListDetailByUserCurrentDayQuery;
using Application.CQRS.Program_.Commands.UpdateExerciseOfProgram; 
using Application.CQRS.Program_.Queries.GetProgramExercisesInteractionByRegion;
using Application.CQRS.Program_.Queries.GetProgramsImprovementsByUser;
using Application.CQRS.Program_.Queries.GetProgramExercisesByDay;
using Application.CQRS.Program_.Queries.GetProgramExercisesInteractionByRegionAndDay;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgramController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProgramController(IMediator mediator) => _mediator = mediator;


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PagingRequest? pagingRequest)
    {
        var response = await _mediator.Send(new GetAllProgramListQuery() { PagingRequest = pagingRequest });
        return Ok(response);
    }

    [HttpGet("GetAllByUser")]
    public async Task<IActionResult> GetAllByUser([FromQuery] GetProgramListByUserQuery programListByUserQuery)
    {
        var response = await _mediator.Send(programListByUserQuery);
        return Ok(response);
    }

    [HttpGet("GetProgramExercisesByDay")]
    public async Task<IActionResult> GetProgramExercisesByDay([FromQuery] GetProgramExercisesByDayQuery programExercisesByDayQuery)
    {
        var response = await _mediator.Send(programExercisesByDayQuery);
        return Ok(response);
    }


    [HttpGet("GetAllDetailCurrentDayByUser")]
    public async Task<IActionResult> GetAllDetailCurrentDayByUser([FromQuery] GetProgramListDetailByUserCurrentDayQuery  programListByUserCurrentDayQuery)
    { 
        var response = await _mediator.Send(programListByUserCurrentDayQuery);
        return Ok(response);
    }

    [HttpGet("GetProgramsImprovementsByUser")]
    public async Task<IActionResult> GetProgramsImprovementsByUser([FromQuery] GetProgramsImprovementsByUserQuery programsImprovementsByUserQuery)
    { 
        var response = await _mediator.Send(programsImprovementsByUserQuery);
        return Ok(response);
    }

    [HttpGet("GetProgramExercisesInteractionByRegion")]
    public async Task<IActionResult> GetProgramExercisesInteractionByRegion([FromQuery] GetProgramExercisesInteractionByRegionQuery getProgramExercisesInteractionByRegionQuery)
    {
        var response = await _mediator.Send(getProgramExercisesInteractionByRegionQuery);
        return Ok(response);
    }


    [HttpGet("GetProgramExercisesInteractionByRegionAndDay")]
    public async Task<IActionResult> GetProgramExercisesInteractionByRegionAndDay([FromQuery] GetProgramExercisesInteractionByRegionAndDayQuery programExercisesInteractionByRegionAndDayQuery)
    {
        var response = await _mediator.Send(programExercisesInteractionByRegionAndDayQuery);
        return Ok(response);
    }
    

    [HttpPost("Save")]
    public async Task<IActionResult> Save(SaveProgramCommand saveProgramCommand)
    {
        var response = await _mediator.Send(saveProgramCommand);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(UpdateProgramCommand updateProgramCommand)
    {
        var response = await _mediator.Send(updateProgramCommand);
        return Ok(response);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] DeleteProgramCommand deleteProgramCommand)
    {
        await _mediator.Send(deleteProgramCommand);
        return Ok();
    }

    [HttpPost("AddExerciseToProgram")]
    public async Task<IActionResult> AddExerciseToProgram(AddExerciseToProgramCommand addExerciseToProgramCommand)
    {
        var response = await _mediator.Send(addExerciseToProgramCommand);
        return Ok(response);
    }
    
    [HttpPut("UpdateExerciseOfProgram")]
    public async Task<IActionResult> UpdateExerciseOfProgram(UpdateExerciseOfProgramCommand updateExerciseOfProgramCommand)
    {
        var response = await _mediator.Send(updateExerciseOfProgramCommand);
        return Ok(response);
    }

    [HttpDelete("RemoveExerciseFromProgram")]
    public async Task<IActionResult> RemoveExerciseFromProgram([FromQuery] RemoveExerciseFromProgramCommand removeExerciseFromProgramCommand)
    {
        await _mediator.Send(removeExerciseFromProgramCommand);
        return Ok();
    }

    [HttpPost("ExerciseCompleted")]
    public async Task<IActionResult> ExerciseCompleted(ExerciseCompletedCommand exerciseCompleted)
    {
        await _mediator.Send(exerciseCompleted);
        return Ok();
    }

    [HttpPost("ExerciseNotCompleted")]
    public async Task<IActionResult> ExerciseNotCompleted(ExerciseNotCompletedCommand exerciseNotCompleted)
    {
        await _mediator.Send(exerciseNotCompleted);
        return Ok();
    }
}
