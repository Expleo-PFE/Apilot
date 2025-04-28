

namespace Apilot.Application.DTOs.Response;

public class CreateResponseRequest
{
    public int StatusCode { get; set; }
    public required string StatusText { get; set; }
    public required Dictionary<string, string> Headers { get; set; }
    public int ResponseTime { get; set; }
    public int ResponseSize { get; set; }
    public required string Body { get; set; }
    public required ResponseCookiesDto CookiesEntity { get; set; }
    
    public int RequestId { get; set; }
}