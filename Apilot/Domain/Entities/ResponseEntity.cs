using Apilot.Domain.Common;
using Microsoft.AspNetCore.Authentication;

namespace Apilot.Domain.Entities;

public class ResponseEntity : BaseEntity
{
    public int StatusCode { get; set; }
    public string StatusText { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public int ResponseTime { get; set; }
    public int ResponseSize { get; set; }
    public string Body { get; set; }
    public ResponseCookiesEntity CookiesEntity { get; set; }
    
    public int RequestId { get; set; }
    public RequestEntity Request { get; set; }
    
    
}