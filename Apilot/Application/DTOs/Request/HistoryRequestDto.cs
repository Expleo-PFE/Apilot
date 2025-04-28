using Apilot.Domain.Entities;
using Apilot.Domain.Enums;

namespace Apilot.Application.DTOs.Request;

public class HistoryRequestDto
{
    public ApiHttpMethod HttpMethod { get; set; }
    public required string Url { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public AuthenticationEntity Authentication { get; set; }
    public required string Body { get; set; }
}