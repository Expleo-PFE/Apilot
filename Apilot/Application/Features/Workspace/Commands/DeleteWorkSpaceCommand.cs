using Apilot.Application.Common.Models;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Workspace.Commands;

public record DeleteWorkSpaceCommand : IRequest<Result<Unit>>
{
    public required int Id { get; init; }
}


public class DeleteWorkSpaceCommandHandler : IRequestHandler<DeleteWorkSpaceCommand, Result<Unit>>
{
    private readonly IWorkspaceService _workspaceService;

    public DeleteWorkSpaceCommandHandler(IWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    public async Task<Result<Unit>> Handle(DeleteWorkSpaceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _workspaceService.DeleteWorkspaceAsync(request.Id);
            return Result<Unit>.Success(Unit.Value);
        }
        catch (KeyNotFoundException ex)
        {
            return Result<Unit>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure($"Failed to delete workspace: {ex.Message}");
        }
    }
}