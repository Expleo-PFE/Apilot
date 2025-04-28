using System.ComponentModel.DataAnnotations;

namespace Apilot.Application.DTOs.Collection;

public class CreateCollectionRequest
{
    [Required]
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    [Required]
    public int WorkSpaceId { get; set; }
}