using Apilot.Application.DTOs.Response;

namespace Apilot.Application.Interfaces;

public interface IResponseService
{
    Task<ResponseDto> CreateResponseAsync(CreateResponseRequest createResponseRequest);
    Task<List<ResponseDto>> GetAllResponsesAsync();
    Task<ResponseDto> GetResponseByIdAsync(int id);
    Task<List<ResponseDto>> GetResponsesByRequestIdAsync(int requestId);
    Task<bool> DeleteResponseAsync(int id);
}