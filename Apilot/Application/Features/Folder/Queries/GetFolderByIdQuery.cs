using Apilot.Application.Common.Models;
using Apilot.Application.DTOs.Folder;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Folder.Queries;

public record GetFolderByIdQuery : IRequest<Result<FolderDto>>
{
    public required int Id { get; init; }
}



public class GetCollectionByIdQueryHandler : IRequestHandler<GetFolderByIdQuery, Result<FolderDto>>
{

    private readonly IFolderService _folderService;

    public GetCollectionByIdQueryHandler(IFolderService folderService)
    {
        _folderService = folderService;
    }


    public async Task<Result<FolderDto>> Handle(GetFolderByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var folder = await _folderService.GetFolderByIdAsync(request.Id);
            return Result<FolderDto>.Success(folder);
        }
        catch (KeyNotFoundException ex)
        {
            return Result<FolderDto>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<FolderDto>.Failure($"Failed to get folder: {ex.Message}");
        }
    }
}