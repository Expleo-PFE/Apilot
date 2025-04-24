namespace Apilot.Application.DTOs.Response;

public class ResponseDto
{
    public int Id { get; set; }
    public int StatusCode { get; set; }
    public required string StatusText { get; set; }
    public required Dictionary<string, string> Headers { get; set; }
    public int ResponseTime { get; set; }
    public int ResponseSize { get; set; }
    public required string Body { get; set; }
    public required ResponseCookiesDto Cookies { get; set; }
    
    public int RequestId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public required string? UpdatedBy { get; set; }
}