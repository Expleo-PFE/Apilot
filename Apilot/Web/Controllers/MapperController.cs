using Apilot.Application.DTOs.Collection;
using Apilot.Application.DTOs.WorkSpace;
using Apilot.Application.Features.Workspace.Commands;
using Apilot.Application.Features.Workspace.Queries;
using Apilot.Application.Interfaces;
using Apilot.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Apilot.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class MapperController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly ICollectionService _workspaceService;

    public MapperController(IMapper mapper, ApplicationDbContext context, ICollectionService workspaceService, IMediator mediator)
    {
        _mapper = mapper;
        _context = context;
        _workspaceService = workspaceService;
        _mediator = mediator;
    }

    [HttpPost("createCollection")]
    public async Task<IActionResult> Save([FromBody] CreateWorkSpaceRequest createRequest)
    {
        var command = new CreateWorkSpaceCommand { WorkSpaceDto = createRequest };
        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return Ok(result.Data);
            
        return BadRequest(result.Error);
    }
    
    [HttpPut("updateCollection")]
    public async Task<IActionResult> Update([FromBody] UpdateWorkSpaceRequest request)
    {
        var command = new UpdateWorkspaceCommand {UpdateWorkspace = request };
        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return NoContent();
            
        return result.Error?.Contains("not found") == true ? NotFound(result.Error) : BadRequest(result.Error);

    }

    [HttpGet("getCollections")]
    public async Task<IActionResult> GetWorkspaces()
    {
        var result = await _mediator.Send(new GetWorkSpacesQuery());
        
        if (result.IsSuccess)
            return Ok(result.Data);
            
        return BadRequest(result.Error);
    }
    
   
}