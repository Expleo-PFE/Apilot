using Apilot.Application.Common.Models;
using Apilot.Application.DTOs.Folder;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Folder.Queries;

public record GetFoldersQuery : IRequest<Result<List<FolderDto>>>;


public class GetFoldersQueryHandler : IRequestHandler<GetFoldersQuery, Result<List<FolderDto>>>
{

    private readonly IFolderService _folderService;

    public GetFoldersQueryHandler(IFolderService folderService)
    {
        _folderService = folderService;
    }

    public async Task<Result<List<FolderDto>>> Handle(GetFoldersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var folders = await _folderService.GetAllFoldersAsync();
            return Result<List<FolderDto>>.Success(folders);
        }
        catch (Exception ex)
        {
            return Result<List<FolderDto>>.Failure($"Failed to get folders: {ex.Message}");
        }
    }
}