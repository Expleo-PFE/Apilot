using Apilot.Application.Common.Models;
using Apilot.Application.DTOs.Collection;
using Apilot.Application.DTOs.WorkSpace;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Collection.Commands;

public record UpdateCollectionCommand : IRequest<Result<Unit>>
{
    public required UpdateCollectionRequest UpdateCollectionRequest { get; init; }
}



public class UpdateCollectionCommandHandler : IRequestHandler<UpdateCollectionCommand, Result<Unit>>
{
    
    private readonly ICollectionService _collectionService;

    public UpdateCollectionCommandHandler(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    public async Task<Result<Unit>> Handle(UpdateCollectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _collectionService.UpdateCollectionAsync(request.UpdateCollectionRequest);
            return Result<Unit>.Success(Unit.Value);
        }
        catch (KeyNotFoundException ex)
        {
            return Result<Unit>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure($"Failed to update collection: {ex.Message}");
        }
    }
}