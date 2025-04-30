using Apilot.Application.Common.Models;
using Apilot.Application.DTOs.WorkSpace;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Workspace.Commands;

public record CreateWorkSpaceCommand : IRequest<Result<WorkSpaceDto>>
{
    public required CreateWorkSpaceRequest WorkSpaceDto { get; init; }
}




public class CreateWorkSpaceCommandHandler : IRequestHandler<CreateWorkSpaceCommand, Result<WorkSpaceDto>>
{
    
    private readonly IWorkspaceService _workspaceService;

    public CreateWorkSpaceCommandHandler(IWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    public async Task<Result<WorkSpaceDto>> Handle(CreateWorkSpaceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var workSpace = await _workspaceService.CreateWorkspaceAsync(request.WorkSpaceDto);
            return Result<WorkSpaceDto>.Success(workSpace);
        }
        catch (Exception ex)
        {
            return Result<WorkSpaceDto>.Failure($"Failed to create workspace: {ex.Message}");
        }
    }
}