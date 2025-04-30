using Apilot.Application.Common.Models;
using Apilot.Application.DTOs.Collection;
using Apilot.Application.Interfaces;
using MediatR;

namespace Apilot.Application.Features.Collection.Queries;

public record GetCollectionsQuery : IRequest<Result<List<CollectionDto>>>;


public class GetCollectionsQueryHandler : IRequestHandler<GetCollectionsQuery, Result<List<CollectionDto>>>
{

    private readonly ICollectionService _collectionService;

    public GetCollectionsQueryHandler(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    public async Task<Result<List<CollectionDto>>> Handle(GetCollectionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var collections = await _collectionService.GetAllCollectionsAsync();
            return Result<List<CollectionDto>>.Success(collections);
        }
        catch (Exception ex)
        {
            return Result<List<CollectionDto>>.Failure($"Failed to get collections: {ex.Message}");
        }
    }
}