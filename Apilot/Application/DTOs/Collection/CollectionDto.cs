using Apilot.Application.DTOs.Folder;
using Apilot.Application.DTOs.Request;

namespace Apilot.Application.DTOs.Collection;

public class CollectionDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int WorkSpaceId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public required string UpdatedBy { get; set; }
    
    
    public List<FolderDto> Folders { get; set; } = new List<FolderDto>();
    public List<RequestDto> HttpRequests { get; set; } = new List<RequestDto>();
}