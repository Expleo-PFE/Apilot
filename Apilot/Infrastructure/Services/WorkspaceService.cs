using Apilot.Application.DTOs.WorkSpace;
using Apilot.Application.Interfaces;
using Apilot.Domain.Entities;
using Apilot.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace Apilot.Infrastructure.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly ILogger<WorkspaceService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public WorkspaceService(ApplicationDbContext context, ILogger<WorkspaceService> logger, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<WorkSpaceDto> CreateWorkspaceAsync(CreateWorkSpaceRequest createWorkSpaceRequest)
    {
        try
        {
            _logger.LogInformation("Creating workspace with name: {Name}", createWorkSpaceRequest.Name);
            
            var workspace = new WorkSpaceEntity
            {
                Name = createWorkSpaceRequest.Name, 
                Description = createWorkSpaceRequest.Description,
                CreatedAt = DateTime.UtcNow,
                IsSync = false,
                IsDeleted = false,
                SyncId = Guid.NewGuid(),
                CreatedBy = "admin",
            };
            
            await _context.WorkSpaces.AddAsync(workspace);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Workspace created successfully with ID: {Id}", workspace.Id);
            return _mapper.Map<WorkSpaceDto>(workspace);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating workspace with name: {Name}", createWorkSpaceRequest.Name);
            throw;
        }
    }

    public async Task<List<WorkSpaceDto>> GetAllWorkspacesAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all workspaces");
            
            var workspaces = await _context.WorkSpaces
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} workspaces", workspaces.Count);
            return _mapper.Map<List<WorkSpaceDto>>(workspaces);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all workspaces");
            throw;
        }
    }

    public async Task<WorkSpaceDto> GetWorkspaceByIdAsync(int id)
    {
        _logger.LogInformation("Attempting to find workspace with ID: {Id}", id);

        try
        {
            var workspace = await _context.WorkSpaces
                .Include(w => w.Histories)
                .Include(w => w.Environements)
                .Include(w => w.Collections)
                .ThenInclude(c => c.Folders)
                .ThenInclude(f => f.HttpRequests)
                .Include(w => w.Collections)
                .ThenInclude(c => c.HttpRequests)
                .FirstOrDefaultAsync(w => w.Id == id);


            if (workspace == null)
            {
                _logger.LogWarning("Workspace with ID {Id} not found ", id);
                throw new KeyNotFoundException($"Workspace with ID {id} not found");
            }

            _logger.LogInformation("Workspace with ID {Id} found", id);
            return _mapper.Map<WorkSpaceDto>(workspace);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the workspace with ID {Id}", id);
            throw;
        }
    }

    public async Task UpdateWorkspaceAsync(UpdateWorkSpaceRequest updateWorkSpaceRequest)
    {
        _logger.LogInformation("Updating workspace with ID: {Id}", updateWorkSpaceRequest.Id);

        try
        {
            var workspace = await _context.WorkSpaces
                .FirstOrDefaultAsync(w => w.Id == updateWorkSpaceRequest.Id );
                
            if (workspace == null)
            {
                _logger.LogWarning("Workspace with ID {Id} not found for update", updateWorkSpaceRequest.Id);
                throw new KeyNotFoundException($"Workspace with ID {updateWorkSpaceRequest.Id} not found");
            }
            
            _mapper.Map(updateWorkSpaceRequest, workspace);
            workspace.UpdatedAt = DateTime.UtcNow;
            workspace.UpdatedBy = "admin";
            workspace.IsSync = false;
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Workspace with ID {Id} updated successfully", updateWorkSpaceRequest.Id);
           
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the workspace with ID {Id}", updateWorkSpaceRequest.Id);
            throw;
        }
    }

    public async Task<bool> DeleteWorkspaceAsync(int id)
    {
        _logger.LogInformation("Soft deleting workspace with ID: {Id}", id);

        try
        {
            var workspace = await _context.WorkSpaces
                .FirstOrDefaultAsync(w => w.Id == id );
                
            if (workspace == null)
            {
                _logger.LogWarning("Workspace with ID {Id} not found for delete", id);
                throw new KeyNotFoundException($"Workspace with ID {id} not found");
            }
            
            workspace.IsDeleted = true;
            workspace.UpdatedAt = DateTime.UtcNow;
            workspace.UpdatedBy = "admin"; 
            workspace.IsSync = false;
            
            _context.WorkSpaces.Update(workspace);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Workspace with ID {Id} deleted successfully", id);
            return true;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the workspace with ID {Id}", id);
            throw;
        }
    }
}