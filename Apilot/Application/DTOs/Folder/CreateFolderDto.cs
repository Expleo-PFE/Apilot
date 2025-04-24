using System.ComponentModel.DataAnnotations;

namespace Apilot.Application.DTOs.Folder;

public class CreateFolderDto
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public int CollectionId { get; set; }
}