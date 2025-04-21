using Application.CQRS.User_.Commands.Delete;
using Application.CQRS.User_.Commands.Update;
using Application.CQRS.User_.Queries.GetAllUserList;
using Application.CQRS.User_.Queries.GetUserInfo;
using Domain.Models.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> Get([FromQuery] GetUserInfoQuery userInfoQuery)
    {
        var response = await _mediator.Send(userInfoQuery);
        return Ok(response);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] PagingRequest? pagingRequest)
    {
        var response = await _mediator.Send(new GetAllUserListQuery() { PagingRequest = pagingRequest } );
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserUpdateCommand updateCommand)
    {
        var response = await _mediator.Send(updateCommand);
        return Ok(response);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] UserDeleteCommand deleteCommand)
    {
        await _mediator.Send(deleteCommand);
        return Ok();
    }
}
