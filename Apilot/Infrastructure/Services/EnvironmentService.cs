using Apilot.Application.DTOs.Envirenoment;
using Apilot.Application.Interfaces;
using Apilot.Domain.Entities;
using Apilot.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace Apilot.Infrastructure.Services;

public class EnvironmentService : IEnvironmentService
{
    private readonly ILogger<EnvironmentService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EnvironmentService(ApplicationDbContext context, ILogger<EnvironmentService> logger, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<EnvironmentDto> CreateEnvironmentAsync(CreateEnvironmentRequest createEnvironmentRequest)
    {
        try
        {
            _logger.LogInformation("Creating environment with name: {Name}", createEnvironmentRequest.Name);
            
            var environment = new EnvironmentEntity()
            {
                Name = createEnvironmentRequest.Name,
                WorkSpaceId = createEnvironmentRequest.WorkSpaceId,
                CreatedAt = DateTime.UtcNow,
                IsSync = false,
                IsDeleted = false,
                SyncId = Guid.NewGuid(),
                CreatedBy = "admin"
            };
            
            await _context.Environments.AddAsync(environment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Environment created successfully with ID: {Id}", environment.Id);
            return _mapper.Map<EnvironmentDto>(environment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating environment with name: {Name}", createEnvironmentRequest.Name);
            throw;
        }
    }

    public async Task<List<EnvironmentDto>> GetAllEnvironmentsAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all environments");
            
            var environments = await _context.Environments
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} environments", environments.Count);
            return _mapper.Map<List<EnvironmentDto>>(environments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all environments");
            throw;
        }
    }

    public async Task<EnvironmentDto> GetEnvironmentByIdAsync(int id)
    {
        _logger.LogInformation("Attempting to find environment with ID: {Id}", id);

        try
        {
            var environment = await _context.Environments
                .FirstOrDefaultAsync(e => e.Id == id);

            if (environment == null)
            {
                _logger.LogWarning("Environment with ID {Id} not found", id);
                throw new KeyNotFoundException($"Environment with ID {id} not found");
            }

            _logger.LogInformation("Environment with ID {Id} found", id);
            return _mapper.Map<EnvironmentDto>(environment);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the environment with ID {Id}", id);
            throw;
        }
    }

    public async Task<List<EnvironmentDto>> GetEnvironmentsByWorkspaceIdAsync(int workspaceId)
    {
        try
        {
            _logger.LogInformation("Fetching environments for workspace ID: {WorkspaceId}", workspaceId);
            
            var environments = await _context.Environments
                .Where(e => e.WorkSpaceId == workspaceId)
                .ToListAsync();
            
            _logger.LogInformation("Retrieved {Count} environments for workspace ID: {WorkspaceId}", 
                environments.Count, workspaceId);
            return _mapper.Map<List<EnvironmentDto>>(environments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching environments for workspace ID: {WorkspaceId}", workspaceId);
            throw;
        }
    }

    public async Task<EnvironmentDto> UpdateEnvironmentAsync(int id, string name)
    {
        try
        {
            _logger.LogInformation("Updating environment with ID: {Id}", id);
            
            var environment = await _context.Environments
                .FirstOrDefaultAsync(e => e.Id == id );
            
            if (environment == null)
            {
                _logger.LogWarning("Environment with ID {Id} not found for update", id);
                throw new KeyNotFoundException($"Environment with ID {id} not found");
            }
            
            environment.Name = name;
            environment.UpdatedAt = DateTime.UtcNow;
            environment.UpdatedBy = "admin"; 
            environment.IsSync = false; 
            
            _context.Environments.Update(environment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Environment with ID: {Id} updated successfully", environment.Id);
            return _mapper.Map<EnvironmentDto>(environment);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating environment with ID: {Id}", id);
            throw;
        }
    }

    public async Task<EnvironmentDto> AddVariablesToEnvironment(int id, Dictionary<string, string> variables)
    {
        try
        {
            _logger.LogInformation("Adding variables to environment with ID: {Id}", id);
            
            var environment = await _context.Environments
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (environment == null)
            {
                _logger.LogWarning("Environment with ID {Id} not found for adding variables", id);
                throw new KeyNotFoundException($"Environment with ID {id} not found");
            }
            
           
            foreach (var variable in variables)
            {
                environment.Variables[variable.Key] = variable.Value;
            }
            
            environment.UpdatedAt = DateTime.UtcNow;
            environment.UpdatedBy = "admin"; 
            environment.IsSync = false; 
            
            _context.Environments.Update(environment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Variables added to environment with ID: {Id} successfully", environment.Id);
            return _mapper.Map<EnvironmentDto>(environment);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding variables to environment with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> DeleteEnvironmentAsync(int id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete environment with ID: {Id}", id);
            
            var environment = await _context.Environments.FindAsync(id);
            
            if (environment == null)
            {
                _logger.LogWarning("Environment with ID {Id} not found for deletion", id);
                throw new KeyNotFoundException($"Environment with ID {id} not found");
            }
            
            
            environment.IsDeleted = true;
            environment.UpdatedAt = DateTime.UtcNow;
            environment.UpdatedBy = "admin"; 
            
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Environment with ID: {Id} deleted successfully", id);
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
    

    public async Task<EnvironmentDto> AddVariableToEnvironmentAsync(int id, string key, string value)
    {
        try
        {
            _logger.LogInformation("Adding variable '{Key}' to environment with ID: {Id}", key, id);
            
            var environment = await _context.Environments
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (environment == null)
            {
                _logger.LogWarning("Environment with ID {Id} not found for adding variable", id);
                throw new KeyNotFoundException($"Environment with ID {id} not found");
            }
            
           
            environment.Variables[key] = value;
            
            environment.UpdatedAt = DateTime.UtcNow;
            environment.UpdatedBy = "admin"; 
            environment.IsSync = false; 
            
            _context.Environments.Update(environment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Variable '{Key}' added to environment with ID: {Id} successfully", key, environment.Id);
            return _mapper.Map<EnvironmentDto>(environment);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding variable '{Key}' to environment with ID: {Id}", key, id);
            throw;
        }
    }

    public async Task<EnvironmentDto> UpdateVariableInEnvironmentAsync(int id, string key, string value)
    {
        try
        {
            _logger.LogInformation("Updating variable '{Key}' in environment with ID: {Id}", key, id);
            
            var environment = await _context.Environments
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (environment == null)
            {
                _logger.LogWarning("Environment with ID {Id} not found for updating variable", id);
                throw new KeyNotFoundException($"Environment with ID {id} not found");
            }
            
            
            if (!environment.Variables.ContainsKey(key))
            {
                _logger.LogWarning("Variable '{Key}' not found in environment with ID: {Id}", key, id);
                throw new KeyNotFoundException($"Variable '{key}' not found in environment with ID {id}");
            }
            
            
            environment.Variables[key] = value;
            
            environment.UpdatedAt = DateTime.UtcNow;
            environment.UpdatedBy = "admin"; 
            environment.IsSync = false; 
            
            _context.Environments.Update(environment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Variable '{Key}' updated in environment with ID: {Id} successfully", key, environment.Id);
            return _mapper.Map<EnvironmentDto>(environment);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating variable '{Key}' in environment with ID: {Id}", key, id);
            throw;
        }
    }

    public async Task<EnvironmentDto> RemoveVariableFromEnvironmentAsync(int id, string key)
    {
        try
        {
            _logger.LogInformation("Removing variable '{Key}' from environment with ID: {Id}", key, id);
            
            var environment = await _context.Environments
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (environment == null)
            {
                _logger.LogWarning("Environment with ID {Id} not found for removing variable", id);
                throw new KeyNotFoundException($"Environment with ID {id} not found");
            }
            
            if (!environment.Variables.ContainsKey(key))
            {
                _logger.LogWarning("Variable '{Key}' not found in environment with ID: {Id}", key, id);
                throw new KeyNotFoundException($"Variable '{key}' not found in environment with ID {id}");
            }
            
            environment.Variables.Remove(key);
            
            environment.UpdatedAt = DateTime.UtcNow;
            environment.UpdatedBy = "admin"; 
            environment.IsSync = false;
            
            _context.Environments.Update(environment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Variable '{Key}' removed from environment with ID: {Id} successfully", key, environment.Id);
            return _mapper.Map<EnvironmentDto>(environment);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing variable '{Key}' from environment with ID: {Id}", key, id);
            throw;
        }
    }

}