using Apilot.Application.DTOs.Authentication;
using Apilot.Application.DTOs.Response;
using Apilot.Domain.Enums;

namespace Apilot.Application.DTOs.Request;

public class RequestDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ApiHttpMethod HttpMethod { get; set; }
    public required string Url { get; set; }
    public required Dictionary<string, string> Headers { get; set; }
    public required AuthenticationDto Authentication { get; set; }
    public required string Body { get; set; }
    
    public int FolderId { get; set; }
    public int CollectionId { get; set; }
    
   
    public List<ResponseDto> Responses { get; set; } = new List<ResponseDto>();
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public required string UpdatedBy { get; set; }
    
}