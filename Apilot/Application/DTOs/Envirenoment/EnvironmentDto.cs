namespace Apilot.Application.DTOs.Envirenoment;

public class EnvironmentDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int WorkSpaceId { get; set; }
    public Dictionary<string, string> Variables { get; set; } = new Dictionary<string, string>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}