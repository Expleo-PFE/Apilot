using Apilot.Application.DTOs.Request;

namespace Apilot.Application.DTOs.History;

public class HistoryDto
{
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; }
    
    public List<RequestDto> Requests { get; set; } = new List<RequestDto>();
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public required string? UpdatedBy { get; set; }
}