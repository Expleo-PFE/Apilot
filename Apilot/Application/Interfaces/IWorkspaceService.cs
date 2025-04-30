using Apilot.Application.DTOs.WorkSpace;

namespace Apilot.Application.Interfaces;

public interface IWorkspaceService
{
    Task<WorkSpaceDto> CreateWorkspaceAsync(CreateWorkSpaceRequest createWorkSpaceRequest);
    Task<List<WorkSpaceDto>> GetAllWorkspacesAsync();
    Task<WorkSpaceDto> GetWorkspaceByIdAsync(int id);
    Task UpdateWorkspaceAsync(UpdateWorkSpaceRequest updateWorkSpaceRequest);
    Task DeleteWorkspaceAsync(int id);
}