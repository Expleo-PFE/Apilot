using System.ComponentModel.DataAnnotations;

namespace Apilot.Application.DTOs.Folder;

public class CreateFolderRequest
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public int CollectionId { get; set; }
}