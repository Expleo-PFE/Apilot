using Apilot.Application.Common.Models;

using Apilot.Application.DTOs.Folder;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Folder.Queries;

public record GetFoldersByCollectionIdQuery : IRequest<Result<List<FolderDto>>>
{
    public required int WorkspaceId { get; init; }
}



public class GetFoldersByCollectionIdQueryHandler : IRequestHandler<GetFoldersByCollectionIdQuery, Result<List<FolderDto>>>
{

    private readonly IFolderService _folderService;

    public GetFoldersByCollectionIdQueryHandler(IFolderService folderService)
    {
        _folderService = folderService;
    }

    public async Task<Result<List<FolderDto>>> Handle(GetFoldersByCollectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var folders = await _folderService.GetFoldersByCollectionIdAsync(request.WorkspaceId);
            return Result<List<FolderDto>>.Success(folders);
        }
        catch (Exception ex)
        {
            return Result<List<FolderDto>>.Failure($"Failed to get folders: {ex.Message}");
        }
    }
}