using Apilot.Application.DTOs.Request;

namespace Apilot.Application.DTOs.Folder;

public class FolderDto
{
    
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CollectionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    public List<RequestDto> HttpRequests { get; set; } = new List<RequestDto>();
}