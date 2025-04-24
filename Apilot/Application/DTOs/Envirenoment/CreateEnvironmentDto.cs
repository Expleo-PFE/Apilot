using System.ComponentModel.DataAnnotations;

namespace Apilot.Application.DTOs.Envirenoment;

public class CreateEnvironmentDto
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public int WorkSpaceId { get; set; }
    
    public required Dictionary<string, string> Variables { get; set; } = new Dictionary<string, string>();
}
