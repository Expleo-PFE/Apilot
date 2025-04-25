using Apilot.Application.DTOs.Authentication;
using Apilot.Domain.Enums;

namespace Apilot.Domain.Entities;

public class RequestHistoryEntity
{
    public string Name { get; set; }
    public ApiHttpMethod HttpMethod { get; set; }
    public string Url { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public AuthenticationDto Authentication { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
}