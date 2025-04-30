using Apilot.Application.DTOs.Collection;

namespace Apilot.Application.Interfaces;

public interface ICollectionService
{
    Task<CollectionDto> CreateCollectionAsync(CreateCollectionRequest createCollectionRequest);
    Task<List<CollectionDto>> GetAllCollectionsAsync();
    Task<CollectionDto> GetCollectionByIdAsync(int id);
    Task<List<CollectionDto>> GetCollectionsByWorkspaceIdAsync(int workspaceId);
    Task UpdateCollectionAsync(UpdateCollectionRequest updateCollectionRequest);
    Task<bool> DeleteCollectionAsync(int id);
}