using Apilot.Application.Common.Models;
using Apilot.Application.DTOs.WorkSpace;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Workspace.Queries;

public record GetWorkSpaceByIdQuery : IRequest<Result<WorkSpaceDto>>
{
    public required int Id { get; init; }
}


public class GetWorkSpaceByIdQueryHandler : IRequestHandler<GetWorkSpaceByIdQuery, Result<WorkSpaceDto>>
{
    private readonly IWorkspaceService _workspaceService;

    public GetWorkSpaceByIdQueryHandler(IWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    public async Task<Result<WorkSpaceDto>> Handle(GetWorkSpaceByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(request.Id);
            return Result<WorkSpaceDto>.Success(workspace);
        }
        catch (KeyNotFoundException ex)
        {
            return Result<WorkSpaceDto>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<WorkSpaceDto>.Failure($"Failed to get workspace: {ex.Message}");
        }
    }
}