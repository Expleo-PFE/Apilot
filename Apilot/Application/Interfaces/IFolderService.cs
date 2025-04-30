using Apilot.Application.DTOs.Folder;

namespace Apilot.Application.Interfaces;

public interface IFolderService
{
    Task<FolderDto> CreateFolderAsync(CreateFolderRequest createFolderRequest);
    Task<List<FolderDto>> GetAllFoldersAsync();
    Task<FolderDto> GetFolderByIdAsync(int id);
    Task<List<FolderDto>> GetFoldersByCollectionIdAsync(int collectionId);
    Task UpdateFolderAsync(UpdateFolderRequest updateFolderRequest);
    Task DeleteFolderAsync(int id);
}