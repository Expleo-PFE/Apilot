using System.ComponentModel.DataAnnotations;

namespace Apilot.Application.DTOs.Folder;

public class UpdateFolderDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    
}