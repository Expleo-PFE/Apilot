using Apilot.Application.DTOs.Request;
using Apilot.Application.DTOs.Response;

namespace Apilot.Application.Interfaces;

public interface IRequestService
{
    
    Task<RequestDto> CreateRequestAsync(CreateRequest createRequest);
    Task<List<RequestDto>> GetAllRequestsAsync();
    Task<RequestDto> GetRequestByIdAsync(int id);
    Task<List<RequestDto>> GetRequestsByCollectionIdAsync(int collectionId);
    Task<List<RequestDto>> GetRequestsByFolderIdAsync(int folderId);
    Task<RequestDto> UpdateRequestAsync(UpdateRequest updateRequest);
    Task<bool> DeleteRequestAsync(int id);
   // Task<ResponseDto> ExecuteRequestAsync(int requestId, int? environmentId, string userId);
}