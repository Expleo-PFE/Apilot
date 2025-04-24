using Apilot.Application.DTOs.Collection;
using Apilot.Application.DTOs.Envirenoment;

namespace Apilot.Application.DTOs.WorkSpace;

public class WorkSpaceDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    
    public List<CollectionDto> Collections { get; set; } = new List<CollectionDto>();
    public List<EnvironmentDto> Environments { get; set; } = new List<EnvironmentDto>();
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public required string UpdatedBy { get; set; }
}