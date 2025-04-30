using Apilot.Application.Common.Models;
using Apilot.Application.DTOs.Folder;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Folder.Commands;

public record CreateFolderCommand : IRequest<Result<FolderDto>>
{
    public required CreateFolderRequest CreateFolderRequest { get; init; }
}


public class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, Result<FolderDto>>
{
    private readonly IFolderService _folderService;

    public CreateFolderCommandHandler(IFolderService folderService)
    {
        _folderService = folderService;
    }


    public async Task<Result<FolderDto>> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var folder = await _folderService.CreateFolderAsync(request.CreateFolderRequest);
            return Result<FolderDto>.Success(folder);
        }
        catch (Exception ex)
        {
            return Result<FolderDto>.Failure($"Failed to create folder: {ex.Message}");
        }
    }
}