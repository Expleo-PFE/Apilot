using System.ComponentModel.DataAnnotations;

namespace Apilot.Application.DTOs.Envirenoment;

public class CreateEnvironmentRequest
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public int WorkSpaceId { get; set; }
    
}
