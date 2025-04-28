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
    private readonly IResponseService _workspaceService;

    public MapperController(IMapper mapper, ApplicationDbContext context, IResponseService workspaceService)
    {
        _mapper = mapper;
        _context = context;
        _workspaceService = workspaceService;
    }


    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateResponseRequest createRequest)
    {

      
        var folder = await _workspaceService.CreateResponseAsync(createRequest);
        return Ok(_mapper.Map<ResponseDto>(folder));
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var folder = await _workspaceService.GetAllResponsesAsync();
        return Ok(_mapper.Map<List<ResponseDto>>(folder));
    }
    
    [HttpGet("getById")]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var folder = await _workspaceService.GetResponseByIdAsync(id);
        return Ok(_mapper.Map<ResponseDto>(folder));
    }
    
    [HttpGet("getByRequestId")]
    public async Task<IActionResult> GetByRequestId([FromQuery] int id)
    {
        var folder = await _workspaceService.GetResponsesByRequestIdAsync(id);
        return Ok(_mapper.Map<List<ResponseDto>>(folder));
    }
    

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var request = await _workspaceService.DeleteResponseAsync(id);
        return Ok("Result is : "+request);
    }
   
}