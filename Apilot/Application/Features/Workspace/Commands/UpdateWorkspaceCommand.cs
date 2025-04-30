using Apilot.Application.Common.Models;
using Apilot.Application.DTOs.WorkSpace;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Workspace.Commands;

public record UpdateWorkspaceCommand : IRequest<Result<Unit>>
{
    public required UpdateWorkSpaceRequest UpdateWorkspace { get; init; }
}

public class UpdateWorkspaceCommandHandler : IRequestHandler<UpdateWorkspaceCommand, Result<Unit>>
{
    private readonly IWorkspaceService _workspaceService;

    public UpdateWorkspaceCommandHandler(IWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    public async Task<Result<Unit>> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _workspaceService.UpdateWorkspaceAsync(request.UpdateWorkspace);
            return Result<Unit>.Success(Unit.Value);
        }
        catch (KeyNotFoundException ex)
        {
            return Result<Unit>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure($"Failed to update workspace: {ex.Message}");
        }
    }
}