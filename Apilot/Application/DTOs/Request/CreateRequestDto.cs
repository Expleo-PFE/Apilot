using System.ComponentModel.DataAnnotations;
using Apilot.Application.DTOs.Authentication;
using Apilot.Domain.Enums;

namespace Apilot.Application.DTOs.Request;

public class CreateRequestDto
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public ApiHttpMethod HttpMethod { get; set; }
    
    [Required]
    public required string Url { get; set; }
    
    public required Dictionary<string, string> Headers { get; set; }
    
    public required AuthenticationDto Authentication { get; set; }
    
    public required string Body { get; set; }
    
    [Required]
    public int FolderId { get; set; }
    
    [Required]
    public int CollectionId { get; set; }
}