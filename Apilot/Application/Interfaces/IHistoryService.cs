using Apilot.Application.DTOs.History;

namespace Apilot.Application.Interfaces;

public interface IHistoryService
{
    Task<HistoryDto> CreateHistoryAsync(CreateHistoryRequest createHistoryRequest);
    Task<List<HistoryDto>> GetAllHistoryAsync();
    Task<HistoryDto> GetHistoryByIdAsync(int id);
    Task<List<HistoryDto>> GetHistoryByWorkspaceIdAsync(int id);
    Task<bool> DeleteHistoryAsync(int id);
    // Task ClearHistoryAsync(string userId);
}