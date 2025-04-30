using Apilot.Application.Common.Models;
using Apilot.Application.DTOs.Folder;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Folder.Commands;

public record UpdateFolderCommand : IRequest<Result<Unit>>
{
    public required UpdateFolderRequest UpdateFolderRequest { get; init; }
}



public class UpdateFolderCommandHandler : IRequestHandler<UpdateFolderCommand, Result<Unit>>
{
    
    private readonly IFolderService _folderService;

    public UpdateFolderCommandHandler(IFolderService folderService)
    {
        _folderService = folderService;
    }


    public async Task<Result<Unit>> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _folderService.UpdateFolderAsync(request.UpdateFolderRequest);
            return Result<Unit>.Success(Unit.Value);
        }
        catch (KeyNotFoundException ex)
        {
            return Result<Unit>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure($"Failed to update folder: {ex.Message}");
        }
    }
}