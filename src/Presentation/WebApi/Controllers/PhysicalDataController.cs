using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.CQRS.PhysicalData_.Commands.SaveOrUpdateHeight;
using Application.CQRS.PhysicalData_.Commands.SaveOrUpdateWeight;
using Application.CQRS.PhysicalData_.Queries.GetPhysicalData;
using Application.CQRS.PhysicalData_.Commands.DeleteWeightHistoryData;
using Application.CQRS.PhysicalData_.Queries.GetWeightHistoryDataByUser;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhysicalDataController : ControllerBase
{
    private readonly IMediator _mediator;
    public PhysicalDataController(IMediator mediator) => _mediator = mediator;


    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPhysicalDataQuery physicalDataQuery)
    {
        var response = await _mediator.Send(physicalDataQuery);
        return Ok(response);
    }

    [HttpGet("WeightHistoryData")]
    public async Task<IActionResult> GetWeightHistoryData([FromQuery] GetWeightHistoryDataByUserQuery weightDataHistoryByUserQuery)
    {
        var response = await _mediator.Send(weightDataHistoryByUserQuery);
        return Ok(response);
    }

    [HttpPost("SaveOrUpdateHeight")]
    public async Task<IActionResult> SaveOrUpdateHeight(SaveOrUpdateHeightCommand saveOrUpdateHeight)
    {
        await _mediator.Send(saveOrUpdateHeight);
        return Ok();
    }

    [HttpPost("SaveOrUpdateWeight")]
    public async Task<IActionResult> SaveOrUpdateWeight(SaveOrUpdateWeightCommand saveOrUpdateWeight)
    {
        await _mediator.Send(saveOrUpdateWeight);
        return Ok();
    }

    [HttpDelete("DeleteWeightHistoryData")]
    public async Task<IActionResult> DeleteWeightHistoryData([FromQuery] DeleteWeightHistoryDataCommand deleteWeightData)
    {
        await _mediator.Send(deleteWeightData);
        return Ok();
    }
}
