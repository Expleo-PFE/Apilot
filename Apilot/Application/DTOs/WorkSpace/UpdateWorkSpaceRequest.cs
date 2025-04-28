namespace Apilot.Application.DTOs.WorkSpace;

public class UpdateWorkSpaceRequest
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    
}