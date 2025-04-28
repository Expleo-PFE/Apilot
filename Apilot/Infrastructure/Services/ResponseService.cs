using Apilot.Application.DTOs.Response;
using Apilot.Application.Interfaces;
using Apilot.Domain.Entities;
using Apilot.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apilot.Infrastructure.Services;

public class ResponseService : IResponseService
{
    private readonly ILogger<ResponseService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ResponseService(ApplicationDbContext context, ILogger<ResponseService> logger, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ResponseDto> CreateResponseAsync(CreateResponseRequest createResponseRequest)
    {
        try
        {
            var cookies = _mapper.Map<ResponseCookiesEntity>(createResponseRequest.CookiesEntity);
            _logger.LogInformation("Creating response for request ID: {RequestId}", createResponseRequest.RequestId);
            
            var response = new ResponseEntity()
            {
                RequestId = createResponseRequest.RequestId,
                StatusCode = createResponseRequest.StatusCode,
                StatusText = createResponseRequest.StatusText,
                Body = createResponseRequest.Body,
                Headers = createResponseRequest.Headers,
                ResponseTime = createResponseRequest.ResponseTime,
                ResponseSize = createResponseRequest.ResponseSize,
                CookiesEntity = cookies,
                CreatedAt = DateTime.UtcNow,
                IsSync = false,
                IsDeleted = false,
                SyncId = Guid.NewGuid(),
                CreatedBy = "admin"
            };
            
            await _context.Responses.AddAsync(response);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Response created successfully with ID: {Id}", response.Id);
            return _mapper.Map<ResponseDto>(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating response for request ID: {RequestId}", createResponseRequest.RequestId);
            throw;
        }
    }

    public async Task<List<ResponseDto>> GetAllResponsesAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all responses");
            
            var responses = await _context.Responses
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} responses", responses.Count);
            return _mapper.Map<List<ResponseDto>>(responses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all responses");
            throw;
        }
    }

    public async Task<ResponseDto> GetResponseByIdAsync(int id)
    {
        _logger.LogInformation("Attempting to find response with ID: {Id}", id);

        try
        {
            var response = await _context.Responses
                .FirstOrDefaultAsync(r => r.Id == id);

            if (response == null)
            {
                _logger.LogWarning("Response with ID {Id} not found", id);
                throw new KeyNotFoundException($"Response with ID {id} not found");
            }

            _logger.LogInformation("Response with ID {Id} found", id);
            return _mapper.Map<ResponseDto>(response);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the response with ID {Id}", id);
            throw;
        }
    }

    public async Task<List<ResponseDto>> GetResponsesByRequestIdAsync(int requestId)
    {
        try
        {
            _logger.LogInformation("Fetching responses for request ID: {RequestId}", requestId);
            
            var responses = await _context.Responses
                .Where(r => r.RequestId == requestId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} responses for request ID: {RequestId}", 
                responses.Count, requestId);
            return _mapper.Map<List<ResponseDto>>(responses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching responses for request ID: {RequestId}", requestId);
            throw;
        }
    }

    public async Task<bool> DeleteResponseAsync(int id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete response with ID: {Id}", id);
            
            var response = await _context.Responses.FindAsync(id);
            
            if (response == null)
            {
                _logger.LogWarning("Response with ID {Id} not found for deletion", id);
                throw new KeyNotFoundException($"Response with ID {id} not found");
            }
            
            response.IsDeleted = true;
            response.UpdatedAt = DateTime.UtcNow;
            response.UpdatedBy = "admin"; 
            response.IsSync = false;
            
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Response with ID: {Id} deleted successfully", id);
            return true;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting response with ID: {Id}", id);
            throw;
        }
    }
}