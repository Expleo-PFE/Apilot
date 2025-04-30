using Apilot.Application.Common.Models;
using Apilot.Application.DTOs.WorkSpace;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Workspace.Queries;

public record GetWorkSpacesQuery : IRequest<Result<List<WorkSpaceDto>>> ;


public class GetWorkSpacesQueryHandler : IRequestHandler<GetWorkSpacesQuery, Result<List<WorkSpaceDto>>>
{
    
    private readonly IWorkspaceService _workspaceService;

    public GetWorkSpacesQueryHandler(IWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    public async Task<Result<List<WorkSpaceDto>>> Handle(GetWorkSpacesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var workspaces = await _workspaceService.GetAllWorkspacesAsync();
            return Result<List<WorkSpaceDto>>.Success(workspaces);
        }
        catch (Exception ex)
        {
            return Result<List<WorkSpaceDto>>.Failure($"Failed to get workspaces: {ex.Message}");
        }
    }
}
