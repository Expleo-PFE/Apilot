using Apilot.Application.DTOs.Folder;
using Apilot.Application.Interfaces;
using Apilot.Domain.Entities;
using Apilot.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Apilot.Infrastructure.Services;

public class FolderService : IFolderService
{
    
    private readonly ILogger<FolderService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public FolderService(ApplicationDbContext context, ILogger<FolderService> logger, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    
    public async Task<FolderDto> CreateFolderAsync(CreateFolderRequest createFolderRequest)
    {
        try
        {
            _logger.LogInformation("Creating folder with name: {Name}", createFolderRequest.Name);
            
            var folder = new FolderEntity()
            {
                Name = createFolderRequest.Name,
                CollectionId = createFolderRequest.CollectionId,
                CreatedAt = DateTime.UtcNow,
                IsSync = false,
                IsDeleted = false,
                SyncId = Guid.NewGuid(),
                CreatedBy = "admin"
            };
            
            await _context.Folders.AddAsync(folder);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Folder created successfully with ID: {Id}", folder.Id);
            return _mapper.Map<FolderDto>(folder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating folder with name: {Name}", createFolderRequest.Name);
            throw;
        }
    }

    public async Task<List<FolderDto>> GetAllFoldersAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all folders");
            
            var folders = await _context.Folders
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} folders", folders.Count);
            return _mapper.Map<List<FolderDto>>(folders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all folders");
            throw;
        }
    }

    public async Task<FolderDto> GetFolderByIdAsync(int id)
    {
        _logger.LogInformation("Attempting to find folder with ID: {Id}", id);

        try
        {
            var folder = await _context.Folders
                .Include(f => f.HttpRequests).ThenInclude(r => r.Responses)
                .FirstOrDefaultAsync(f => f.Id == id );

            if (folder == null)
            {
                _logger.LogWarning("Folder with ID {Id} not found", id);
                throw new KeyNotFoundException($"Folder with ID {id} not found");
            }

            _logger.LogInformation("Folder with ID {Id} found", id);
            return _mapper.Map<FolderDto>(folder);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the folder with ID {Id}", id);
            throw;
        }
    }

    public async Task<List<FolderDto>> GetFoldersByCollectionIdAsync(int collectionId)
    {
        try
        {
            _logger.LogInformation("Fetching folders for collection ID: {CollectionId}", collectionId);
            
            var folders = await _context.Folders
                .Include(f => f.HttpRequests).ThenInclude(r => r.Responses)
                .Where(f => f.CollectionId == collectionId)
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} folders for collection ID: {CollectionId}", 
                folders.Count, collectionId);
            return _mapper.Map<List<FolderDto>>(folders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching folders for collection ID: {CollectionId}", collectionId);
            throw;
        }
    }

    public async Task<FolderDto> UpdateFolderAsync(UpdateFolderRequest updateFolderRequest)
    {
        try
        {
            _logger.LogInformation("Updating folder with ID: {Id}", updateFolderRequest.Id);
            
            var folder = await _context.Folders
                .FirstOrDefaultAsync(f => f.Id == updateFolderRequest.Id && !f.IsDeleted);
            
            if (folder == null)
            {
                _logger.LogWarning("Folder with ID {Id} not found for update", updateFolderRequest.Id);
                throw new KeyNotFoundException($"Folder with ID {updateFolderRequest.Id} not found");
            }
            
            folder.Name = updateFolderRequest.Name;
            folder.UpdatedAt = DateTime.UtcNow;
            folder.UpdatedBy = "admin"; 
            folder.IsSync = false; 
            
            _context.Folders.Update(folder);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Folder with ID: {Id} updated successfully", folder.Id);
            return _mapper.Map<FolderDto>(folder);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating folder with ID: {Id}", updateFolderRequest.Id);
            throw;
        }
    }

    public async Task<bool> DeleteFolderAsync(int id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete folder with ID: {Id}", id);
            
            var folder = await _context.Folders.FindAsync(id);
            
            if (folder == null)
            {
                _logger.LogWarning("Folder with ID {Id} not found for deletion", id);
                throw new KeyNotFoundException($"Folder with ID {id} not found");
            }
            
            folder.IsDeleted = true;
            folder.UpdatedAt = DateTime.UtcNow;
            folder.UpdatedBy = "admin"; 
            folder.IsSync = false;
            
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Folder with ID: {Id} deleted successfully", id);

            return true;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting folder with ID: {Id}", id);
            throw;
        }
    }
}