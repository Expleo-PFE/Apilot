﻿using Apilot.Application.DTOs.Request;
using Apilot.Application.Interfaces;
using Apilot.Domain.Entities;
using Apilot.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace Apilot.Infrastructure.Services;

public class RequestService : IRequestService
{
    private readonly ILogger<RequestService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RequestService(ApplicationDbContext context, ILogger<RequestService> logger, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<RequestDto> CreateRequestAsync(CreateRequest createRequest)
    {
        try
        {
            var auth = _mapper.Map<AuthenticationEntity>(createRequest.Authentication);
            _logger.LogInformation("Creating request with name: {Name}", createRequest.Name);
            
            var request = new RequestEntity()
            {
                Name = createRequest.Name,
                Url = createRequest.Url,
                HttpMethod = createRequest.HttpMethod,
                Body = createRequest.Body,
                Authentication = auth,
                CollectionId = createRequest.CollectionId,
                FolderId = createRequest.FolderId,
                Headers = createRequest.Headers,
                CreatedAt = DateTime.UtcNow,
                IsSync = false,
                IsDeleted = false,
                SyncId = Guid.NewGuid(),
                CreatedBy = "admin"
            };
            
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Request created successfully with ID: {Id}", request.Id);
            return _mapper.Map<RequestDto>(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating request with name: {Name}", createRequest.Name);
            throw;
        }
    }

    public async Task<List<RequestDto>> GetAllRequestsAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all requests");
            
            var requests = await _context.Requests
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} requests", requests.Count);
            return _mapper.Map<List<RequestDto>>(requests);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all requests");
            throw;
        }
    }

    public async Task<RequestDto> GetRequestByIdAsync(int id)
    {
        _logger.LogInformation("Attempting to find request with ID: {Id}", id);

        try
        {
            var request = await _context.Requests
                .Include(req => req.Responses)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
            {
                _logger.LogWarning("Request with ID {Id} not found", id);
                throw new KeyNotFoundException($"Request with ID {id} not found");
            }

            _logger.LogInformation("Request with ID {Id} found", id);
            return _mapper.Map<RequestDto>(request);
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

    public async Task<List<RequestDto>> GetRequestsByCollectionIdAsync(int collectionId)
    {
        try
        {
            _logger.LogInformation("Fetching requests for collection ID: {CollectionId}", collectionId);
            
            var requests = await _context.Requests
                .Include(req => req.Responses)
                .Where(r => r.CollectionId == collectionId)
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} requests for collection ID: {CollectionId}", 
                requests.Count, collectionId);
            return _mapper.Map<List<RequestDto>>(requests);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching requests for collection ID: {CollectionId}", collectionId);
            throw;
        }
    }

    public async Task<List<RequestDto>> GetRequestsByFolderIdAsync(int folderId)
    {
        try
        {
            _logger.LogInformation("Fetching requests for folder ID: {FolderId}", folderId);
            
            var requests = await _context.Requests
                .Include(req => req.Responses)
                .Where(r => r.FolderId == folderId)
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} requests for folder ID: {FolderId}", 
                requests.Count, folderId);
            return _mapper.Map<List<RequestDto>>(requests);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching requests for folder ID: {FolderId}", folderId);
            throw;
        }
    }

    public async Task<RequestDto> UpdateRequestAsync(UpdateRequest updateRequest)
    {
        try
        {
            _logger.LogInformation("Updating request with ID: {Id}", updateRequest.Id);
            
            var request = await _context.Requests
                .FirstOrDefaultAsync(r => r.Id == updateRequest.Id);
            
            if (request == null)
            {
                _logger.LogWarning("Request with ID {Id} not found for update", updateRequest.Id);
                throw new KeyNotFoundException($"Request with ID {updateRequest.Id} not found");
            }
            var auth = _mapper.Map<AuthenticationEntity>(updateRequest.Authentication);
            request.Name = updateRequest.Name;
            request.Url = updateRequest.Url;
            request.HttpMethod = updateRequest.HttpMethod;
            request.Body = updateRequest.Body;
            request.Headers = updateRequest.Headers;
            request.FolderId = updateRequest.FolderId;
            request.Authentication = auth;
            request.UpdatedAt = DateTime.UtcNow;
            request.UpdatedBy = "admin"; 
            request.IsSync = false; 
            
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Request with ID: {Id} updated successfully", request.Id);
            return _mapper.Map<RequestDto>(request);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating request with ID: {Id}", updateRequest.Id);
            throw;
        }
    }

    public async Task<bool> DeleteRequestAsync(int id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete request with ID: {Id}", id);
            
            var request = await _context.Requests.FindAsync(id);
            
            if (request == null)
            {
                _logger.LogWarning("Request with ID {Id} not found for deletion", id);
                throw new KeyNotFoundException($"Request with ID {id} not found");
            }
            
            request.IsDeleted = true;
            request.UpdatedAt = DateTime.UtcNow;
            request.UpdatedBy = "admin"; 
            request.IsSync = false;
            
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Request with ID: {Id} deleted successfully", id);
            return true;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting request with ID: {Id}", id);
            throw;
        }
    }
}