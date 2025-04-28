using Apilot.Application.DTOs.History;
using Apilot.Application.DTOs.Request;
using Apilot.Application.Interfaces;
using Apilot.Domain.Entities;
using Apilot.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apilot.Infrastructure.Services;

public class HistoryService : IHistoryService
{
    
    private readonly ILogger<HistoryService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public HistoryService(ApplicationDbContext context, ILogger<HistoryService> logger, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    
    public async Task<HistoryDto> CreateHistoryAsync(CreateHistoryRequest createHistoryRequest)
    {
        try
        {
            _logger.LogInformation("Creating history for the request : {URL}", createHistoryRequest.Request.Url);
            var historyRequest = _mapper.Map<HistoryRequestEntity>(createHistoryRequest.Request);
            var history = new HistoryEntity
            {
                Requests = new List<HistoryRequestEntity>{historyRequest},
                CreatedAt = DateTime.UtcNow,
                WorkSpaceId = createHistoryRequest.WorkspaceId,
                IsSync = false,
                IsDeleted = false,
                SyncId = Guid.NewGuid(),
                CreatedBy = "admin"
            };
            
            await _context.Histories.AddAsync(history);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("History created successfully with ID: {Id}", history.Id);
            return _mapper.Map<HistoryDto>(history);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating History for the request: {URL}", createHistoryRequest.Request.Url);
            throw;
        }
       
    }

    public async Task<List<HistoryDto>> GetAllHistoryAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all Histories");
            
            var histories = await _context.Histories
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} histories", histories.Count);
            return _mapper.Map<List<HistoryDto>>(histories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all histories");
            throw;
        }
    }

    public async Task<HistoryDto> GetHistoryByIdAsync(int id)
    {
        _logger.LogInformation("Attempting to find request with ID: {Id}", id);

        try
        {
            var history = await _context.Histories
                .FirstOrDefaultAsync(r => r.Id == id);

            if (history == null)
            {
                _logger.LogWarning("History with ID {Id} not found", id);
                throw new KeyNotFoundException($"History with ID {id} not found");
            }

            _logger.LogInformation("History with ID {Id} found", id);
            return _mapper.Map<HistoryDto>(history);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the request with ID {Id}", id);
            throw;
        }
    }

    public async Task<List<HistoryDto>> GetHistoryByWorkspaceIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Fetching histories for workspace ID: {WorkspaceId}", id);
            
            var histories = await _context.Histories
                .Where(e => e.WorkSpaceId == id)
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} histories for workspace ID: {WorkspaceId}", 
                histories.Count, id);
            return _mapper.Map<List<HistoryDto>>(histories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching histories for workspace ID: {WorkspaceId}", id);
            throw;
        }
    }

    public async Task<bool> DeleteHistoryAsync(int id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete history with ID: {Id}", id);
            
            var environment = await _context.Histories.FindAsync(id);
            
            if (environment == null)
            {
                _logger.LogWarning("History with ID {Id} not found for deletion", id);
                throw new KeyNotFoundException($"History with ID {id} not found");
            }
            
            
            environment.IsDeleted = true;
            environment.UpdatedAt = DateTime.UtcNow;
            environment.UpdatedBy = "admin"; 
            
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("History with ID: {Id} deleted successfully", id);
            return true;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting environment with ID: {Id}", id);
            throw;
        }
    }
}