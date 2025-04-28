using System.ComponentModel.DataAnnotations;

namespace Apilot.Application.DTOs.Folder;

public class UpdateFolderRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    
}