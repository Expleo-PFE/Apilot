using System.ComponentModel.DataAnnotations;

namespace Apilot.Application.DTOs.Collection;

public class UpdateCollectionRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    public required string Description { get; set; }
}