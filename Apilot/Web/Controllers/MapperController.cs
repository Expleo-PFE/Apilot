using Apilot.Application.DTOs.Collection;
using Apilot.Application.DTOs.Envirenoment;
using Apilot.Application.DTOs.Folder;
using Apilot.Application.DTOs.History;
using Apilot.Application.DTOs.Request;
using Apilot.Application.DTOs.Response;
using Apilot.Application.DTOs.WorkSpace;
using Apilot.Application.Interfaces;
using Apilot.Domain.Entities;
using Apilot.Infrastructure.Data;
using Apilot.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apilot.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class MapperController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    private readonly IHistoryService _workspaceService;

    public MapperController(IMapper mapper, ApplicationDbContext context, IHistoryService workspaceService)
    {
        _mapper = mapper;
        _context = context;
        _workspaceService = workspaceService;
    }

    [HttpPost("workspace")]
    public async Task<IActionResult> Save([FromBody] CreateWorkSpaceRequest createRequest)
    {
        var workspace = new WorkSpaceEntity
        {
            Name = createRequest.Name,
            Description = createRequest.Description,
            CreatedAt = DateTime.Now,
            CreatedBy = "admin",
            IsDeleted = false,
            IsSync = false,
            SyncId = Guid.NewGuid()
        };
        await _context.WorkSpaces.AddAsync(workspace);
       await _context.SaveChangesAsync();
        return Ok(workspace);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateHistoryRequest createRequest)
    {
        var folder = await _workspaceService.CreateHistoryAsync(createRequest);
        return Ok(_mapper.Map<HistoryDto>(folder));
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var folder = await _workspaceService.GetAllHistoryAsync();
        return Ok(_mapper.Map<List<HistoryDto>>(folder));
    }
    
    [HttpGet("getById")]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var folder = await _workspaceService.GetHistoryByIdAsync(id);
        return Ok(_mapper.Map<HistoryDto>(folder));
    }
    
    [HttpGet("getByWorkSpaceId")]
    public async Task<IActionResult> GetByRequestId([FromQuery] int id)
    {
        var folder = await _workspaceService.GetHistoryByWorkspaceIdAsync(id);
        return Ok(_mapper.Map<List<HistoryDto>>(folder));
    }
    

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var request = await _workspaceService.DeleteHistoryAsync(id);
        return Ok("Result is : "+request);
    }
   
}