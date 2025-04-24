using Apilot.Application.DTOs.Request;

namespace Apilot.Application.DTOs.History;

public class CreateHistoryDto
{
    
    public required RequestDto Requests { get; set; } 
    
    public required string CreatedBy { get; set; }
   
}