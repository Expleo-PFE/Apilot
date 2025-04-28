using Apilot.Application.DTOs.Envirenoment;

namespace Apilot.Application.Interfaces;

public interface IEnvironmentService
{
    Task<EnvironmentDto> CreateEnvironmentAsync(CreateEnvironmentRequest createEnvironmentRequest);
    Task<List<EnvironmentDto>> GetAllEnvironmentsAsync();
    Task<EnvironmentDto> GetEnvironmentByIdAsync(int id);
    Task<List<EnvironmentDto>> GetEnvironmentsByWorkspaceIdAsync(int workspaceId);
    
    Task<EnvironmentDto> UpdateEnvironmentAsync(int id, string name);
    Task<EnvironmentDto> AddVariablesToEnvironment(int id, Dictionary<string, string> variables);
    Task<bool> DeleteEnvironmentAsync(int id);
    
    
    Task<EnvironmentDto> AddVariableToEnvironmentAsync(int id, string key, string value);
    Task<EnvironmentDto> UpdateVariableInEnvironmentAsync(int id, string key, string value);
    Task<EnvironmentDto> RemoveVariableFromEnvironmentAsync(int id, string key);

    
}