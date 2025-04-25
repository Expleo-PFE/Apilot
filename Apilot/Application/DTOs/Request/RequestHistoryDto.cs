using Apilot.Application.DTOs.Authentication;
using Apilot.Application.DTOs.Response;
using Apilot.Domain.Enums;

namespace Apilot.Application.DTOs.Request;

public class RequestHistoryDto
{
    public required string Name { get; set; }
    public ApiHttpMethod HttpMethod { get; set; }
    public required string Url { get; set; }
    public required Dictionary<string, string> Headers { get; set; }
    public required AuthenticationDto Authentication { get; set; }
    public required string Body { get; set; }
    public DateTime CreatedAt { get; set; }
    
}