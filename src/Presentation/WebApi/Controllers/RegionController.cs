using Application.CQRS.Region_.Commands.Create;
using Application.CQRS.Region_.Commands.Delete;
using Application.CQRS.Region_.Commands.Update;
using Application.CQRS.Region_.Queries.Get;
using Application.CQRS.Region_.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public RegionController(IMediator mediator, IWebHostEnvironment hostingEnvironment)
    {
        _mediator = mediator;
        _hostingEnvironment = hostingEnvironment;
    }


    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromQuery] GetRegionQuery regionQuery)
    {
        var response = await _mediator.Send(regionQuery);
        return Ok(response);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRegionQuery allRegionQuery)
    {
        var response = await _mediator.Send(allRegionQuery);
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(RegionCreateCommand regionCreateModel)
    {
        //if (regionCreateModel.File == null || regionCreateModel.File.Length == 0)
        //{
        //    return BadRequest("No file provided.");
        //}

        //var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Regions");

        //if (!Directory.Exists(uploadFolder))
        //{
        //    Directory.CreateDirectory(uploadFolder);
        //}
         
        //var fileName = Path.GetFileName(regionCreateModel.File.FileName);
        //var filePath = Path.Combine(uploadFolder, fileName);
         
        //using (var stream = new FileStream(filePath, FileMode.Create))
        //{
        //    await regionCreateModel.File.CopyToAsync(stream);
        //}
         
        //var fileUrl = $"/Regions/{fileName}";

        var response = await _mediator.Send(regionCreateModel);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(RegionUpdateCommand regionUpdateCommand)
    {
        var response = await _mediator.Send(regionUpdateCommand);
        return Ok(response);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] RegionDeleteCommand regionDeleteCommand)
    {
        await _mediator.Send(regionDeleteCommand);
        return Ok();
    }

    public class RegionCreateModel: RegionCreateCommand
    {
        public IFormFile? File { get; set; }
    }
}
