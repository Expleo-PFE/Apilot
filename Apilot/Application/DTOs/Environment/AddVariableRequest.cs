using System.ComponentModel.DataAnnotations;

namespace Apilot.Application.DTOs.Environment;

public class AddVariableRequest
{
    [Required]
    public int EnvironmentId { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}