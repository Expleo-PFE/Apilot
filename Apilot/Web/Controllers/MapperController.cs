using Apilot.Application.DTOs.Collection;
using Apilot.Application.DTOs.Folder;
using Apilot.Application.DTOs.History;
using Apilot.Application.DTOs.Request;
using Apilot.Application.DTOs.Response;
using Apilot.Application.DTOs.WorkSpace;
using Apilot.Domain.Entities;
using Apilot.Infrastructure.Data;
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

    public MapperController(IMapper mapper, ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    
    [HttpPost("createWorkSpace")]
    public async Task<IActionResult> CreateWorkspace([FromBody] CreateWorkSpaceDto request)
    {
        var workspace = new WorkSpaceEntity
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.Now,
            IsSync = false,
            IsDeleted = false,
            CreatedBy = "admin",
            UpdatedBy = "admin",
            SyncId = Guid.NewGuid()
        };
        
        await _context.WorkSpaces.AddAsync(workspace);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<WorkSpaceDto>(workspace));
    }

    [HttpGet("getAllWorkspaces")]
    public async Task<IActionResult> GetAllWorkspaces()
    {
        var workspaces = await _context
            .WorkSpaces
            .Include(w => w.Collections)
            .Include(w => w.Environements)
            .ToListAsync();
        
        return Ok(_mapper.Map<IEnumerable<WorkSpaceDto>>(workspaces));
    }

    [HttpDelete("deleteWorkspaces")]
    public async Task<IActionResult> DeleteWorkSpace([FromQuery] int id)
    {
        var workspace = await _context.WorkSpaces.FindAsync(id);

        if (workspace == null)
        {
            return NotFound("Not Found");
        }
        _context.WorkSpaces.Remove(workspace);
        await _context.SaveChangesAsync();
        return Ok("Workspace deleted");
    }
    
    
    
    //Collection
    [HttpPost("createCollection")]
    public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionDto request)
    {
        var collection = new CollectionEntity
        {
            Name = request.Name,
            Description = request.Description,
            WorkSpaceId = request.WorkSpaceId,
            CreatedAt = DateTime.Now,
            IsSync = false,
            IsDeleted = false,
            CreatedBy = "admin",
            UpdatedBy = "admin",
            SyncId = Guid.NewGuid()
        };
        
        await _context.Collections.AddAsync(collection);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<CollectionDto>(collection));
    }
    
    [HttpGet("getAllCollections")]
    public async Task<IActionResult> GetAllCollections()
    {
        var collections = await _context
            .Collections
            .Include(w => w.Folders)
            .Include(w => w.HttpRequests)
            .ToListAsync();
        
        return Ok(_mapper.Map<IEnumerable<CollectionDto>>(collections));
    }
    
    [HttpDelete("deleteCollection")]
    public async Task<IActionResult> DeleteCollection([FromQuery] int id)
    {
        var collection = await _context.Collections.FindAsync(id);

        if (collection == null)
        {
            return NotFound("Not Found");
        }
        _context.Collections.Remove(collection);
        await _context.SaveChangesAsync();
        return Ok("Collection deleted");
    }
    
    //Folder
    [HttpPost("createFolder")]
    public async Task<IActionResult> CreateFolder([FromBody] CreateFolderDto request)
    {
        var folder = new FolderEntity
        {
            Name = request.Name,
            CollectionId = request.CollectionId,
            CreatedAt = DateTime.Now,
            IsSync = false,
            IsDeleted = false,
            CreatedBy = "admin",
            UpdatedBy = "admin",
            SyncId = Guid.NewGuid()
        };
        
        await _context.Folders.AddAsync(folder);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<FolderDto>(folder));
    }
    
    [HttpGet("getAllFolders")]
    public async Task<IActionResult> GetAllFolders()
    {
        var folders = await _context
            .Folders
            .Include(w => w.HttpRequests)
            .ToListAsync();
        
        return Ok(_mapper.Map<IEnumerable<FolderDto>>(folders));
    }
    
    [HttpDelete("deleteFolder")]
    public async Task<IActionResult> DeleteFolder([FromQuery] int id)
    {
        var folder = await _context.Folders.FindAsync(id);

        if (folder == null)
        {
            return NotFound("Not Found");
        }
        _context.Folders.Remove(folder);
        await _context.SaveChangesAsync();
        return Ok("folder deleted");
    }
    
    
    //Request
    [HttpPost("createRequest")]
    public async Task<IActionResult> CreateRequest([FromBody] CreateRequestDto createRequestDto)
    {
        var auth = _mapper.Map<AuthenticationEntity>(createRequestDto.Authentication);
        var req = new RequestEntity
        {
            Name = createRequestDto.Name,
            CollectionId = createRequestDto.CollectionId,
            Url = createRequestDto.Url,
            HttpMethod = createRequestDto.HttpMethod,
            Body = createRequestDto.Body,
            Headers = createRequestDto.Headers,
            Authentication = auth,
            CreatedAt = DateTime.Now,
            CreatedBy = "admin",
            UpdatedBy = "admin",
            SyncId = Guid.NewGuid(),
            IsDeleted = false,
            IsSync = false,
           
        };
        
        
        await _context.Requests.AddAsync(req);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<RequestDto>(req));
    }
    
    [HttpGet("getAllRequests")]
    public async Task<IActionResult> GetAllRequests()
    {
        var requests = await _context
            .Requests
            .Include(w => w.Responses)
            .ToListAsync();
        
        return Ok(_mapper.Map<IEnumerable<RequestDto>>(requests));
    }
   
    [HttpDelete("deleteRequest")]
    public async Task<IActionResult> DeleteRequest([FromQuery] int id)
    {
        var request = await _context.Requests.FindAsync(id);

        if (request == null)
        {
            return NotFound("Not Found");
        }
        _context.Requests.Remove(request);
        await _context.SaveChangesAsync();
        return Ok("request deleted");
    }
    
    
    //Response
    [HttpPost("createResponse")]
    public async Task<IActionResult> CreateResponse([FromBody] CreateResponseDto createResponseDto)
    {
        var cookies = _mapper.Map<ResponseCookiesEntity>(createResponseDto.CookiesEntity);
        var res = new ResponseEntity
        {
            Body = createResponseDto.Body,
            StatusCode = createResponseDto.StatusCode,
            StatusText = createResponseDto.StatusText,
            ResponseTime = createResponseDto.ResponseTime,
            ResponseSize = createResponseDto.ResponseSize,
            Headers = createResponseDto.Headers,
            CookiesEntity = cookies,
            RequestId = createResponseDto.RequestId,
            CreatedAt = DateTime.Now,
            CreatedBy = "admin",
            UpdatedBy = "admin",
            SyncId = Guid.NewGuid(),
            IsDeleted = false,
            IsSync = false,
           
        };
        
        
        await _context.Responses.AddAsync(res);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<ResponseDto>(res));
    }
    
    [HttpGet("getAllResponses")]
    public async Task<IActionResult> GetAllResponses()
    {
        var responses = await _context
            .Responses
            .ToListAsync();
        
        return Ok(_mapper.Map<IEnumerable<ResponseDto>>(responses));
    }
    
    [HttpDelete("deleteResponse")]
    public async Task<IActionResult> DeleteResponse([FromQuery] int id)
    {
        var response = await _context.Responses.FindAsync(id);

        if (response == null)
        {
            return NotFound("Not Found");
        }
        _context.Responses.Remove(response);
        await _context.SaveChangesAsync();
        return Ok("response deleted");
    }
    
    
    //History
    [HttpPost("createHistory")]
    public async Task<IActionResult> CreateHistory([FromBody] CreateHistoryDto createHistoryDto)
    {
       var req = _mapper.Map<RequestEntity>(createHistoryDto.Requests);
       var requestList = new List<RequestEntity>{req};
       
        var history = new HistoryEntity
        {
            Requests = requestList,
            CreatedAt = DateTime.Now,
            CreatedBy = "admin",
            UpdatedBy = "admin",
            SyncId = Guid.NewGuid(),
            IsDeleted = false,
            IsSync = false,
           
        };
        
        
        await _context.Histories.AddAsync(history);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<HistoryEntity>(history));
    }
}